using Fuzzing.Manipulations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fuzzer.FuzzerTests.dll")]

namespace Fuzzing.Fuzzer
{
    public abstract class Fuzzer<TStrategy, TInput, TManipType> where TStrategy : Strategy, new()
    {
        private static readonly Type SeedType = typeof(Manipulation<TManipType>);
        private static readonly Type[] AllManipTypes = SeedType.Assembly.GetTypes();

        protected readonly TStrategy Strategy;
        protected readonly Random Random;
        protected readonly List<Manipulation<TManipType>> LoadedManipulations; 

        public Fuzzer()
        {
            this.Strategy = new TStrategy();
            this.Random = new Random();
            this.LoadedManipulations = LoadManipulations();
        }

        public Fuzzer(TStrategy strategy, int randomSeed)
        {
            this.Strategy = strategy;
            this.Random = new Random(randomSeed);

            this.LoadedManipulations = LoadManipulations(randomSeed);
        }

        public List<Manipulation<TManipType>> LoadManipulations(int? randomSeed = null)
        {
            if (this.Strategy?.ValidManipulations == null || this.Strategy.ValidManipulations.Count == 0)
                return null;

            var manipulations = new List<Manipulation<TManipType>>(this.Strategy.ValidManipulations.Count);

            foreach (var manipName in this.Strategy.ValidManipulations)
            {
                try
                {
                    var type = AllManipTypes.FirstOrDefault(s => string.Equals(s.Name, manipName));
                    if (type is Manipulation<TManipType>)
                    {
                        Manipulation<TManipType> result;
                        if (randomSeed.HasValue)
                        {
                            result = (Manipulation<TManipType>)Activator.CreateInstance(type, randomSeed.Value);
                        }
                        else
                        {
                            result = (Manipulation<TManipType>)Activator.CreateInstance(type);
                        }

                        manipulations.Add(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something messed up happened - {e.ToString()}");
                }
            }

            return manipulations;
        }

        public abstract TInput Fuzz(TInput input = default);

        protected TFuzz DoFuzzingWork<TFuzz>(List<Manipulation<TFuzz>> manips, TFuzz input = default)
        {
            var numberOfManips = this.Random.Next(1, Strategy.MaxManipulations + 1);

            TFuzz fuzzed = input;
            for (int x = 0; x < numberOfManips; x++)
            {
                int whichManip = Random.Next(0, this.LoadedManipulations.Count);
                var manip = manips[whichManip];
                fuzzed = manip.Manipulate(fuzzed);
            }

            return fuzzed;
        }
    }
}
