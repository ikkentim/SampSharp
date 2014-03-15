<%@ Page Language="C#" Debug="true" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<script runat="server">
	void Page_Load ()
	{
		Server.Transfer ("transfer2.aspx");
	}
</script>
<html>
<head><title>Transfer test 1</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
This will never be seen on the browser. Miguel sucks.
</body>
</html>

