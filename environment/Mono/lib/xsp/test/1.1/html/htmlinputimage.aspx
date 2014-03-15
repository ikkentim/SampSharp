<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<title>HtmlInputImage</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form id="myForm" name="myform" action="htmlinputimage.aspx" runat="server">
<input type=image ID="myImageAgain" 
src="http://www.mono-project.com/files/8/8d/Mono-gorilla-aqua.100px.png"
runat="server" alt="The Ximian dancing monkey logo" align="middle">
</form>
</body>
</html>

