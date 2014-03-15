<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>DataGrid + Remove command</title>
<style type="text/css">
<!--
.Normal
{
    font-family: Verdana, Helvetica, sans-serif;
    font-size: 11px;
    font-weight: normal;
    line-height: 12px    
}

.Bold
{
    font-family: Verdana, Helvetica, sans-serif;
    font-size: 11px;
    font-weight: bold
}
//-->
</style>
<script runat="server">
	// Most of the code in the script is dealing with the ArrayList stuff,
	// but you can use other sources for your data.
	ArrayList list;

	// InitList is only called once per session when a GET is received for the page.
	void InitList ()
	{
		list = new ArrayList ();
		list.Add (new Datum ("Spain", "es", "Europe"));
		list.Add (new Datum ("Japan", "jp", "Asia"));
		list.Add (new Datum ("Austria", "at", "Europe"));
		list.Add (new Datum ("France", "fr", "Europe"));
		list.Add (new Datum ("Great Britain", "gb", "Europe"));
		list.Add (new Datum ("Italia", "it", "Europe"));
		list.Add (new Datum ("India", "in", "Asia"));
		list.Add (new Datum ("Brazil", "br", "America"));
		list.Add (new Datum ("Germany", "de", "Europe"));
		list.Add (new Datum ("Mexico", "mx", "America"));
	}

	void Page_Load (object o, EventArgs e)
	{
		// For this sample, we keep the list in ViewState.
		// If you use a database, get the data from there.
		if (!IsPostBack || ViewState ["%%list"] == null) {
			// Create initial data list and keep it in ViewState.
			// If your data is in a DB, you don't need this.
			InitList ();
			ViewState ["%%list"] = list;
		} else {
			// IsPostBack is true when we get a POST, so we restore the list from
			// the viewstate here.
			list = (ArrayList) ViewState ["%%list"];
		}

		dg.DataSource = list;
		// DataBind actually creates the control hierarchy for the DataGrid.
		// Ie, all those headers, rows, cells, linkbuttons are created after DataBind is called on dg.
		dg.DataBind ();
	}

	// This is invoked whenever a 'Delete' linkbutton is pressed for a row.
	void dg_Delete (object sender, DataGridCommandEventArgs e)
	{
		// e.Item.ItemIndex contains the row index starting from 0.
		Console.WriteLine ("Delete for " + e.Item.ItemIndex);
		
		// Remove the item from our datasource.
		// If you use a database, you should do a Delete from xxx here.
		list.RemoveAt (e.Item.ItemIndex);
		
		// Recreate the control hierarchy because the datasource has been changed.
		dg.DataBind ();
	}

	// In our case, this class needs to be serializable because it's gonna be serialized using the
	// BinaryFormatter when LosFormatter finds this elements inside the ArrayList
	// If you use a dataset to feed the datagrid, you don't need this code.
	[Serializable]
	public class Datum 
	{
		private string country;
		private string abbr;
		private string continent;

		public Datum (string country, string abbr, string continent)
		{
			this.country = country;
			this.abbr = abbr;
			this.continent = continent;
		}

		public string Country 
		{
			get { return country; }
		}

		public string Abbr 
		{
			get { return abbr; }
		}

		public string Continent 
		{
			get { return continent; }
		}

		public override string ToString ()
		{
			return country + " " + abbr + " " + continent;
		}
	}
</script>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<h3>DataGrid sample</h1>
<form runat="server">
	<asp:datagrid id="dg" border="1" AutoGenerateColumns="false"
		EnableViewState="false" runat="server" OnDeleteCommand="dg_Delete">
	    <Columns>
	    	<%-- This is a column which has a 'Delete' LinkButton in it 
		---- When the button is clicked, DeleteCommand in the DataGrid will be called.
		--%>
		<asp:ButtonColumn ButtonType="LinkButton" HeaderText="Delete row" CommandName="Delete" Text="Delete"
			ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="Bold" />
		<asp:BoundColumn HeaderText="Country" DataField="Country"
			ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="Bold" />
		<asp:BoundColumn HeaderText="Continent" DataField="Continent"
			ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="Bold"/>
		<asp:BoundColumn HeaderText="Abbr" DataField="Abbr"
			ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="Bold"/>
	    </Columns>
	</asp:datagrid>
</form>
</body>
</html>

