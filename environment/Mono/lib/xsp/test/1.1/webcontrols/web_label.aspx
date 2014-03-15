<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<script runat=server>
	void Page_Load (object o, EventArgs e)
	{
		lbl1.Text += ". This added in Page_Load.";
	}
</script>
<head>
<title>Label</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
<asp:label id="lbl1" Text="Text as property" runat="server"/>
<br>
<asp:label id="lbl2" runat="server">Text between tags</asp:label>
</form>
</body>
</html>

