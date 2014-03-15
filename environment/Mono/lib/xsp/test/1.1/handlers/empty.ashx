<%@ WebHandler Language="c#" class="XSPTest.EmptyTest" %>

using System;
using System.Web;

namespace XSPTest
{
	public class EmptyTest : IHttpHandler
	{
		public void ProcessRequest (HttpContext context)
		{
			HttpResponse response = context.Response;
			response.StatusCode = 202;
		}

		public bool IsReusable {
			get { return true; }
		}
	}
}

