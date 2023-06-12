using TurtleApi.Services.Programs.Generators;

namespace TurtleApi.Tests.Generators.Tests;

public class ValueDiggerGeneratorTests
{
    [Theory]
    [InlineData(2, 2, 2)]
    [InlineData(10, 10, 2)]
    [InlineData(32, 10, 2)]
    [InlineData(20, 32, 7)]
    public void MoveTurtle_VerifyBackToOrigin(int length, int width, int layers)
    {
        var cut = new ValueDiggerGenerator();
        var args = new ValueDiggerGenerator.ValueDiggerArgs(length, width, layers);
        var turtle = new TurtleApi.Models.Turtle();
        cut.MoveTurtle(turtle, args);

        Assert.Equal(0, turtle.Location.X);
        Assert.Equal(0, turtle.Location.Y);
        Assert.Equal(0, turtle.Location.Z);

        Assert.Equal(1, turtle.FacingDirection.X);
        Assert.Equal(0, turtle.FacingDirection.Y);

        Assert.Equal(length - 1, turtle.ExcavatedGeometry.MaxPoint.X);
        Assert.Equal(0, turtle.ExcavatedGeometry.MaxPoint.Y);
        Assert.Equal(0, turtle.ExcavatedGeometry.MaxPoint.Z);

        Assert.Equal(0, turtle.ExcavatedGeometry.MinPoint.X);
        Assert.Equal(-width + 1, turtle.ExcavatedGeometry.MinPoint.Y);
        Assert.Equal(-3 * (layers - 1), turtle.ExcavatedGeometry.MinPoint.Z);

        Assert.Equal(args.Length * args.Width * args.Layers + ((args.Layers - 1) * 2), turtle.ExcavatedGeometry.Volume);
        
        var max = turtle.InspectedGeometry.MaxPoint;
        var min = turtle.InspectedGeometry.MinPoint;
        var l = max.X - min.X + 1;
        var w = max.Y - min.Y + 1;
        var h = max.Z - min.Z + 1;

        var vol = l * w * h;
        Assert.Equal(vol, turtle.InspectedGeometry.Volume);
    }
}