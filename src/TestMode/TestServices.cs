using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode;

namespace TestMode
{
    public interface ITestService : IService
    {
        void Test();
    }
    public interface ITestServiceA : ITestService
    {
    }

    public interface ITestServiceB : ITestService
    {
    }

    public class TestServiceA : Service, ITestServiceA
    {
        public TestServiceA(BaseMode gameMode) : base(gameMode)
        {
        }

        #region Implementation of ITestService

        public void Test()
        {
            Console.WriteLine("Hit TestServicesA.Test");
            var service = GameMode.Services.GetService<ITestServiceB>();
            service.Test();
        }

        #endregion
    }
    public class TestServiceB : Service, ITestServiceB
    {
        public TestServiceB(BaseMode gameMode)
            : base(gameMode)
        {
        }

        #region Implementation of ITestService

        public void Test()
        {
            Console.WriteLine("Hit TestServicesB.Test");
        }

        #endregion
    }
}
