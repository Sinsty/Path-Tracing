using RayTracing.CameraRendering;

namespace RayTracing.Geometry
{
    internal interface ICompositeObject
    {
        public ICameraRenderObject[] GetObjects();
    }
}
