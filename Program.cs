using System.Diagnostics;
namespace PracticeConsoleFramework;

internal static class Program
{
    static async Task Main(string[] args)
    {
        await Menu();
        await Listener();
        Debug.WriteLine("DOne");
    }

    private static Task Menu()
    {
        Clear();
        Source.Instances.Instance.ShowMenu();
        return Task.CompletedTask;
    }

    static async Task Listener()
    {
        first:
        if (int.TryParse(ReadLine(), out int key))
        {
            Clear();
            Source.Instances.Instance.Run(key);
            Clear();
            await Menu();
            goto first;
        }
    }
}