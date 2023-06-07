using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TurtleApi.Db;
using TurtleApi.Services.Programs.Generators;

namespace TurtleApi.Services.Programs;

public interface IProgramService
{
    Task Generate(string turtleId, string programName, string[]? args);
    Task<string?> GetNextMove(string turtleId);
}

public class ProgramService : IProgramService
{
    private readonly TurtleDbContext _context;
    private readonly Dictionary<string, IProgramGenerator> _generators;

    public ProgramService(TurtleDbContext context, IEnumerable<IProgramGenerator> generators)
    {
        _context = context;
        _generators = generators.ToDictionary(x => x.ProgramName, x => x);
    }

    public async Task Generate(string turtleName, string programName, string[]? args)
    {
        var generator = _generators[programName];
        var steps = generator.GenerateSteps(args);
        var turtle = await GetOrCreateTurtle(turtleName);
        await CreateProgram(turtle.Id, programName, steps);
    }

    private async Task<int> CreateProgram(int id, string programName, List<string> steps)
    {
        var dbSteps = steps.Select(x => new Step { Action = x }).ToList();
        dbSteps[0].State = 1;
        var program = _context.Programs.Add(new TurtleProgram { TurtleId = id, Steps = dbSteps });
        await _context.SaveChangesAsync();
        return program.Entity.Id;
    }

    private async Task<Db.Turtle> GetOrCreateTurtle(string turtleName)
    {
        var turtle = await _context.Turtles.FirstOrDefaultAsync(x => x.Name == turtleName);
        if (turtle is null)
        {
            turtle = new Db.Turtle { Name = turtleName };
            _context.Turtles.Add(turtle);
            await _context.SaveChangesAsync();
        }
        return turtle;
    }

    public async Task<string?> GetNextMove(string turtleName)
    {
        var nextStep = await _context.Steps.FirstOrDefaultAsync(x => x.Program.IsCompleted == false && x.Program.Turtle.Name == turtleName && x.State == 1);
        return nextStep?.Action;
    }
}