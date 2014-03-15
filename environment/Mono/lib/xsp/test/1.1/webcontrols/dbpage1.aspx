<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%@ import namespace="System.Configuration" %>
<%@ import namespace="System.Data" %>
<%@ import namespace="System.Reflection" %>
<%@ import namespace="System.IO" %>
<html>
<script runat=server>

	static Type cncType = null;

	void GetConnectionData (out string providerAssembly, out string cncTypeName, out string cncString)
	{
		providerAssembly = null;
		cncTypeName = null;
		cncString = null;
		NameValueCollection config = ConfigurationSettings.AppSettings as NameValueCollection;
		if (config != null) {
			foreach (string s in config.Keys) {
				if (0 == String.Compare ("DBProviderAssembly", s, true)) {
					providerAssembly = config [s];
				} else if (0 == String.Compare ("DBConnectionType", s, true)) {
					cncTypeName = config [s];
				} else if (0 == String.Compare ("DBConnectionString", s, true)) {
					cncString = config [s];
				}
			}
		}

	        Version ver = Environment.Version;
		if (providerAssembly == null || providerAssembly == "")
			if (ver.Major == 2)
	                         providerAssembly = "Mono.Data.Sqlite, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";
	                else if (ver.Major == 4)
	            		 providerAssembly = "Mono.Data.Sqlite, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";
			else
				throw new InvalidOperationException (String.Format ("Framework version {0} is not supported by this demo.", ver));

		if (cncTypeName == null || cncTypeName == "")
			cncTypeName = "Mono.Data.Sqlite.SqliteConnection";
		
		if (cncString == null || cncString == "") {
	                string dbPath = Path.Combine (Path.GetDirectoryName (Request.MapPath (Request.FilePath)), "dbpage1.sqlite");
			cncString = String.Format ("URI=file:{0},Version=3", dbPath);
	        }
	}

	void ShowError (Exception exc)
	{
		noDBLine.InnerHtml += "<p><b>The error was:</b>\n<pre> " + exc + "</pre><p>";
		theForm.Visible = false;
		noDBLine.Visible = true;
	}

	IDbConnection cnc;
	void Page_Init (object sender, EventArgs e)
	{
		string connectionTypeName;
		string providerAssemblyName;
		string cncString;

		GetConnectionData (out providerAssemblyName, out connectionTypeName, out cncString);
		if (cncType == null) {		
			Assembly dbAssembly = Assembly.LoadWithPartialName (providerAssemblyName);
                        if (dbAssembly == null)
                                throw new ApplicationException (String.Format ("Data provider assembly '{0}' not found",
                                                                providerAssemblyName));

			cncType = dbAssembly.GetType (connectionTypeName, true);
			if (!typeof (IDbConnection).IsAssignableFrom (cncType))
				throw new ApplicationException ("The type '" + cncType +
								"' does not implement IDbConnection.\n" +
								"Check 'DbConnectionType' in server.exe.config.");
		}

		cnc = (IDbConnection) Activator.CreateInstance (cncType);
		cnc.ConnectionString = cncString;
		try {
			cnc.Open ();
		} catch (Exception exc) {
			ShowError (exc);
		}
	}

	void Page_Unload ()
	{
		if (cnc != null) {
			try {
				cnc.Close ();
			} catch {}
			cnc = null;
		}
	}

	void Page_Load (object sender, EventArgs e)
	{
		if (!IsPostBack){
			PersonFilter.Text = "%";
			MailFilter.Text = "%";
			UpdateTable (PersonFilter.Text, MailFilter.Text);
		}
	}

	void Filter_Changed (object sender, EventArgs e)
	{
		UpdateTable (PersonFilter.Text, MailFilter.Text);
	}

	void UpdateTable (string filterPerson, string filterMail)
	{
		if (cnc == null)
			return;

		IDbCommand selectCommand = cnc.CreateCommand();
		IDataReader reader;

		string selectCmd = "SELECT * FROM test " + 
				   "WHERE person like '" + filterPerson  + "' AND " +
					 "email like '" + filterMail + "'";

		selectCommand.CommandText = selectCmd;
                int rowCount = 0;
		try {
			reader = selectCommand.ExecuteReader ();
			while (reader.Read ()) {
				TableRow row = new TableRow ();
				for (int i = 0; i < reader.FieldCount; i++) {
					TableCell cell = new TableCell ();
					cell.Controls.Add (new LiteralControl (reader.GetValue (i).ToString ()));
					row.Cells.Add (cell);
				}
				myTable.Rows.Add (row);
				rowCount++;
			}
			if (rowCount == 0) {
				TableRow row = new TableRow ();
				TableCell cell = new TableCell ();
				cell.Controls.Add (new LiteralControl ("No results returned from query."));
				row.Cells.Add (cell);
				myTable.Rows.Add (row);
			}
		} catch (Exception exc) {
			ShowError (exc);
		}
	}

</script>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>Some DB testing</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<span runat="server" visible="false" id="noDBLine">
<h3>Database Error</h3>
Sorry, a database error has occurred.
<p>
You should set up a database of your choice and then modify the variables DBProviderAssembly, DBConnectionType and
DBConnectionString in <tt>xsp.exe.config</tt> (or <tt>mod-mono-server.exe.config</tt> if running the tests under Apache/mod_mono)
file to fit your needs.
<p>
The database should have a table called customers created with the following command (or similar):
<pre>
CREATE TABLE "test" (
	"person" character varying(256) NOT NULL,
	"email" character varying(256) NOT NULL
);

</pre>
</span>

<form id="theForm" runat="server">
Choose the SQL filters and click 'Submit'.
<p>
<asp:Label runat="server" Text="Person Filter: "/>
<asp:TextBox id="PersonFilter" Text="" TextMode="singleLine" runat="server" maxlength="40" />
<p>
<asp:Label runat="server" Text="Mail Filter: " />
<asp:TextBox id="MailFilter" Text="" TextMode="singleLine" runat="server" maxlength="40" />
<p>
<asp:Button id="btn" runat="server" Text="Submit" OnClick="Filter_Changed" />
<p>
<asp:Table id="myTable" HorizontalAlign="Left" Font-Size="12pt" GridLines="both" 
CellPadding="5" runat="server"/>
</form>
</body>
</html>
