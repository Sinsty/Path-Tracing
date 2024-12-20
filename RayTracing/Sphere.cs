using System;

namespace RayTracing
{
    internal class Sphere : ICameraRenderObject
    {
        public Vector3f Position { get; set; }
        public float Radius { get; private set; }
        public Material AppliedMaterial { get; set; }

        public Sphere(Vector3f position, float radius, Material material)
        {
            Position = position;
            Radius = radius;
            AppliedMaterial = material;
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
