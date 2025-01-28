using PathTracing.CameraRendering;

namespace PathTracing.Geometry
{
    internal interface ICompositeObject
    {
        public ICameraRenderObject[] GetObjects();
    }
}
