<%@ language="C#"%>
<%@ Import namespace="System.IO" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
<title>Welcome to Mono XSP!</title>
<link href="favicon.ico" rel="SHORTCUT ICON" />
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
</head>
<body>
<mono:MonoSamplesHeader runat="server"/>
<p><table width=100%>
<tr valign="top">
<td>
<form id="form1" runat="server">
    <asp:SiteMapDataSource runat="server" id="SamplesSiteMap"/>
    <asp:TreeView style="margin:10px" id="TreeView2" runat="server" DataSourceId="SamplesSiteMap"
		  EnableClientScript="true"
		  PopulateNodesFromClient="false"  
		  ExpandDepth="2"/>
</form>
</td>
<td><p align="right"><img style="float:right" src="mono-powered-big.png" alt="Mono Powered"></p></td>
</tr></table>
</p>
</html>

