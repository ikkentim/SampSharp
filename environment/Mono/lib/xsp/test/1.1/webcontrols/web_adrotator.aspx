<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>AdRotator</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
<asp:AdRotator id="adr" runat="server"
	AdvertisementFile="web_adrotator.xml"
	Target="_top" />
</form>
</body>
</html>

