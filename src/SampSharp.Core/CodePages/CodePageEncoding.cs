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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace SampSharp.Core.CodePages;

/// <summary>Represents the encoding of a code page.</summary>
public sealed class CodePageEncoding : Encoding
{
    private int _codePage = -1;
    private readonly Dictionary<ushort, char> _cpToUni = new();
    private readonly Dictionary<char, ushort> _uniToCp = new();
    private bool _hasDoubleByteChars;

    private CodePageEncoding()
    {
    }

    /// <summary>Gets a read-only conversion table.</summary>
    public IReadOnlyDictionary<char, ushort> ConversionTable => new ReadOnlyDictionary<char, ushort>(_uniToCp);

    /// <inheritdoc />
    public override int CodePage => _codePage;

    /// <summary>Serializes the specified code page to the specified stream.</summary>
    /// <param name="codePage">The code page to serialize.</param>
    /// <param name="stream">The stream to serialize the code page to.</param>
    public static void Serialize(CodePageEncoding codePage, Stream stream)
    {
        if (codePage == null) throw new ArgumentNullException(nameof(codePage));
        if (stream == null) throw new ArgumentNullException(nameof(stream));

        var buffer = new byte[1024];

        buffer[0] = codePage._hasDoubleByteChars
            ? (byte)1
            : (byte)0;

        var length = codePage._cpToUni.Count;
        var lengthBytes = BitConverter.GetBytes(length);
        Array.Copy(lengthBytes, 0, buffer, 1, 4);

        var head = 5;

        void Flush()
        {
            if (head == 0)
                return;

            stream.Write(buffer, 0, head);
            head = 0;
        }

        foreach (var kv in codePage._cpToUni)
        {
            if (head + 4 > buffer.Length)
                Flush();

            var val = (ushort)kv.Value;

            var keyL = (byte)(kv.Key & 0xff);
            var keyH = (byte)(kv.Key >> 8);

            var valL = (byte)(val & 0xff);
            var valH = (byte)(val >> 8);

            buffer[head++] = keyL;
            buffer[head++] = keyH;
            buffer[head++] = valL;
            buffer[head++] = valH;
        }

        Flush();
    }

    /// <summary>Deserializes a code page.</summary>
    /// <param name="stream">The stream.</param>
    /// <param name="codePage">The code page identifier of the contents of the stream or -1 if the encoding in the stream does not represent a numbered code page.</param>
    /// <returns>The deserialized code page.</returns>
    public static CodePageEncoding Deserialize(Stream stream, int codePage = -1)
    {
        if (stream == null) throw new ArgumentNullException(nameof(stream));

        var result = new CodePageEncoding
        {
            _codePage = codePage,
            _hasDoubleByteChars = stream.ReadByte() != 0
        };

        var buffer = new byte[4 * 256];
        stream.Read(buffer, 0, 4);

        var length = BitConverter.ToInt32(buffer, 0);

        var i = 0;
        int head;

        void Read()
        {
            var max = (length - i) * 4;
            var rd = Math.Min(max, buffer.Length);

            if (rd != 0)
                stream.Read(buffer, 0, rd);

            head = 0;
        }

        Read();

        for (; i < length; i++)
        {
            if (head == buffer.Length)
                Read();

            var keyL = buffer[head++];
            var keyH = buffer[head++];
            var valL = buffer[head++];
            var valH = buffer[head++];

            var key = (ushort)(keyL | (keyH << 8));
            var val = (char)(ushort)(valL | (valH << 8));
            result._cpToUni[key] = val;
            result._uniToCp[val] = key;
        }

        return result;
    }

