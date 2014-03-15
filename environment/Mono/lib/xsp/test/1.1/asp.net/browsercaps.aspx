<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%@ Import namespace="System.Reflection" %>
<style type="text/css">
<!--
.bold {
    font-family: Verdana, Helvetica, sans-serif;
    font-size: 11px;
    font-weight: bold
}

div.normal {
    font-family: Verdana, Helvetica, sans-serif;
    font-size: 11px;
    text-align: left;
}

.normal {
    font-family: Verdana, Helvetica, sans-serif;
    font-size: 11px;
    text-align: center;
}

.error {
    font-family: Verdana, Helvetica, sans-serif;
    color: red;
    font-size: 11px;
    text-align: center;
}
//-->
</style>
<script runat="server">
	void Page_Load (object o, EventArgs e) 
	{
		if (!IsPostBack) {
			PropertyInfo [] props = typeof (HttpBrowserCapabilities).GetProperties ();
			ArrayList list = new ArrayList ();
			Property.Browser = Request.Browser;
			foreach (PropertyInfo prop in props) {
				list.Add (new Property (prop));
			}
			list.Sort (new PropertyComparer ());
			dg.DataSource = list;
			dg.DataBind ();
		}
	}

	public class Property
	{
		PropertyInfo prop;
		static HttpBrowserCapabilities browser;

		public static HttpBrowserCapabilities Browser {
			set { browser = value; }
		}
		
		public Property (PropertyInfo prop)
		{
			this.prop = prop;
		}

		public string PropertyName {
			get { return prop.Name; }
		}

		public string PropertyValue {
			get {
				try {
					return prop.GetValue (browser, null).ToString ();
				} catch (Exception e) {
					return String.Format ("<div class=\"error\">Error: {0}</div>", e.Message);
				}
			}
		}
	}

	class PropertyComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			return String.Compare (((Property)x).PropertyName, ((Property)y).PropertyName);
		}
	}	

</script>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<h3>HttpBrowserCapabilities</h3>
<form runat="server">
	<div class="normal">User agent: <%= Request.UserAgent %></div><br>
	<asp:datagrid id="dg" border="1" AutoGenerateColumns="false" EnableViewState="false" runat="server">
	    <Columns>
		<asp:BoundColumn HeaderText="Property" DataField="PropertyName"
			ItemStyle-Cssclass="normal"
			HeaderStyle-HorizontalAlign="Center" HeaderStyle-Cssclass="bold"/>
		<asp:BoundColumn HeaderText="Value" DataField="PropertyValue"
			ItemStyle-Cssclass="normal"
			HeaderStyle-HorizontalAlign="Center" HeaderStyle-Cssclass="bold"/>
	    </Columns>
	</asp:datagrid>
</form>
</body>
</html>

