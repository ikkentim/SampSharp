<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%@ Import Namespace="System.Web.Security" %>
<html>
<script language="C#" runat=server>
	void Page_Load (object sender, EventArgs e)
	{
		Welcome.Text = "Hello, " + User.Identity.Name;
	}

	void Signout_Click (object sender, EventArgs e)
	{
		FormsAuthentication.SignOut ();
		Response.Redirect ("/1.1/authtest/login.aspx");
	}
</script>
<head>
<title>Using Cookies Authentication</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<h3>Using Cookies Authentication</h3>
<form runat=server>
	<h3><asp:label id="Welcome" runat=server/></h3>
	<asp:button text="Signout" OnClick="Signout_Click" runat=server/>
</form>
</body>
</html>

