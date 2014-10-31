// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using SampSharp.GameMode;

namespace NHibernateTest
{
    public class GameMode : BaseMode
    {
        public override bool OnGameModeInit()
        {
            //Lets store our current session
            using (ISession session = DbSession.OpenSession())
            {
                session.Save(new ServerInfo {Number = new Random().Next(0,100)});
            }

            //...

            //... lets open a bunch of sessions
            for (int x = 0; x < 10; x++)
            {
                using (ISession session = DbSession.OpenSession())
                {
                    //Get first 10 sessions
                    IQueryable<ServerInfo> serverinfos = session.Query<ServerInfo>().Take(10);

                    foreach (ServerInfo si in serverinfos)
                    {
                        Console.WriteLine(si);
                    }
                }
            }

            return base.OnGameModeInit();
        }
    }
}