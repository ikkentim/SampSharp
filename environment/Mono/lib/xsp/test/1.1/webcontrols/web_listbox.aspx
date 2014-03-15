<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>ListBox</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
Single selection:
<p>
<asp:ListBox id="lbs" rows="5" SelectionMode="single" Width="80px" runat="server">
	<asp:ListItem>1</asp:ListItem>
	<asp:ListItem>2</asp:ListItem>
	<asp:ListItem>3</asp:ListItem>
	<asp:ListItem>4</asp:ListItem> 
	<asp:ListItem>5</asp:ListItem> 
	<asp:ListItem>6</asp:ListItem>
	<asp:ListItem>7</asp:ListItem>
	<asp:ListItem>8</asp:ListItem>
	<asp:ListItem>9</asp:ListItem> 
	<asp:ListItem>10</asp:ListItem> 
</asp:ListBox>
<p>
Multiple selection:
<p>
<asp:ListBox id="lbm" rows="5" SelectionMode="Multiple" Width="80px" runat="server">
	<asp:ListItem>1</asp:ListItem>
	<asp:ListItem>2</asp:ListItem>
	<asp:ListItem>3</asp:ListItem>
	<asp:ListItem>4</asp:ListItem> 
	<asp:ListItem>5</asp:ListItem> 
	<asp:ListItem>6</asp:ListItem>
	<asp:ListItem>7</asp:ListItem>
	<asp:ListItem>8</asp:ListItem>
	<asp:ListItem>9</asp:ListItem> 
	<asp:ListItem>10</asp:ListItem> 
</asp:ListBox>
</form>
</body>
</html>

