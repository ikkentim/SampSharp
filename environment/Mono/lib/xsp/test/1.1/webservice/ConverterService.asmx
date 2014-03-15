<%@ WebService Language="c#" Codebehind="ConverterService.asmx.cs" Class="WebServiceTests.ConverterService" %>

using System;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WebServiceTests
{
	public class UserInfo : SoapHeader 
	{
		public int userId;
	}
	
	public class CurrencyInfo
	{
		public CurrencyInfo ()
		{
		}

		public CurrencyInfo (string name, double rate) 
		{
			Name = name;
			Rate = rate;
		}
		
		public string Name;
		public double Rate;
	}

	[Dump]
	[Encrypt]
	[WebServiceAttribute (Description="Web service that can make currency conversions")]
	public class ConverterService : System.Web.Services.WebService
	{
		static int userCount = 0;
		static Hashtable conversionTable;
		
		public UserInfo userInfo;

		static ConverterService ()
		{
			conversionTable = new Hashtable ();
			InternalSetCurrencyRate ("USD", 1);
			InternalSetCurrencyRate ("EUR", 0.883884 );
			InternalSetCurrencyRate ("GBP", 0.611817 );
			InternalSetCurrencyRate ("JPY", 118.271 );
			InternalSetCurrencyRate ("CAD", 1.36338 );
			InternalSetCurrencyRate ("AUD", 1.51485 );
			InternalSetCurrencyRate ("CHF", 1.36915 );
			InternalSetCurrencyRate ("RUR", 30.4300 );
			InternalSetCurrencyRate ("CNY", 8.27740 );
			InternalSetCurrencyRate ("ZAR", 7.62645 );
			InternalSetCurrencyRate ("MXN", 10.5025 );
		}
		
		[WebMethod (Description="Registers the user into the system")]
		[SoapHeaderAttribute ("userInfo", Direction = SoapHeaderDirection.Out)]
		[TraceExtension]
		public void Login (string a)
		{
			userInfo = new UserInfo ();
			userInfo.userId = ++userCount;
		}

		[WebMethod (Description="Converts an amount from one currency to another currency")]
		[SoapHeaderAttribute ("userInfo")]
		[TraceExtension]
		public double Convert (string sourceCurrency, string targetCurrency, double value)
		{
			CheckUser ();
			double usd = (1 / GetCurrencyRate (sourceCurrency)) * value;
			return usd * GetCurrencyRate (targetCurrency);
		}
		
		[WebMethod (Description="Returns a list of currency rates")]
		[SoapHeaderAttribute ("userInfo")]
		[TraceExtension]
		public CurrencyInfo[] GetCurrencyInfo ()
		{
			CheckUser ();
			
			lock (conversionTable)
			{
				CurrencyInfo[] info = new CurrencyInfo[conversionTable.Count];
				int n = 0;
				foreach (CurrencyInfo cinfo in conversionTable.Values)
					info [n++] = cinfo;
				return info;
			}
		}
		
		[WebMethod (Description="Sets the rate of a currency")]
		[SoapHeaderAttribute ("userInfo")]
		[TraceExtension]
		public void SetCurrencyRate (string currency, double rate)
		{
			CheckUser ();
			InternalSetCurrencyRate (currency, rate);
		}

		static void InternalSetCurrencyRate (string currency, double rate)
		{
			lock (conversionTable)
			{
				conversionTable [currency] = new CurrencyInfo (currency, rate);
			}
		}

		[WebMethod (Description="Returns the rate of a currency")]
		[SoapHeaderAttribute ("userInfo")]
		[TraceExtension]
		public double GetCurrencyRate (string cname)
		{
			CheckUser ();
			lock (conversionTable)
			{
				if (!conversionTable.ContainsKey (cname))
					throw new SoapException ("Unknown currency '" + cname + "'", SoapException.ServerFaultCode);
					
				return ((CurrencyInfo) conversionTable [cname]).Rate;
			}
		}
		
		void CheckUser ()
		{
			if (userInfo == null) 
				throw new SoapException ("User not logged", SoapException.ServerFaultCode);
		}
	}
}
