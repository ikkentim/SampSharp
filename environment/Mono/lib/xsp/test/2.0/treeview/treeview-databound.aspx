<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<HTML>
  <head>
    <title>TreeView - DataBound</title>
    <link rel="stylesheet" type="text/css" href="/mono-xsp.css">
  </head>
  <BODY bgcolor="white">
    <mono:MonoSamplesHeader runat="server"/>
    <form runat="server">
      <asp:xmldatasource
        id="XmlDataSource1"
        runat="server">
		<data>
			<books>
			   <languagebooks>
				 <book Title="Pure JavaScript" Author="Wyke, Gilliam, and Ting"/>
				 <book Title="Effective C++: 50 Specific Ways to Improve Your Programs and Designs" author="Scott Meyers">
					 <book Title="Pure JavaScript" Author="Wyke, Gilliam, and Ting"/>
				 </book>
				 <book Title="Assembly Language: Step-By-Step" author="Jeff Duntemann"/>
				 <book Title="Oracle PL/SQL Best Practices" author="Steven Feuerstein"/>                
			   </languagebooks>
			   <securitybooks>
				  <book Title="Counter Hack: A Step-by-Step Guide to Computer Attacks and Effective Defenses" author="Ed Skoudis"/>
			   </securitybooks>
			</books>
		</data>
      </asp:xmldatasource>

      <asp:treeview
        id="TreeView1"
        runat="server"
	  	ExpandDepth="0"
		MaxDataBindDepth = "-1"
		NodeIndent = "50"
		LineImagesFolder = "TreeLineImages"
		ShowLines="true"
		ExpandImageToolTip="expandeix!"
		NodeWrap="true"
		ShowCheckBoxes = "All"
		ShowExpandCollapse="true"
		EnableClientScript="true"
		SelectedNodeStyle-BackColor="yellow"
        datasourceid="XmlDataSource1">
        <databindings>
          <asp:treenodebinding datamember="book" textfield="Title"/>
        </databindings>
      </asp:TreeView>

    </form>
  </BODY>
</HTML>
