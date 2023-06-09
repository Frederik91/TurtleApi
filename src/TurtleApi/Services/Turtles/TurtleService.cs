using Microsoft.EntityFrameworkCore;
using TurtleApi.Db;

namespace TurtleApi.Services.Turtles;

public interface ITurtleService
{
    Task<TurtleResponses.Turtle[]> GetAllTurtles(CancellationToken cancellationToken);
}

internal sealed class TurtleService : ITurtleService
{
    private readonly TurtleDbContext _dbContext;

    public TurtleService(TurtleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TurtleResponses.Turtle[]> GetAllTurtles(CancellationToken cancellationToken)
    {
        return await _dbContext.Turtles
            .Select(x => new TurtleResponses.Turtle(x.Id, x.Name))
            .ToArrayAsync();
    }
}