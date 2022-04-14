using System;
using System.Collections.Generic;
using SampSharp.Core.Callbacks;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests.Callbacks
{
    public class CallbackTests
    {
        [Fact]
        public void For_should_correctly_extract_parameter_types()
        {
            // arrange
            var handler = (bool _, bool[] _, int _, int[] _, int _, float _, float[] _, int _, string _) => { };
            
            // act
            var sut = Callback.For(handler.Target, handler.Method);

            // assert
            sut.Parameters.Length.ShouldBe(9);
            sut.Parameters[0].ShouldBeOfType<CallbackParameterBoolean>();
            sut.Parameters[1].ShouldBeOfType<CallbackParameterBooleanArray>();
            sut.Parameters[2].ShouldBeOfType<CallbackParameterInt>();
            sut.Parameters[3].ShouldBeOfType<CallbackParameterIntArray>();
            sut.Parameters[5].ShouldBeOfType<CallbackParameterSingle>();
            sut.Parameters[6].ShouldBeOfType<CallbackParameterSingleArray>();
            sut.Parameters[8].ShouldBeOfType<CallbackParameterString>();
        }
        
        [Theory]
        [MemberData(nameof(For_TestScenarios))]
        public void For_should_assign_array_length_offset(Delegate handler, int parameterIndex, int expectedLengthOffset)
        {
            // act
            var sut = Callback.For(handler.Target, handler.Method);

            // assert
            var param = sut.Parameters[parameterIndex].ShouldBeAssignableTo<ICallbackArrayParameter>()!;
            param.LengthOffset.ShouldBe(expectedLengthOffset);
        }

        public static IEnumerable<object[]> For_TestScenarios()
        {
            yield return new object[] { (bool[] _, int _, int _) => { }, 0, 1 };
            yield return new object[] { (int[] _, int _, int _) => { }, 0, 1 };
            yield return new object[] { (float[] _, int _, int _) => { }, 0, 1 };
            yield return new object[] { (int _, bool[] _, int _, int _) => { }, 1, 1 };
            yield return new object[] { (int _, int[] _, int _, int _) => { }, 1, 1 };
            yield return new object[] { (int _, float[] _, int _, int _) => { }, 1, 1 };
            yield return new object[] { ([ParameterLength(2)] bool[] _, int _, int _) => { }, 0, 2 };
            yield return new object[] { ([ParameterLength(2)] int[] _, int _, int _) => { }, 0, 2 };
            yield return new object[] { ([ParameterLength(2)] float[] _, int _, int _) => { }, 0, 2 };
            yield return new object[] { (int _, [ParameterLength(3)] bool[] _, int _, int _) => { }, 1, 2 };
            yield return new object[] { (int _, [ParameterLength(3)] int[] _, int _, int _) => { }, 1, 2 };
            yield return new object[] { (int _, [ParameterLength(3)] float[] _, int _, int _) => { }, 1, 2 };
            yield return new object[] { (int _, [ParameterLength(0)] bool[] _, int _, int _) => { }, 1, -1 };
            yield return new object[] { (int _, [ParameterLength(0)] int[] _, int _, int _) => { }, 1, -1 };
            yield return new object[] { (int _, [ParameterLength(0)] float[] _, int _, int _) => { }, 1, -1 };
        }
        
        [Fact]
        public void For_should_throw_when_parameter_length_out_of_bounds()
        {
            // arrange
            var handler = ([ParameterLength(2)] bool[] _, int _) => { };

            // act
            var ex = Should.Throw<InvalidOperationException>(() => Callback.For(handler.Target, handler.Method));

            // assert
            ex.Message.ShouldContain("out of bounds");
        }

        [Fact]
        public void For_should_throw_when_manual_parameter_length_out_of_bounds()
        {
            // arrange
            var handler = (object[] _) => { };

            // act
            var ex = Should.Throw<InvalidOperationException>(() => Callback.For(handler.Target, handler.Method,
                new[] { typeof(int[]), typeof(int) }, new uint?[] { 2, null }));

            // assert
            ex.Message.ShouldContain("out of bounds");
        }

        [Fact]
        public void For_should_throw_when_parameter_length_is_not_an_integer()
        {
            // arrange
            var handler = ([ParameterLength(2)] int[] _, int _, float _) => { };

            // act
            var ex = Should.Throw<InvalidOperationException>(() => Callback.For(handler.Target, handler.Method));
            
            // assert
            ex.Message.ShouldContain("integer");
        }

        [Fact]
        public void For_should_succeed_with_manual_parameter_types()
        {
            // arrange
            var handler = (object[] _) => { };
            var types = new[]
            {
                typeof(bool[]), typeof(bool), typeof(int[]), typeof(int), typeof(float[]), typeof(float),
                typeof(string)
            };

            // act
            var ex = Should.Throw<InvalidOperationException>(() => Callback.For(handler.Target, handler.Method, types));
            
            // assert
            ex.Message.ShouldContain("integer");
        }
        
        [Fact]
        public void For_should_throw_with_unknown_parameter_types()
        {
            // arrange
            var handler = (int _, double _) => { };

            // act
            var ex = Should.Throw<InvalidOperationException>(() => Callback.For(handler.Target, handler.Method));
            
            // assert
            ex.Message.ShouldContain("unsupported");
        }

        [Fact]
        public void For_should_throw_with_manual_unknown_parameter_types()
        {
            // arrange
            var handler = (object[] _) => { };

            // act
            var ex = Should.Throw<InvalidOperationException>(() =>
                Callback.For(handler.Target, handler.Method, new[] { typeof(int), typeof(double) }));
            
            // assert
            ex.Message.ShouldContain("unsupported");
        }

        [Fact]
        public void For_should_succeed_with_manual_parameter_lengths()
        {
            // arrange
            var handler = (object[] _) => { };
            var types = new[] { typeof(int), typeof(int[]), typeof(int), typeof(int) };
            var lengths = new uint?[] { null, 3, null, null };

            // act
            var sut = Callback.For(handler.Target, handler.Method, types, lengths);
            
            // assert
            var param = sut.Parameters[1].ShouldBeAssignableTo<ICallbackArrayParameter>()!;
            param.LengthOffset.ShouldBe(2);
        }
        
        [Fact]
        public unsafe void Invoke_should_call_handler()
        {
            // cannot test with arguments because it only works on 32-bit and our test suite runs 64 bit.

            // arrange
            var called = false;
            var handler = () => called = true;

            var sut = Callback.For(handler.Target, handler.Method);

            var args = new[] { 0 };
            var retval = 0;

            // act
            fixed (int* argsPtr = args)
            {
                sut.Invoke(IntPtr.Zero, (IntPtr)argsPtr, (IntPtr)(&retval));
            }

            // assert
            called.ShouldBeTrue();
        }

        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        [InlineData(123, 123)]
        [InlineData(-123, -123)]
        [InlineData(0f, 0)]
        [InlineData(1.23f, 0x3f9d70a4)]
        [InlineData(-1.23f, unchecked((int)0xbf9d70a4))]
        public unsafe void Invoke_should_set_retval(object result, int expectedRetval)
        {
            // cannot test with arguments because it only works on 32-bit and our test suite runs 64 bit.

            // arrange
            var handler = () => result;

            var sut = Callback.For(handler.Target, handler.Method);

            var args = new[] { 0 };
            var retval = 0;

            // act
            fixed (int* argsPtr = args)
            {
                sut.Invoke(IntPtr.Zero, (IntPtr)argsPtr, (IntPtr)(&retval));
            }

            // assert
            retval.ShouldBe(expectedRetval);
        }
    }
}