    /// <summary>Creates a new <see cref="CodePageEncoding" /> instance based on the data provided by the specified stream.</summary>
    /// <param name="stream">The steam to load the code page encoding from.</param>
    /// <param name="codePage">The code page identifier of the contents of the stream or -1 if the encoding in the stream does not represent a numbered code page.</param>
    /// <returns>The newly created encoding instance.</returns>
    public static CodePageEncoding Load(Stream stream, int codePage = -1)
    {
        if (stream == null) throw new ArgumentNullException(nameof(stream));

        var result = new CodePageEncoding { _codePage = codePage };
        using var reader = new StreamReader(stream);

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (line == null)
            {
                continue;
            }

            var ln = line.Trim();

            if (ln.StartsWith("#", StringComparison.InvariantCulture))
                continue;

            var spl = ln.Split(new[] { ' ', '\t' });

            string? from = null, to = null;
            foreach (var p in spl)
            {
                if (p.StartsWith("#", StringComparison.InvariantCulture))
                    break;

                if (from == null)
                {
                    from = p;
                }
                else
                {
                    to = p;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
                continue;

            try
            {
                var fromNum = System.Convert.ToUInt16(from, 16);
                var toNum = System.Convert.ToUInt16(to, 16);

                if (fromNum > 0xff)
                    result._hasDoubleByteChars = true;
                result._cpToUni[fromNum] = (char)toNum;
                result._uniToCp[(char)toNum] = fromNum;
            }
            catch (ArgumentOutOfRangeException)
            {
                //
            }
        }

        return result;
    }

    /// <summary>Creates a new <see cref="CodePageEncoding" /> instance based on the data provided by the specified stream.</summary>
    /// <param name="path">The path to the code page file to load the code page encoding from.</param>
    /// <param name="codePage">The code page identifier of the contents of the stream or -1 if the encoding in the stream does not represent a numbered code page.</param>
    /// <returns>The newly created encoding instance.</returns>
    public static CodePageEncoding Load(string path, int codePage = -1)
    {
        return Load(File.OpenRead(path), codePage);
    }

    /// <inheritdoc />
    public override int GetByteCount(char[] chars, int index, int count)
    {
        if (count == 0)
            return 0;

        if (chars == null) throw new ArgumentNullException(nameof(chars));
        if (index < 0 || index >= chars.Length) throw new ArgumentOutOfRangeException(nameof(index));
        if (count < 0 || index + count > chars.Length) throw new ArgumentOutOfRangeException(nameof(count));

        var result = 0;
        for (var i = index; i < index + count; i++)
            if (_uniToCp.TryGetValue(chars[i], out var ch))
                result += ch > 0xff
                    ? 2
                    : 1;

        return result;
    }

    /// <inheritdoc />
    public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
        if (chars == null) throw new ArgumentNullException(nameof(chars));
        if (bytes == null) throw new ArgumentNullException(nameof(bytes));

        if (charCount == 0)
            return 0;

        if (charIndex < 0 || charIndex >= chars.Length) throw new ArgumentOutOfRangeException(nameof(charIndex));
        if (byteIndex < 0 || byteIndex >= bytes.Length) throw new ArgumentOutOfRangeException(nameof(byteIndex));
        if (charCount < 0 || charIndex + charCount > chars.Length)
            throw new ArgumentOutOfRangeException(nameof(charCount));

        var result = 0;
        for (var i = charIndex; i < charIndex + charCount; i++)
        {
            if (!_uniToCp.TryGetValue(chars[i], out var ch))
                continue;

            if (ch > 0xff)
            {
                if (byteIndex + 1 >= bytes.Length)
                    throw new ArgumentException(
                        "bytes does not have enough capacity from byteIndex to the end of the array to accommodate the resulting bytes.", nameof(bytes));

                bytes[byteIndex++] = (byte)(ch >> 8);
                bytes[byteIndex++] = (byte)(ch & 0xff);
                result += 2;
            }
            else
            {
                if (byteIndex >= bytes.Length)
                    throw new ArgumentException(
                        "bytes does not have enough capacity from byteIndex to the end of the array to accommodate the resulting bytes.", nameof(bytes));

                bytes[byteIndex++] = (byte)ch;
                result++;
            }
        }

        return result;
    }

    /// <inheritdoc />
    public override int GetCharCount(byte[] bytes, int index, int count)
    {
        if (bytes == null) throw new ArgumentNullException(nameof(bytes));
        if (index < 0 || index >= bytes.Length) throw new ArgumentOutOfRangeException(nameof(index));
        if (count < 0 || index + count > bytes.Length) throw new ArgumentOutOfRangeException(nameof(count));

        var result = 0;
        ushort prefix = 0;
        for (var i = index; i < index + count; i++)
            if (_cpToUni.ContainsKey((ushort)(prefix | bytes[i])))
            {
                result++;
                prefix = 0;
            }
            else
            {
                prefix = (ushort)(bytes[i] << 8);
            }

        return result;
    }

    /// <inheritdoc />
    public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
        if (bytes == null) throw new ArgumentNullException(nameof(bytes));
        if (chars == null) throw new ArgumentNullException(nameof(chars));

        if (byteCount == 0)
            return 0;

        if (byteIndex < 0 || byteIndex >= bytes.Length) throw new ArgumentOutOfRangeException(nameof(byteIndex));
        if (byteCount < 0 || byteIndex + byteCount > bytes.Length)
            throw new ArgumentOutOfRangeException(nameof(byteCount));
        if (charIndex < 0 || charIndex >= chars.Length) throw new ArgumentOutOfRangeException(nameof(charIndex));

        var result = 0;
        ushort prefix = 0;
        for (var i = byteIndex; i < byteIndex + byteCount; i++)
            if (_cpToUni.TryGetValue((ushort)(prefix | bytes[i]), out var ch))
            {
                if (charIndex >= chars.Length)
                    throw new ArgumentException(
                        "chars does not have enough capacity from charIndex to the end of the array to accommodate the resulting chars.", nameof(chars));

                result++;
                chars[charIndex++] = ch;
                prefix = 0;
            }
            else
            {
                prefix = (ushort)(bytes[i] << 8);
            }

        return result;
    }

    /// <inheritdoc />
    public override int GetMaxByteCount(int charCount)
    {
        return _hasDoubleByteChars
            ? charCount * 2
            : charCount;
    }

    /// <inheritdoc />
    public override int GetMaxCharCount(int byteCount)
    {
        return _hasDoubleByteChars
            ? byteCount / 2
            : byteCount;
    }
}