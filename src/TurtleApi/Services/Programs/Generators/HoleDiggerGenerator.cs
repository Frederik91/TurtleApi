using TurtleApi.Models;

namespace TurtleApi.Services.Programs.Generators;


internal sealed class HoleDiggerGenerator : IProgramGenerator
{
    internal sealed record HoleDiggerArgs(int Length, int Width, int Layers)
    {
        internal static HoleDiggerArgs Parse(string[] args)
        {
            if (args.Length != 3)
                throw new InvalidDataException("args must contain exactly three arguments, Length, Width and Layers");
            if (!int.TryParse(args[0], out var length))
                throw new InvalidDataException("Could not parse length as an integer");
            if (!int.TryParse(args[1], out var width))
                throw new InvalidDataException("Could not parse width as an integer");
            if (width % 2 == 0)
                throw new InvalidDataException("Width must be divisible by two");

            if (!int.TryParse(args[2], out var layers))
                throw new InvalidDataException("Could not parse length as an integer");
            return new(length, width, layers);
        }
    }


    public string ProgramName => "HoldeDigger";

    public List<string> GenerateSteps(string[]? args)
    {
        if (args is null)
            throw new InvalidDataException("The HoleDigger program required a Length, Width and Layers argument. No arguments were provided");

        var holdeDiggerArgs = HoleDiggerArgs.Parse(args);

        var turtle = new Models.Turtle();
        MoveTurtle(turtle, holdeDiggerArgs);
        return turtle.GetMovements();
    }

    internal void MoveTurtle(Models.Turtle turtle, HoleDiggerArgs holdeDiggerArgs)
    {
        for (var i = 0; i < holdeDiggerArgs.Layers; i++)
        {
            MineLayer(turtle, holdeDiggerArgs);
            turtle.Up(turtle.Offset.Z);
            turtle.TurnAround();
            turtle.PlaceIntoChest();
            turtle.TurnAround();
            if (i < holdeDiggerArgs.Layers)
                turtle.Down(i * 3);
        }
    }

    private void MineForward(Models.Turtle turtle, int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            turtle.DigUp();
            turtle.DigDown();
            turtle.Forward();
        }
    }

    private void MineLayer(Models.Turtle turtle, HoleDiggerArgs args)
    {
        for (int i = 0; i < args.Width / 2; i++)
        {
            MineForward(turtle, args.Length);
            turtle.TurnLeft();
            MineForward(turtle);
            turtle.TurnLeft();
            MineForward(turtle, args.Length);
            turtle.TurnRight();
            MineForward(turtle);
            turtle.TurnRight();
        }

        // Return to start position
        turtle.TurnRight();
        MineForward(turtle, args.Width);
        turtle.TurnLeft();
    }
}