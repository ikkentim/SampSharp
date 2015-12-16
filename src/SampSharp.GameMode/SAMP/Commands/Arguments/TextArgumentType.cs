using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Events;

namespace SampSharp.GameMode.SAMP.Commands.Arguments
{
    public class TextCommandParameterType : ICommandParameterType
    {
        #region Implementation of ICommandParameterType

        public bool GetValue(ref string commandText, out object output)
        {
            var text = commandText.Trim();

            if (string.IsNullOrEmpty(text))
            {
                output = null;
                return false;
            }

            output = text;
            commandText = string.Empty;
            return true;
        }

        #endregion
    }
}
