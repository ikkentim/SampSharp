<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
	<script runat="server">
	private void Page_Load (object sender, EventArgs e)
	{
		if (!IsPostBack){
			optionsList.Add ("One");
			optionsList.Add ("Two");
			optionsList.Add ("Three");
			optionsList.Add ("Four");
			optionsList.Add ("Five");
			list.DataSource = optionsList;
			list.DataBind();
		}
		else
			msg.Text = "Selected option: " + list.SelectedItem.Text;
	}
	</script>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
	<object id="optionsList" runat="server" class="System.Collections.ArrayList" />
	<h3>Data binding using an array list</h3>
	<form id="form" runat="server">     
		<asp:DropDownList id="list" runat="server" autopostback="true" />
		<asp:Label id="msg" runat="server" />
	</form>
</body>
</html>

