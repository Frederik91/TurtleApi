using System;
using System.Collections.Generic;
using System.Linq;

namespace TurtleApi.Models;
public class Geometry
{
    public IReadOnlyCollection<Vector> Points => _points;
    private HashSet<Vector> _points = new();

    public Vector MinPoint => CalculateMinPoint();

    public Vector MaxPoint => CalculateMaxPoint();
    public double Volume => _points.Count;

    public void AddPoint(Vector point)
    {
        if (_points.Contains(point))
            return;

        _points.Add(point);
    }

    private Vector CalculateMinPoint()
    {
        if (_points.Count == 0)
            return Vector.Zero;

        var x = _points.Min(p => p.X);
        var y = _points.Min(p => p.Y);
        var z = _points.Min(p => p.Z);
        return new(x, y, z);
    }

    private Vector CalculateMaxPoint()
    {
        if (_points.Count == 0)
            return Vector.Zero;

        var x = _points.Max(p => p.X);
        var y = _points.Max(p => p.Y);
        var z = _points.Max(p => p.Z);
        return new(x, y, z);
    }
}
