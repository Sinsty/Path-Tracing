using System;
using System.Diagnostics;

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

        public VectorColor CastRay(Ray ray, int maxBounces, VectorColor rayColor, VectorColor incomingLight)
        {
            HitInfo hit;
            CameraRenderObject intersectObject = GetObjectFromRay(ray, out hit);

            if (intersectObject != null)
            {
                if (maxBounces == 0)
                {
                    return incomingLight;
                }

                Ray reflectedRay = RayDirectionHandle(hit);
                incomingLight = CastRay(reflectedRay, maxBounces - 1, rayColor, incomingLight);

                Material material = intersectObject.AppliedMaterial;
                VectorColor brdf = BRDF.Brdf(-hit.Normal, ray.direction, reflectedRay.direction, material.Color, material.F0, material.Roughtness, material.Metalness);
                Debug.WriteLine(brdf.Rgb.x + " " + brdf.Rgb.y + " " + brdf.Rgb.z);
                return (material.Color * material.LightIntencity) + (brdf * 100 * incomingLight * Vector3f.Dot(reflectedRay.direction, -hit.Normal));
            }
            else
            {
                return incomingLight;
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
