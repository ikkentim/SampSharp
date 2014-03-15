<%@ Page language="C#" debug="true"%>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%@ Import namespace="System.ComponentModel" %>
<%@ Import namespace="System.Globalization" %>
<!-- This test was used to fix bug #59495
   Authors:
  	Gonzalo Paniagua Javier (gonzalo@ximian.com)
-->

<html>
<head>
<title>ViewState + TypeConverter</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<script runat="server">
	public class MyStuffConverter : TypeConverter
	{
		public override bool CanConvertFrom (ITypeDescriptorContext context, Type sourceType)
		{
			// This is the second thing called. Type: string
			HttpContext.Current.Response.Write ("<!-- CanConvertFrom called -->");
			return true;
			throw new Exception (sourceType.ToString ());
		}

		public override bool CanConvertTo (ITypeDescriptorContext context, Type destinationType)
		{
			// This is called first. Type: System.String
			// return false -> cannot be serialized
			HttpContext.Current.Response.Write ("<!-- CanConvertTo called -->");
			return true;
			//throw new Exception (destinationType.ToString ());
		}

		public override object ConvertFrom (ITypeDescriptorContext context,
						    CultureInfo culture,
						    object value)
		{
			// This is called after clicking the button, even before CanConvertFrom
			HttpContext.Current.Response.Write ("<!-- ConvertFrom called -->");
			MyStuff ms = new MyStuff ();
			ms.somevalue = Convert.ToInt32 (value);
			return ms;
			//throw new Exception (culture.Name);
		}

		public override object ConvertTo (ITypeDescriptorContext context,
						  CultureInfo culture,
						  object value,
						  Type destinationType)
		{
			HttpContext.Current.Response.Write ("<!-- ConvertTo called -->");
			// Third. culture.Name is null or "" -> Invariant. Destination: string
			MyStuff ms = (MyStuff) value;
			return ms.somevalue.ToString (culture);
			//throw new Exception ("\"" + culture.Name + "\""+ " " + destinationType.Name);
		}
	}

	[TypeConverter (typeof (MyStuffConverter))]
	public class MyStuff {
		public int somevalue;
	}

	void Page_Load ()
	{
		if (IsPostBack) {
			MyStuff old = (MyStuff) ViewState ["mystuff"];
			lbl.Text = String.Format ("<br><b>Old value:<b> {0}<br>", old.somevalue);
			old.somevalue++;
			ViewState ["mystuff"] = old;
		} else {
			MyStuff ms = new MyStuff ();
			ViewState ["mystuff"] = ms;
		}
	}
</script>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
<asp:button type="submit" runat="server" Text="Click here" />
<asp:label id="lbl" runat="server" />
</form>
</body>
</html>

