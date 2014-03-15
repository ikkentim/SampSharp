<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
	<script runat="server">
	public class NumberMessage
	{
		private int number;
		private string message;
		
		public NumberMessage (int number, string message)
		{
			this.number = number;
			this.message = message;
		}

		public int Number 
		{
			get { return number; }
		}

		public string Message 
		{
			get { return message; }
		}
	}
	
	private void Page_Load (object sender, EventArgs e)
	{
		if (!IsPostBack){
			optionsList.Add (new NumberMessage (1, "One"));
			optionsList.Add (new NumberMessage (2, "Two"));
			optionsList.Add (new NumberMessage (3, "Three"));
			optionsList.Add (new NumberMessage (4, "Four"));
			optionsList.Add (new NumberMessage (5, "Five"));
			list.DataSource = optionsList;
			list.DataBind();
		}
		else
			msg.Text = "Selected option: " + list.SelectedItem.Text + " " + list.SelectedItem.Value;
	}
	</script>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
	<object id="optionsList" runat="server" class="System.Collections.ArrayList" />
	<h3>Data binding using an array list containing a class</h3>
	DataTextField and DataValueField must contain property names of the
	class bound to the DropDownList.
	<form id="form" runat="server">     
		<asp:DropDownList id="list" runat="server" autopostback="true" 
		datatextfield="Message" datavaluefield="Number"/>
		<asp:Label id="msg" runat="server" />
	</form>
</body>
</html>

