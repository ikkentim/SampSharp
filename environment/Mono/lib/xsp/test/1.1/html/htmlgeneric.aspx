<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<script runat="server">
	public void Page_Load (object sender, EventArgs e)
	{
		mySpan.InnerText = "This is ok";
	}
</script>
<title>Just a HtmlGenericControl (a span in this case) fullfilled
in Page_Load ()</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<span id="mySpan" runat="server" />
</body>
</html>

