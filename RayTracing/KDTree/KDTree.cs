using PathTracing.CameraRendering;
using PathTracing.Geometry;
using System.Collections.Generic;

namespace PathTracing.ThreeDimensionalTree
{
    internal class KDTree
    {
        private KDTreeNode _root;
        private readonly int _maxDepth;
        private readonly int _numberOfDivisions;
        private readonly int _minObjectsInBox;

        public static KDTree CreateKDTree(Scene scene, int maxDepth, int sahNumberOfDivisions, int minObjectsInBox)
        {
            List<IBoundingBoxable> boxable = new List<IBoundingBoxable>();

            foreach (var sceneObject in scene.Objects)
            {
                if (sceneObject is ICompositeObject)
                {
                    foreach (var fromCompositeObject in ((ICompositeObject)sceneObject).GetObjects())
                    {
                        if (fromCompositeObject is IBoundingBoxable)
                            boxable.Add((IBoundingBoxable)fromCompositeObject);
                    }
                }
                else if (sceneObject is IBoundingBoxable)
                {
                    boxable.Add((IBoundingBoxable)sceneObject);
                }
            }

            return new KDTree(boxable.ToArray(), maxDepth, sahNumberOfDivisions, minObjectsInBox);
        }

        public KDTree(IBoundingBoxable[] boxable, int maxDepth, int sahNumberOfDivisions, int minObjectsInBox)
        {
            _root = new KDTreeNode(null);
            _maxDepth = maxDepth;
            _numberOfDivisions = sahNumberOfDivisions;
            _minObjectsInBox = minObjectsInBox;

            _root.Box = BoundingBox.CreateAroundObjects(boxable);
            RecursionNodesCreate(_root, Vector3f.Axis.X, 0);
        }

        public ICameraRenderObject FindRayIntersection(Ray ray, out HitInfo hit)
        {
            return RecursionFindRayIntersection(_root, ray, out hit);
        }

        private ICameraRenderObject RecursionFindRayIntersection(KDTreeNode node, Ray ray, out HitInfo hit)
        {
            if (node.IsLeaf)
            {
                return node.GetObjectFromRay(ray, out hit);
            }

            bool isLeftIntersect = node.LeftChild.Box.IsRayIntersect(ray);
            bool isRightIntersect = node.RightChild.Box.IsRayIntersect(ray);

            if (isLeftIntersect && isRightIntersect)
            {
                ICameraRenderObject leftChildObject = RecursionFindRayIntersection(node.LeftChild, ray, out HitInfo leftChildHit);
                ICameraRenderObject rightChildObject = RecursionFindRayIntersection(node.RightChild, ray, out HitInfo rightChildHit);

                if (leftChildHit.Distance < rightChildHit.Distance)
                {
                    hit = leftChildHit;
                    return leftChildObject;
                }
                else
                {
                    hit = rightChildHit;
                    return rightChildObject;
                }
            }
            else if (isLeftIntersect)
            {
                return RecursionFindRayIntersection(node.LeftChild, ray, out hit);
            }
            else if (isRightIntersect)
            {
                return RecursionFindRayIntersection(node.RightChild, ray, out hit);
            }
            else
            {
                hit = new HitInfo();
                return null;
            }
        }

        private void RecursionNodesCreate(KDTreeNode node, Vector3f.Axis axis, int recursionCount)
        {
            if (recursionCount >= _maxDepth)
                return;

            if (node.SplitBox(_numberOfDivisions, axis, _maxDepth))
            {
                RecursionNodesCreate(node.LeftChild, (Vector3f.Axis)(((int)axis + 1) % 3), recursionCount + 1);
                RecursionNodesCreate(node.RightChild, (Vector3f.Axis)(((int)axis + 1) % 3), recursionCount + 1);
            }
        }
    }
}
