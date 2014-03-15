<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<script runat="server">
	void Clicked (object sender, EventArgs e)
	{
	       label1.Text = " &lt;- you've just clicked it!";
	}

	void OnLb2Command (object sender, CommandEventArgs e)
	{
	       if (e.CommandName == "Remove_this") {
	              lb2.Visible = false;
	              label2.Text = "There used to be a link here, but you have removed it";
	       }
	}
</script>
<title>LinkButton as submit and command</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
<asp:LinkButton id="lb1" Text="Click me!" OnClick="Clicked" runat="server"/>
<asp:Label id="label1" Text="" runat="server"/>
<br>
<asp:LinkButton id="lb2" CommandName="Remove_this" OnCommand="OnLb2Command" runat="server">
Remove this link.
</asp:LinkButton>
<asp:Label id="label2" Text="" runat="server"/>
</form>
</body>
</html>

