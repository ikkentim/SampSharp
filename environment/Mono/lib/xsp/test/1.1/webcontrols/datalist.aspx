<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<HTML>

	<HEAD>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
		<title>DataList</title>

		<script runat="server">
	void Page_Load (object o, EventArgs e) 
	{
		if (!IsPostBack) {
			ArrayList list = new ArrayList ();
			list.Add (new Data ("Spain"));
			list.Add (new Data ("Japan"));
			list.Add (new Data ("Mexico"));
			dl.DataSource = list;
			dl.DataBind ();
		}
	}

	public class Data 
	{
		private string name;

		public Data (string testname)
		{
			this.name = testname;
		}

		public string Name 
		{
			get { return name; }
		}

		public override string ToString ()
		{
			return name;
		}
	}
</script>

	</HEAD>

	<body><mono:MonoSamplesHeader runat="server"/>

		<h3>Datalist sample</h3>

		<form runat="server" ID="Form1">

			<asp:DataList id="dl" runat="server" RepeatDirection="Horizontal"
RepeatColumns="5">

				<ItemTemplate>

					<asp:Label Text='<%# DataBinder.Eval (Container.DataItem,"Name") %>'
					runat="server" />

				</ItemTemplate>

			</asp:DataList>

		</form>

	</body>

</HTML>
