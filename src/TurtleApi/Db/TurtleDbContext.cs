using Microsoft.EntityFrameworkCore;

namespace TurtleApi.Db;

public class TurtleDbContext : DbContext
{
    public DbSet<Turtle> Turtles { get; set; } = null!;
    public DbSet<TurtleProgram> Programs { get; set; } = null!;
    public DbSet<Step> Steps { get; set; } = null!;

    public TurtleDbContext(DbContextOptions<TurtleDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Turtle>()
            .HasKey(x => x.Id);

        modelBuilder
            .Entity<Turtle>()
            .HasMany<TurtleProgram>();

        modelBuilder
            .Entity<TurtleProgram>()
            .HasOne<Turtle>()
            .WithMany(x => x.Programs)
            .HasForeignKey(x => x.TurtleId);

        modelBuilder
            .Entity<TurtleProgram>()
            .HasMany<Step>();

        modelBuilder
            .Entity<TurtleProgram>()
            .HasKey(x => x.Id);

        modelBuilder
            .Entity<Step>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Step>()
            .HasIndex(x => x.State);
    }
}