using Microsoft.EntityFrameworkCore;
using TurtleApi;
using TurtleApi.Db;
using TurtleApi.Services.Programs;
using TurtleApi.Services.Programs.Generators;
using TurtleApi.Services.Programs.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IProgramService, ProgramService>();
builder.Services.AddTransient<IProgramGenerator, HoleDiggerGenerator>();

builder.Services.AddDbContext<TurtleDbContext>(c =>
{
    c.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<TurtleDbContext>();
dbContext.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var programs = app.MapGroup("Programs");

programs
    .MapGet("", (IProgramService service) => service.GetPrograms())
    .WithOpenApi();

programs
    .MapPost("Programs/Generate", Generate)
    .WithOpenApi();

Task Generate(IProgramService service,  GenerateProgramRequest request)
{
    return service.Generate(request);
}

app
    .MapGet("command/{turtleName}", (string turtleName, IProgramService service) => service.GetNextMove(turtleName))
    .WithOpenApi();

app.Run();
