namespace DotnetModelFuzzer.Manipulations
{
    public interface IMutationManipulation<T>
    {
        T Manipulate(T input);
    }
}
