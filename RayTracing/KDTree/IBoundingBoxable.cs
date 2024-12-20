using RayTracing.CameraRendering;

namespace RayTracing.ThreeDimensionalTree
{
    internal interface IBoundingBoxable : ICameraRenderObject
    {
        public Vector3f BoundingBoxMin { get; }
        public Vector3f BoundingBoxMax { get; }
    }
}
