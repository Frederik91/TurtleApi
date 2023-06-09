using Microsoft.EntityFrameworkCore;

namespace TurtleApi.Db;

public class TurtleDbContext : DbContext
{
    public DbSet<Turtle> Turtles { get; set; } = null!;
    public DbSet<Program> Programs { get; set; } = null!;
    public DbSet<Step> Steps { get; set; } = null!;

    public TurtleDbContext(DbContextOptions<TurtleDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Turtle>(x =>
        {
            x.HasMany(t => t.Programs)
            .WithOne(p => p.Turtle)
            .HasForeignKey(p => p.TurtleId);

            x.HasKey(t => t.Id);
        });


        modelBuilder.Entity<Program>(x =>
        {
            x.HasMany(p => p.Steps)
            .WithOne(s => s.Program)
            .HasForeignKey(s => s.ProgramId);

            x.HasKey(p => p.Id);
        });

        modelBuilder.Entity<Step>(x =>
        {
            x.HasKey(s => s.Id);
            x.HasIndex(s => s.State);
        });
    }
}