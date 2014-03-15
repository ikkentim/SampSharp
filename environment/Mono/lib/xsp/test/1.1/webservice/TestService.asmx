<%@ WebService Language="c#" Codebehind="TestService.asmx.cs" Class="WebServiceTests.TestService" %>

using System;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WebServiceTests
{
	public class TestService : System.Web.Services.WebService
	{
		[WebMethod]
		public string Echo (string a)
		{
			return a;
		}

		[WebMethod]
		public int Add (int a, int b)
		{
			return a + b;
		}
	}
}
