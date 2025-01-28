using PathTracing.CameraRendering;
using System.Diagnostics;

namespace PathTracing.Geometry
{
    internal class Mesh : ICompositeObject, ICameraRenderObject
    {
        public Material AppliedMaterial { get; set; }
        public Vector3f Position { get; set; }
        public Triangle[] Triangles { get; private set; }

        public Mesh(Vector3f position, string pathToObjModel, Material material)
        {
            Position = position;
            AppliedMaterial = material;

            (Vector3f[] vertices, int[] faces) = ObjReader.Parse(pathToObjModel);

            int trianglesCount = faces.Length / 3;
            Triangles = new Triangle[trianglesCount];

            for (int i = 0; i < faces.Length - 2; i += 3)
            {
                Vector3f v0 = vertices[faces[i + 0] - 1];
                Vector3f v1 = vertices[faces[i + 1] - 1];
                Vector3f v2 = vertices[faces[i + 2] - 1];

                int triangleIndex = i / 3;
                Triangles[triangleIndex] = new Triangle(Position, [v0, v1, v2], AppliedMaterial);
            }
        }

        public bool RayIntersect(Ray ray, out HitInfo hit)
        {
            hit = new HitInfo(float.MaxValue);
            Triangle closestTriangle = null;
            foreach (Triangle triangle in Triangles)
            {
                if (triangle.RayIntersect(ray, out HitInfo objectHit))
                {
                    if (objectHit.Distance < hit.Distance)
                    {
                        closestTriangle = triangle;
                        hit = objectHit;
                    }
                }
            }

            if (closestTriangle != null)
            {
                return true;
            }
            else
            {
                hit = new HitInfo();
                return false;
            }
        }

        public ICameraRenderObject[] GetObjects()
        {
            return Triangles;
        }
    }
}
