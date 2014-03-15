<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
   <h3>Xml Example</h3>
   <form runat="server">
      <asp:Xml id="xml1" 
           DocumentSource="people.xml" 
           TransformSource="peopletable.xsl" 
           runat="server" />
   </form>
</body>
</html>

