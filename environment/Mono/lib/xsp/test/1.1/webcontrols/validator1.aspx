<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<script runat=server>
	void txt_Changed (object sender, EventArgs e)
	{
	}
</script>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>The textboxes should be filled</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
Multiline:
<br>
<asp:TextBox id="txt1" Text="multiline" TextMode="MultiLine" OnTextChanged="txt_Changed" runat="server" rows=5 />
<asp:RequiredFieldValidator runat="server" id="validTxt1" ControlToValidate="txt1" display="Dynamic" ErrorMessage="Not filled!" />
<br>
Single:
<br>
<asp:TextBox id="txt2" Text="singleline" TextMode="singleLine" OnTextChanged="txt_Changed" runat="server" maxlength=40 />
<asp:RequiredFieldValidator runat="server" id="validtxt2" ControlToValidate="txt2" display="Dynamic" ErrorMessage="Not filled!" />
<br>
<br>
Password:
<br>
<asp:TextBox id="txt3" Text="badifyouseethis" TextMode="password" OnTextChanged="txt_Changed" runat="server" maxlength=15 />
<br>
<asp:RequiredFieldValidator runat="server" id="validtxt3" ControlToValidate="txt3" display="Dynamic" ErrorMessage="Not filled!" />
<br>
<asp:Button id="btn"
     Text="Submit"
     runat="server"/>
</form>
</body>
</html>

