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

            return TraceRay(ray, 1, 10);
        }

        private Color TraceRay(Ray ray, int raysCount, int maxBounces)
        {
            return CastRay(ray, maxBounces, new VectorColor(1, 1, 1), new VectorColor(0, 0, 0));
        }

        private Color CastRay(Ray ray, int maxBounces, VectorColor rayColor, VectorColor incomingLight)
        {
            Sphere closestObject = null;
            RaycastHit closestObjectHitInfo = new RaycastHit(float.MaxValue, Vector3f.Zero, Vector3f.Zero);

            foreach (var sceneObject in _renderObjects)
            {
                if (sceneObject.RayIntersect(ray, out RaycastHit hit))
                {
                    if (hit.Distance < closestObjectHitInfo.Distance)
                    {
                        closestObject = (Sphere)sceneObject;
                        closestObjectHitInfo = hit;
                    }
                }
            }
            

            if (closestObject != null)
            {
                rayColor *= closestObject.AppliedMaterial.Color;
                incomingLight += rayColor * closestObject.AppliedMaterial.LightIntencity;

                if (maxBounces == 0)
                {
                    return incomingLight.ToBaseColor();
                }

                Vector3f reflectedRayDirection = ray.direction - 2 * Vector3f.Dot(ray.direction, closestObjectHitInfo.Normal) * closestObjectHitInfo.Normal;
                Ray reflectedRay = new Ray(closestObjectHitInfo.Point, reflectedRayDirection);

                return CastRay(reflectedRay, maxBounces - 1, rayColor, incomingLight);
            }
            else
            {
                return incomingLight.ToBaseColor();
            }
        }

        //private Color CastRay(Ray ray)
        //{
        //    float closestIntersectionDistance = float.MaxValue;
        //    CameraRenderObject closestSphere = null;
        //    Vector3f closestSphereNormal = Vector3f.Zero;

        //    foreach (var sceneObject in _renderObjects)
        //    {
        //        if (sceneObject.RayIntersect(ray, out RaycastHit hit))
        //        {
        //            if (hit.Distance < closestIntersectionDistance)
        //            {
        //                closestSphere = sceneObject;
        //                closestIntersectionDistance = hit.Distance;
        //                closestSphereNormal = hit.Normal;
        //            }
        //        }
        //    }

        //    if (closestSphere != null)
        //    {
        //        return CalculateLightForColorInPosition(closestSphere.AppliedMaterial,
        //                                                closestSphere.Position,
        //                                                closestSphereNormal);
        //    }
        //    else
        //    {
        //        return Color.AliceBlue;
        //    }
        //}

        //private Color CalculateLightForColorInPosition(Material material, Vector3f position, Vector3f surfaceNormal)
        //{
        //    float diffuseLightIntensity = 0;
        //    foreach (Light light in _sceneLight)
        //    {
        //        Vector3f lightDirection = (light.Position - position).GetNormalized();
        //        Vector3f viewDirection = (Position - position).GetNormalized();
        //        diffuseLightIntensity += light.Intensity * MathF.Max(0, Vector3f.Dot(lightDirection, surfaceNormal));
        //    }

        //    Vector3f diffuseLightColor = material.Color * diffuseLightIntensity;
        //    diffuseLightColor.ClampValuesFromVector(Vector3f.Zero, material.Color);


        //    return Color.FromArgb((int)diffuseLightColor.x, (int)diffuseLightColor.y, (int)diffuseLightColor.z);
        //}
    }
}
