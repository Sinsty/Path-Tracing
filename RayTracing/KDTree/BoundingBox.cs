using System;
using System.Collections.Generic;

namespace RayTracing.Tree
{
    internal class BoundingBox
    {
        public Vector3f Max { get; private set; }
        public Vector3f Min { get; private set; }
        public List<Triangle> Triangles { get; private set; } = new List<Triangle>();
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

        public BoundingBox(Vector3f maxPoint, Vector3f minPoint, Triangle[] triangles)
        {
            maxPoint.x = MathF.Max(minPoint.x, maxPoint.x);
            maxPoint.y = MathF.Max(minPoint.y, maxPoint.y);
            maxPoint.z = MathF.Max(minPoint.z, maxPoint.z);

            Max = maxPoint;
            Min = minPoint;
            Triangles = new List<Triangle>(triangles);
            SurfaceArea = CalculateSurfaceArea(Max, Min);
        }

        public static float CalculateSurfaceArea(Vector3f max, Vector3f min)
        {
            float xzArea = (max.x - min.x) * (max.z - min.z);
            float yzArea = (max.y - min.y) * (max.z - min.z);
            float xyArea = (max.x - min.x) * (max.y - min.y);

            return 2 * (xzArea + yzArea + xyArea);
        }
    }
}
