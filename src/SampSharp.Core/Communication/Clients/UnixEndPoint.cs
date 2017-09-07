// SampSharp
// Copyright 2017 Tim Potze
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
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SampSharp.Core.Communication.Clients
{
    internal class UnixEndPoint : EndPoint
    {
        public UnixEndPoint(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Cannot be empty.", nameof(path));
        }

        public string Path { get; private set; }

        public override AddressFamily AddressFamily => AddressFamily.Unix;

        public override EndPoint Create(SocketAddress socketAddress)
        {
            if ((int) AddressFamily != (socketAddress[1] << 8) + socketAddress[0])
                throw new ArgumentException("Address is not a unix socket address.", nameof(socketAddress));

            if (socketAddress.Size == 2)
            {
                // Empty path.
                // Probably from RemoteEndPoint which on linux does not return the path.
                return new UnixEndPoint("a") { Path = "" };
            }

            var size = socketAddress.Size - 2;
            var bytes = new byte[size];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = socketAddress[i + 2];

                if (bytes[i] == 0)
                {
                    size = i;
                    break;
                }
            }

            var name = Encoding.UTF8.GetString(bytes, 0, size);
            return new UnixEndPoint(name);
        }

        public override SocketAddress Serialize()
        {
            var bytes = Encoding.UTF8.GetBytes(Path);
            var sa = new SocketAddress(AddressFamily, 2 + bytes.Length + 1);

            for (var i = 0; i < bytes.Length; i++)
            {
                sa[2 + i] = bytes[i];
            }

            sa[2 + bytes.Length] = 0;

            return sa;
        }

        public override string ToString()
        {
            return Path;
        }

        #region Equality members

        protected bool Equals(UnixEndPoint other)
        {
            return string.Equals(Path, other.Path);
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((UnixEndPoint) obj);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Path != null ? Path.GetHashCode() : 0;
        }

        /// <summary>
        ///     Returns a value that indicates whether the values of two
        ///     <see cref="T:SampSharp.Core.Communication.Clients.UnixEndPoint" /> objects are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        ///     true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise,
        ///     false.
        /// </returns>
        public static bool operator ==(UnixEndPoint left, UnixEndPoint right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Returns a value that indicates whether two <see cref="T:SampSharp.Core.Communication.Clients.UnixEndPoint" /> objects
        ///     have different values.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(UnixEndPoint left, UnixEndPoint right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}