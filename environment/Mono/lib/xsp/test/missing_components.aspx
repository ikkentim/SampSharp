<%@ Page Language="C#" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<head>
<link href="favicon.ico" rel="SHORTCUT ICON" />
<link rel="stylesheet" type="text/css" href="/mono-xsp.css">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Error: Missing components</title>
</head>
<body>
<h2>Missing required components</h2>
<p>
Some components required to run the XSP test suite are missing. XSP must be configured
to serve the test suite from a directory in which the test suite has been installed after
compiling it. It will not work if ran from the source directory.
</p>
<p>
Please use the following commands to compile and run the test suite (current directory is assumed
to be the XSP toplevel directory in the source distribution):
<blockquote><pre>
# Execute this only if you are using the source code from SVN:
./autogen.sh --prefix=/usr/local/

# If everything went correctly (i.e. no errors are reported), compile XSP and the test suite
make

# If the above command completed without errors, become root and install XSP
su -
make install

# At this point you should be able to run either xsp or xsp2 and access the test suite:
xsp2 --root /usr/local/lib/xsp/test/
</pre></blockquote>
</p>
<p>
If all of the above steps completed without errors, you should be able to visit the test
suite at the default XSP address - http://localhost:8080.
</p>
<p>
If you are not using the XSP source distribution but instead using a binary version shipped with
your operating system, please contact the operating system XSP package maintainers or, if that fails,
ask for help on the <a href="http://mono-project.com/Mailing_Lists">Mono Users List</a>.
</p>
</body>
</html>
