namespace TurtleApi.Services.Programs.Requests;

public record GenerateProgramRequest(string TurtleName, string ProgramName, string[] args);
public record GetTurtleProgramsRequest(string TurtleName);
public record CancelProgramRequest(string programId);