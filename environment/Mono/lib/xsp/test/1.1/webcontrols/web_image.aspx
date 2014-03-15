<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>Image</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
<asp:Image id="im" runat="server"
	AlternateText="Yes, powered by Mono"
	ImageAlign="left"
	ImageUrl="http://www.mono-project.com/files/0/08/Mono-powered.png"/>
</form>
</body>
</html>
