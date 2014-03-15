<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>HyperLink</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<asp:HyperLink id="hyper"
	ImageUrl="http://www.mono-project.com/files/8/8d/Mono-gorilla-aqua.100px.png"
	NavigateUrl="http://www.ximian.com"
	Text="Ximian"
	Target="_top"
	runat="server">

</asp:HyperLink>

</body>
</html>
