using System.Numerics;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class ColorTests
{
    [Fact]
    public void Ctor_byte_rgba_should_set_components()
    {
        var c = new Color((byte)10, (byte)20, (byte)30, (byte)40);
        c.R.ShouldBe((byte)10);
        c.G.ShouldBe((byte)20);
        c.B.ShouldBe((byte)30);
        c.A.ShouldBe((byte)40);
    }

    [Fact]
    public void Ctor_byte_rgb_should_default_alpha_to_255()
    {
        var c = new Color((byte)1, (byte)2, (byte)3);
        c.A.ShouldBe((byte)255);
    }

    [Fact]
    public void Ctor_byte_rgb_with_float_alpha_should_scale_to_byte()
    {
        var c = new Color((byte)1, (byte)2, (byte)3, 0.5f);
        c.A.ShouldBe((byte)127);
    }

    [Fact]
    public void Ctor_byte_rgb_with_float_alpha_should_clamp_above_one()
    {
        var c = new Color((byte)1, (byte)2, (byte)3, 5f);
        c.A.ShouldBe((byte)255);
    }

    [Fact]
    public void Ctor_byte_rgb_with_float_alpha_should_clamp_below_zero()
    {
        var c = new Color((byte)1, (byte)2, (byte)3, -1f);
        c.A.ShouldBe((byte)0);
    }

    [Fact]
    public void Ctor_int_rgba_should_clamp_above_255()
    {
        var c = new Color(300, 400, 500, 600);
        c.R.ShouldBe((byte)255);
        c.G.ShouldBe((byte)255);
        c.B.ShouldBe((byte)255);
        c.A.ShouldBe((byte)255);
    }

    [Fact]
    public void Ctor_int_rgba_should_clamp_below_zero()
    {
        var c = new Color(-10, -20, -30, -40);
        c.R.ShouldBe((byte)0);
        c.G.ShouldBe((byte)0);
        c.B.ShouldBe((byte)0);
        c.A.ShouldBe((byte)0);
    }

    [Fact]
    public void Ctor_int_rgb_should_default_alpha_to_255()
    {
        var c = new Color(10, 20, 30);
        c.A.ShouldBe((byte)255);
    }

    [Fact]
    public void Ctor_float_rgba_should_scale_to_byte_range()
    {
        var c = new Color(1.0f, 0.5f, 0.0f, 1.0f);
        c.R.ShouldBe((byte)255);
        c.G.ShouldBe((byte)127);
        c.B.ShouldBe((byte)0);
        c.A.ShouldBe((byte)255);
    }

    [Fact]
    public void Ctor_float_rgba_should_clamp_out_of_range_values()
    {
        var c = new Color(2.0f, -1.0f, 1.5f, -0.5f);
        c.R.ShouldBe((byte)255);
        c.G.ShouldBe((byte)0);
        c.B.ShouldBe((byte)255);
        c.A.ShouldBe((byte)0);
    }

    [Fact]
    public void Ctor_float_rgb_should_default_alpha_to_one()
    {
        var c = new Color(0.5f, 0.5f, 0.5f);
        c.A.ShouldBe((byte)255);
    }

    [Fact]
    public void Ctor_int_packed_should_unpack_as_RGBA()
    {
        // 0xAABBCCDD with RGBA => R=AA G=BB B=CC A=DD
        var c = new Color(unchecked((int)0xAABBCCDD));
        c.R.ShouldBe((byte)0xAA);
        c.G.ShouldBe((byte)0xBB);
        c.B.ShouldBe((byte)0xCC);
        c.A.ShouldBe((byte)0xDD);
    }

    [Fact]
    public void Ctor_uint_packed_should_unpack_as_RGBA()
    {
        var c = new Color(0xAABBCCDDu);
        c.R.ShouldBe((byte)0xAA);
        c.G.ShouldBe((byte)0xBB);
        c.B.ShouldBe((byte)0xCC);
        c.A.ShouldBe((byte)0xDD);
    }

    [Fact]
    public void Brightness_should_be_computed_correctly()
    {
        var c = new Color((byte)100, (byte)150, (byte)200);
        c.Brightness.ShouldBe(0.212655f * 100 + 0.715158f * 150 + 0.072187f * 200, 0.0001f);
    }

    [Fact]
    public void Brightness_for_white_should_be_close_to_max()
    {
        var c = Color.White;
        c.Brightness.ShouldBe(0.212655f * 255 + 0.715158f * 255 + 0.072187f * 255, 0.0001f);
    }

    [Fact]
    public void ToInteger_RGBA_should_pack_components_correctly()
    {
        var c = new Color((byte)0xAA, (byte)0xBB, (byte)0xCC, (byte)0xDD);
        unchecked
        {
            c.ToInteger(ColorFormat.RGBA).ShouldBe((int)0xAABBCCDD);
        }
    }

    [Fact]
    public void ToInteger_ARGB_should_pack_components_correctly()
    {
        var c = new Color((byte)0xAA, (byte)0xBB, (byte)0xCC, (byte)0xDD);
        unchecked
        {
            c.ToInteger(ColorFormat.ARGB).ShouldBe((int)0xDDAABBCC);
        }
    }

    [Fact]
    public void ToInteger_RGB_should_pack_components_correctly()
    {
        var c = new Color((byte)0xAA, (byte)0xBB, (byte)0xCC, (byte)0xDD);
        c.ToInteger(ColorFormat.RGB).ShouldBe(0xAABBCC);
    }

    [Fact]
    public void ToInteger_should_return_zero_for_unknown_format()
    {
        var c = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        c.ToInteger((ColorFormat)99).ShouldBe(0);
    }

    [Fact]
    public void FromInteger_RGBA_should_unpack_correctly()
    {
        var c = Color.FromInteger(0xAABBCCDDu, ColorFormat.RGBA);
        c.R.ShouldBe((byte)0xAA);
        c.G.ShouldBe((byte)0xBB);
        c.B.ShouldBe((byte)0xCC);
        c.A.ShouldBe((byte)0xDD);
    }

    [Fact]
    public void FromInteger_ARGB_should_unpack_correctly()
    {
        var c = Color.FromInteger(0xDDAABBCCu, ColorFormat.ARGB);
        c.A.ShouldBe((byte)0xDD);
        c.R.ShouldBe((byte)0xAA);
        c.G.ShouldBe((byte)0xBB);
        c.B.ShouldBe((byte)0xCC);
    }

    [Fact]
    public void FromInteger_RGB_should_unpack_and_set_full_alpha()
    {
        var c = Color.FromInteger(0xAABBCCu, ColorFormat.RGB);
        c.R.ShouldBe((byte)0xAA);
        c.G.ShouldBe((byte)0xBB);
        c.B.ShouldBe((byte)0xCC);
        c.A.ShouldBe((byte)0xFF);
    }

    [Fact]
    public void FromInteger_signed_overload_should_pass_through_to_unsigned()
    {
        var u = Color.FromInteger(0xAABBCCDDu, ColorFormat.RGBA);
        var s = Color.FromInteger(unchecked((int)0xAABBCCDD), ColorFormat.RGBA);
        s.ShouldBe(u);
    }

    [Fact]
    public void ToInteger_then_FromInteger_RGBA_should_roundtrip()
    {
        var c = new Color((byte)17, (byte)33, (byte)49, (byte)65);
        var back = Color.FromInteger(unchecked((uint)c.ToInteger(ColorFormat.RGBA)), ColorFormat.RGBA);
        back.ShouldBe(c);
    }

    [Fact]
    public void ToInteger_then_FromInteger_ARGB_should_roundtrip()
    {
        var c = new Color((byte)17, (byte)33, (byte)49, (byte)65);
        var back = Color.FromInteger(unchecked((uint)c.ToInteger(ColorFormat.ARGB)), ColorFormat.ARGB);
        back.ShouldBe(c);
    }

    [Fact]
    public void FromString_RGBA_should_parse_valid_8_hex_chars()
    {
        var c = Color.FromString("AABBCCDD", ColorFormat.RGBA);
        c.R.ShouldBe((byte)0xAA);
        c.G.ShouldBe((byte)0xBB);
        c.B.ShouldBe((byte)0xCC);
        c.A.ShouldBe((byte)0xDD);
    }

    [Fact]
    public void FromString_RGB_should_parse_valid_6_hex_chars()
    {
        var c = Color.FromString("AABBCC", ColorFormat.RGB);
        c.R.ShouldBe((byte)0xAA);
        c.G.ShouldBe((byte)0xBB);
        c.B.ShouldBe((byte)0xCC);
        c.A.ShouldBe((byte)0xFF);
    }

    [Fact]
    public void FromString_should_accept_0x_prefix()
    {
        var c = Color.FromString("0xAABBCC", ColorFormat.RGB);
        c.R.ShouldBe((byte)0xAA);
    }

    [Fact]
    public void FromString_should_return_white_for_invalid_input()
    {
        Color.FromString("not-a-color", ColorFormat.RGB).ShouldBe(Color.White);
    }

    [Fact]
    public void FromString_should_return_white_when_length_does_not_match_format()
    {
        Color.FromString("AABBCC", ColorFormat.RGBA).ShouldBe(Color.White);
    }

    [Fact]
    public void Lerp_at_zero_should_return_value1_rgb()
    {
        var a = new Color((byte)0, (byte)0, (byte)0, (byte)10);
        var b = new Color((byte)100, (byte)100, (byte)100, (byte)200);
        var result = Color.Lerp(a, b, 0f);
        result.R.ShouldBe((byte)0);
        result.G.ShouldBe((byte)0);
        result.B.ShouldBe((byte)0);
        result.A.ShouldBe((byte)10);
    }

    [Fact]
    public void Lerp_at_one_should_return_value2_rgb_but_keep_value1_alpha_when_blendAlpha_is_false()
    {
        var a = new Color((byte)0, (byte)0, (byte)0, (byte)10);
        var b = new Color((byte)100, (byte)100, (byte)100, (byte)200);
        var result = Color.Lerp(a, b, 1f);
        result.R.ShouldBe((byte)100);
        result.G.ShouldBe((byte)100);
        result.B.ShouldBe((byte)100);
        result.A.ShouldBe((byte)10);
    }

    [Fact]
    public void Lerp_at_one_with_blendAlpha_should_return_value2_alpha()
    {
        var a = new Color((byte)0, (byte)0, (byte)0, (byte)10);
        var b = new Color((byte)100, (byte)100, (byte)100, (byte)200);
        var result = Color.Lerp(a, b, 1f, blendAlpha: true);
        result.A.ShouldBe((byte)200);
    }

    [Fact]
    public void Lerp_should_clamp_amount_above_one()
    {
        var a = new Color((byte)0, (byte)0, (byte)0);
        var b = new Color((byte)100, (byte)100, (byte)100);
        Color.Lerp(a, b, 5f).R.ShouldBe((byte)100);
    }

    [Fact]
    public void Lerp_should_clamp_amount_below_zero()
    {
        var a = new Color((byte)20, (byte)20, (byte)20);
        var b = new Color((byte)100, (byte)100, (byte)100);
        Color.Lerp(a, b, -1f).R.ShouldBe((byte)20);
    }

    [Fact]
    public void Darken_at_full_amount_should_return_black_rgb()
    {
        var result = Color.White.Darken(1f);
        result.R.ShouldBe((byte)0);
        result.G.ShouldBe((byte)0);
        result.B.ShouldBe((byte)0);
    }

    [Fact]
    public void Lighten_at_full_amount_should_return_white_rgb()
    {
        var result = Color.Black.Lighten(1f);
        result.R.ShouldBe((byte)255);
        result.G.ShouldBe((byte)255);
        result.B.ShouldBe((byte)255);
    }

    [Fact]
    public void Grayscale_should_set_rgb_to_brightness()
    {
        var c = new Color((byte)100, (byte)150, (byte)200);
        var gs = c.Grayscale();
        gs.R.ShouldBe(gs.G);
        gs.G.ShouldBe(gs.B);
    }

    [Fact]
    public void AddGammaCorrection_then_RemoveGammaCorrection_should_roundtrip_close_to_original()
    {
        var c = new Color((byte)128, (byte)200, (byte)50, (byte)255);
        var roundtrip = c.AddGammaCorrection().RemoveGammaCorrection();
        ((int)roundtrip.R).ShouldBeInRange(126, 130);
        ((int)roundtrip.G).ShouldBeInRange(198, 202);
        ((int)roundtrip.B).ShouldBeInRange(48, 52);
    }

    [Fact]
    public void AddGammaCorrection_should_keep_white_close_to_white()
    {
        var c = Color.White.AddGammaCorrection();
        ((int)c.R).ShouldBeGreaterThanOrEqualTo(254);
        ((int)c.G).ShouldBeGreaterThanOrEqualTo(254);
        ((int)c.B).ShouldBeGreaterThanOrEqualTo(254);
    }

    [Fact]
    public void RemoveGammaCorrection_should_keep_black_as_black()
    {
        Color.Black.RemoveGammaCorrection().ShouldBe(Color.Black);
    }

    [Fact]
    public void ToString_should_use_RGB_format_by_default()
    {
        var c = new Color((byte)0xAA, (byte)0xBB, (byte)0xCC, (byte)0xDD);
        c.ToString().ShouldBe("{AABBCC}");
    }

    [Fact]
    public void ToString_RGBA_should_format_as_curly_8_hex()
    {
        var c = new Color((byte)0xAA, (byte)0xBB, (byte)0xCC, (byte)0xDD);
        c.ToString(ColorFormat.RGBA).ShouldBe("{AABBCCDD}");
    }

    [Fact]
    public void ToString_ARGB_should_format_as_curly_8_hex()
    {
        var c = new Color((byte)0xAA, (byte)0xBB, (byte)0xCC, (byte)0xDD);
        c.ToString(ColorFormat.ARGB).ShouldBe("{DDAABBCC}");
    }

    [Fact]
    public void Implicit_to_int_should_use_RGBA_format()
    {
        var c = new Color((byte)0x11, (byte)0x22, (byte)0x33, (byte)0x44);
        int value = c;
        value.ShouldBe(c.ToInteger(ColorFormat.RGBA));
    }

    [Fact]
    public void Implicit_int_to_Color_should_use_RGBA_format()
    {
        Color c = unchecked((int)0xAABBCCDDu);
        c.R.ShouldBe((byte)0xAA);
        c.A.ShouldBe((byte)0xDD);
    }

    [Fact]
    public void Implicit_uint_to_Color_should_use_RGBA_format()
    {
        Color c = 0xAABBCCDDu;
        c.R.ShouldBe((byte)0xAA);
        c.A.ShouldBe((byte)0xDD);
    }

    [Fact]
    public void Implicit_Colour_to_Color_should_copy_components()
    {
        var src = new Colour(10, 20, 30, 40);
        Color c = src;
        c.R.ShouldBe((byte)10);
        c.G.ShouldBe((byte)20);
        c.B.ShouldBe((byte)30);
        c.A.ShouldBe((byte)40);
    }

    [Fact]
    public void Implicit_Color_to_Colour_should_copy_components()
    {
        var src = new Color((byte)10, (byte)20, (byte)30, (byte)40);
        Colour c = src;
        c.R.ShouldBe((byte)10);
        c.G.ShouldBe((byte)20);
        c.B.ShouldBe((byte)30);
        c.A.ShouldBe((byte)40);
    }

    [Fact]
    public void EqualityOperator_should_return_true_for_same_components()
    {
        var a = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        var b = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        (a == b).ShouldBeTrue();
    }

    [Fact]
    public void EqualityOperator_should_return_false_for_differing_components()
    {
        var a = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        var b = new Color((byte)9, (byte)2, (byte)3, (byte)4);
        (a == b).ShouldBeFalse();
    }

    [Fact]
    public void InequalityOperator_should_invert_equality()
    {
        var a = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        var b = new Color((byte)9, (byte)2, (byte)3, (byte)4);
        (a != b).ShouldBeTrue();
    }

    [Fact]
    public void MultiplyOperator_should_scale_components()
    {
        var c = new Color((byte)100, (byte)100, (byte)100, (byte)100) * 0.5f;
        c.R.ShouldBe((byte)50);
        c.G.ShouldBe((byte)50);
        c.B.ShouldBe((byte)50);
        c.A.ShouldBe((byte)50);
    }

    [Fact]
    public void MultiplyOperator_should_clamp_above_255()
    {
        var c = new Color((byte)200, (byte)200, (byte)200, (byte)200) * 3f;
        c.R.ShouldBe((byte)255);
    }

    [Fact]
    public void MultiplyOperator_should_clamp_negative_to_zero()
    {
        var c = new Color((byte)200, (byte)200, (byte)200, (byte)200) * -1f;
        c.R.ShouldBe((byte)0);
    }

    [Fact]
    public void Explicit_Vector3_conversion_should_normalize_to_0_to_1_range()
    {
        var v = (Vector3)new Color((byte)255, (byte)128, (byte)0);
        v.X.ShouldBe(1f, 0.001f);
        v.Y.ShouldBe(128f / 255f, 0.001f);
        v.Z.ShouldBe(0f, 0.001f);
    }

    [Fact]
    public void Equals_object_should_return_true_when_color_is_equivalent()
    {
        var a = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        object b = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        a.Equals(b).ShouldBeTrue();
    }

    [Fact]
    public void Equals_object_should_return_false_for_non_Color()
    {
        var a = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        a.Equals("not a color").ShouldBeFalse();
    }

    [Fact]
    public void Equals_object_should_return_false_for_null()
    {
        var a = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        a.Equals(null).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_should_be_equal_for_equal_colors()
    {
        var a = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        var b = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        a.GetHashCode().ShouldBe(b.GetHashCode());
    }

    [Fact]
    public void GetHashCode_should_differ_for_different_colors()
    {
        var a = new Color((byte)1, (byte)2, (byte)3, (byte)4);
        var b = new Color((byte)1, (byte)2, (byte)3, (byte)9);
        a.GetHashCode().ShouldNotBe(b.GetHashCode());
    }

    [Fact]
    public void White_should_have_max_components()
    {
        Color.White.R.ShouldBe((byte)0xFF);
        Color.White.G.ShouldBe((byte)0xFF);
        Color.White.B.ShouldBe((byte)0xFF);
        Color.White.A.ShouldBe((byte)0xFF);
    }

    [Fact]
    public void Black_should_have_zero_rgb_and_full_alpha()
    {
        Color.Black.R.ShouldBe((byte)0);
        Color.Black.G.ShouldBe((byte)0);
        Color.Black.B.ShouldBe((byte)0);
        Color.Black.A.ShouldBe((byte)0xFF);
    }

    [Fact]
    public void Red_should_be_pure_red()
    {
        Color.Red.R.ShouldBe((byte)0xFF);
        Color.Red.G.ShouldBe((byte)0);
        Color.Red.B.ShouldBe((byte)0);
    }

    [Fact]
    public void Green_should_match_HTML_green_0x008000()
    {
        // HTML/CSS "Green" is 0x008000, not 0x00FF00
        Color.Green.R.ShouldBe((byte)0);
        Color.Green.G.ShouldBe((byte)0x80);
        Color.Green.B.ShouldBe((byte)0);
    }

    [Fact]
    public void Blue_should_be_pure_blue()
    {
        Color.Blue.R.ShouldBe((byte)0);
        Color.Blue.G.ShouldBe((byte)0);
        Color.Blue.B.ShouldBe((byte)0xFF);
    }

    [Fact]
    public void Transparent_should_have_zero_alpha()
    {
        Color.Transparent.A.ShouldBe((byte)0);
    }
}
