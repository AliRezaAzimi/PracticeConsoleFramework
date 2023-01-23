namespace PracticeConsoleFramework.Source;
/// <summary>
/// Interface for define practice class
/// </summary>
internal interface IClass
{
    /// <summary>
    /// Gets or sets name of practice for presentation
    /// </summary>
    public string PrcName { get; init; }

    /// <summary>
    /// Trigger of Practice file
    /// </summary>
    public void Trigger();
}