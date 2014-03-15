<%@ Page Language = "C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<script runat="server">
	string [] msgs = new string [] { "hi!", "hello", "hola", 
					 "Ciao", "adios"};
</script>
<head>
<title>Code Render</title>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
</head>
<body><mono:MonoSamplesHeader runat="server"/>
	<% for (int i = 0; i < 5; i++) {%>
	<%= msgs [i] %> message number <%= i %>.
	<p>
	<% } %>
	<form runat=server>
		<% for (int i = 4; i <= 0; i--) {%>
		<%= msgs [i] %> reverse message number <%= i %>.
		<% } %>
		<h3>One more calendar</h3>
		<asp:calendar id="Calendar1"
		Font-Name="Arial" showtitle="true"
		runat="server">
			<SelectedDayStyle BackColor="Blue" 
					ForeColor="Red"/>
			<TodayDayStyle BackColor="#CCAACC" 
					ForeColor="#000000"/>
		</asp:Calendar>

	</form>
	This should say hello: <%= msgs [1] %>
</body>
</html>

