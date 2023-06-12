using TurtleApi.Models;

namespace TurtleApi.Services.Programs.Generators;


internal sealed class ValueDiggerGenerator : IProgramGenerator
{
    internal sealed record ValueDiggerArgs(int Length, int Width, int Layers)
    {
        internal static ValueDiggerArgs Parse(string[] args)
        {
            if (args.Length != 3)
                throw new InvalidDataException("args must contain exactly three arguments, Length, Width and Layers");
            if (!int.TryParse(args[0], out var length))
                throw new InvalidDataException("Could not parse length as an integer");
            if (!int.TryParse(args[1], out var width))
                throw new InvalidDataException("Could not parse width as an integer");
            if (width % 2 != 0)
                throw new InvalidDataException("Width must be divisible by two");

            if (!int.TryParse(args[2], out var layers))
                throw new InvalidDataException("Could not parse length as an integer");
            return new(length, width, layers);
        }
    }


    public string ProgramName => "ValueDigger";

    public List<string> GenerateSteps(string[]? args)
    {
        if (args is null)
            throw new InvalidDataException("The ValueDigger program required a Length, Width and Layers argument. No arguments were provided");

        var valueDiggerArgs = ValueDiggerArgs.Parse(args);

        var turtle = new Models.Turtle();
        MoveTurtle(turtle, valueDiggerArgs);
        return turtle.GetMovements();
    }

    internal void MoveTurtle(Models.Turtle turtle, ValueDiggerArgs args)
    {
        for (var i = 1; i <= args.Layers; i++)
        {
            MineLayer(turtle, args);
            turtle.Up(-turtle.Location.Z);
            turtle.TurnAround();
            turtle.PlaceIntoChest();
            turtle.TurnAround();
            if (i < args.Layers)
                turtle.Down(i * 3);
        }
    }

    private void MineForward(Models.Turtle turtle, int length = 1)
    {
        for (var i = 0; i < length; i++)
        {
            turtle.DigUpWhenValuable();
            turtle.DigDownWhenValuable();
            turtle.Forward();
        }
    }

    private void MineLayer(Models.Turtle turtle, ValueDiggerArgs args)
    {
        var iterations = args.Width / 2;
        for (var i = 1; i <= iterations; i++)
        {
            MineForward(turtle, args.Length - 1);
            turtle.TurnLeft();
            MineForward(turtle);
            turtle.TurnLeft();
            MineForward(turtle, args.Length - 1);
            if (i == iterations)
                break;

            turtle.TurnRight();
            MineForward(turtle);
            turtle.TurnRight();
        }

        // Return to start position
        turtle.TurnLeft();
        MineForward(turtle, args.Width - 1);
        turtle.TurnLeft();
    }
}