namespace TurtleApi.Services.Programs.Requests;

public class ProgramResponses
{
    public record Program(int Id, string turtleName, bool IsCompleted, int TotalStepCount, int CompletedStepCount);
}