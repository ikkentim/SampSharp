<%@ Page language="c#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>List Items</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>

    <h3>ListItem test</h3>
    <form runat="server">
        <asp:DropDownList id="NumberList" runat="server">
          <asp:ListItem>One</asp:ListItem>
          <asp:ListItem>Two</asp:ListItem>
          <asp:ListItem>Three</asp:ListItem>
          <asp:ListItem>Cuatro</asp:ListItem>
          <asp:ListItem>Five</asp:ListItem>
          <asp:ListItem>Six</asp:ListItem>
          <asp:ListItem>Seven</asp:ListItem>
          <asp:ListItem>Eight</asp:ListItem>
        </asp:DropDownList>       
    </form>
</body>
</html>
