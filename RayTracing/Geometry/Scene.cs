using System;
using RayTracing.CameraRendering;

namespace RayTracing.Geometry
{
    internal class Scene
    {
        public readonly ICameraRenderObject[] Objects;
        public readonly Camera Camera;

        public Scene(ICameraRenderObject[] objects, Camera camera)
        {
            if (objects == null)
                throw new ArgumentNullException(nameof(camera));
            if (camera == null)
                throw new ArgumentNullException(nameof(camera));

            Objects = objects;
            Camera = camera;
        }
    }
}
