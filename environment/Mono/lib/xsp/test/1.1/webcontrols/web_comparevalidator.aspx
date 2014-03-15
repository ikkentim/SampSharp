<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<script runat="server">
void Check_Click(Object src, EventArgs E) {
	message.Text = "Entered data is " + (Page.IsValid ? "valid." : "invalid!");
}
</script>

<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>CompareValidator</title>
</head>

<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
<asp:Label text="Enter twice the same string:" runat="server"/><br>
<asp:TextBox id="Text1" runat="server" /> ==
<asp:TextBox id="Text2" runat="server" />
<asp:CompareValidator runat="server"
	EnableClientScript="true"
	ControlToValidate="Text1" ControlToCompare="Text2" Operation="Equal"
	ErrorMessage="Strings do not match!"/><br />
	
<br />

<asp:Button text="Check" onclick="Check_Click" runat="server"/><br/ >

<br />

<asp:Label id="message" runat="server"/>
</form>
</body>
</html>

