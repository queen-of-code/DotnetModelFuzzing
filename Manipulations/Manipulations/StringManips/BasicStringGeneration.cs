namespace Manipulations.StringManips
{
    public class BasicStringGeneration : Manipulation, IGenerationManipulation<string>
    {
        public BasicStringGeneration() : base() { }
        public BasicStringGeneration(int seed) : base(seed) { }

        public string Manipulate()
        {
            int length = this.Random.Next(0, short.MaxValue);

            return GenerateRandomAsciiString(length);
        }
    }
}
