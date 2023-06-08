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
        modelBuilder.Entity<Turtle>()
            .HasKey(x => x.Id);

        modelBuilder
            .Entity<Program>()
            .HasKey(x => x.Id);

        modelBuilder
            .Entity<Program>()
            .HasOne<Turtle>()
            .WithMany(x => x.Programs)
            .HasForeignKey(x => x.TurtleId);

        modelBuilder
            .Entity<Step>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Step>()
            .HasIndex(x => x.State);

        modelBuilder
            .Entity<Step>()
            .HasOne<Program>()
            .WithMany(x => x.Steps)
            .HasForeignKey(x => x.ProgramId);
    }
}