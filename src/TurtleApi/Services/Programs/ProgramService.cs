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
    List<string> GetProgramsTypes();
    Task<ProgramResponses.Program[]> GetTurtlePrograms(int turtleId);
    Task CancelProgram(int programId);
    Task<ProgramResponses.Program[]> GetAllPrograms();
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
        var nextStep = await _context.Steps.FirstOrDefaultAsync(x => x.Program.IsCompleted == false && x.Program.Turtle.Name == turtleName && x.State == 0);
        if (nextStep is null)
            return null;

        await _context.Steps.Where(x => x.Id == nextStep.Id).ExecuteUpdateAsync(x => x.SetProperty(y => y.State, 1));
        return nextStep.Action;
    }

    public List<string> GetProgramsTypes()
    {
        return _generators.Select(x => x.Key).ToList();
    }

    public async Task<ProgramResponses.Program[]> GetTurtlePrograms(int turtleId)
    {
        var q = _context.Programs
            .Where(x => x.TurtleId == turtleId);

        return await MapProgram(q).ToArrayAsync();
    }

    private static IQueryable<ProgramResponses.Program> MapProgram(IQueryable<Db.Program> q)
    {
        return q
            .Include(x => x.Steps)
            .Include(x => x.Turtle)
            .Select(x => new ProgramResponses.Program(x.Id, x.Turtle.Name, x.IsCompleted, x.Steps.Count(), x.Steps.Count(x => x.State > 0)));
    }


    public async Task CancelProgram(int programId)
    {
        await _context.Programs.Where(x => x.Id == programId).ExecuteUpdateAsync(x => x.SetProperty(y => y.IsCompleted, true));
        await _context.Steps.Where(x => x.ProgramId == programId & x.State == 0).ExecuteUpdateAsync(x => x.SetProperty(y => y.State, 10));
    }

    public async Task<ProgramResponses.Program[]> GetAllPrograms()
    {
        return await MapProgram(_context.Programs).ToArrayAsync();
    }
}