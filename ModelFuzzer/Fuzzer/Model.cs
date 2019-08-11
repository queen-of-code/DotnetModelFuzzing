using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Fuzzing.Manipulations;

[assembly: InternalsVisibleTo("Fuzzer.FuzzerTests")]

namespace Fuzzing.Fuzzer
{
    /// <summary>
    /// Base class for all model-based fuzzing. 
    /// </summary>
    /// <typeparam name="TStrategy"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TManipType"></typeparam>
    public abstract class Model<TStrategy, TModel, TManipType> where TStrategy : Strategy, new()
                                                                where TModel : class
    {
        protected readonly TStrategy Strategy;
        protected readonly Random Random;
        protected readonly List<Manipulation<TManipType>> LoadedManipulations;
        protected readonly int? RandomSeed;

        public Model()
        {
            this.Strategy = new TStrategy();
            this.Random = new Random();
            this.LoadedManipulations = LoadManipulations();
        }

        public Model(TStrategy strategy, int randomSeed)
        {
            this.Strategy = strategy;
            this.Random = new Random(randomSeed);
            this.RandomSeed = randomSeed;

            this.LoadedManipulations = LoadManipulations();
        }

        private List<Manipulation<TManipType>> LoadManipulations()
        {
            int rand = this.RandomSeed.HasValue ? this.RandomSeed.Value : Random.Next(Int32.MaxValue);

            if (this.Strategy.UseAllRelevantManipulations)
            {
                return ManipulationCache.LoadAllManipulations<TManipType>(rand);
            }
            else if (this.Strategy?.ValidManipulations != null && this.Strategy.ValidManipulations.Count > 0)
            {
                var manipulations = new List<Manipulation<TManipType>>(this.Strategy.ValidManipulations.Count);

                foreach (var manipName in this.Strategy.ValidManipulations)
                {
                    var manip = ManipulationCache.GetOrAdd<TManipType>(manipName, rand);
                    if (manip != null)
                    {
                        manipulations.Add(manip);
                    }
                }

                return manipulations;
            }
            else
            {
                return null;
            }
        }

        public abstract TModel Fuzz(TModel input = default);

        protected TFuzz DoFuzzingWork<TFuzz>(List<Manipulation<TFuzz>> manips, TFuzz input = default)
        {
            if (this.LoadedManipulations == null || !this.LoadedManipulations.Any())
                return input;

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

        internal int LoadedManipulationsCount()
        {
            if (this.LoadedManipulations == null)
                return 0;

            return this.LoadedManipulations.Count;
        }

        internal List<Manipulation<TManipType>> GetManipsForTesting()
        {
            return this.LoadedManipulations;
        }
    }
}
