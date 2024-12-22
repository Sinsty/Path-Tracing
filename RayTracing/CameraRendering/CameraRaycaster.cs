using System;

namespace RayTracing.CameraRendering
{
    internal static class CameraRaycaster
    {
        private static Random _random = new Random();

        //pdf = cos(theta) / pi
        public static CameraRaycastInfo CastRay(Ray ray, int maxBounces)
        {
            HitInfo hit;
            ICameraRenderObject intersectObject = MainRender.Scene.Tree != null ?
                                                  MainRender.Scene.Tree.FindRayIntersection(ray, out hit) :
                                                  GetObjectFromRay(ray, out hit);

            if (intersectObject != null)
            {
                //return new CameraRaycastInfo(Vector3f.One * 100000000, 0, true);

                Material material = intersectObject.AppliedMaterial;

                Vector3f selfLight = material.Color.Rgb * material.LightIntencity;

                if (maxBounces <= 0)
                {
                    return new CameraRaycastInfo(selfLight, 0, false);
                }

                Ray reflectedRay = HandleReflectedRay(hit);
                CameraRaycastInfo reflectedRayInfo = CastRay(reflectedRay, maxBounces - 1);

                if (reflectedRayInfo.IsHit == false)
                {
                    return new CameraRaycastInfo(selfLight, hit.Distance, true);
                }

                Vector3f incomingLight = reflectedRayInfo.Color;

                VectorColor brdf = BRDF.Brdf(hit.Normal, ray.direction, reflectedRay.direction, material);
                float lDotN = MathF.Max(Vector3f.Dot(reflectedRay.direction, hit.Normal), 0);

                Vector3f reflectedColor = Vector3f.MultiplyByElements(brdf.Rgb, incomingLight) * lDotN;

                Vector3f distanceReflectedColor = reflectedColor;

                if (reflectedRayInfo.Distance > 1)
                {
                    distanceReflectedColor /= MathF.Pow(reflectedRayInfo.Distance, 2);
                }

                Vector3f color = selfLight + distanceReflectedColor;

                return new CameraRaycastInfo(color, hit.Distance, true);
            }
            else
            {
                return new CameraRaycastInfo(Vector3f.Zero, 0, false);
            }
        }

        private static ICameraRenderObject GetObjectFromRay(Ray ray, out HitInfo hit)
        {
            ICameraRenderObject closestObject = null;
            hit = new HitInfo(float.MaxValue);

            foreach (ICameraRenderObject boxable in MainRender.Scene.Objects)
            {
                if (boxable.RayIntersect(ray, out HitInfo objectHit))
                {
                    if (objectHit.Distance < hit.Distance)
                    {
                        closestObject = boxable;
                        hit = objectHit;
                    }
                }
            }

            return closestObject;
        }

        private static Ray HandleReflectedRay(HitInfo rayObjectHit)
        {
            Vector3f reflectedRayDirection = getRandomPointInSphere(rayObjectHit.Point + rayObjectHit.Normal) - rayObjectHit.Point;

            Ray reflectedRay = new Ray(rayObjectHit.Point, reflectedRayDirection);

            return reflectedRay;
        }

        private static Vector3f getRandomPointInSphere(Vector3f spherePosition)
        {
            Vector3f point = Vector3f.Zero;

            float d = 2;
            while (d >= 1)
            {
                point.x = (float)_random.NextDouble() * 2 - 1;
                point.y = (float)_random.NextDouble() * 2 - 1;
                point.z = (float)_random.NextDouble() * 2 - 1;

                d = point.GetLength();
            }

            return point + spherePosition;
        }
    }

    internal struct CameraRaycastInfo
    {
        public Vector3f Color;
        public float Distance;
        public bool IsHit;

        public CameraRaycastInfo(Vector3f color, float distance, bool isHit)
        {
            Color = color;
            Distance = distance;
            IsHit = isHit;
        }
    }
}
