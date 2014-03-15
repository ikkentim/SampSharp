<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%@ Import Namespace="System.Web.Security" %>
<html>
<script language="C#" runat=server>
	void Login_Click (object sender, EventArgs e)
	{
		if ((UserEmail.Value == "jdoe@somewhere.com") && (UserPass.Value == "password")) {
			FormsAuthentication.RedirectFromLoginPage (UserEmail.Value, PersistCookie.Checked);
		} else {
			Msg.Text = "Invalid Credentials: Please try again";
		}
	}
</script>
<head><title>Login</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat=server>
	<h3><font face="Verdana">Login Page</font></h3>
	<table>
		<tr>
		<td>Email:</td>
		<td><input id="UserEmail" type="text" runat=server/></td>
		<td><ASP:RequiredFieldValidator ControlToValidate="UserEmail"
			 Display="Static" ErrorMessage="*" runat=server/></td>
		</tr>
		<tr>
		<td>Password:</td>
		<td><input id="UserPass" type=password runat=server/></td>
		<td><ASP:RequiredFieldValidator ControlToValidate="UserPass"
			 Display="Static" ErrorMessage="*" runat=server/></td>
		</tr>
		<tr>
		<td>Persistent Cookie:</td>
		<td><ASP:CheckBox id=PersistCookie runat="server" /> </td>
		<td></td>
		</tr>
	</table>
	<asp:button text="Login" OnClick="Login_Click" runat=server/>
	<p>
	<asp:Label id="Msg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat=server />
</form>
</body>
</html>

