<%@ Page Language="c#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head runat="server">
    <title>Simple Menu</title>
    <link rel="stylesheet" type="text/css" href="/mono-xsp.css">
	<META kk="iii">
</head>
<body>
    <mono:MonoSamplesHeader runat="server"/>
    <form id="form1" runat="server">

    <asp:Label ID="Label1" Runat="Server" />
	
	
    <asp:Menu ID="Menu12"
        Orientation="Horizontal"
		DynamicHoverStyle-BackColor = "Red"
		StaticHoverStyle-BackColor = "Red"
        Runat="Server">

  <StaticMenuStyle 
    BackColor="#eeeeee"
    BorderColor="Black"
    BorderStyle="Solid"
    BorderWidth="10" />

  <StaticMenuItemStyle
    HorizontalPadding="5" />      

  <DynamicMenuStyle
    BorderColor="Black"
    BorderStyle="Solid"
    BorderWidth="10" />

  <DynamicMenuItemStyle
    BackColor="#eeeeee"
    HorizontalPadding="5" 
    VerticalPadding="3" />  

  <DynamicSelectedStyle
    BackColor="yellow"
     />  
	
    <Items>
    <asp:MenuItem Text="File">
        <asp:MenuItem 
            Text="New" />
        <asp:MenuItem 
            Text="Open..." />
        <asp:MenuItem 
            Text="Close" />
    </asp:MenuItem>
    <asp:MenuItem Text="Edit">
        <asp:MenuItem 
            Text="Cut"  
            ImageUrl="stock_cut_24.png" />
        <asp:MenuItem 
            Text="Copy"  
            ImageUrl="stock_copy_24.png" />
        <asp:MenuItem 
            Text="Paste" 
            ImageUrl="stock_paste_24.png" 
            />
			
			<asp:MenuItem Text="Edit">
				<asp:MenuItem 
					Text="Cut"  
					ImageUrl="stock_cut_24.png" />
				<asp:MenuItem 
					Text="Copy"  
					ImageUrl="stock_copy_24.png" />
				<asp:MenuItem 
					Text="Paste" 
					ImageUrl="stock_paste_24.png" 
					/>
				<asp:MenuItem Text="Select All" />
			</asp:MenuItem>
			
			
        <asp:MenuItem Text="Select All" />
		
			<asp:MenuItem Text="A large one">
				<asp:MenuItem Text="Option 1" />
				<asp:MenuItem Text="Option 2" />
				<asp:MenuItem Text="Option 3" />
				<asp:MenuItem Text="Option 4" />
				<asp:MenuItem Text="Option 5" />
				<asp:MenuItem Text="Option 6" />
				<asp:MenuItem Text="Option 7" />
				<asp:MenuItem Text="Option 8" />
				<asp:MenuItem Text="Option 9" />
				<asp:MenuItem Text="Option 10" />
				<asp:MenuItem Text="Option 11" />
				<asp:MenuItem Text="Option 12" />
				<asp:MenuItem Text="Option 13" />
				<asp:MenuItem Text="Option 14" />
				<asp:MenuItem Text="Option 15" />
				<asp:MenuItem Text="Option 16" />
				<asp:MenuItem Text="Option 17" />
				<asp:MenuItem Text="Option 18" />
				<asp:MenuItem Text="Option 19" />
				<asp:MenuItem Text="Option 20" />
				<asp:MenuItem Text="Option 21" />
				<asp:MenuItem Text="Option 22" />
				<asp:MenuItem Text="Option 23" />
				<asp:MenuItem Text="Option 24" />
				<asp:MenuItem Text="Option 25" />
				<asp:MenuItem Text="Option 26" />
				<asp:MenuItem Text="Option 27" />
				<asp:MenuItem Text="Option 28" />
				<asp:MenuItem Text="Option 29" />
				<asp:MenuItem Text="Option 30" />
				<asp:MenuItem Text="Option 31" />
				<asp:MenuItem Text="Option 32" />
				<asp:MenuItem Text="Option 33" />
				<asp:MenuItem Text="Option 34" />
				<asp:MenuItem Text="Option 35" />
				<asp:MenuItem Text="Option 36" />
				<asp:MenuItem Text="Option 37" />
				<asp:MenuItem Text="Option 38" />
				<asp:MenuItem Text="Option 39" />
				<asp:MenuItem Text="Option 40" />
				<asp:MenuItem Text="Option 41" />
				<asp:MenuItem Text="Option 42" />
				<asp:MenuItem Text="Option 43" />
				<asp:MenuItem Text="Option 44" />
				<asp:MenuItem Text="Option 45" />
				<asp:MenuItem Text="Option 46" />
				<asp:MenuItem Text="Option 47" />
				<asp:MenuItem Text="Option 48" />
				<asp:MenuItem Text="Option 49" />
				<asp:MenuItem Text="Option 50" />
				<asp:MenuItem Text="Option 51" />
				<asp:MenuItem Text="Option 52" />
				<asp:MenuItem Text="Option 53" />
				<asp:MenuItem Text="Option 54" />
				<asp:MenuItem Text="Option 55" />
				<asp:MenuItem Text="Option 56" />
				<asp:MenuItem Text="Option 57" />
				<asp:MenuItem Text="Option 58" />
				<asp:MenuItem Text="Option 59" />
				<asp:MenuItem Text="Option 60" />
			</asp:MenuItem>
    </asp:MenuItem>
    </Items>
    </asp:Menu>    
    </form>
	<br/>
	<br/>
	<br/>
	<br/>
	<br/>
	<br/>
	<br/>
	<br/>
	<br/>
	<br/>
</body>
</html>
