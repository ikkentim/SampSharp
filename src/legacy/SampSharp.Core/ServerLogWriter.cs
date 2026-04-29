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
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SampSharp.Core;

internal class ServerLogWriter : TextWriter
{
    private readonly IGameModeClient _gameModeClient;

    public ServerLogWriter(IGameModeClient gameModeClient)
    {
        _gameModeClient = gameModeClient ?? throw new ArgumentNullException(nameof(gameModeClient));
    }

    public override Encoding Encoding => Encoding.ASCII;

    public override void WriteLine(char value)
    {
        Write(value);
    }

    public override void WriteLine(string? value)
    {
        Write(value);
    }

    public override void WriteLine()
    {
        Write(string.Empty);
    }

    public override void WriteLine(string format, object? arg0)
    {
        Write(format, arg0);
    }

    public override void WriteLine(string format, object? arg0, object? arg1)
    {
        Write(format, arg0, arg1);
    }

    public override void WriteLine(string format, object? arg0, object? arg1, object? arg2)
    {
        Write(format, arg0, arg1, arg2);
    }

    public override void WriteLine(string format, params object?[] arg)
    {
        Write(format, arg);
    }

    public override void WriteLine(bool value)
    {
        Write(value);
    }

    public override void WriteLine(char[]? buffer)
    {
        Write(buffer);
    }

    public override void WriteLine(char[] buffer, int index, int count)
    {
        Write(buffer, index, count);
    }

    public override void WriteLine(decimal value)
    {
        Write(value);
    }

    public override void WriteLine(double value)
    {
        Write(value);
    }


    public override void WriteLine(float value)
    {
        Write(value);
    }

    public override void WriteLine(int value)
    {
        Write(value);
    }

    public override void WriteLine(long value)
    {
        Write(value);
    }

    public override void WriteLine(object? value)
    {
        Write(value);
    }

    public override void WriteLine(uint value)
    {
        Write(value);
    }

    public override void WriteLine(ulong value)
    {
        Write(value);
    }

    public override void Write(char value)
    {
        // TODO: Buffer until a line break is sent
        switch (value)
        {
            case '\r':
            case '\n':
                /*
                 * Do not print \r \n characters.
                 * There are emitted by Console.WriteLine, but Native.Print breaks for itself.
                 */
                break;
            default:
                Write(value.ToString());
                break;
        }
    }

    public override void Write(string? value)
    {
        if (value == null)
        {
            _gameModeClient.Print(string.Empty);
            return;
        }
        foreach (var ln in value.Split('\n'))
        {
            var line = ln.Trim('\r');
            while (line.Length > 512)
            {
                var block = line[..512];
                line = line[512..];
                _gameModeClient.Print(block);
            }

            _gameModeClient.Print(line);
        }
    }

    public override void Write(bool value)
    {
        Write(value.ToString());
    }

    public override void Write(string format, object? arg0)
    {
        // ReSharper disable once RedundantStringFormatCall
        Write(string.Format(CultureInfo.InvariantCulture, format, arg0));
    }

    public override void Write(string format, object? arg0, object? arg1)
    {
        // ReSharper disable once RedundantStringFormatCall
        Write(string.Format(CultureInfo.InvariantCulture, format, arg0, arg1));
    }

    public override void Write(string format, object? arg0, object? arg1, object? arg2)
    {
        // ReSharper disable once RedundantStringFormatCall
        Write(string.Format(CultureInfo.InvariantCulture, format, arg0, arg1, arg2));
    }

    public override void Write(string format, params object?[] arg)
    {
        // ReSharper disable once RedundantStringFormatCall
        Write(string.Format(CultureInfo.InvariantCulture, format, arg));
    }

    public override void Write(char[]? buffer)
    {
        if (buffer == null)
        {
            Write(string.Empty);
            return;
        }
        Write(string.Join(string.Empty, buffer));
    }

    public override void Write(char[] buffer, int index, int count)
    {
        Write(new string(buffer.AsSpan(index, count)));
    }

    public override void Write(decimal value)
    {
        Write(value.ToString(CultureInfo.InvariantCulture));
    }

    public override void Write(double value)
    {
        Write(value.ToString(CultureInfo.InvariantCulture));
    }

    public override void Write(float value)
    {
        Write(value.ToString(CultureInfo.InvariantCulture));
    }

    public override void Write(int value)
    {
        Write(value.ToString(CultureInfo.InvariantCulture));
    }

    public override void Write(long value)
    {
        Write(value.ToString(CultureInfo.InvariantCulture));
    }

    public override void Write(object? value)
    {
        Write(value?.ToString() ?? string.Empty);
    }

    public override void Write(uint value)
    {
        Write(value.ToString(CultureInfo.InvariantCulture));
    }

    public override void Write(ulong value)
    {
        Write(value.ToString(CultureInfo.InvariantCulture));
    }

    // TODO: Improve Async variants
    public override Task WriteAsync(char value)
    {
        return new Task(() => Write(value));
    }

    public override Task WriteAsync(char[] buffer, int index, int count)
    {
        return new Task(() => Write(buffer, index, count));
    }

    public override Task WriteAsync(string? value)
    {
        return new Task(() => Write(value));
    }

    public override Task WriteLineAsync()
    {
        return new Task(WriteLine);
    }

    public override Task WriteLineAsync(char value)
    {
        return new Task(() => Write(value));
    }

    public override Task WriteLineAsync(char[] buffer, int index, int count)
    {
        return new Task(() => WriteLine(buffer, index, count));
    }

    public override Task WriteLineAsync(string? value)
    {
        return new Task(() => WriteLine(value));
    }
}