<%@ WebHandler Language="c#" class="XSPTest.ChunkedTest" %>

using System;
using System.Web;

namespace XSPTest
{
	public class ChunkedTest : IHttpHandler
	{
		public void ProcessRequest (HttpContext context)
		{
			HttpResponse response = context.Response;
			for (int i = 0; i < 10; i++) {
				string msg = new string ((char) (i + 'a'), 10000);
				response.Output.WriteLine (msg);
				response.Flush ();
			}
		}

		public bool IsReusable {
			get { return true; }
		}
	}
}

