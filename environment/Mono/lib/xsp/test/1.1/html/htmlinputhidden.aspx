<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>HtmlInputHidden</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form id="myForm" name="myform" action="htmlinputhidden.aspx" method="post" 
runat="server">
This page should contain a hidden input field.
<input id="myHidden" type=hidden value="I'm hidden" runat="server"> 
</form>
</body>
</html>

