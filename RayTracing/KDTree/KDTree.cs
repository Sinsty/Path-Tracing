using RayTracing;

internal class KDTree
{
    private KDTreeNode _root;
    private int _maxDepth = 32;
    private int _numberOfDivisions = 32;
    private int _minTriangles = 6;

    public void CreateTree(IBoundingBoxable[] boxable)
    {
        _root = new KDTreeNode(null);

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
