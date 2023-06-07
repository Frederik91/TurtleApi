using Microsoft.EntityFrameworkCore;
using TurtleApi;
using TurtleApi.Db;
using TurtleApi.Services.Programs;
using TurtleApi.Services.Programs.Generators;

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .MapGet("generate/{turtleName}/{program}", Generate)
    .WithOpenApi();

Task Generate(IProgramService service, string turtleName, string program, string[]? args = null)
{
    return service.Generate(turtleName, program, args);
}

app
    .MapGet("command/{turtleName}", (string turtleName, IProgramService service) => service.GetNextMove(turtleName))
    .WithOpenApi();

app.Run();
