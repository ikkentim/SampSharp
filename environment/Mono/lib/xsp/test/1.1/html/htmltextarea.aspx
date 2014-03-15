<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<script runat="server">
	void Page_Load (object sender, EventArgs e) 
	{
		 myTA.InnerText = "Hi there!\nCool!";
	}
</script>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>HtmlTextArea</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
<textarea id="myTA" cols=25 rows=5 runat="server" />
</form>
</body>
</html>

