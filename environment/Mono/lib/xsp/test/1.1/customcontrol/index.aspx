<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%@ Register TagPrefix="mono" TagName="FileList" src="~/controls/FileList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
<title>Directory index</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
  <p>The following directories and files are found in this directory:</p>
  <blockquote>
    <mono:FileList runat="server"/>
  </blockquote>
</form>
</body>
</html>
