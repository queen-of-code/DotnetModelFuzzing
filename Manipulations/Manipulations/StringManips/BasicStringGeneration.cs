using DotnetModelFuzzing.Manipulations;

namespace DotnetModelFuzzer.Manipulations.StringManips
{
    public class BasicStringGeneration : Manipulation<string>, IGenerationManipulation<string>
    {
        public BasicStringGeneration() : base() { }
        public BasicStringGeneration(int seed) : base(seed) { }

        public override string Manipulate(string input = default)
        {
            int length = Random.Next(0, short.MaxValue);

            return GenerateRandomAsciiString(length);
        }
    }
}
