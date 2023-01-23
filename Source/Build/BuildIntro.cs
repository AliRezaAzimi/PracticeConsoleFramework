namespace PracticeConsoleFramework.Source.Build;

internal class BuildIntro : IClass
{
    /// <summary>
    /// Intro of system
    /// </summary>
    public BuildIntro()
    {
        PrcName = "Intro System";
    }
    public string PrcName { get; init; }
    public void Trigger()
    {
        MessageWithDelay("Practice Platform.\n");
        MessageWithDelay("Each Class is a practice file:\n");
        MessageWithDelay("\tthe Class inherits from IClass;\n");
        MessageWithDelay("\tthe Class has two attribute:\n");
        MessageWithDelay("\t\t1.PrcName as the string;\n");
        MessageWithDelay("\t\t2.Trigger as a method to run practice.\n");
        MessageWithDelay("Now, Create your practice class in source folder\n");
        MessageWithDelay("Naming Convention is Prc + ClassName, like PrcHelloWord");
        ReadLine();
    }

    private async void MessageWithDelay(string message, int delay = 300)
    {
        Write(message);
        await Task.Delay(delay);
    }

    public void Dispose()
    {

    }
}