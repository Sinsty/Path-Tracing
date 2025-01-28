using System;
using PathTracing.CameraRendering;
using PathTracing.ThreeDimensionalTree;

namespace PathTracing.Geometry
{
    internal class Scene
    {
        public readonly ICameraRenderObject[] Objects;
        public readonly Camera Camera;

        public KDTree Tree { get; private set; }

        public Scene(ICameraRenderObject[] objects, Camera camera)
        {
            if (objects == null)
                throw new ArgumentNullException(nameof(camera));
            if (camera == null)
                throw new ArgumentNullException(nameof(camera));

            Objects = objects;
            Camera = camera;
        }

        public void GenerateKDTree(int maxDepth, int sahNumberOfDivisions, int minObjectsInBox)
        {
            Tree = KDTree.CreateKDTree(this, maxDepth, sahNumberOfDivisions, minObjectsInBox);
        }
    }
}
