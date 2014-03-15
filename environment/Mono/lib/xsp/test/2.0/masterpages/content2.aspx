<%@ Page Language="C#" MasterPageFile="frame.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="server">
Welcome again!
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="server">
<p>This is another content page for the same master page file.</p>
<p>Click <a href="content1.aspx">here</a> to see another content page.</p>
</asp:Content>
