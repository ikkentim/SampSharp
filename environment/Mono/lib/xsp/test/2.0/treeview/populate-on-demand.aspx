<%@ Page Language="C#" Inherits="Populate"%>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Treeview - populate-on-demand</title>
    <link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body>
    <mono:MonoSamplesHeader runat="server"/>
    <form id="form1" runat="server">
	<p>A sample tree populated on demand using callback (no postback).<br/>The code that generates the nodes can be found in <a href="populate.cs">populate.cs</a></p>
    <div>
        <asp:TreeView ID="TreeView1" Runat="server" OnTreeNodePopulate="TreeView1_TreeNodePopulate"
        EnableClientScript="true"
        PopulateNodesFromClient="true"  
        ExpandDepth="0"
        >
        <Nodes>
        
          <asp:TreeNode Text="Inventory" 
            SelectAction="Expand"  
            PopulateOnDemand="true"/>
        
        </Nodes>
        </asp:TreeView>
    
    </div>
    </form>
</body>
</html>
