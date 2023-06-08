namespace TurtleApi.Services.Programs.Requests;

public record GenerateProgramRequest(string TurtleName, string ProgramName, string[] args);