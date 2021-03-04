using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotnetModelFuzzer.Manipulations
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

            // We have to do this insanity because of nuget. All the fuzzdb attacks are Base64 encoded, and get decoded on first load.
            var files = Directory.GetFiles(fullPath, "*.txt");
            if (files == null || files.Length == 0)
            {
                var base64 = Directory.GetFiles(fullPath, "*.base64");
                foreach (var file in base64)
                {
                    var fileText = File.ReadAllText(file);
                    var base64EncodedBytes = System.Convert.FromBase64String(fileText);
                    File.WriteAllText(file.Replace(".base64", ""), System.Text.Encoding.UTF8.GetString(base64EncodedBytes));
                }
            }

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
