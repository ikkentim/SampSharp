<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>HtmlImage</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<img ID="myImage" src="http://www.mono-project.com/files/8/8d/Mono-gorilla-aqua.100px.png"
runat="server" alt="The Ximian dancing monkey logo" align="middle"/>
</body>
</html>

