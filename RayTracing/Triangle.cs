using System;

namespace RayTracing
{
    internal class Triangle : CameraRenderObject
    {
        public override Vector3f Position { get; set; }
        public override Material AppliedMaterial { get; protected set; }

        private Vector3f v0, v1, v2;

        public Triangle(Vector3f position, Vector3f[] vertices, Material material)
        {
            Position = position;
            AppliedMaterial = material;

            v0 = vertices[0];
            v1 = vertices[1];
            v2 = vertices[2];
        }

        public override bool RayIntersect(Ray ray, out HitInfo hit)
        {

            throw new NotImplementedException();
        }
    }
}
