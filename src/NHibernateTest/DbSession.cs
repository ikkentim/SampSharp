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

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateTest
{
    public static class DbSession
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory Factory
        {
            get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
        }

        public static ISession OpenSession()
        {
            return Factory.OpenSession();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                        .ConnectionString("Server=localhost;Port=3307;Database=samp;User ID=root;Password=usbw;")
                //Your connectionstring goes here.
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<DbMappings>())
                .ExposeConfiguration(TreatConfiguration)
                .BuildSessionFactory();
        }


        private static void TreatConfiguration(Configuration configuration)
        {
            var update = new SchemaUpdate(configuration);
            update.Execute(false, true);
        }
    }
}