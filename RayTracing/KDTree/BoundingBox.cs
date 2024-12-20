using RayTracing;
using System;
using System.Collections.Generic;
using System.Diagnostics;

internal class BoundingBox
{
    public Vector3f Max { get; private set; }
    public Vector3f Min { get; private set; }
    public List<IBoundingBoxable> Boxables { get; private set; } = new List<IBoundingBoxable>();
    public float SurfaceArea { get; private set; }

    public BoundingBox(Vector3f maxPoint, Vector3f minPoint)
    {
        maxPoint.x = MathF.Max(minPoint.x, maxPoint.x);
        maxPoint.y = MathF.Max(minPoint.y, maxPoint.y);
        maxPoint.z = MathF.Max(minPoint.z, maxPoint.z);

        Max = maxPoint;
        Min = minPoint;
        SurfaceArea = CalculateSurfaceArea(Max, Min);
    }

    public BoundingBox(Vector3f maxPoint, Vector3f minPoint, IBoundingBoxable[] boxables)
    {
        maxPoint.x = MathF.Max(minPoint.x, maxPoint.x);
        maxPoint.y = MathF.Max(minPoint.y, maxPoint.y);
        maxPoint.z = MathF.Max(minPoint.z, maxPoint.z);

        Max = maxPoint;
        Min = minPoint;
        Boxables = new List<IBoundingBoxable>(boxables);
        SurfaceArea = CalculateSurfaceArea(Max, Min);
    }

    public static float CalculateSurfaceArea(Vector3f max, Vector3f min)
    {
        float xzArea = (max.x - min.x) * (max.z - min.z);
        float yzArea = (max.y - min.y) * (max.z - min.z);
        float xyArea = (max.x - min.x) * (max.y - min.y);

        return 2 * (xzArea + yzArea + xyArea);
    }

    public static BoundingBox CreateAroundObjects(IBoundingBoxable[] boxables)
    {
        Vector3f max = Vector3f.One * float.MinValue;
        Vector3f min = Vector3f.One * float.MaxValue;

        foreach (IBoundingBoxable boxable in boxables)
        {
            max.x = MathF.Max(boxable.BoundingBoxMax.x, max.x);
            max.y = MathF.Max(boxable.BoundingBoxMax.y, max.y);
            max.z = MathF.Max(boxable.BoundingBoxMax.z, max.z);

            min.x = MathF.Min(boxable.BoundingBoxMin.x, min.x);
            min.y = MathF.Min(boxable.BoundingBoxMin.y, min.y);
            min.z = MathF.Min(boxable.BoundingBoxMin.z, min.z);
        }

        return new BoundingBox(max, min, boxables);
    }

    public bool IsTriangleFullInside(Triangle triangle)
    {
        return IsPointInside(triangle.point1) && IsPointInside(triangle.point2) && IsPointInside(triangle.point3);
    }

    public bool IsPointInside(Vector3f point)
    {
        return point.x >= Min.x && point.y >= Min.y && point.z >= Min.z &&
               point.x <= Max.x && point.y <= Max.y && point.z <= Max.z;
    }

    public bool IsRayIntersect(Ray ray)
    {
        float tMin = (Min.x - ray.origin.x) / ray.direction.x;
        float tMax = (Max.x - ray.origin.x) / ray.direction.x;

        if (tMin > tMax)
            (tMax, tMin) = (tMin, tMax);

        float tYMin = (Min.y - ray.origin.y) / ray.direction.y;
        float tYMax = (Max.y - ray.origin.y) / ray.direction.y;

        if (tYMin > tYMax)
            (tYMax, tYMin) = (tYMin, tYMax);

        if (tMin > tYMax || tYMin > tMax)
            return false;

        if (tYMin > tMin) tMin = tYMin;
        if (tYMax < tMax) tMax = tYMax;

        float tZMin = (Min.z - ray.origin.z) / ray.direction.z;
        float tZMax = (Max.z - ray.origin.z) / ray.direction.z;

        if (tZMin > tZMax)
            (tZMax, tZMin) = (tZMin, tZMax);

        if (tMin > tZMax || tZMin > tMax)
            return false;

        if (tZMin > tMin) tMin = tZMin;
        if (tZMax < tMax) tMax = tZMax;

        if (tMax > 0 || tMin > 0)
            return true;
        else
            return false;
    }
}
