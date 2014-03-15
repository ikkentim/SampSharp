<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>HtmlInputFile</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form id="myForm" name="myform" action="htmlinputfile.aspx" method="post" enctype="image/jpeg" runat="server">
Pick a JPEG file:
<input id="myFile" type="file" runat="server"> 
<br>
<input id="smt" type="submit" value="Go send it!" runat="server">
</form>
</body>
</html>

