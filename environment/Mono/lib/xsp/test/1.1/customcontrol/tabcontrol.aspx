<%@ Page Language="C#" %>
<%@ Import namespace="System.Reflection" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%@ Register TagPrefix="Mono" NAmespace="Mono.Controls" assembly="tabcontrol" %>
<html>
<!-- Authors:
--	Gonzalo Paniagua Javier (gonzalo@ximian.com)
--	(c) 2002 Ximian, Inc (http://www.ximian.com)
--
--   Displays a page with the properties of the user control in tabcontrol and allows
--   modifying them to view the result. You can add/remove properties in tabcontrol.cs
--   without needing to modify this page (unless properties are not of types int or string.
-->
<!-- You must compile tabcontrol.cs and copy the dll to the output/ directory -->

<title>User Control 2</title>
<script runat="server">
	PropertyInfo [] props = null;
	private void EnsureProps ()
	{
		if (props == null) {
			Type t = tabs.GetType ();
			PropertyInfo [] pi = t.GetProperties ();
			int count = 0;
			foreach (PropertyInfo p in pi) {
				if (p.DeclaringType == t)
					count++;
			}

			props = new PropertyInfo [count];
			count = 0;
			foreach (PropertyInfo p in pi) {
				if (p.DeclaringType == t) {
					props [count] = p;
					count++;
				}
			}
		}

	}
	
	void Page_Init (object sender, EventArgs e)
	{
		AddToPlaceHolder ();
	}

	void Page_Load (object sender, EventArgs e)
	{
		if (!IsPostBack)
			UpdateValues ();
	}
	
	private void AddToPlaceHolder ()
	{
		EnsureProps ();
		place.Controls.Clear ();
		foreach (PropertyInfo prop in props) {
			TextBox t = new TextBox ();
			t.ID = "_" + prop.Name;
			t.TextChanged += new EventHandler (PropChanged);
			place.Controls.Add (new LiteralControl (prop.Name + ": "));
			place.Controls.Add (t);
			place.Controls.Add (new LiteralControl ("<p>"));
		}
	}

	private PropertyInfo GetPropInfo (string name)
	{
		EnsureProps ();
		PropertyInfo prop = null;
		foreach (PropertyInfo p in props) {
			if (0 == String.Compare (p.Name, name, true)) {
				prop = p;
				break;
			}
		}
		return prop;
	}

	private object GetPropertyValue (string propName)
	{
		PropertyInfo prop = GetPropInfo (propName);
		MethodInfo method = prop.GetGetMethod ();
		return method.Invoke (tabs, null).ToString ();
	}

	private void SetPropertyValue (string propName, object value)
	{
		PropertyInfo prop = GetPropInfo (propName);
		object new_value;
		if (prop.PropertyType == typeof (string)) {
			new_value = value;
		} else if (prop.PropertyType == typeof (int)) {
			new_value = Int32.Parse ((string) value);
		} else {
			//???
			Console.WriteLine ("Surprise!!!");
			new_value = "";
		}
		MethodInfo method = prop.GetSetMethod ();
		method.Invoke (tabs, new object [] {new_value});
	}

	private void UpdateValues ()
	{
		foreach (Control t in place.Controls) {
			if (t is TextBox)
				((TextBox) t).Text = (string) GetPropertyValue (t.ID.Substring (1));
		}
	}
	
	void SubmitClicked (object sender, EventArgs events)
	{
		if (name.Text == String.Empty)
			return;

		try {
			tabs.AddTab (name.Text, url.Text);
			msg.Text = "";
			name.Text = "";
			url.Text = "";
			UpdateValues ();
		} catch (Exception e) {
			msg.Text = "Error: " + e.Message;
			msg.Style.Add ("color", "red");
		}
	}

	void PropChanged (object sender, EventArgs events)
	{
		TextBox s = sender as TextBox;
		if (s == null)
			return;

		SetPropertyValue (s.ID.Substring (1), s.Text);
	}
</script>
<head><title>Tab Control</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<p>
<strong>Note that when you add the first tab, its link will not be visible as the new tab will be the current one, and
current tabs render without the link. In order to see that the link has been correctly associated with the tab, add
another one making sure that you put the name of it in both <tt>Name</tt> and <tt>CurrentTabName</tt> input fields.
</strong>
</p>
<center>
<h3>Test for Tabs user control (tabcontrol.dll)</h3>
<hr>
</center>
<form runat="server">
<asp:Label id="msg" runat="server" />
<table>
<tbody>
<tr>
<td width="50%">
<font size=+1>Enter label name and link to add:</font><p>
Name: <asp:TextBox runat="server" id="name" Text="Mono Project" />
<p>
Link: <asp:TextBox runat="server" id="url" Text="http://www.mono-project.com/"/>
<p>
<asp:Button runat="server" id="submit" OnClick="SubmitClicked" Text="Submit" />
</td>
<td>
<font size=+1>Changes on this values will affect properties of the user control:</font><p>
<asp:PlaceHolder id="place" runat="server" />
</td>
</tr>
</tbody>
</table>
<hr>
<Mono:Tabs runat="server" id="tabs"/>
</form>
</body>
</html>

