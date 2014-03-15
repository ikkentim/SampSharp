<%@ WebHandler Language="c#" class="XSPTest.AsyncTest" debug="true" %>

using System;
using System.Web;
using System.Threading;

namespace XSPTest
{
	public class AsyncTest : IHttpAsyncHandler
	{
		EventHandler evt;
		HttpContext context;
		
		void Func (object o, EventArgs args)
		{
			context.Response.Write ("In async callback\n");
			context.Response.ContentType = "text/plain";
		}

		public void ProcessRequest (HttpContext context)
		{
			throw new Exception ("Should not be called");
		}

		public IAsyncResult BeginProcessRequest (HttpContext context, AsyncCallback cb, object state)
		{
			this.context = context;
			evt = new EventHandler (Func);
			return evt.BeginInvoke (null, null, cb, state);
		}

		public void EndProcessRequest (IAsyncResult ares)
		{
			context.Response.Write ("End request being invoked.");
		}

		public bool IsReusable {
			get { return false; }
		}
	}
}

