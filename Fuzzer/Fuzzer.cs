using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Fuzzing.Manipulations;

[assembly: InternalsVisibleTo("Fuzzer.FuzzerTests.dll")]

namespace Fuzzing.Fuzzer
{
    public class Fuzzer
    {
        private readonly Strategy Strategy;
        private readonly List<Manipulation> Manipulations = new List<Manipulation>();

        public Fuzzer()
        {
            Strategy = new Strategy();
        }

        public Fuzzer(Strategy strategy)
        {
            Strategy = strategy;
        }

        public static List<Manipulation> LoadManipulations(Strategy strategy)
        {
            if (strategy?.ValidManipulations == null)
                return null;

            List<Manipulation> manipulations = new List<Manipulation>(strategy.ValidManipulations.Count);

            foreach (var manip in strategy.ValidManipulations)
            {
                try
                {
                    var type = Type.GetType(manip);
                    var result = (Manipulation)Activator.CreateInstance(type);
                    manipulations.Add(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something messed up happened - {e.ToString()}");
                }
            }

            return manipulations;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string FuzzString()
        {

            return null;
        }
    }
}
