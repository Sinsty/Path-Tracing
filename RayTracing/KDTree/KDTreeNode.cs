using System;
using System.Diagnostics;

namespace RayTracing.Tree
{
    internal class KDTreeNode
    {
        public BoundingBox Box { get; set; }
        public KDTreeNode LeftChild { get; private set; }
        public KDTreeNode RightChild { get; private set; }
        public KDTreeNode Parent { get; }
        public bool IsLeaf { get; private set; }

        private const float _cI = 1;
        private const float _cT = 1;

        public KDTreeNode(KDTreeNode parent)
        {
            IsLeaf = true;
            Parent = parent;
        }

        private bool SetLeft(KDTreeNode node)
        {
            if (LeftChild != null) return false;
            if (node == null) throw new NullReferenceException();

            LeftChild = node;

            IsLeaf = false;
            return true;
        }

        private bool SetRight(KDTreeNode node)
        {
            if (RightChild != null) return false;
            if (node == null) throw new NullReferenceException();

            RightChild = node;

            IsLeaf = false;
            return true;
        }

        public bool SplitBox(int planesCount, Vector3f.Axis axis)
        {
            int[] high = new int[planesCount + 1]; //left high[i]
            int[] low = new int[planesCount + 1]; //right low[i + 1]

            float distance = Box.Max.GetAxis(axis) - Box.Min.GetAxis(axis);

            float distancePerPlane = distance / (planesCount + 1);

            foreach (Triangle triangle in Box.Triangles)
            {
                float min = MathF.Min(triangle.point1.GetAxis(axis), MathF.Min(triangle.point2.GetAxis(axis), triangle.point3.GetAxis(axis)));
                float max = MathF.Max(triangle.point1.GetAxis(axis), MathF.Max(triangle.point2.GetAxis(axis), triangle.point3.GetAxis(axis)));

                int iHigh = (int)(max / distancePerPlane);
                int iLow = (int)(min / distancePerPlane);

                high[iHigh] += 1;
                low[iLow] += 1;
            }

            for (int i = 1; i < high.Length; i++)
            {
                high[i] += high[i - 1];
                low[i] += low[i - 1];
            }

            float minSAH = float.MaxValue;
            float minSAHAxisValue = 0;
            Vector3f minSAHLeftBoxMax = Vector3f.Zero;
            Vector3f minSAHLeftBoxMin = Vector3f.Zero;
            Vector3f minSAHRightBoxMax = Vector3f.Zero;
            Vector3f minSAHRightBoxMin = Vector3f.Zero;
            for (int i = 0; i < planesCount; i++)
            {
                float axisValue = distancePerPlane * (i + 1);
                //left
                Vector3f leftBoxMax = Box.Max - Vector3f.AxisToVector(axis) * (Box.Max.GetAxis(axis) - Box.Min.GetAxis(axis) - axisValue);
                Vector3f leftBoxMin = Box.Min;

                float leftBoxArea = BoundingBox.CalculateSurfaceArea(leftBoxMax, leftBoxMin);
                //right
                Vector3f rightBoxMax = Box.Max;
                Vector3f rightBoxMin = Box.Min + Vector3f.AxisToVector(axis) * axisValue;

                float rightBoxArea = BoundingBox.CalculateSurfaceArea(rightBoxMax, rightBoxMin);

                float sah = CalculateSAH(Box.SurfaceArea, high[i], low[i + 1], leftBoxArea, rightBoxArea);

                if (sah < minSAH)
                {
                    minSAH = sah;
                    minSAHLeftBoxMax = leftBoxMax;
                    minSAHLeftBoxMin = leftBoxMin;
                    minSAHRightBoxMax = rightBoxMax;
                    minSAHRightBoxMin = rightBoxMin;
                    minSAHAxisValue = axisValue;
                }
            }

            if (minSAH > _cI * (planesCount + 1))
            {
                Debug.WriteLine(minSAH + " " + _cI * (planesCount + 1));
                return false;
            }

            if (RightChild == null)
                SetRight(new KDTreeNode(this));

            if (LeftChild == null)
                SetLeft(new KDTreeNode(this));

            LeftChild.Box = new BoundingBox(minSAHLeftBoxMax, minSAHLeftBoxMin);
            RightChild.Box = new BoundingBox(minSAHRightBoxMax, minSAHRightBoxMin);

            foreach (Triangle triangle in Box.Triangles)
            {
                float min = MathF.Min(triangle.point1.GetAxis(axis), MathF.Min(triangle.point2.GetAxis(axis), triangle.point3.GetAxis(axis)));
                float max = MathF.Max(triangle.point1.GetAxis(axis), MathF.Max(triangle.point2.GetAxis(axis), triangle.point3.GetAxis(axis)));

                if (min <= minSAHAxisValue)
                {
                    LeftChild.Box.Triangles.Add(triangle);
                }

                if (max >= minSAHAxisValue)
                {
                    RightChild.Box.Triangles.Add(triangle);
                }
            }

            return true;
        }

        private static float CalculateSAH(float parentBoxArea, int leftTrianglesCount, int rightTrianglesCount, float leftBoxArea, float rightBoxArea)
        {
            return _cT + _cI * (leftBoxArea * leftTrianglesCount + rightBoxArea * rightTrianglesCount) / parentBoxArea;
        }
    }
}
