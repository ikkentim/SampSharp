using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMode.Tests
{
    public class ServicesTest : ITest
    {
        #region Implementation of ITest

        public void Start(GameMode gameMode)
        {
            gameMode.Services.AddService<ITestServiceA>(new TestServiceA(gameMode));
            gameMode.Services.AddService(typeof(ITestServiceB), new TestServiceB(gameMode));

            var a = gameMode.Services.GetService<ITestServiceA>();
            a.Test();
        }

        #endregion
    }
}
