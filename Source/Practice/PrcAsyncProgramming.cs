namespace PracticeConsoleFramework.Source.Practice;

internal class PrcAsyncProgramming : IClass
{
    public PrcAsyncProgramming()
    {
        PrcName = "Asynchronous Programming";
    }
    public string PrcName { get; init; }
    public void Trigger()
    {
        Task task = new Task(Start);
        task.Start();
        task.Wait();
    }

    private async void Start()
    {
        var count = await Method1();
        Method2();
        Method3(count);
        WriteLine("Done!");
        ReadKey();
    }
    private async Task<int> Method1()
    {
        int count = 0;
        await Task.Run(() =>
        {
            for (int i = 0; i < 50; i++)
            {
                WriteLine($" ({i + 1}/50).Method 1");
                // Do something
                Task.Delay(100).Wait();
                count++;
            }
        });
        return count;
    }


    private void Method2()
    {
        for (int i = 0; i < 25; i++)
        {
            WriteLine($" ({i + 1}/25).Method 2");
            // Do something
            Task.Delay(100).Wait();
        }
    }

    private void Method3(int len)
    {
        WriteLine($"\n\t Total run ia {len}");
    }
    public void Dispose()
    {

    }
}