using TurtleApi.Models;

namespace TurtleApi.Tests.Models;

public class GeometryTests
{
    [Fact]
    public void AddExistingPoint_DoNotUpdateList()
    {
        var point1 = new Vector(1, 0, 0);
        var point2 = new Vector(1, 0, 0);
        var geometry = new Geometry();
        geometry.AddPoint(point1);
        geometry.AddPoint(point2);

        Assert.Single(geometry.Points);
    }
}