using Microsoft.EntityFrameworkCore;

namespace TurtleApi;

public class TurtleDbContext : DbContext
{
    public TurtleDbContext(DbContextOptions<TurtleDbContext> options) : base(options)
    {

    }
}