<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<title>HtmlInputText: text and password</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form method=post runat="server">
<input type="text" id="asText" maxlength=40 size=40 value="Your name goes here" runat="server"/>
<br>
<input type="password" id="asPassword" maxlength=8 size=8 value="Your name goes here" runat="server"/>
</form>
</body>
</html>

