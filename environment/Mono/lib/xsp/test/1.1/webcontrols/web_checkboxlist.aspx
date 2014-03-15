<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>CheckBoxList</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
Flow layout:
<p>
<asp:CheckBoxList id="l1" RepeatLayout="flow" runat="server">
<asp:ListItem>One</asp:ListItem>
<asp:ListItem>Two</asp:ListItem>
<asp:ListItem>Three</asp:ListItem>
<asp:ListItem>Five</asp:ListItem>
</asp:CheckBoxList>
<p>
Table layout:
<p>
<asp:CheckBoxList id="l2" RepeatLayout="table" runat="server">
<asp:ListItem>One</asp:ListItem>
<asp:ListItem>Two</asp:ListItem>
<asp:ListItem>Three</asp:ListItem>
<asp:ListItem>Five</asp:ListItem>
</asp:CheckBoxList>
</form>
</body>
</html>

