using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DotnetModelFuzzer.Manipulations;

[assembly: InternalsVisibleTo("DotnetModelFuzzer.Tests")]

namespace DotnetModelFuzzer.Fuzzer
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
            Strategy = new TStrategy();
            Random = new Random();
            LoadedManipulations = LoadManipulations();
        }

        public Model(TStrategy strategy, int randomSeed)
        {
            Strategy = strategy;
            Random = new Random(randomSeed);
            RandomSeed = randomSeed;

            LoadedManipulations = LoadManipulations();
        }

        private List<Manipulation<TManipType>> LoadManipulations()
        {
            int rand = RandomSeed.HasValue ? RandomSeed.Value : Random.Next(int.MaxValue);

            if (Strategy.UseAllRelevantManipulations)
            {
                return ManipulationCache.LoadAllManipulations<TManipType>(rand);
            }
            else if (Strategy?.ValidManipulations != null && Strategy.ValidManipulations.Count > 0)
            {
                var manipulations = new List<Manipulation<TManipType>>(Strategy.ValidManipulations.Count);

                foreach (var manipName in Strategy.ValidManipulations)
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
            if (LoadedManipulations == null || !LoadedManipulations.Any() || manips == null || !manips.Any())
                return input;

            var numberOfManips = Random.Next(1, Strategy.MaxManipulations + 1);

            TFuzz fuzzed = input;
            for (int x = 0; x < numberOfManips; x++)
            {
                int whichManip = Random.Next(0, manips.Count);
                var manip = manips[whichManip];
                fuzzed = manip.Manipulate(fuzzed);
            }

            return fuzzed;
        }

        internal int LoadedManipulationsCount()
        {
            if (LoadedManipulations == null)
                return 0;

            return LoadedManipulations.Count;
        }

        internal List<Manipulation<TManipType>> GetManipsForTesting()
        {
            return LoadedManipulations;
        }
    }
}
