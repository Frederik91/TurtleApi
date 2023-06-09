namespace TurtleApi.Db;

public class Program
{
    public int Id { get; set; }
    public bool IsCompleted { get; set; }
    public required int TurtleId { get; set; }
    public virtual Turtle Turtle { get; set; } = null!;
    public virtual ICollection<Step> Steps { get; set; } = null!;
}