namespace TurtleApi.Models;

public class Turtle
{
    private static readonly string LUA_PREFIX = "turtle.";
    private static readonly string CUSTOM_PREFIX = "custom.";
    private List<string> movements = new List<string>();

    public Vector FacingDirection { get; private set; } = new(1, 0, 0);
    public Vector Location { get; private set; } = new(0, 0, 0);

    public Geometry ExcavatedGeometry { get; } = new();

    private void AddExcavatedLocation(Vector location)
    {
        ExcavatedGeometry.AddPoint(location);
    }

    public void Up(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            movements.Add(CUSTOM_PREFIX + "up");
            Location += new Vector(0, 0, 1);
            AddExcavatedLocation(Location);
        }
    }

    public void Down(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            movements.Add(CUSTOM_PREFIX + "down");
            Location -= new Vector(0, 0, 1);
            AddExcavatedLocation(Location);
        }
    }

    public void Forward(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            movements.Add(CUSTOM_PREFIX + "forward");
            Location += FacingDirection;
            AddExcavatedLocation(Location);
        }
    }

    public void Back(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            movements.Add(LUA_PREFIX + "back");
            Location -= FacingDirection;
            AddExcavatedLocation(Location);
        }
    }

    public void TurnLeft()
    {
        movements.Add(LUA_PREFIX + "turnLeft");
        FacingDirection = FacingDirection.RotateLeft();
    }

    public void TurnRight()
    {
        movements.Add(LUA_PREFIX + "turnRight");
        FacingDirection = FacingDirection.RotateRight();
    }

    public void Dig()
    {
        movements.Add(LUA_PREFIX + "dig");
        AddExcavatedLocation(Location + FacingDirection);
    }

    public void DigUp()
    {
        movements.Add(CUSTOM_PREFIX + "digUp");
        AddExcavatedLocation(Location + new Vector(0, 0, 1));
    }

    public void DigDown()
    {
        movements.Add(CUSTOM_PREFIX + "digDown");
        AddExcavatedLocation(Location - new Vector(0, 0, 1));
    }

    public void TurnAround()
    {
        movements.Add(CUSTOM_PREFIX + "turnAround");
        FacingDirection = FacingDirection.Flip();
    }
    public void CheckAndDumpInventory() => movements.Add(CUSTOM_PREFIX + "checkAndDumpInventory");
    public void RefuelFromInventory() => movements.Add(CUSTOM_PREFIX + "refuelFromInventory");
    public void PlaceIntoChest() => movements.Add(CUSTOM_PREFIX + "placeIntoChest");

    public List<string> GetMovements()
    {
        return movements;
    }

    public void ResetMovements()
    {
        movements.Clear();
    }
}
