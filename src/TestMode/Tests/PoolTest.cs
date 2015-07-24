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
    ///     Keeps track of a pool of owned and identified instances.
    /// </summary>
    /// <typeparam name="TInstance">Base type of instances to keep track of.</typeparam>
    /// <typeparam name="TOwner">Base type of the owner</typeparam>
    public abstract class OldIdentifiedOwnedPool<TInstance, TOwner> : Pool<TInstance>
        where TInstance : class, IIdentifiable, IOwnable<TOwner>
        where TOwner : IdentifiedPool<TOwner>
    {
        /// <summary>
        ///     The type to initialize when adding an instance to this pool by id.
        /// </summary>
        protected static Type InstanceType { get; private set; }

        private static PropertyInfo _idProperty;
        private static PropertyInfo _ownerProperty;

        /// <summary>
        ///     Registers the type to use when initializing new instances.
        /// </summary>
        /// <typeparam name="TRegister">The Type to use when initializing new instances.</typeparam>
        public static void Register<TRegister>()
        {
            InstanceType = typeof(TRegister);

            var idProperty = InstanceType.GetProperty("Id");
            var ownerProperty = InstanceType.GetProperty("Owner");

            if (idProperty == null)
                throw new Exception("The specified type has no Id property");

            if (ownerProperty == null)
                throw new Exception("The specified type has no Owner property");

            _idProperty = idProperty;
            _ownerProperty = ownerProperty;
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="owner" /> and <paramref name="id" />".
        /// </summary>
        /// <param name="owner">The owner of the instance to find.</param>
        /// <param name="id">The identity of the instance to find.</param>
        /// <returns>The found instance.</returns>
        public static TInstance Find(TOwner owner, int id)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            return All.FirstOrDefault(i => i.Owner == owner && i.Id == id);
        }

        /// <summary>
        ///     Initializes a new instance with the given <paramref name="owner" /> and <paramref name="id" />.
        /// </summary>
        /// <param name="owner">The owner of the instance to create.</param>
        /// <param name="id">The identity of the instance to create.</param>
        /// <returns>The initialized instance.</returns>
        public static TInstance Create(TOwner owner, int id)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            var instance = (TInstance)Activator.CreateInstance(InstanceType);
            _ownerProperty.SetValue(instance, owner);
            _idProperty.SetValue(instance, id);
            return instance;
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="owner" /> and <paramref name="id" /> or initializes a new one.
        /// </summary>
        /// <param name="owner">The owner of the instance to find or create.</param>
        /// <param name="id">The identity of the instance to find or create.</param>
        /// <returns>The found instance.</returns>
        public static TInstance FindOrCreate(TOwner owner, int id)
        {
            return Find(owner, id) ?? Create(owner, id);
        }
    }

    public class OwnerTest : IdentifiedPool<OwnerTest>
    {
    }

    public class OldPoolTest : OldIdentifiedOwnedPool<OldPoolTest, OwnerTest>, IIdentifiable, IOwnable<OwnerTest>
    {
        public int Id { get; private set; }

        public override string ToString()
        {
            return "old " + Id;
        }

        public OwnerTest Owner { get; private set; }
    }

    public class NewPoolTest : IdentifiedOwnedPool<NewPoolTest, OwnerTest>
    {
        public override string ToString()
        {
            return "new " + Id;
        }
    }

    internal class PoolTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            OwnerTest.Register<OwnerTest>();
            OldPoolTest.Register<OldPoolTest>();
            NewPoolTest.Register<NewPoolTest>();

            var sw = new Stopwatch();

            // Fill
            Console.WriteLine("Filling old pool...");

            sw.Start();

            for (var o = 0; o < 100; o++)
            {
                var owner = OwnerTest.FindOrCreate(o);
                for (var i = 0; i < 100; i++)
                    OldPoolTest.Create(owner, i);
            }

            if(OldPoolTest.All.Count() != 100 * 100)
                throw new Exception("OldPoolTest count error");

            // Result
            sw.Stop();
            Console.WriteLine("Took {0}", sw.Elapsed);
            sw.Reset();

            // Fill
            Console.WriteLine("Filling new pool...");

            sw.Start();

            for (var o = 0; o < 100; o++)
            {
                var owner = OwnerTest.FindOrCreate(o);
                for (var i = 0; i < 100; i++)
                    NewPoolTest.Create(owner, i);
            }

            if (NewPoolTest.All.Count() != 100 * 100)
                throw new Exception("OldPoolTest count error");

            // Result
            sw.Stop();
            Console.WriteLine("Took {0}", sw.Elapsed);
            sw.Reset();

            // Find
            Console.WriteLine("Finding all instances in old pool 5x");
            sw.Start();

            for (var x = 0; x < 5; x++)
                for (var o = 0; o < 100; o++)
                {
                    var owner = OwnerTest.Find(o);
                    for (var i = 0; i < 100; i++)
                        OldPoolTest.Find(owner, i);
                }

            // Result
            sw.Stop();
            Console.WriteLine("Took {0}", sw.Elapsed);
            sw.Reset();

            // Find
            Console.WriteLine("Finding all instances in new pool 5x");
            sw.Start();

            for (var x = 0; x < 5; x++)
                for (var o = 0; o < 100; o++)
                {
                    var owner = OwnerTest.Find(o);
                    for (var i = 0; i < 100; i++)
                        NewPoolTest.Find(owner, i);
                }

            // Result
            sw.Stop();
            Console.WriteLine("Took {0}", sw.Elapsed);
            sw.Reset();

        }
    }
}
