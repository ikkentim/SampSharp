using System;
using System.Runtime.InteropServices;

namespace SampSharp.SourceGenerator;

public static class Constants
{
    // System

    /// <summary>
    /// System.Span&lt;byte&gt;
    /// </summary>
    public const string SpanOfBytesFQN = "System.Span<byte>";

    /// <summary>
    /// System.Delegate
    /// </summary>
    public static readonly string DelegateFQN = typeof(Delegate).FullName!;

    /// <summary>
    /// System.Runtime.InteropServices.Marshal
    /// </summary>
    public static readonly string MarshalFQN = typeof(Marshal).FullName!;

    /// <summary>
    /// System.IEquatable{T}
    /// </summary>
    public static readonly string IEquatableFQN = typeof(IEquatable<>).FullName!.Split('`')[0];

    /// <summary>
    /// System.Runtime.InteropServices.Marshalling.MarshalUsingAttribute
    /// </summary>
    public const string MarshalUsingAttributeFQN = "System.Runtime.InteropServices.Marshalling.MarshalUsingAttribute";

    /// <summary>
    /// System.Runtime.InteropServices.Marshalling.MarshalUsingAttribute
    /// </summary>
    public const string NativeMarshallingAttributeFQN = "System.Runtime.InteropServices.Marshalling.NativeMarshallingAttribute";

    /// <summary>
    /// System.Runtime.InteropServices.Marshalling.CustomMarshallerAttribute
    /// </summary>
    public const string CustomMarshallerAttributeFQN = "System.Runtime.InteropServices.Marshalling.CustomMarshallerAttribute";

    /// <summary>
    /// System.Runtime.InteropServices.Marshalling.CustomMarshallerAttribute.GenericPlaceholder
    /// </summary>
    public const string GenericPlaceholderFQN = "System.Runtime.InteropServices.Marshalling.CustomMarshallerAttribute.GenericPlaceholder";
    
    // SampSharp.OpenMp.Core
    
    /// <summary>
    /// SampSharp.OpenMp.Core.OpenMpApiAttribute
    /// </summary>
    public const string ApiAttributeFQN = "SampSharp.OpenMp.Core.OpenMpApiAttribute";

    /// <summary>
    /// SampSharp.OpenMp.Core.OpenMpApiPartialAttribute
    /// </summary>
    public const string ApiPartialAttributeFQN = "SampSharp.OpenMp.Core.OpenMpApiPartialAttribute";

    /// <summary>
    /// SampSharp.OpenMp.Core.OpenMpEventHandlerAttribute
    /// </summary>
    public const string EventHandlerAttributeFQN = "SampSharp.OpenMp.Core.OpenMpEventHandlerAttribute";

    /// <summary>
    /// SampSharp.OpenMp.Core.OpenMpApiOverloadAttribute
    /// </summary>
    public const string OverloadAttributeFQN = "SampSharp.OpenMp.Core.OpenMpApiOverloadAttribute";

    /// <summary>
    /// SampSharp.OpenMp.Core.OpenMpApiFunctionAttribute
    /// </summary>
    public const string FunctionAttributeFQN = "SampSharp.OpenMp.Core.OpenMpApiFunctionAttribute";

    /// <summary>
    /// SampSharp.OpenMp.Core.NumberedTypeGeneratorAttribute
    /// </summary>
    public const string NumberedTypeGeneratorAttributeFQN = "SampSharp.OpenMp.Core.NumberedTypeGeneratorAttribute";
    
    /// <summary>
    /// SampSharp.OpenMp.Core.StartupContext
    /// </summary>
    public const string StartupContextFQN = "SampSharp.OpenMp.Core.StartupContext";

    /// <summary>
    /// SampSharp.OpenMp.Core.SampSharpInitParams
    /// </summary>
    public const string InitParamsFQN = "SampSharp.OpenMp.Core.SampSharpInitParams";

    /// <summary>
    /// SampSharp.OpenMp.Core.IUnmanagedInterface
    /// </summary>
    public const string UnmanagedInterfaceFQN = "SampSharp.OpenMp.Core.IUnmanagedInterface";

    // SampSharp.OpenMp.Core.Std
    
    /// <summary>
    /// SampSharp.OpenMp.Core.Std.StringViewMarshaller
    /// </summary>
    public const string StringViewMarshallerFQN = "SampSharp.OpenMp.Core.Std.StringViewMarshaller";

    /// <summary>
    /// SampSharp.OpenMp.Core.Std.BooleanMarshaller
    /// </summary>
    public const string BooleanMarshallerFQN = "SampSharp.OpenMp.Core.Std.BooleanMarshaller";

    // SampSharp.OpenMp.Core.Api

    /// <summary>
    /// SampSharp.OpenMp.Core.Api.IComponent
    /// </summary>
    public const string IComponentFQN = "SampSharp.OpenMp.Core.Api.IComponent";

    /// <summary>
    /// SampSharp.OpenMp.Core.Api.IEventHandler{TEventHandler}
    /// </summary>
    public const string EventHandlerFQN = "SampSharp.OpenMp.Core.Api.IEventHandler";
    
    /// <summary>
    /// SampSharp.OpenMp.Core.Api.EventHandlerMarshaller{TEventHandler}
    /// </summary>
    public const string EventHandlerMarshallerFQN = "SampSharp.OpenMp.Core.Api.EventHandlerMarshaller";

    /// <summary>
    /// SampSharp.OpenMp.Core.Api.IEventHandlerMarshaller{TEventHandler}
    /// </summary>
    public const string IEventHandlerMarshallerFQN = "SampSharp.OpenMp.Core.Api.IEventHandlerMarshaller";
}