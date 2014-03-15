<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%-- we can even override the asp prefix with @ register --%>
<%@ Register TagPrefix="Acme" namespace="System.Web.UI.WebControls" assembly="System.Web" %>
<%@ Register TagPrefix="Acme" TagName="One" Src="registertest1.ascx" %>
<%@ Register TagPrefix="Acme" TagName="Two" Src="registertest2.ascx" %>

<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
