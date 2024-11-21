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

        public Vector3f CastRay(Ray ray, int maxBounces)
        {
            HitInfo hit;
            CameraRenderObject intersectObject = GetObjectFromRay(ray, out hit);

            if (intersectObject != null)
            {
                Material material = intersectObject.AppliedMaterial;

                Vector3f selfLight = material.Color.Rgb * material.LightIntencity;

                if (maxBounces <= 0)
                {
                    return selfLight;
                }

                Ray reflectedRay = HandleReflectedRay(hit);
                Vector3f incomingLight = CastRay(reflectedRay, maxBounces - 1);

                VectorColor brdf = BRDF.Brdf(-hit.Normal, ray.direction, reflectedRay.direction, material);

                Vector3f color = selfLight + (Vector3f.MultiplyByElements(brdf.Rgb, incomingLight) * MathF.Max(Vector3f.Dot(reflectedRay.direction, -hit.Normal), 0) /*  divide by reflected ray length */);

                //if (color.x != 0 || color.y != 0 || color.z != 0)
                //{
                //    Debug.WriteLine("Material color: " + material.Color.Rgb);
                //    Debug.WriteLine("Light intencity: " + material.LightIntencity);
                //    Debug.WriteLine("SelfLight: " + selfLight);
                //    Debug.WriteLine("End color: " + color);
                //    Debug.WriteLine("=============");
                //}

                return color;
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

        private Ray HandleReflectedRay(HitInfo rayObjectHit)
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
