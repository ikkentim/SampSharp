using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Communication;

namespace SampSharp.Core.Natives
{
    public class Native
    {
        private readonly IGameModeClient _gameModeClient;
        private readonly int _handle;
        private readonly NativeParameterInfo[] _parameters;

        public Native(IGameModeClient gameModeClient, int handle, params NativeParameterInfo[] parameters)
        {
            _gameModeClient = gameModeClient ?? throw new ArgumentNullException(nameof(gameModeClient));
            _handle = handle;
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));

            if (parameters.Any(info => info.RequiresLength && info.LengthIndex >= parameters.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(parameters), "Invalid parameter length index.");
            }
        }

        public int Invoke(params object[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (_parameters.Length != args.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(args), "Invalid argument count");
            }

            IEnumerable<byte> data = ValueConverter.GetBytes(_handle);

            for (var i = 0; i < _parameters.Length; i++)
            {
                data = data.Concat(new[] { (byte) _parameters[i].ArgumentType });

                var length = 0;

                if (_parameters[i].RequiresLength)
                {
                    var lengthObj = args[_parameters[i].LengthIndex];
                    if (args[_parameters[i].LengthIndex] is int len)
                    {
                        length = len;
                    }
                    else
                    {
                        throw new ArgumentException("Argument is expected to be of type int, but is " + (lengthObj?.GetType().Name ?? "null"),
                            nameof(args));
                    }
                }

                data = data.Concat(_parameters[i].GetBytes(args[i], length));
            }
            
            var response = _gameModeClient.InvokeNative(_handle, data);

            if (response.Length < 4)
            {
                return 0;
            }

            var respPos = 4;
            
            for (var i = 0; i < _parameters.Length; i++)
            {
                var value = _parameters[i].GetReferenceArgument(response, ref respPos);
                if (value != null)
                {
                    args[i] = value;
                }
            }

            return ValueConverter.ToInt32(response, 0);
        }
    }
}
