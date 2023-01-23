using System.Reflection;

namespace PracticeConsoleFramework.Source;

internal class Instances : IDisposable
{
    Dictionary<int, Type>? _types;
    private static Instances? _instance;

    public static Instances Instance
    {
        get
        {
            if (_instance is null)
                _instance = new Instances();
            return _instance;
        }
    }

    public void ShowMenu()
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        var assemblyName = assembly.FullName?
                               .Split(',')[0] ??
                           throw new Exception("Assembly Name and Null");

        _types = new Dictionary<int, Type>();
        var i = 0;
        var namespaces = $"{assemblyName}.Source.Build";

        var result = assembly.GetTypes().Where(t => t.Name.StartsWith("Build")
                                                    && t.Namespace == namespaces
                                                    && t.IsClass);
        PrintItmes();

        namespaces = $"{assemblyName}.Source.Practice";
        result = assembly.GetTypes().Where(t => t.Name.StartsWith("Prc")
                                    && t.Namespace == namespaces
                                    && t.IsClass);
        PrintItmes();
        void PrintItmes()
        {
            foreach (var type in result)
            {
                var instance = Activator.CreateInstance(type);
                PropertyInfo? proInfo = instance?.GetType().GetProperty("PrcName");
                var value = proInfo?.GetValue(instance) ?? "null";
                Message($"{++i}.{value}\n");
                _types.Add(i, type);
            }
        }
    }

    private Task Message(string message, int delay = 100)
    {
        Write(message);
        Thread.Sleep(delay);
        return Task.CompletedTask;
    }

    public void Run(int appCode)
    {
        if (_types is null) return;
        var type = _types[appCode];
        var methodInfo = type.GetMethod("Trigger");
        var instance = Activator.CreateInstance(type);
        var invoke = methodInfo?.Invoke(instance, null);
    }

    ~Instances()
    {
        Dispose();
    }
    public void Dispose()
    {
        if (_types is not null) _types = null;
        if (_instance is not null) _instance = null;
    }
}