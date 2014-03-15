<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%@ Register TagPrefix="Mono" Namespace="Mono.Controls" assembly="typedesc" %>
<html>
<head>
<title>Property that needs TypeConverter</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<Mono:WeirdControl id="weird" runat="server" WeirdObject="Passed!"/>
</body>
</html>
