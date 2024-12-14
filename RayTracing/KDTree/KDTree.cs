using System;
using System.Diagnostics;
namespace RayTracing.Tree
{
    internal class KDTree
    {
        private KDTreeNode _root;

        public void CreateTree(Triangle[] triangles)
        {
            _root = new KDTreeNode(null);

            Vector3f max = Vector3f.One * float.MinValue;
            Vector3f min = Vector3f.One * float.MaxValue;

            foreach (Triangle triangle in triangles)
            {
                max.x = MathF.Max(MathF.Max(triangle.point1.x, MathF.Max(triangle.point2.x, triangle.point3.x)), max.x);
                max.y = MathF.Max(MathF.Max(triangle.point1.y, MathF.Max(triangle.point2.y, triangle.point3.y)), max.y);
                max.z = MathF.Max(MathF.Max(triangle.point1.z, MathF.Max(triangle.point2.z, triangle.point3.z)), max.z);

                min.x = MathF.Min(MathF.Min(triangle.point1.x, MathF.Min(triangle.point2.x, triangle.point3.x)), max.x);
                min.y = MathF.Min(MathF.Min(triangle.point1.y, MathF.Min(triangle.point2.y, triangle.point3.y)), max.y);
                min.z = MathF.Min(MathF.Min(triangle.point1.z, MathF.Min(triangle.point2.z, triangle.point3.z)), max.z);
            }

            _root.Box = new BoundingBox(max, min);
            RecursionNodeCreate(_root, Vector3f.Axis.X);
        }

        private static float CalculateSAH(float parentBoxArea, int leftTrianglesCount, int rightTrianglesCount, float leftBoxArea, float rightBoxArea)
        {
            return 1 + 1 * (leftBoxArea * leftTrianglesCount + rightBoxArea * rightTrianglesCount) / parentBoxArea;
        }

        private void RecursionNodeCreate(KDTreeNode node, Vector3f.Axis axis)
        {
            Debug.WriteLine("Split");

            if (node.SplitBox(32, axis))
            {
                RecursionNodeCreate(node.LeftChild, (Vector3f.Axis)(((int)axis + 1) % 3));
                RecursionNodeCreate(node.RightChild, (Vector3f.Axis)(((int)axis + 1) % 3));
            }
        }
    }
}
