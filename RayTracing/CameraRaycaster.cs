using System;
using System.Diagnostics;
using System.Drawing;

namespace RayTracing
{
    internal class CameraRaycaster
    {
        private readonly CameraRenderObject[] _renderObjects;
        private Random _random = new Random();

        public CameraRaycaster(CameraRenderObject[] renderObjects)
        {
            _renderObjects = renderObjects;
        }

        public Vector3f CastRay(Ray ray, int maxBounces, int ramification)
        {
            HitInfo hit;
            CameraRenderObject intersectObject = GetObjectFromRay(ray, out hit);

            if (intersectObject != null)
            {
                Material material = intersectObject.AppliedMaterial;

                Vector3f endColor = material.Color.Rgb * material.LightIntencity;

                if (maxBounces == 0)
                {
                    return endColor;
                }

                for (int i = 0; i < ramification; i++)
                {
                    Ray reflectedRay = RayDirectionHandle(hit);
                    Vector3f incomingLight = CastRay(reflectedRay, maxBounces - 1, ramification);

                    VectorColor brdf = BRDF.Brdf(-hit.Normal, ray.direction, reflectedRay.direction, material.Color, material.F0, material.Roughness, material.Metalness);

                    endColor += brdf.Rgb * incomingLight * MathF.Max(Vector3f.Dot(reflectedRay.direction, -hit.Normal), 0);
                }

                return endColor;

                //Ray reflectedRay = RayDirectionHandle(hit);
                //incomingLight = CastRay(reflectedRay, maxBounces - 1, ramification, rayColor, incomingLight);

                //VectorColor brdf = BRDF.Brdf(-hit.Normal, ray.direction, reflectedRay.direction, material.Color, material.F0, material.Roughness, material.Metalness);

                //Vector3f color = (material.Color.Rgb * material.LightIntencity) + (brdf.Rgb * incomingLight * MathF.Max(Vector3f.Dot(reflectedRay.direction, -hit.Normal), 0));

                //return new Vector3f(color.x, color.y, color.z);
            }
            else
            {
                return Vector3f.Zero;
            }
        }

        private CameraRenderObject GetObjectFromRay(Ray ray, out HitInfo hit)
        {
            CameraRenderObject closestObject = null;
            hit = new HitInfo(float.MaxValue);

            foreach (var sceneObject in _renderObjects)
            {
                if (sceneObject.RayIntersect(ray, out HitInfo objectHit))
                {
                    if (objectHit.Distance < hit.Distance)
                    {
                        closestObject = (Sphere)sceneObject;
                        hit = objectHit;
                    }
                }
            }

            return closestObject;
        }

        private Ray RayDirectionHandle(HitInfo rayObjectHit)
        {
            Vector3f reflectedRayDirection = getRandomPointInSphere(rayObjectHit.Point - rayObjectHit.Normal) - rayObjectHit.Point;

            Ray reflectedRay = new Ray(rayObjectHit.Point, reflectedRayDirection);

            return reflectedRay;
        }

        private Vector3f getRandomPointInSphere(Vector3f spherePosition)
        {
            Vector3f point = Vector3f.Zero;

            float d = 2;
            while (d >= 1)
            {
                point.x = (float)_random.NextDouble() * 2 - 1;
                point.y = (float)_random.NextDouble() * 2 - 1;
                point.z = (float)_random.NextDouble() * 2 - 1;

                d = point.x * point.x + point.y * point.y + point.z * point.z;
            }

            point += spherePosition;

            return point;
        }
    }
}
