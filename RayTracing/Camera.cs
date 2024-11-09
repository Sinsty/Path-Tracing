using System;
using System.Drawing;

namespace RayTracing
{
    internal class Camera : SceneObject
    {
        public override Vector3f Position { get; set; }
        public float Fov { get; private set; }
        public float MaxViewDistance { get; private set; }

        private CameraRenderObject[] _renderObjects;

        private Random _random = new Random();
        private CameraRaycaster _raycaster;

        public Camera(Vector3f position, float fov, float maxViewDistance, CameraRenderObject[] renderObjects)
        {
            Position = position;
            Fov = fov;
            _renderObjects = renderObjects;
            MaxViewDistance = maxViewDistance;

            _raycaster = new CameraRaycaster(_renderObjects);
        }

        public Color GetPixelColor(int x, int y, int imageWidth, int imageHeight)
        {
            float directionX = (2 * (x + 0.5f) / imageWidth - 1) * MathF.Tan(Vector3f.DegreesToRadians(Fov / 2)) * imageWidth / imageHeight;
            float directionY = -(2 * (y + 0.5f) / imageHeight - 1) * MathF.Tan(Vector3f.DegreesToRadians(Fov / 2));

            Ray ray = new Ray(Position, new Vector3f(directionX, directionY, 1));

            return TraceRay(ray, 5, 5);
        }

        private Color TraceRay(Ray ray, int raysCount, int maxBounces)
        {
            VectorColor currentColor = _raycaster.CastRay(ray, maxBounces, new VectorColor(1, 1, 1), new VectorColor(0, 0, 0));

            int i = 1;
            while (i < raysCount)
            {
                VectorColor rayColor = _raycaster.CastRay(ray, maxBounces, new VectorColor(1, 1, 1), new VectorColor(0, 0, 0));

                float R = (rayColor.Rgb.x + currentColor.Rgb.x * i) / (i + 1);
                float G = (rayColor.Rgb.y + currentColor.Rgb.y * i) / (i + 1);
                float B = (rayColor.Rgb.z + currentColor.Rgb.z * i) / (i + 1);

                currentColor = new VectorColor(R, G, B);

                i++;
            }

            return currentColor.ToBaseColor();
        }

        
    }
}
