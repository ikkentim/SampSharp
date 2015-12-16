namespace SampSharp.GameMode.SAMP.Commands.Arguments
{
    public interface ICommandParameterType
    {
        bool GetValue(ref string commandText, out object output);
    }
}