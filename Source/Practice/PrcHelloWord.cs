namespace PracticeConsoleFramework.Source.Practice;

internal class PrcHelloWord : IClass
{
    public void Dispose()
    {

    }

    public PrcHelloWord()
    {
        PrcName = "Hello Word!";
    }
    public string PrcName { get; init; }
    public void Trigger()
    {
        WriteLine("Hello Word!");
        ReadLine();
    }
}