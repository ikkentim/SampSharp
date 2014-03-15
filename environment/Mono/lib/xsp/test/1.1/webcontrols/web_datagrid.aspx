<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>DataGrid</title>
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
	void Page_Load (object o, EventArgs e) 
	{
		if (!IsPostBack) {
			ArrayList list = new ArrayList ();
			list.Add (new Datum ("Spain", "es", "Europe"));
			list.Add (new Datum ("Japan", "jp", "Asia"));
			list.Add (new Datum ("Mexico", "mx", "America"));
			dg.DataSource = list;
			dg.DataBind ();
		}
	}

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
<h3>DataGrid sample</h3>
<form runat="server">
	<asp:datagrid id="dg" border="1" AutoGenerateColumns="false"
		EnableViewState="false" runat="server">
	    <Columns>
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

