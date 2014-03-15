<%@ WebHandler Language="c#" class="Mono.Website.Handlers.MonodocHandler" %>
<%@ Assembly name="monodoc" %>

//
// Mono.Web.Handlers.MonodocHandler.  
//
// Authors:
//     Ben Maurer (bmaurer@users.sourceforge.net)
//
// (C) 2003 Ben Maurer
//

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;
using Monodoc;

namespace Mono.Website.Handlers
{
       public class MonodocHandler : IHttpHandler
       {
               static RootTree help_tree;
               static MonodocHandler ()
               {
                       help_tree = RootTree.LoadTree ();
               }

               void IHttpHandler.ProcessRequest (HttpContext context)
               {
                       string link = (string) context.Request.Params["link"];
                       if (link == null)
                               link = "root:";
                       
                       if (link.StartsWith ("source-id:") && (link.EndsWith (".gif") || link.EndsWith (".jpeg") || link.EndsWith (".jpg")  || link.EndsWith(".png")))
                       {
                               switch (link.Substring (link.LastIndexOf ('.') + 1))
                               {
                                       case "gif":
                                               context.Response.ContentType = "image/gif";
                                               break;
                                       case "jpeg":
                                       case "jpg":
                                               context.Response.ContentType = "image/jpeg";
                                               break;
                                       case "png":
                                               context.Response.ContentType = "image/png";
                                               break;
                                       default:
                                               throw new Exception ("Internal error");
                               }
                               
                               Stream s = help_tree.GetImage (link);
                               
                               if (s == null)
                                       throw new HttpException (404, "File not found");
                               
                               Copy (s, context.Response.OutputStream);
                               return;
                       }
                       
                       Node n;
                       string content = help_tree.RenderUrl (link, out n);
                       PrintDocs (content, context);
               }
               
               
               
               void Copy (Stream input, Stream output)
               {
                       const int BUFFER_SIZE=8192; // 8k buf
                       byte [] buffer = new byte [BUFFER_SIZE];
               
                       int len;
                       while ( (len = input.Read (buffer, 0, BUFFER_SIZE)) > 0)
                               output.Write (buffer, 0, len);
                       
                       output.Flush();
               }

	       string requestPath;               
               void PrintDocs (string content, HttpContext ctx)
               {
                       Node n;
                       
                       ctx.Response.Write (@"
<html>
<head>
<script>
<!--
function load ()
{
	objs = document.getElementsByTagName('a');
	for (i = 0; i < objs.length; i++) {
		e = objs [i];
		if (e.href == null) continue;
		
		objs[i].href = makeLink (objs[i].href);
	}
	
	objs = document.getElementsByTagName('img');
	for (i = 0; i < objs.length; i++)
	{
		e = objs [i];
		if (e.src == null) continue;
		
		objs[i].src = makeLink (objs[i].src);
	}
}

function makeLink (link)
{
	if (link == '') return '';
	if (link.charAt(0) == '#') return link;
	
	protocol = link.substring (0, link.indexOf (':'));
	switch (protocol)
	{
		case 'http':
		case 'ftp':
		case 'mailto':
			return link;
			
		default:
			return '" + ctx.Request.Path + @"?link=' + link.replace(/\+/g, '%2B');
	}
}
-->
</script>
<title>Mono Documentation</title>
</head>
<body onLoad='load()'>

                       ");
                       
			 requestPath=ctx.Request.Path;
                        string output;

                        if (content == null)
                                output = "No documentation available on this topic";
                        else {
                                output = MakeLinks(content);
                        }
                        ctx.Response.Write (output);
                        ctx.Response.Write (@"</body></html>");
               }

		string MakeLinks(string content)
                {
                        MatchEvaluator linkUpdater=new MatchEvaluator(MakeLink);
                        if(content.Trim().Length<1|| content==null)
                                return content;
                        try
                        {
                                string updatedContents=Regex.Replace(content,"(<a[^>]*href=['\"])([^'\"]+)(['\"][^>]*)(>)", linkUpdater);
                                return(updatedContents);
                        }
                        catch(Exception e)
                        {
                                return "LADEDA" + content+"!<!--Exception:"+e.Message+"-->";
                        }
                }
                
                // Delegate to be called from MakeLinks for fixing <a> tag
                string MakeLink (Match theMatch)
                {
                        string updated_link = null;

                        // Return the link without change if it of the form
                        //      $protocol://... or #...
                        string link = theMatch.Groups[2].ToString();
                        if (Regex.Match(link, @"^\w+:\/\/").Success || Regex.Match(link, "^#").Success)
                                updated_link = theMatch.Groups[0].ToString();
                        else {
                                updated_link = String.Format ("{0}{1}?link={2}{3} target=\"content\"{4}",
                                        theMatch.Groups[1].ToString(),
                                        requestPath,
                                        HttpUtility.UrlEncode (link.Replace ("file://","")),
                                                theMatch.Groups[3].ToString(),
                                                theMatch.Groups[4].ToString());

                        }
                        return updated_link;
                }

               bool IHttpHandler.IsReusable
               {
                       get {
                               return true;
                       }
               }

       }
}
