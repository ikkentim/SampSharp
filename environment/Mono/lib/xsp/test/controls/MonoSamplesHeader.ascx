<%@ Control Language="C#" %>
<script runat="server">
  void Page_Init (object sender, EventArgs args)
  {
      Control crumbs;
      crumbs = LoadControl ("~/controls/BreadCrumbs_2.0.ascx");
    
      BreadCrumbs.Controls.Clear ();
      BreadCrumbs.Controls.Add (crumbs);
  }
</script>
<div class="header">
  <a style="float:left; padding-right: 20px" class="header" href="http://mono-project.com/"><img src="/monobutton.png" alt="Mono site"></a>
  <h1 class="header">Welcome to Mono XSP!</h1>
  <div>    <h2 class="header">XSP is a simple web server written in C# that can be used to run your ASP.NET 
<% Response.Write ("2.0"); %> applications
    </h2>
  </div>
  <asp:PlaceHolder runat="server" id="BreadCrumbs" />
</div>
