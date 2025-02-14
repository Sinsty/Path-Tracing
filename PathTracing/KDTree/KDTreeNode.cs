using PathTracing.CameraRendering;
using System;

namespace PathTracing.ThreeDimensionalTree
{
    internal class KDTreeNode
    {
        public BoundingBox Box { get; set; }
        public KDTreeNode Parent { get; }
        public bool IsLeaf { get; private set; } = true;

        private KDTreeNode _leftChild = null;
        public KDTreeNode LeftChild
        {
            get { return _leftChild; }
            set { SetLeft(value); }
        }

        private KDTreeNode _rightChild = null;
        public KDTreeNode RightChild
        {
            get { return _rightChild; }
            set { SetRight(value); }
        }

        private const float _cI = 1;
        private const float _cT = 1;

        public KDTreeNode(KDTreeNode parent)
        {
            Parent = parent;
        }

        public bool SplitBox(int planesCount, Vector3f.Axis axis, int minObjectsInBox)
        {
            if (Box.Boxables.Count <= minObjectsInBox && IsLeaf == true)
                return false;

            int[] high = new int[planesCount + 1]; // left high[i]
            int[] low = new int[planesCount + 1]; // right low[i + 1]

            float distance = Box.Max.GetAxis(axis) - Box.Min.GetAxis(axis);
            float distancePerPlane = distance / (planesCount + 1);

            foreach (IBoundingBoxable boxable in Box.Boxables)
            {
                float max = boxable.BoundingBoxMax.GetAxis(axis);
                float min = boxable.BoundingBoxMin.GetAxis(axis);

                int iHigh = (int)((max - Box.Min.GetAxis(axis)) / distancePerPlane);
                int iLow = (int)((min - Box.Min.GetAxis(axis)) / distancePerPlane);

                if (iHigh < high.Length && iHigh >= 0)
                    high[iHigh] += 1;
                if (iLow < low.Length && iLow >= 0)
                    low[iLow] += 1;
            }

            for (int i = 1; i < planesCount + 1; i++)
            {
                low[low.Length - (i + 1)] += low[low.Length - i];
                high[i] += high[i - 1];
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

                Vector3f leftBoxMax = Box.Max;
                leftBoxMax.SetAxis(axis, Box.Min.GetAxis(axis) + axisValue);
                Vector3f leftBoxMin = Box.Min;

                Vector3f rightBoxMin = Box.Min;
                rightBoxMin.SetAxis(axis, Box.Min.GetAxis(axis) + axisValue);
                Vector3f rightBoxMax = Box.Max;

                float leftBoxArea = BoundingBox.CalculateSurfaceArea(leftBoxMax, leftBoxMin);
                float rightBoxArea = BoundingBox.CalculateSurfaceArea(rightBoxMax, rightBoxMin);

                int commonElements = Box.Boxables.Count - high[i] - low[i + 1];

                float sah = CalculateSAH(Box.SurfaceArea, high[i] + commonElements, low[i + 1] + commonElements, leftBoxArea, rightBoxArea);

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

            RightChild = new KDTreeNode(this);
            LeftChild = new KDTreeNode(this);

            LeftChild.Box = new BoundingBox(minSAHLeftBoxMax, minSAHLeftBoxMin);
            RightChild.Box = new BoundingBox(minSAHRightBoxMax, minSAHRightBoxMin);

            foreach (IBoundingBoxable boxable in Box.Boxables)
            {
                float max = boxable.BoundingBoxMax.GetAxis(axis);
                float min = boxable.BoundingBoxMin.GetAxis(axis);

                if (min <= Box.Min.GetAxis(axis) + minSAHAxisValue)
                    LeftChild.Box.Boxables.Add(boxable);

                if (max >= Box.Min.GetAxis(axis) + minSAHAxisValue)
                    RightChild.Box.Boxables.Add(boxable);
            }

            return true;
        }

        private static float CalculateSAH(float parentBoxArea, int leftTrianglesCount, int rightTrianglesCount, float leftBoxArea, float rightBoxArea)
        {
            return _cT + _cI * (leftBoxArea * leftTrianglesCount + rightBoxArea * rightTrianglesCount) / parentBoxArea;
        }

        public IBoundingBoxable GetObjectFromRay(Ray ray, out HitInfo hit)
        {
            IBoundingBoxable closestObject = null;
            hit = new HitInfo(float.MaxValue);

            foreach (IBoundingBoxable boxable in Box.Boxables)
            {
                if (boxable.RayIntersect(ray, out HitInfo objectHit))
                {
                    if (objectHit.Distance < hit.Distance)
                    {
                        closestObject = boxable;
                        hit = objectHit;
                    }
                }
            }

            return closestObject;
        }

        private bool SetLeft(KDTreeNode node)
        {
            if (_leftChild != null) return false;
            if (node == null) throw new NullReferenceException();

            _leftChild = node;

            IsLeaf = false;
            return true;
        }

        private bool SetRight(KDTreeNode node)
        {
            if (_rightChild != null) return false;
            if (node == null) throw new NullReferenceException();

            _rightChild = node;

            IsLeaf = false;
            return true;
        }
    }
}
