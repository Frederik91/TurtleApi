namespace TurtleApi.Models;

public class Turtle
{
    private static readonly string LUA_PREFIX = "turtle.";
    private static readonly string CUSTOM_PREFIX = "custom.";
    private List<string> movements = new List<string>();

    public Vector FacingDirection { get; private set; } = new(1, 0, 0);
    public Vector Offset { get; private set; } = new(0, 0, 0);

    public void Up(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            movements.Add(CUSTOM_PREFIX + "up");
            Offset += new Vector(0, 0, 1);
        }
    }

    public void Down(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            movements.Add(CUSTOM_PREFIX + "down");
            Offset -= new Vector(0, 0, -1);
        }
    }

    public void Forward(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            movements.Add(CUSTOM_PREFIX + "forward");
            Offset += FacingDirection;
        }
    }

    public void Back(int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            movements.Add(LUA_PREFIX + "back");
            Offset -= FacingDirection;
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

    public void Dig() => movements.Add(LUA_PREFIX + "dig");

    public void DigUp() => movements.Add(CUSTOM_PREFIX + "digUp");

    public void DigDown() => movements.Add(CUSTOM_PREFIX + "digDown");

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
