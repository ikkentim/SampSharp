using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{

    /// <summary>
    ///     Keeps track of a pool of identifyable instances.
    /// </summary>
    /// <typeparam name="TInstance">Base type of instances to keep track of.</typeparam>
    public abstract class OldIdentifiedPool<TInstance> : Pool<TInstance> where TInstance : class, IIdentifiable
    {
        /// <summary>
        ///     The type to initialize when adding an instance to this pool by id.
        /// </summary>
        protected static Type InstanceType { get; private set; }

        private static PropertyInfo _idProperty;

        /// <summary>
        ///     Registers the type to use when initializing new instances.
        /// </summary>
        /// <typeparam name="TRegister">The <see cref="Type" /> to use when initializing new instances.</typeparam>
        public static void Register<TRegister>() where TRegister : TInstance
        {
            InstanceType = typeof(TRegister);

            var idProperty = InstanceType.GetProperty("Id");

            if (idProperty == null)
                throw new Exception("The specified type has no Id property");

            _idProperty = idProperty;
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" />".
        /// </summary>
        /// <param name="id">The identity of the instance to find.</param>
        /// <returns>The found instance.</returns>
        public static TInstance Find(int id)
        {
            return All.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        ///     Initializes a new instance with the given id.
        /// </summary>
        /// <param name="id">The identity of the instance to create.</param>
        /// <returns>The initialized instance.</returns>
        public static TInstance Create(int id)
        {
            var instance = (TInstance)Activator.CreateInstance(InstanceType);
            _idProperty.SetValue(instance, id);
            return instance;
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" /> or initializes a new one.
        /// </summary>
        /// <param name="id">The identity of the instance to find or create.</param>
        /// <returns>The found instance.</returns>
        public static TInstance FindOrCreate(int id)
        {
            return Find(id) ?? Create(id);
        }
    }

    public class OldPoolTest : OldIdentifiedPool<OldPoolTest>, IIdentifiable
    {
        public int Id { get; private set; }

        public override string ToString()
        {
            return "old " + Id;
        }
    }

    public class NewPoolTest : IdentifiedPool<NewPoolTest>
    {
        public override string ToString()
        {
            return "new " + Id;
        }
    }

    class PoolTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            OldPoolTest.Register<OldPoolTest>();
            NewPoolTest.Register<NewPoolTest>();

            var sw = new Stopwatch();

            Console.WriteLine("Filling old pool...");

            sw.Start();

            for (var i = 0; i < 1000; i++)
                OldPoolTest.Create(i);
            for (var i = 0; i < 100; i++)
                OldPoolTest.Create(-1);

            sw.Stop();
            Console.WriteLine("Took {0}", sw.Elapsed);
            sw.Reset();

            Console.WriteLine("Filling new pool...");

            sw.Start();

            for (var i = 0; i < 1000; i++)
                NewPoolTest.Create(i);
            for (var i = 0; i < 100; i++)
                NewPoolTest.Create(-1);

            sw.Stop();
            Console.WriteLine("Took {0}", sw.Elapsed);
            sw.Reset();

            Console.WriteLine("Finding all instances in old pool 5x");
            sw.Start();
            for(var x=0;x<5;x++)
                for (var i = 0; i < 1000; i++)
                    OldPoolTest.Find(i);

            sw.Stop();
            Console.WriteLine("Took {0}", sw.Elapsed);
            sw.Reset();

            Console.WriteLine("Finding all instances in new pool 5x");
            sw.Start();
            for (var x = 0; x < 5; x++)
                for (var i = 0; i < 1000; i++)
                    NewPoolTest.Find(i);

            sw.Stop();
            Console.WriteLine("Took {0}", sw.Elapsed);
            sw.Reset();

        }
    }
}
