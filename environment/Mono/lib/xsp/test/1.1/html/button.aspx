<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
	<script runat="server">
	void Button1_OnClick(object Source, EventArgs e) 
	{
		HtmlButton button = (HtmlButton) Source;
		if (button.InnerText == "Enabled 1"){
			Span1.InnerHtml="You deactivated Button1";
			button.InnerText = "Disabled 1";
		}
		else {
			Span1.InnerHtml="You activated Button1";
			button.InnerText = "Enabled 1";
		}
	}

	</script>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
	<h3>HtmlButton Sample</h3>
	<form id="ServerForm" runat="server">     
		<button id=Button1 runat="server" OnServerClick="Button1_OnClick">
		Button1
		</button>
		&nbsp;
		<span id=Span1 runat="server" />
	</form>
</body>
</html>

