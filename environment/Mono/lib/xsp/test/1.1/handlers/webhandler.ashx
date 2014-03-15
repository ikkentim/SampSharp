<%@ WebHandler Language="c#" Class="WebHandlerTest.SimplePage" %>

using System.Web;

namespace WebHandlerTest
{
	class SimplePage : IHttpHandler
	{
		public void ProcessRequest (HttpContext context)
		{
			HttpResponse resp = context.Response;
			resp.Write ("<html><body><h1>Hi there!</h1></body></html>\n");
		}

		public bool IsReusable {
			get { return true; }
		}
	}
}

