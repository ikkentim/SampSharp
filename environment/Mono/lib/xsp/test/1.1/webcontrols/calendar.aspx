<%@ Page Language = "C#" %>
<%@ Register TagPrefix="mono" TagName="MonoSamplesHeader" src="~/controls/MonoSamplesHeader.ascx" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<title>Calendar Test</title>
</head>
<body><mono:MonoSamplesHeader runat="server"/>
	<form runat=server>
		<h3>Calendar and properties</h3>
		<asp:calendar id="Calendar1"
		Font-Name="Arial" showtitle="true"
		runat="server">
			<SelectedDayStyle BackColor="Blue" 
					ForeColor="Red"/>
			<TodayDayStyle BackColor="#CCAACC" 
					ForeColor="#000000"/>
		</asp:Calendar>

	</form>
</body>
</html>

