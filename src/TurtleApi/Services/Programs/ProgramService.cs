using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TurtleApi.Db;
using TurtleApi.Services.Programs.Generators;
using TurtleApi.Services.Programs.Requests;

namespace TurtleApi.Services.Programs;

public interface IProgramService
{
    Task Generate(GenerateProgramRequest request);
    Task<string?> GetNextMove(string turtleId);
    List<string> GetPrograms();
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

    public async Task Generate(GenerateProgramRequest request)
    {
        var generator = _generators[request.ProgramName];
        var steps = generator.GenerateSteps(request.args);
        var turtle = await GetOrCreateTurtle(request.TurtleName);
        await CreateProgram(turtle.Id, request.ProgramName, steps);
    }

    private async Task<int> CreateProgram(int id, string programName, List<string> steps)
    {
        var dbSteps = steps.Select(x => new Step { Action = x }).ToList();
        dbSteps[0].State = 1;
        var program = _context.Programs.Add(new Db.Program { TurtleId = id, Steps = dbSteps });
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

    public List<string> GetPrograms()
    {
        return _generators.Select(x => x.Key).ToList();
    }
}