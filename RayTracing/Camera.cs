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
        private Light[] _sceneLight;

        public Camera(Vector3f position, float fov, float maxViewDistance, CameraRenderObject[] renderObjects, Light[] light)
        {
            Position = position;
            Fov = fov;
            _renderObjects = renderObjects;
            MaxViewDistance = maxViewDistance;
            _sceneLight = light;
        }

        public Color GetPixelColor(int x, int y, int imageWidth, int imageHeight)
        {
            float directionX = (2 * (x + 0.5f) / imageWidth - 1) * MathF.Tan(Vector3f.DegreesToRadians(Fov / 2)) * imageWidth / imageHeight;
            float directionY = -(2 * (y + 0.5f) / imageHeight - 1) * MathF.Tan(Vector3f.DegreesToRadians(Fov / 2));

            Ray ray = new Ray(Position, new Vector3f(directionX, directionY, 1));

            return CastRay(ray);
        }

        private Color CastRay(Ray ray)
        {
            float closestIntersectionDistance = float.MaxValue;
            CameraRenderObject closestSphere = null;
            Vector3f closestSphereNormal = Vector3f.Zero;

            foreach (var sceneObject in _renderObjects)
            {
                if (sceneObject.RayIntersect(ray, out RaycastHit hit))
                {
                    if (hit.Distance < closestIntersectionDistance)
                    {
                        closestSphere = sceneObject;
                        closestIntersectionDistance = hit.Distance;
                        closestSphereNormal = hit.Normal;
                    }
                }
            }

            if (closestSphere != null)
            {
                return CalculateLightForColorInPosition(closestSphere.AppliedMaterial.Color, 
                                                        closestSphere.Position, 
                                                        closestSphereNormal);
            }
            else
            {
                return Color.Black;
            }
        }

        private Color CalculateLightForColorInPosition(Vector3f rawColor, Vector3f position, Vector3f surfaceNormal)
        {
            float diffuseLightIntensity = 0;
            foreach (Light light in _sceneLight)
            {
                Vector3f lightDirection = (light.Position - position).GetNormalized();
                diffuseLightIntensity += light.Intensity + MathF.Max(0, Vector3f.Dot(lightDirection, surfaceNormal));
            }

            Vector3f diffuseLightColor = rawColor * diffuseLightIntensity;
            diffuseLightColor.ClampValues(0, 255);

            return Color.FromArgb((int)diffuseLightColor.x, (int)diffuseLightColor.y, (int)diffuseLightColor.z);
        }
    }
}
