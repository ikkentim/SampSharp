<%@ Page Language="C#" MasterPageFile="frame.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="server">
Welcome to Master Pages!
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="server">
<p>This is the content of the master page file.</p>
<p>This sample master page has two placeholders,
one for the content and one for the title.</p>
<p>Click <a href="content2.aspx">here</a> to see another content page.</p>
</asp:Content>
