namespace SampSharp.GameMode.SAMP.Commands
{
    public enum CommandCallableResponse
    {
        /// <summary>
        /// The specified parameters don't allow this command to be called.
        /// </summary>
        False,
        /// <summary>
        /// The specified parameters require this command to be called.
        /// </summary>
        True,
        /// <summary>
        /// The specified parameters allow this command to be called unless a different command accepts the parameters with a 'True' response.
        /// </summary>
        Optional
    }
}