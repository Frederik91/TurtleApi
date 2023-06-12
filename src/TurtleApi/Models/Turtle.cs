namespace TurtleApi.Models;

public class Turtle
{
    private static readonly string LUA_PREFIX = "turtle.";
    private static readonly string CUSTOM_PREFIX = "movements.";
    private List<string> movements = new List<string>();

    public Vector FacingDirection { get; private set; } = new(1, 0, 0);
    public Vector Location { get; private set; } = new(0, 0, 0);

    public Geometry ExcavatedGeometry { get; } = new();
    public Geometry InspectedGeometry { get; } = new();

    public Turtle()
    {
        AddExcavatedLocation(Location);
    }

    private void AddExcavatedLocation(Vector location)
    {
        ExcavatedGeometry.AddPoint(location);
        InspectedGeometry.AddPoint(location);
    }

    private void AddInspectedGeometry(Vector location)
    {
        InspectedGeometry.AddPoint(location);
    }

    private void AddMovement(string movement)
    {
        movements.Add(movement + "()");
    }

    public void Up(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            AddMovement(CUSTOM_PREFIX + "up");
            Location += new Vector(0, 0, 1);
            AddExcavatedLocation(Location);
        }
    }

    public void Down(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            AddMovement(CUSTOM_PREFIX + "down");
            Location -= new Vector(0, 0, 1);
            AddExcavatedLocation(Location);
        }
    }

    public void Forward(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            AddMovement(CUSTOM_PREFIX + "forward");
            Location += FacingDirection;
            AddExcavatedLocation(Location);
        }
    }

    public void Back(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            AddMovement(LUA_PREFIX + "back");
            Location -= FacingDirection;
            AddExcavatedLocation(Location);
        }
    }

    public void TurnLeft()
    {
        AddMovement(LUA_PREFIX + "turnLeft");
        FacingDirection = FacingDirection.RotateLeft();
    }

    public void TurnRight()
    {
        AddMovement(LUA_PREFIX + "turnRight");
        FacingDirection = FacingDirection.RotateRight();
    }

    public void Dig()
    {
        AddMovement(LUA_PREFIX + "dig");
        AddExcavatedLocation(Location + FacingDirection);
    }

    public void DigUp()
    {
        AddMovement(CUSTOM_PREFIX + "digUp");
        AddExcavatedLocation(Location + new Vector(0, 0, 1));
    }

    public void DigDown()
    {
        AddMovement(CUSTOM_PREFIX + "digDown");
        AddExcavatedLocation(Location - new Vector(0, 0, 1));
    }

    public void TurnAround()
    {
        AddMovement(CUSTOM_PREFIX + "turnAround");
        FacingDirection = FacingDirection.Flip();
    }
    public void CheckAndDumpInventory() => AddMovement(CUSTOM_PREFIX + "checkAndDumpInventory");
    public void RefuelFromInventory() => AddMovement(CUSTOM_PREFIX + "refuelFromInventory");
    public void PlaceIntoChest() => AddMovement(CUSTOM_PREFIX + "placeIntoChest");

    public List<string> GetMovements()
    {
        return movements;
    }

    internal void DigUpWhenValuable()
    {
        AddMovement(CUSTOM_PREFIX + "digUpWhenValuable");
        AddInspectedGeometry(Location + new Vector(0, 0, 1));
    }

    internal void DigDownWhenValuable()
    {
        AddMovement(CUSTOM_PREFIX + "digDownWhenValuable");
        AddInspectedGeometry(Location - new Vector(0, 0, 1));
    }
}
