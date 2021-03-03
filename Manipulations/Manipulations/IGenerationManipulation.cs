namespace DotnetModelFuzzing.Manipulations
{
    public interface IGenerationManipulation<T>
    {
        T Manipulate(T input = default);
    }
}
