namespace TurtleApi.Services.Programs.Generators;

public interface IProgramGenerator
{
    string ProgramName { get; }
    List<string> GenerateSteps(string[]? args);
}