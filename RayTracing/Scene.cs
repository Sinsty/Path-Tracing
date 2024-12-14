using System;

namespace RayTracing
{
    internal class Scene
    {
        public readonly CameraRenderObject[] Objects;
        public readonly Camera Camera;

        public Scene(CameraRenderObject[] objects, Camera camera)
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
