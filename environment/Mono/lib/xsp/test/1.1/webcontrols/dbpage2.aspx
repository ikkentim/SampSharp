<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%@ import namespace="System.Data" %>
<%@ import namespace="System.Data.SqlClient" %>
<%@ import namespace="System.Reflection" %>
<%@ Import namespace="System.IO" %>
<%@ Register TagPrefix="Mono" Namespace="Mono.Controls" assembly="tabcontrol2" %>
<html>
<!-- You must compile tabcontrol2.cs and copy the dll to the output/ directory -->
<!-- Authors:
--	Gonzalo Paniagua Javier (gonzalo@ximian.com)
--	(c) 2002 Ximian, Inc (http://www.ximian.com)
-->
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
	                string dbPath = Path.Combine (Path.GetDirectoryName (Request.MapPath (Request.FilePath)), "dbpage2.sqlite");
			cncString = String.Format ("URI=file:{0},Version=3", dbPath);
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

	void Page_PreRender (object sender, EventArgs e)
	{
		if (cnc == null)
			return;

		if (tabs.CurrentTabName == "Browse") {
			string selectCmd = "SELECT id, name, address FROM customers ORDER by id";
			UpdateTable (selectCmd, browse);
			return;
		}

		if (deleteID.Visible == true)
			deleteIDLabel.InnerHtml = "ID: ";

		if (confirmDelete.Visible == true){
			string s_deleteID = deleteID.Text.Trim ();
                        if (s_deleteID != null && s_deleteID.Length > 0) {
			       uint dbid = UInt32.Parse (s_deleteID);
			       string selectCmd = String.Format ("SELECT id, name, address FROM customers WHERE id = {0}", dbid);
			       UpdateTable (selectCmd, deleteTable);
                        }
			return;
		}
	}

	private void UpdateTable (string selectCmd, Table table)
	{
		IDbCommand selectCommand = cnc.CreateCommand();

		selectCommand.CommandText = selectCmd;
		try {
			IDataReader reader = selectCommand.ExecuteReader ();
			table.Rows.Clear ();
			while (reader.Read ()) {
				TableRow row = new TableRow ();
				for (int i = 0; i < reader.FieldCount; i++) {
					TableCell cell = new TableCell ();
					object data = reader.GetValue (i);
					if (data == null)
						data = "(null)";
					cell.Controls.Add (new LiteralControl (data.ToString ()));
					row.Cells.Add (cell);
				}
				table.Rows.Add (row);
			}
			reader.Close ();
		} catch (Exception exc) {
			ShowError (exc);
		}
	}

	private void DoInsert (uint dbid, string dbname, string dbaddress)
	{
		string dbNameRep = dbname.Replace ("'", "\\'");
		string dbAddressRep = dbaddress.Replace ("'", "\\'");

		string insertCmd = String.Format ("INSERT INTO customers VALUES ({0}, '{1}', '{2}')",
						   dbid, dbNameRep, dbAddressRep);
		IDbCommand insertCommand = cnc.CreateCommand();
		insertCommand.CommandText = insertCmd;
		int i;
		try {
			i = insertCommand.ExecuteNonQuery ();
			statusLine.InnerHtml = String.Format ("{0} rows(s) inserted", i);
			dbID.Text = "";
			dbName.Text = "";
			dbAddress.Text = "";
		} catch (Exception e) {
			errorLine.InnerHtml = "<b>Error inserting row: " + e.Message + "</b>";
		}
	}
	
	void InsertData (object o, EventArgs args)
	{
		string s_dbid = dbID.Text.Trim ();
		string s_dbname = dbName.Text.Trim ();
		string s_dbaddress = dbAddress.Text.Trim ();
		if (s_dbid == "" || s_dbname == "" || s_dbaddress == "") {
			errorLine.InnerHtml = "<b>All fields must be filled</b>";
			return;
		}

		try {
			uint dbid = UInt32.Parse (s_dbid);
			DoInsert (dbid, s_dbname, s_dbaddress);
		} catch (Exception e) {
			errorLine.InnerHtml = "<b>Error parsing ID: " + e.Message + "</b>";
		}
	}

	void DeleteData (object o, EventArgs args)
	{
		string s_deleteID = deleteID.Text.Trim ();
		if (s_deleteID == "") {
			errorLine.InnerHtml = "<b>Empty ID!</b>";
			return;
		}

		try {
			uint dbid = UInt32.Parse (s_deleteID);
			deleteSubmit.Visible = false;
			deleteID.Visible = false;
			deleteTable.Visible = true;
			confirmDelete.Visible = true;
			deleteIDLabel.InnerHtml = "ID: " + dbid;
		} catch (Exception e) {
			errorLine.InnerHtml = "<b>Error parsing ID: " + e.Message + "</b>" + " " + s_deleteID;
		}
	}

	void ConfirmDeleteData (object o, EventArgs args)
	{
		string s_deleteID = deleteIDLabel.InnerHtml.Substring (4).Trim ();
		try {
			uint dbid = UInt32.Parse (s_deleteID);
			DoDelete (dbid);
		} catch (Exception e) {
			errorLine.InnerHtml = "<b>Error parsing ID: " + e.Message + "</b>" + " " + s_deleteID;
		}

		deleteSubmit.Visible = true;
		deleteID.Visible = true;
		deleteIDLabel.InnerHtml = "ID: ";
		deleteTable.Visible = false;
		deleteID.Text = "";
	}

	private void DoDelete (uint dbid)
	{
		string deleteCmd = String.Format ("DELETE FROM customers WHERE id = {0}", dbid);
		IDbCommand deleteCommand = cnc.CreateCommand();
		deleteCommand.CommandText = deleteCmd;
		int i;
		try {
			i = deleteCommand.ExecuteNonQuery ();
			statusLine.InnerHtml = String.Format ("{0} row(s) deleted", i);
		} catch (Exception e) {
			errorLine.InnerHtml = "<b>Error deleting row: " + e.Message + "</b>";
		}
	}

	void UpdateData (object o, EventArgs args)
	{
		uint dbid = 0;
		try {
			dbid = UInt32.Parse (updateID.Text.Trim ());
		} catch (Exception e) {
			errorLine.InnerHtml = "<b>Error parsing ID: " + e.Message + "</b>" + " " + updateID.Text;
			return;
		}

		string s_updatename = updateName.Text.Trim ();
		string s_updateaddress = updateAddress.Text.Trim ();
		if (s_updatename == "" && s_updateaddress == "") {
			errorLine.InnerHtml = "<b>At least one of name or address must be filled.</b>";
			return;
		}

		DoUpdate (dbid, s_updatename, s_updateaddress);
	}

	void RefreshUpdateData (object o, EventArgs args)
	{
		uint dbid = 0;
		try {
			dbid = UInt32.Parse (updateID.Text.Trim ());
		} catch (Exception e) {
			errorLine.InnerHtml = "<b>Error parsing ID: " + e.Message + "</b>" + " " + updateID.Text;
			return;
		}

		string selectCmd = String.Format ("SELECT name, address FROM customers WHERE id = {0}", dbid);
		
		IDbCommand selectCommand = cnc.CreateCommand();
		selectCommand.CommandText = selectCmd;
		IDataReader reader = selectCommand.ExecuteReader ();
		if (!reader.Read ()) {
			errorLine.InnerHtml = "<b>No such ID: " + dbid + "</b>";
			updateName.Text = "";
			updateAddress.Text = "";
			reader.Close ();
			return;
		}
		updateName.Text = reader.GetValue (0) as string;
		updateAddress.Text = reader.GetValue (1) as string;
		reader.Close ();
	}

	private void DoUpdate (uint dbid, string dbname, string dbaddress)
	{
		string dbNameRep = dbname.Replace ("'", "\\'");
		string dbAddressRep = dbaddress.Replace ("'", "\\'");

		string updateCmd = String.Format ("UPDATE customers SET name = '{1}', address = '{2}' WHERE id = {0}",
						   dbid, dbNameRep, dbAddressRep);
		IDbCommand updateCommand = cnc.CreateCommand();
		updateCommand.CommandText = updateCmd;
		int i;
		try {
			i = updateCommand.ExecuteNonQuery ();
			statusLine.InnerHtml = String.Format ("{0} rows(s) updated", i);
			updateID.Text = "";
			updateName.Text = "";
			updateAddress.Text = "";
		} catch (Exception e) {
			errorLine.InnerHtml = "<b>Error updating row: " + e.Message + "</b>";
		}
	}
</script>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>More DB testing plus tabcontrol2.dll</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<span runat="server" visible="false" id="noDBLine">
<h3>Database Error</h3>
Sorry, could not connect to a database.
<p>
You should set up a database for user <i>'monotest'</i>,
password <i>'monotest'</i> and dbname <i>'monotest'</i>
<p>
Then modify the variables DBProviderAssembly, DBConnectionType and
DBConnectionString in server.exe.config file to fit your needs.
<p>
The database should have a table called customers created with the following command:
<pre>
CREATE TABLE "customers" (
	"id" integer NOT NULL,
	"name" character varying(256) NOT NULL,
	"address" character varying(256) NOT NULL
);

CREATE UNIQUE INDEX id_idx ON customers USING btree (id);
</pre>
</span>
<form id="theForm" runat="server">
	<Mono:Tabs2 runat="server" id="tabs">
		<Mono:TabContent id="BrowseTab" runat="server" label="Browse">
			<p>
			Below, the rows of the table are displayed (if any).
			<p>
			<asp:Table EnableViewState="false" id="browse" HorizontalAlign="Left" Font-Size="12pt"
				   GridLines="both" CellPadding="5" runat="server"/>
			<br>
			<p>
			<asp:Button runat="server" id="refreshBrowse" Text="Refresh" />
		</Mono:TabContent>
		<Mono:TabContent id="InsertTab" runat="server" label="Insert">
			<p>
			Fill in the data and click the button when done. All fields are mandatory.
			<p>
			ID: <asp:TextBox runat="server" id="dbID" columns="10" />
			<p>
			Name: <asp:TextBox runat="server" id="dbName" columns="40" />
			<p>
			Address: <asp:TextBox runat="server" id="dbAddress" columns="40" />
			<p>
			<asp:Button runat="server" id="insertSubmit" Text="Insert data" OnClick="InsertData" />
		</Mono:TabContent>
		<Mono:TabContent id="DeleteTab" runat="server" label="Delete">
			<p>
			Deletes a row by its ID.
			<p>
			<span runat="server" id="deleteIDLabel">ID:</span>
			<asp:TextBox runat="server" id="deleteID" columns="10" />
			<p>
			<asp:Table EnableViewState="false" visible="false" id="deleteTable" HorizontalAlign="Left" 
				   Font-Size="12pt" GridLines="both" CellPadding="5" runat="server"/>
			<br>
			<p>
			<asp:Button runat="server" id="deleteSubmit" Text="Delete" OnClick="DeleteData" />
			<asp:Button runat="server" id="confirmDelete" Visible="false" Text="Really delete?" OnClick="ConfirmDeleteData" />
		</Mono:TabContent>
		<Mono:TabContent id="UpdateTab" runat="server" label="Update">
			<p> The ID field acts as unique index. The other fields will be modified.<br>
			If you fill the ID, you can push "Refresh data" to get name and address from the database.
			<p>
			ID: <asp:TextBox runat="server" id="updateID" columns="10" />
			<p>
			Name: <asp:TextBox runat="server" id="updateName" columns="40" />
			<p>
			Address: <asp:TextBox runat="server" id="updateAddress" columns="40" />
			<p>
			<asp:Button runat="server" id="updateSubmit" Text="Update DB" OnClick="UpdateData" />
			<asp:Button runat="server" id="refreshUpdateSubmit" Text="Refresh data" OnClick="RefreshUpdateData" />
		</Mono:TabContent>
	</Mono:Tabs2>
	<p>
	<span runat="server" style="color: blue;" EnableViewState="false" id="statusLine" Text="" />
	&nbsp;
	<p>
	<span runat="server" style="color: red;" EnableViewState="false" id="errorLine" Text="" />
</form>
</body>
</html>

