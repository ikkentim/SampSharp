// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using SampSharp.Core.CodePages;

namespace SampSharp.Core;

/// <summary>Provides extended functionality for <see cref="GameModeBuilder" /> for configuring the active encoding.</summary>
public static class EncodingGameModeBuilderExtensions
{
    /// <summary>Use the code page described by the file at the specified <paramref name="path" /> when en/decoding text messages sent to/from the server.</summary>
    /// <param name="builder">The game mode builder.</param>
    /// <param name="path">The path to the code page file.</param>
    /// <returns>The updated game mode configuration builder.</returns>
    public static GameModeBuilder UseEncoding(this GameModeBuilder builder, string path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        return builder.UseEncoding(CodePageEncoding.Load(path));
    }

    /// <summary>Use the code page described by the specified <paramref name="stream" /> when en/decoding text messages sent to/from the server.</summary>
    /// <param name="builder">The game mode builder.</param>
    /// <param name="stream">The stream containing the code page definition.</param>
    /// <returns>The updated game mode configuration builder.</returns>
    public static GameModeBuilder UseEncoding(this GameModeBuilder builder, Stream stream)
    {
        return builder.UseEncoding(CodePageEncoding.Load(stream));
    }

    /// <summary>Uses the encoding code page.</summary>
    /// <param name="builder">The game mode builder.</param>
    /// <param name="pageName">Name of the page.</param>
    /// <returns>The updated game mode configuration builder.</returns>
    public static GameModeBuilder UseEncodingCodePage(this GameModeBuilder builder, string pageName)
    {
        if (pageName == null) throw new ArgumentNullException(nameof(pageName));

        var normalizedPageName = pageName.ToLowerInvariant()
            .Trim();

        var type = typeof(CodePageEncoding);
        var name = $"{type.Namespace}.data.{normalizedPageName}.dat";
        using var stream = type.Assembly.GetManifestResourceStream(name);

        if (stream == null)
        {
            throw new GameModeBuilderException($"Code page with name '{pageName}' is not available.");
        }

        if (!normalizedPageName.StartsWith("cp", StringComparison.InvariantCulture) || normalizedPageName.Length <= 2 ||
            !int.TryParse(normalizedPageName.AsSpan(2), out var codePage))
        {
            codePage = -1;
        }

        var encoding = CodePageEncoding.Deserialize(stream, codePage);
        return builder.UseEncoding(encoding);
    }
}