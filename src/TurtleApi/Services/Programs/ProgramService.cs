using System.Threading.Tasks;

namespace TurtleApi.Services.Programs;

public interface IProgramService
{
    Task<string> GetNextMove(string turtleId);
}

public class ProgramService : IProgramService
{
    public Task<string> GetNextMove(string turtleId)
    {
        return Task.FromResult(Turtle.Forward());
    }
}