using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzing.Manipulations
{
    /// <summary>
    /// Caches all the manipulations from the DLL into a static class, for much faster lookup and in
    /// and across models. Each manipulation is considered unique based on the combo of random seed and
    /// name.
    /// </summary>
    public static class ManipulationCache
    {
        private static readonly Type SeedType = typeof(Manipulation<string>);
        private static readonly Type[] AllManipTypes = SeedType.Assembly.GetTypes();
        private static ConcurrentDictionary<string, IManupulation> Cache = new ConcurrentDictionary<string, IManupulation>();

        public static IManupulation GetOrAdd(string name, int randomSeed)
        {
            var key = GetKey(name, randomSeed);
            return Cache.GetOrAdd(key, (key) =>
            {
                return LoadManipulation(name, randomSeed);
            });
        }

        public static Manipulation<TManipType> GetOrAdd<TManipType>(string name, int randomSeed)
        {
            var manip = GetOrAdd(name, randomSeed);

            return manip as Manipulation<TManipType>;
        }

        /// <summary>
        /// Ensure that the manipulations are uniquely keyed while taking the random
        /// seed into account that each might have been created with.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="randomSeed"></param>
        /// <returns></returns>
        private static string GetKey(string name, int randomSeed)
        {
            return $"{name} + {randomSeed.ToString()}";
        }

        public static List<Manipulation<TManipType>> LoadAllManipulations<TManipType>(int randomSeed)
        {
            List<Manipulation<TManipType>> manips = new List<Manipulation<TManipType>>();
            foreach (var type in AllManipTypes)
            { 
                var m = GetOrAdd<TManipType>(type.Name, randomSeed);
                if (m != null) 
                    manips.Add(m); ;
            }

            return manips;
        }

        private static IManupulation LoadManipulation(string manipName, int? randomSeed)
        {
            try
            {
                var type = AllManipTypes.FirstOrDefault(s => string.Equals(s.Name, manipName));
                {
                    IManupulation result;

                    if (type.IsGenericType || type.IsAbstract)
                        return null;

                    if (randomSeed.HasValue)
                    {
                        result = (IManupulation)Activator.CreateInstance(type, randomSeed.Value);
                    }
                    else
                    {
                        result = (IManupulation)Activator.CreateInstance(type);
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something messed up happened - {e.ToString()}");
                return null;
            }
        }
    }
}
