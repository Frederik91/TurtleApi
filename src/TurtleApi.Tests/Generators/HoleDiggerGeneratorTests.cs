using TurtleApi.Services.Programs.Generators;

namespace TurtleApi.Tests.Generators.Tests;

public class HoleDiggerGeneratorTests
{
    [Fact]
    public void MoveTurtle_VerifyBackToOrigin()
    {
        var cut = new HoleDiggerGenerator();
        var args = new HoleDiggerGenerator.HoleDiggerArgs(10, 10, 3);
        var turtle = new Models.Turtle();
        cut.MoveTurtle(turtle, args);

        Assert.Equal(0, turtle.Offset.X);
        Assert.Equal(0, turtle.Offset.Y);
        Assert.Equal(0, turtle.Offset.Z);

        Assert.Equal(1, turtle.FacingDirection.X);
        Assert.Equal(0, turtle.FacingDirection.Y);
    }
}