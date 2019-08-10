using System.Collections.Generic;
using System.IO;

namespace Fuzzing.Manipulations
{
    public class FuzzDbManipulation : Manipulation
    {
        private const string FuzzDbAttackPathBase = "fuzzdb";

        protected IList<string> ViableInputs;

        public FuzzDbManipulation(string inputFiles) : base()
        {
            ViableInputs = LoadFromPath(inputFiles);
        }

        public FuzzDbManipulation(int seed, string inputFiles) : base(seed)
        {
            ViableInputs = LoadFromPath(inputFiles);
        }

        protected IList<string> LoadFromPath(string baseDir)
        {
            if (string.IsNullOrEmpty(baseDir))
                return null;

            var fullPath = Path.Combine("fuzzdb", "attack", baseDir);
            if (!Directory.Exists(fullPath))
                return null;

            var allInputs = new List<string>();
            foreach (var file in Directory.GetFiles(fullPath, "*.txt"))
            {
                allInputs.AddRange(File.ReadAllLines(file));
            }

            return allInputs;
        }

        protected IList<string> LoadFromFiles(string[] filePaths)
        {
            if (filePaths == null)
                return null;

            var allInputs = new List<string>();
            foreach (var file in filePaths)
            {
                var fullPath = Path.Combine("fuzzdb", "attack", file);

                if (!File.Exists(fullPath))
                    continue;

                allInputs.AddRange(File.ReadAllLines(fullPath));
            }

            return allInputs;
        }
    }
}
