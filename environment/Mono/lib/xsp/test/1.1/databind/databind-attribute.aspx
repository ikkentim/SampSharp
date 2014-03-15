<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
	<script runat="server">
	private void Page_Load (object sender, EventArgs e)
	{
		if (!IsPostBack){
			ArrayList optionsList = new ArrayList ();
			optionsList.Add ("One");
			optionsList.Add ("Two");
			optionsList.Add ("Three");
			optionsList.Add ("Four");
			optionsList.Add ("Five");
			list.DataSource = optionsList;
			list.DataBind();
		}
		else 
			msg.DataBind ();
	}
	</script>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
	<h3>Data binding in attribute values</h3>
	Another silly example for your pleasure...
	<form id="form" runat="server">     
		<asp:DropDownList id="list" runat="server" autopostback="true" />
		<p>
		<asp:Label id="msg" runat="server" text="<%# list.SelectedItem.Text %>"/>
	</form>
</body>
</html>

