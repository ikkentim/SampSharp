<%@ Control Language="C#" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Text" %>
<%
   ArrayList dirs = new ArrayList ();
   ArrayList files = new ArrayList ();
   string physPath = Path.GetDirectoryName (Request.PhysicalPath);
   DirectoryInfo dir = new DirectoryInfo (physPath);
   
   foreach (string d in Directory.GetDirectories (physPath))
          dirs.Add (d);
   foreach (string f in Directory.GetFiles (physPath)) {
          if (f == "index.aspx" || f == "default.aspx")
                 continue;

          files.Add (f);
   }

   StringBuilder sb = new StringBuilder ("<ul class=\"dirlist\">");
   dirs.Sort ();
   foreach (string d in dirs)
          sb.AppendFormat ("<li><a href=\"{0}\" class=\"indexDirectory\">{0}</a></li>", Path.GetFileName (d));
  
   files.Sort ();
   foreach (string f in files)
          sb.AppendFormat ("<li><a href=\"{0}\" class=\"indexFile\">{0}</a></li>", Path.GetFileName (f));

   Response.Write (sb.ToString ());
%>
