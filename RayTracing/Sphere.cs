using System;

namespace RayTracing
{
    internal class Sphere : CameraRenderObject
    {
        public override Vector3f Position { get; set; }
        public float Radius { get; private set; }
        public override Material AppliedMaterial { get; protected set; }

        public Sphere(Vector3f position, float radius, Material material)
        {
            Position = position;
            Radius = radius;
            AppliedMaterial = material;
        }

        public override bool RayIntersect(Ray ray, out HitInfo hit)
        {
            Vector3f oc = ray.origin - Position;
            float a = Vector3f.Dot(ray.direction, ray.direction);
            float b = 2 * Vector3f.Dot(oc, ray.direction);
            float c = Vector3f.Dot(oc, oc) - Radius * Radius;
            float discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                hit = new HitInfo(0, Vector3f.Zero, Vector3f.Zero);
                return false;
            }
            else
            {
                float distance = (-b - MathF.Sqrt(discriminant)) / (2 * a);

                Vector3f hitPosition = ray.origin + ray.direction * distance;
                Vector3f normal = hitPosition - Position;

                hit = new HitInfo(distance, hitPosition, normal);

                return true;
            }
        }
    }
}
