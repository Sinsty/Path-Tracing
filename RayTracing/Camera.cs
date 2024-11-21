using System;
using System.Diagnostics;
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

            return TraceRay(ray, 8, 8);
        }

        private Color TraceRay(Ray ray, int raysCount, int maxBounces)
        {
            Vector3f currentColor = _raycaster.CastRay(ray, maxBounces);

            int skippedCount = 0;
            int i = 1;
            while (i < raysCount)
            {
                Vector3f rayColor = _raycaster.CastRay(ray, maxBounces);

                if (rayColor.GetLength() != 0)
                {
                    float R = (rayColor.x + currentColor.x * (i - skippedCount)) / (i + 1 - skippedCount);
                    float G = (rayColor.y + currentColor.y * (i - skippedCount)) / (i + 1 - skippedCount);
                    float B = (rayColor.z + currentColor.z * (i - skippedCount)) / (i + 1 - skippedCount);

                    currentColor = new Vector3f(R, G, B);
                }
                else
                {
                    skippedCount++;
                }

                i++;
            }

            //if (currentColor.x != 0 || currentColor.y != 0 || currentColor.z != 0)
            //{
            //    Debug.WriteLine(currentColor);
            //    Debug.WriteLine(VectorColor.GammaCorrection(VectorColor.AcesFilmicTonemapping(currentColor)).ToBaseColor());
            //}

            return VectorColor.GammaCorrection(VectorColor.AcesFilmicTonemapping(currentColor)).ToBaseColor();
        }
    }
}
