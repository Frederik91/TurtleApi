using Microsoft.EntityFrameworkCore;
using TurtleApi.Db;
using TurtleApi.Services.Programs;
using TurtleApi.Services.Programs.Generators;
using TurtleApi.Services.Programs.Requests;
using TurtleApi.Services.Turtles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IProgramService, ProgramService>();
builder.Services.AddTransient<IProgramGenerator, HoleDiggerGenerator>();
builder.Services.AddTransient<ITurtleService, TurtleService>();

builder.Services.AddDbContext<TurtleDbContext>(c =>
{
    c.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<TurtleDbContext>();
dbContext.Database.EnsureCreated();
dbContext.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();

var programs = app.MapGroup("Program");

programs
    .MapGet("types", (IProgramService service) => Results.Ok(service.GetProgramsTypes()))
    .WithDescription("Get all program types")
    .WithOpenApi(operation => new(operation)
    {
        Summary = "Get all program types",
    });

programs
    .MapGet("", async (IProgramService service) => Results.Ok(await service.GetAllPrograms()))
    .WithDescription("Get all programs")
    .WithOpenApi(operation => new(operation)
    {
        Summary = "Get all programs",
    });

programs
    .MapGet("turtle/{turtleId}", async (IProgramService service, int turtleId) => Results.Ok(await service.GetTurtlePrograms(turtleId)))
    .WithDescription("Get all programs for a turtle")
    .WithOpenApi(operation => new(operation)
    {
        Summary = "Get all programs for a turtle",
    });

programs
    .MapPost("Generate", (IProgramService service, GenerateProgramRequest request) => service.Generate(request))
    .WithDescription("Generate a program for a turtle")
    .WithOpenApi(operation => new(operation)
    {
        Summary = "Generate a program for a turtle",
    });

programs
    .MapPost("{int}/Cancel", (IProgramService service, int programId) => service.CancelProgram(programId))
    .WithDescription("Cancel all remaining steps for a program")
    .WithOpenApi(operation => new(operation)
    {
        Summary = "Cancel all remaining steps for a program",
    });


var turtle = app.MapGroup("Turtle");

turtle
    .MapGet("", async (ITurtleService service, CancellationToken CancellationToken) => Results.Ok(await service.GetAllTurtles(CancellationToken)))
    .WithDescription("Get all turtles")
    .WithOpenApi(operation => new(operation)
    {
        Summary = "Get all turtles",
    });

var steps = app.MapGroup("Steps");

steps
    .MapGet("/Next/{turtleName}", async (string turtleName, IProgramService service) =>
    {
        var step = await service.GetNextMove(turtleName);
        return step is null ? Results.NoContent() : Results.Ok(step);
    })
    .WithDescription("Get the next step for turtle by name")
    .WithOpenApi(operation => new(operation)
    {
        Summary = "Get the next step for turtle by name",
    });

app.Run();
