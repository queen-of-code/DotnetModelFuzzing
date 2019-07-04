namespace Fuzzing.Manipulations
{
    public interface IGenerationManipulation<T>
    {
        T Manipulate();
    }
}
