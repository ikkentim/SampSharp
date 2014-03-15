<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<script runat=server>
	private static int _clicked = 0;
	void Clicked (object o, EventArgs e)
	{
		uno.InnerText = String.Format ("Somebody pressed me {0} times.", ++_clicked);
	}

	private static int _txt_changed = 0;
	void txt_Changed (object sender, EventArgs e)
	{
		dos.InnerText = String.Format ("Text have changed {0} times.", ++_txt_changed);
	}
</script>
<head>
<title>Session Test</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
<asp:Button id="btn"
     Text="Submit"
     OnClick="Clicked"
     runat="server"/>
<br>
<span runat=server id="uno"></span>
<br>
<span runat=server id="dos"></span>
<br>
<asp:TextBox id="txt1" Text="You can write here." TextMode="MultiLine" OnTextChanged="txt_Changed" runat="server" rows=5 />
<br>
</form>
</body>
</html>
