<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>RadioButton</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
	<asp:RadioButton id="r1" Text="One" GroupName="group1" runat="server" Checked="True" />
	<br>
	<asp:RadioButton id="r2" Text="Two" GroupName="group1" runat="server"/>
	<br>
	<asp:RadioButton id="r3"  Text="Three" GroupName="group1" runat="server"/>
	<br>
	Here another group of radio buttons.
	<br>
	<asp:RadioButton id="r4" Text="Ein" GroupName="group2" runat="server"/>
	<br>
	<asp:RadioButton id="r5"  Text="Zwei" GroupName="group2" runat="server" checked="true"/>
	<br>
</form>
</body>
</html>
