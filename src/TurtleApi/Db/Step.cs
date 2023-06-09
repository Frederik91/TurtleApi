namespace TurtleApi.Db;

public class Step
{
    public int Id { get; set; }
    public required string Action { get; set; }
    public int State { get; set; }
    public int ProgramId { get; set; }
    public virtual Program Program { get; set; } = null!;
}