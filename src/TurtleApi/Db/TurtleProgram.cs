namespace TurtleApi.Db;

public class TurtleProgram
{
    public int Id { get; set; }
    public bool IsCompleted { get; set; }
    public required int TurtleId { get; set; }
    public virtual Turtle Turtle { get; set; } = null!;
    public ICollection<Step> Steps { get; set; } = null!;
}