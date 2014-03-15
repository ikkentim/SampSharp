<%@ Page Language="c#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<script runat="server">

    void Menu1_MenuItemClick(Object s, System.Web.UI.WebControls.MenuEventArgs e)
    {
        Label1.Text = "You selected " + e.Item.Text;
		Page.Header.Title = "You selected " + e.Item.Text;
    }
    
</script>
<html>
<head runat="server">
    <title>Simple Menu</title>
    <link rel="stylesheet" type="text/css" href="/mono-xsp.css">
	<META kk="iii">
</head>
<body style="background-color: #f8f8f4; padding:0; margin:0">
    <mono:MonoSamplesHeader runat="server"/>
    <form id="form1" runat="server">
	<table width="100%" style="height:100%"><tr>
	<td style="background: #505050; padding:10px" valign=top>
    <asp:Menu
        id="Menu1"
		StaticDisplayLevels = "1"
        OnMenuItemClick="Menu1_MenuItemClick"
		Orientation="Vertical"
		DynamicHorizontalOffset = "5"
		DynamicVerticalOffset = "0"
		DynamicHoverStyle-ForeColor = "Green"
		StaticHoverStyle-BackColor = "gray"
		StaticMenuStyle-ForeColor = "white"
		DynamicSelectedStyle-BackColor = "Red"
        Runat="Server">
	  <DynamicMenuStyle
		BackColor = "#f8f8f4"
		ForeColor = "gray"
		BorderColor="#505050"
		BorderStyle="Solid"
		BorderWidth="1" />
	  <DynamicMenuItemStyle
		HorizontalPadding="5" 
		VerticalPadding="3" />  
    <Items>
    <asp:MenuItem Text="Part I">
        <asp:MenuItem Text="Chapter 1" ImageUrl="http://msdn.microsoft.com/msdn-online/shared/graphics/icons/rtg_email.gif"/>
        <asp:MenuItem Text="Chapter 2 bsdfsdf" />
        <asp:MenuItem Text="Chapter 3 aux">
			<asp:MenuItem Text="Chapter 3.1" />
			<asp:MenuItem Text="Chapter 3.2" />
			<asp:MenuItem Text="Chapter 3.3">
				<asp:MenuItem Text="Chapter 3.3.1" />
				<asp:MenuItem Text="Chapter 3.3.2" />
			</asp:MenuItem>
	    </asp:MenuItem>
        <asp:MenuItem Text="Chapter 4" />
    </asp:MenuItem>
    <asp:MenuItem Text="Part II">
        <asp:MenuItem Text="Chapter 5" />
        <asp:MenuItem Text="Chapter 6" />
    </asp:MenuItem>
    <asp:MenuItem Text="Part III"/>
    </Items>
    </asp:Menu>
	</td>
	<td width="100%">
    <p align=center>
    <asp:Label ID="Label1" Runat="Server" >This is a vertical menu sample</asp:Label>
	</p>
	</td>
	</tr>
	</table>
	
    </form>
	<br/>
</body>
</html>
