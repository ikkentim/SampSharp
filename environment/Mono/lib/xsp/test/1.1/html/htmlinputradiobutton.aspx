<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<title>HtmlInputRadioButton</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form method=post runat="server">
<input id="rb1" type=radio name="group1" checked runat="server"/>
One<br>
<input id="rb2" type=radio name="group1" checked runat="server"/>
Two<br>
<input id="rb3" type=radio name="group1" checked runat="server"/>
Three<br>
<input id="rb4" type=radio name="group2" checked runat="server"/>
One bis<br>
<input id="rb5" type=radio name="group2" checked runat="server"/>
Two bis<br>
</form>
</body>
</html>

