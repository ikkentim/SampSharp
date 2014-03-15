//
// CompressExtension.cs
//
// Author:
//   Lluis Sanchez Gual (lluis@ximian.com)
//
// Copyright (C) Ximian, Inc. 2003
//

using System;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Services.Description;
using System.Web.Services.Configuration;
using System.CodeDom;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.GZip;
using System.ComponentModel;

public class CompressExtension : SoapExtension 
{
	Stream netStream;
	MemoryStream tempStream;

	int minLength;

	public CompressExtension ()
	{
	}

	public override Stream ChainStream (Stream stream)
	{
		netStream = stream;
		tempStream = new MemoryStream ();
		return tempStream;
	}

	public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute) 
	{
		return ((CompressAttribute)attribute).MinLength;
	}

	public override object GetInitializer (Type webServiceType) 
	{
		return 0;
	}

	public override void Initialize(object initializer) 
	{
		minLength = (int) initializer;
	}

	public override void ProcessMessage(SoapMessage message) 
	{
		switch (message.Stage) 
		{
			case SoapMessageStage.BeforeSerialize:
				break;
			case SoapMessageStage.AfterSerialize:
				Compress (message);
				break;
			case SoapMessageStage.BeforeDeserialize:
				Decompress (message);
				break;
			case SoapMessageStage.AfterDeserialize:
				break;
			default:
				throw new Exception("invalid stage");
		}
	}

	public void Compress (SoapMessage message)
	{
		if (tempStream.Length >= minLength)
		{
			MemoryStream mems = new MemoryStream ();
			GZipOutputStream zos = new GZipOutputStream (mems);
			zos.Write (tempStream.GetBuffer (), 0, (int) tempStream.Length);
			zos.Finish ();
			Console.WriteLine ("msg len:" + tempStream.Length);
			// Convert the compressed content to a base 64 string
			string compString = Convert.ToBase64String (mems.GetBuffer (), 0, (int)mems.Length);
			byte[] compBytes = Encoding.UTF8.GetBytes (compString);
			netStream.WriteByte ((byte)'C');	// Compressing flag
			netStream.Write (compBytes, 0, compBytes.Length);
			Console.WriteLine ("cmp len:" + compBytes.Length);
			netStream.Flush ();
			zos.Close ();
		}
		else
		{
			netStream.WriteByte ((byte)'N');	// Not Compressing flag
			netStream.Write (tempStream.GetBuffer(), 0, (int) tempStream.Length);
			netStream.Flush ();
		}
	}

	public void Decompress (SoapMessage message)
	{
		char cf = (char) netStream.ReadByte ();
		Stream sourceStream;
		
		if (cf == 'C') {
			StreamReader sr = new StreamReader (netStream, Encoding.UTF8);
			string compString = sr.ReadToEnd ();
			sr.Close ();
	
			byte[] compBytes = Convert.FromBase64String (compString);
	
			MemoryStream mems = new MemoryStream (compBytes);		
			sourceStream = new GZipInputStream (mems);
		}
		else {
			sourceStream = netStream;
		}
		
		int len = 0;
		byte[] buffer = new byte[1024];
		while ((len = sourceStream.Read (buffer, 0, buffer.Length)) != 0)
			tempStream.Write (buffer, 0, len);
				
//		sourceStream.Close ();
		tempStream.Position = 0;
	}
}

[AttributeUsage(AttributeTargets.Method)]
public class CompressAttribute: SoapExtensionAttribute
{
	private int priority = 0;
	private int minLength = 0;
	
	public override Type ExtensionType 
	{
		get { return typeof (CompressExtension); }
	}

	public override int Priority 
	{
		get { return priority; }
		set { priority = value; }
	}
	
	public int MinLength 
	{
		get { return minLength; }
		set { minLength = value; }
	}
}

public class CompressExtensionImporter : SoapExtensionImporter
{
	public override void ImportMethod (CodeAttributeDeclarationCollection metadata)
	{
		CompressOperationBinding cob = ImportContext.OperationBinding.Extensions.Find (typeof (CompressOperationBinding)) as CompressOperationBinding;
		if (cob == null) return;	// Extension element not present

		CodeAttributeDeclaration att = new CodeAttributeDeclaration ("Compress");
		if (cob.MinLength != 0) att.Arguments.Add (new CodeAttributeArgument ("MinLength", new CodePrimitiveExpression(cob.MinLength)));
		metadata.Add (att);
	}
}

public class CompressExtensionReflector : SoapExtensionReflector
{
	public override void ReflectMethod ()
	{
		object[] ats = ReflectionContext.Method.MethodInfo.GetCustomAttributes (typeof (CompressAttribute), true);
		if (ats.Length > 0)
		{
			CompressAttribute at = (CompressAttribute) ats[0];
			CompressOperationBinding opBinding = new CompressOperationBinding();
			opBinding.MinLength = at.MinLength;
			ReflectionContext.OperationBinding.Extensions.Add (opBinding);
		}
	}
}

[XmlFormatExtension ("compress", "http://www.go-mono.org/Samples", typeof (OperationBinding))]
[XmlFormatExtensionPrefix ("mono", "http://www.go-mono.org/Samples")]
public class CompressOperationBinding : ServiceDescriptionFormatExtension
{
	int minLength;
	
	[XmlAttribute]
	[DefaultValue (0)]
	public int MinLength
	{
		get { return minLength; }
		set { minLength = value; }
	}
}
