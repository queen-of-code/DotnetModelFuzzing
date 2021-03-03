using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fuzzing.Manipulations
{
    public abstract class FuzzDbManipulation<T> : Manipulation<T>
    {
        private const string FuzzDbAttackPathBase = "fuzzdb";

        protected IList<string> ViableInputs;

        public FuzzDbManipulation(string inputFiles, string[] excludeFiles = null) : base()
        {
            ViableInputs = LoadFromPath(inputFiles, excludeFiles);
        }

        public FuzzDbManipulation(int seed, string inputFiles, string[] excludeFiles = null) : base(seed)
        {
            ViableInputs = LoadFromPath(inputFiles, excludeFiles);
        }

        protected IList<string> LoadFromPath(string baseDir, string[] excludedFileNames = null)
        {
            if (string.IsNullOrEmpty(baseDir))
                return null;

            var fullPath = Path.Combine(FuzzDbAttackPathBase, "attack", baseDir);
            if (!Directory.Exists(fullPath))
                return null;

            var allInputs = new List<string>();
            foreach (var file in Directory.GetFiles(fullPath, "*.txt"))
            {
                if (excludedFileNames != null)
                {
                    if (excludedFileNames.Contains(Path.GetFileNameWithoutExtension(file)))
                        continue;
                }

                allInputs.AddRange(File.ReadAllLines(file).Where(s => !string.IsNullOrEmpty(s)));
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
