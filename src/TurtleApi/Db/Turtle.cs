namespace TurtleApi.Db;

public class Turtle
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Program> Programs { get; set; } = null!;
}