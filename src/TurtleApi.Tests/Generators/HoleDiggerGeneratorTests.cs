using TurtleApi.Services.Programs.Generators;

namespace TurtleApi.Tests.Generators.Tests;

public class HoleDiggerGeneratorTests
{
    [Fact]
    public void MoveTurtle_VerifyBackToOrigin()
    {
        var cut = new HoleDiggerGenerator();
        var args = new HoleDiggerGenerator.HoleDiggerArgs(10, 10, 2);
        var turtle = new TurtleApi.Models.Turtle();
        cut.MoveTurtle(turtle, args);

        Assert.Equal(0, turtle.Location.X);
        Assert.Equal(0, turtle.Location.Y);
        Assert.Equal(0, turtle.Location.Z);

        Assert.Equal(1, turtle.FacingDirection.X);
        Assert.Equal(0, turtle.FacingDirection.Y);

        Assert.Equal(9, turtle.ExcavatedGeometry.MaxPoint.X);
        Assert.Equal(0, turtle.ExcavatedGeometry.MaxPoint.Y);
        Assert.Equal(1, turtle.ExcavatedGeometry.MaxPoint.Z);

        Assert.Equal(0, turtle.ExcavatedGeometry.MinPoint.X);
        Assert.Equal(-9, turtle.ExcavatedGeometry.MinPoint.Y);
        Assert.Equal(-4, turtle.ExcavatedGeometry.MinPoint.Z);

        Assert.Equal(args.Length * args.Width * args.Layers * 3, turtle.ExcavatedGeometry.Volume);
    }
}