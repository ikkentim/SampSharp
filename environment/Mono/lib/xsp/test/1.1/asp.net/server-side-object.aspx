<%@ Page Language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
    <head><title>Server-side object</title>
    <link rel="stylesheet" type="text/css" href="/mono-xsp.css">
    </head>
   <object id="items" runat="server" class="System.Collections.ArrayList" />
   <script language="C#" runat=server>
      void Page_Load(Object sender, EventArgs e) {
         items.Add("One");
         items.Add("Two");
         items.Add("Three");

         MyList.DataSource = items;
         MyList.DataBind();
      }
   </script>

   <body><mono:MonoSamplesHeader runat="server"/>
      <asp:datalist id="MyList" runat=server>
         <ItemTemplate>
            Here is a value: <%# Container.DataItem %>
         </ItemTemplate>
      </asp:datalist>
   </body>
</html>

