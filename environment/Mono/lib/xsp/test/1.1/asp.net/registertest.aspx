<%@ Page language="C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<%-- we can even override the asp prefix with @ register --%>
<%@ Register TagPrefix="Acme" namespace="System.Web.UI.WebControls" assembly="System.Web" %>
<%@ Register TagPrefix="Acme" TagName="One" Src="registertest1.ascx" %>
<%@ Register TagPrefix="Acme" TagName="Two" Src="registertest2.ascx" %>

<html>
<head><title>Register test</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<script language="C#" runat="server">
      void Clicked (object sender, EventArgs e)
      {
          One.Text = "Message text changed!";
          One.Color = "red";
          Two.Text = "Message text changed2!";
          Two.Color = "red";
	  Three.Text = "Text changed!";
      }
</script>

<body><mono:MonoSamplesHeader runat="server"/>
<form runat="server">
    <Acme:One id="One" Text="This is a default One!" Color="blue" runat="server"/>
    <p>
    <Acme:Two id="Two" Text="This is a default Two!" Color="blue" runat="server"/>
    <p>
    <Acme:Label id="Three" Text="This is a label!" Color="blue" runat="server"/>
    <p>

    <asp:Button text="Change" OnClick="Clicked" runat=server/>

  </form>

</body>
</html>

