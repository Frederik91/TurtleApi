using TurtleApi.Models;

namespace TurtleApi.Tests;

public class TurtleTests
{
    [Fact]
    public void Forward_Should_UpdateOffset()
    {
        // Arrange
        var turtle = new Turtle();
        var expectedOffset = new Vector(1, 0, 0);

        // Act
        turtle.Forward();

        // Assert
        Assert.Equal(expectedOffset, turtle.Offset);
    }

    [Theory]
    [InlineData(1, 0, -1)]
    [InlineData(2, -1, 0)]
    [InlineData(3, 0, 1)]
    [InlineData(4, 1, 0)]
    public void TurnLeft_Should_UpdateFacingDirection(int turns, int x, int y)
    {
        // Arrange
        var turtle = new Turtle();
        var expectedFacingDirection = new Vector(x, y, 0);

        // Act
        for (int i = 0; i < turns; i++)
        {
            turtle.TurnLeft();
        }

        // Assert
        Assert.Equal(expectedFacingDirection, turtle.FacingDirection);
    }

    [Theory]
    [InlineData(1, 0, 1)]
    [InlineData(2, -1, 0)]
    [InlineData(3, 0, -1)]
    [InlineData(4, 1, 0)]
    public void TurnRight_Should_UpdateFacingDirection(int turns, int x, int y)
    {
        // Arrange
        var turtle = new Turtle();
        var expectedFacingDirection = new Vector(x, y, 0);

        // Act
        for (int i = 0; i < turns; i++)
        {
            turtle.TurnRight();
        }

        // Assert
        Assert.Equal(expectedFacingDirection, turtle.FacingDirection);
    }

    [Fact]
    public void Dig_Should_AddToMovementsList()
    {
        // Arrange
        var turtle = new Turtle();
        List<string> expectedMovements = new List<string> { "turtle.dig" };

        // Act
        turtle.Dig();

        // Assert
        Assert.Equal(expectedMovements, turtle.GetMovements());
    }
}