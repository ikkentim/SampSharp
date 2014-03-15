<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>A HtmlForm with a HtmlButton inside</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form method=post runat="server">
<input id="myButton" type=submit value="Enter" runat="server"/>
</form>
</body>
</html>

