<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<script runat=server>
	void Clicked (object o, EventArgs e)
	{
	}
</script>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>CheckBox</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
<asp:CheckBox id="chk"
	Text="Click here!"
	AutoPostBack="True"
	OnCheckedChanged="Clicked"
	runat="server"/>
<br>
<asp:CheckBox id="chk2"
	Text="Click also here!"
	AutoPostBack="True"
	align=right
	OnCheckedChanged="Clicked"
	runat="server"/>
</form>
</body>
</html>
