<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>HtmlSelect</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
Select a language:<br>
	<select id="lselect" runat="server">
	<option>C</option>
	<option>C#</option>
	<option>C++</option>
	</select>
</form>
</body>
</html>
