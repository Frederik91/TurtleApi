namespace TurtleApi.Models;

public struct Vector
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Vector(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector Zero => new Vector(0, 0, 0);

    public static Vector operator +(Vector vector1, Vector vector2)
    {
        int newX = vector1.X + vector2.X;
        int newY = vector1.Y + vector2.Y;
        int newZ = vector1.Z + vector2.Z;
        return new Vector(newX, newY, newZ);
    }

    public static Vector operator -(Vector vector1, Vector vector2)
    {
        int newX = vector1.X - vector2.X;
        int newY = vector1.Y - vector2.Y;
        int newZ = vector1.Z - vector2.Z;
        return new Vector(newX, newY, newZ);
    }

    public Vector RotateLeft()
    {
        return new Vector(Y, -X, Z);
    }

    public Vector RotateRight()
    {
        return new Vector(-Y, X, Z);
    }

    public Vector Flip()
    {
        return new Vector(-X, -Y, -Z);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}