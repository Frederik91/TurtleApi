using System;
using System.Collections.Generic;
using System.Linq;

namespace TurtleApi.Models;
public class Geometry
{
    public IReadOnlyCollection<Vector> Points => _points;
    private HashSet<Vector> _points = new();
    private Vector minPoint;
    private Vector maxPoint;
    private double volume;

    public Vector MinPoint => minPoint;
    public Vector MaxPoint => maxPoint;
    public double Volume => volume;

    public Geometry()
    {
        CalculateBoundingBox();
        CalculateVolume();
    }

    public void AddPoint(Vector point)
    {
        if (_points.Contains(point))
            return;
            
        _points.Add(point);
        Recalculate();
    }

    private void CalculateBoundingBox()
    {
        if (_points.Count == 0)
        {
            minPoint = Vector.Zero;
            maxPoint = Vector.Zero;
            return;
        }

        int minX = _points.Min(p => p.X);
        int minY = _points.Min(p => p.Y);
        int minZ = _points.Min(p => p.Z);
        int maxX = _points.Max(p => p.X);
        int maxY = _points.Max(p => p.Y);
        int maxZ = _points.Max(p => p.Z);

        minPoint = new Vector(minX, minY, minZ);
        maxPoint = new Vector(maxX, maxY, maxZ);
    }

    private void CalculateVolume()
    {
        volume = _points.Count;
    }

    private void Recalculate()
    {
        CalculateBoundingBox();
        CalculateVolume();
    }
}
