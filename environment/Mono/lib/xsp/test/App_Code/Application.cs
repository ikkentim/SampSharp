using System;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples
{
	public class Application
	{
		public Application (HttpContext ctx)
		{
			SiteMapSection sms = WebConfigurationManager.GetSection ("system.web/siteMap") as SiteMapSection;
			if (sms != null)
				sms.Enabled = true;
		}
	}
}
