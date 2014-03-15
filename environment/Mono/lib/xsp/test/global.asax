<%@ Import Namespace="System.IO" %>
<script runat="server" language="c#" >
	static object appV2 = null;

	void Application_Start (object o, EventArgs args)
	{
		Console.WriteLine ("Application_Start");
	        Type type;

		type = Type.GetType ("Samples.Application, App_Code", false);
		if (type != null)
			appV2 = Activator.CreateInstance (type, new object[] {HttpContext.Current});
	}

	void SetMissingComponents ()
	{
	        Application.Lock ();
	        Application.Add ("MissingComponentsFlag", true);
	        Application.UnLock ();
	}

	void SetShowingMissingComponents ()
	{
	        Application.Lock ();
	        Application.Add ("ShowingMissingComponentsFlag", true);
	        Application.UnLock ();
	}
	void MissingComponents ()
	{
	        Response.Redirect ("/missing_components.aspx");
	}

	void Application_End (object o, EventArgs args)
	{
		Console.WriteLine ("Application_End");
	}

	void Application_BeginRequest (object o, EventArgs args)
	{
	}
</script>
