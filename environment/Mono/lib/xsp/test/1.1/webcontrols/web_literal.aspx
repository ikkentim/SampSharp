<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>Literal</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
<asp:literal id="lit" Text="Hi there!" runat="server"/>
</form>
</body>
</html>

