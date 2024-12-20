using System;
using RayTracing.CameraRendering;
using RayTracing.ThreeDimensionalTree;

namespace RayTracing.Geometry
{
    internal class Sphere : IBoundingBoxable
    {
        public Vector3f Position { get; set; }
        public float Radius { get; private set; }
        public Material AppliedMaterial { get; set; }

        public Vector3f BoundingBoxMin { get; private set; }
        public Vector3f BoundingBoxMax { get; private set; }

        public Sphere(Vector3f position, float radius, Material material)
        {
            Position = position;
            Radius = radius;
            AppliedMaterial = material;

            BoundingBoxMax = Position + Vector3f.One * Radius;
            BoundingBoxMin = Position - Vector3f.One * Radius;
        }

        public bool RayIntersect(Ray ray, out HitInfo hit)
        {
            Vector3f oc = ray.origin - Position;
            float a = Vector3f.Dot(ray.direction, ray.direction);
            float b = 2 * Vector3f.Dot(oc, ray.direction);
            float c = Vector3f.Dot(oc, oc) - Radius * Radius;
            float discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                hit = new HitInfo();
                return false;
            }
            else
            {
                float t = (-b - MathF.Sqrt(discriminant)) / (2 * a);

                if (t < 0)
                {
                    hit = new HitInfo();
                    return false;
                }
                Vector3f hitPosition = ray.origin + ray.direction * t;
                Vector3f normal = hitPosition - Position;

                hit = new HitInfo(t, hitPosition, normal);

                return true;
            }
        }
    }
}
