<%@ Page Language="C#" Debug="true" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head><title>Transfer test 2</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
FilePath: <%= Request.FilePath %> <br>
CurrentExecutionFilePath: <%= Request.CurrentExecutionFilePath %> <br>
</body>
</html>

