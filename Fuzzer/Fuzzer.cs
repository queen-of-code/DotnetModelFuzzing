using Fuzzing.Manipulations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fuzzer.FuzzerTests.dll")]

namespace Fuzzing.Fuzzer
{
    public abstract class Fuzzer<T>
    {
        private static readonly Type SeedType = typeof(Manipulation<T>);
        private static readonly Type[] AllManipTypes = SeedType.Assembly.GetTypes();

        protected readonly Strategy Strategy;
        protected readonly Random Random;
        protected readonly List<Manipulation<T>> LoadedManipulations; 

        public Fuzzer()
        {
            this.Strategy = new Strategy();
            this.Random = new Random();
            this.LoadedManipulations = LoadManipulations();
        }

        public Fuzzer(Strategy strategy, int randomSeed)
        {
            this.Strategy = strategy;
            this.Random = new Random(randomSeed);

            this.LoadedManipulations = LoadManipulations(randomSeed);
        }

        public List<Manipulation<T>> LoadManipulations(int? randomSeed = null)
        {
            if (this.Strategy?.ValidManipulations == null || this.Strategy.ValidManipulations.Count == 0)
                return null;

            var manipulations = new List<Manipulation<T>>(this.Strategy.ValidManipulations.Count);

            foreach (var manipName in this.Strategy.ValidManipulations)
            {
                try
                {
                    var type = AllManipTypes.FirstOrDefault(s => string.Equals(s.Name, manipName));
                    Manipulation<T> result;
                    if (randomSeed.HasValue)
                    {
                        result = (Manipulation<T>)Activator.CreateInstance(type, randomSeed.Value);
                    }
                    else
                    {
                        result = (Manipulation<T>)Activator.CreateInstance(type);
                    }

                    manipulations.Add(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something messed up happened - {e.ToString()}");
                }
            }

            return manipulations;
        }

        public abstract T Fuzz(T input = default);
    }
}
