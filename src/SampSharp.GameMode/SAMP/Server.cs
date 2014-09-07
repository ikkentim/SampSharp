using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.SAMP
{
    public class Server
    {
        /// <summary>
        /// Blocks an IP address from further communication with the server 
        /// for a set amount of time (with wildcards allowed). 
        /// Players trying to connect to the server with a blocked IP address 
        /// will receive the generic "You are banned from this server." message. 
        /// Players that are online on the specified IP before the block 
        /// will timeout after a few seconds and, upon reconnect, 
        /// will receive the same message.
        /// </summary>
        /// <param name="ip">
        /// The IP to block.
        /// 
        /// <remarks>
        /// Wildcards can be used with this function, 
        /// for example blocking the IP '6.9.*.*' will block all IPs where the first two octets are 6 and 9 respectively.
        /// Any number can be in place of an asterisk.
        /// </remarks>
        /// 
        /// </param>
        /// <param name="time">The time that the connection will be blocked for. 0 can be used for an indefinite block.</param>
        public static void BlockIPAddress(string ip, TimeSpan time)
        {
            Native.BlockIpAddress(ip, (int) time.TotalMilliseconds);
        }

        /// <summary>
        /// Unblock an IP address that was previously blocked using <see cref="BlockIPAddress"/>.
        /// </summary>
        /// <param name="ip">The IP address to unblock</param>
        public static void UnBlockIPAddress(string ip)
        {
            Native.UnBlockIpAddress(ip);
        }

        /// <summary>
        /// Retrieve a server variable.
        /// 
        /// </summary>
        /// <typeparam name="T">
        /// If <see cref="T"/> is <see cref="int"/> the <see cref="varName"/> will be readed as an int variable
        /// If <see cref="T"/> is <see cref="bool"/> the <see cref="varName"/> will be readed as an boolean variable
        /// If <see cref="T"/> is <see cref="string"/> the <see cref="varName"/> will be readed as an string variable
        /// </typeparam>
        /// <param name="varName">The server variable to read</param>
        /// <returns>The value of the server variable</returns>
        /// <exception cref="NotSupportedException"><see cref="T"/> is not supported by SA:MP</exception>
        public static T Get<T>(string varName)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(Native.GetServerVarAsString(varName), TypeCode.String);
            }
            
            if (typeof (T) == typeof (bool))
            {
                return (T)Convert.ChangeType(Native.GetServerVarAsBool(varName), TypeCode.Boolean);
            }
            
            if (typeof (T) == typeof (int))
            {
                return (T)Convert.ChangeType(Native.GetServerVarAsInt(varName), TypeCode.Int32);
            }

            throw new NotSupportedException("Type " + typeof(T) + " is not supported by SA:MP");
        }
    }
}
