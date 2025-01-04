using System;
using System.Diagnostics;
using System.Numerics;

namespace RayTracing.CameraRendering
{
    internal static class CameraRaycaster
    {
        private static Random _random = new Random();

        public static CameraRaycastInfo CastRay(Ray ray, int maxBounces)
        {
            HitInfo hit;
            ICameraRenderObject intersectObject = MainRender.Scene.Tree != null ?
                                                  MainRender.Scene.Tree.FindRayIntersection(ray, out hit) :
                                                  GetObjectFromRay(ray, out hit);

            if (intersectObject != null)
            {
                Material material = intersectObject.AppliedMaterial;

                Vector3f selfLight = material.Color.Rgb * material.LightIntencity;

                if (maxBounces <= 0)
                {
                    return new CameraRaycastInfo(selfLight, 0, false);
                }

                Ray reflectedRay = HandleReflectedRay(hit, out float pdf);
                CameraRaycastInfo reflectedRayInfo = CastRay(reflectedRay, maxBounces - 1);

                if (reflectedRayInfo.IsHit == false)
                {
                    return new CameraRaycastInfo(selfLight, hit.Distance, true);
                }

                Vector3f incomingLight = reflectedRayInfo.Color;

                VectorColor brdf = BRDF.Brdf(hit.Normal, -ray.direction, reflectedRay.direction, material);
                float lDotN = MathF.Max(Vector3f.Dot(reflectedRay.direction, hit.Normal), 0.000001f);

                Vector3f reflectedColor = Vector3f.MultiplyByElements(brdf.Rgb, incomingLight) * lDotN;
                reflectedColor /= pdf;

                Vector3f color = selfLight + reflectedColor;

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

        private static Ray HandleReflectedRay(HitInfo rayObjectHit, out float pdf)
        {
            Vector3f reflectedRayDirection = CosineWeightedHemisphere(rayObjectHit.Normal, out pdf);

            Ray reflectedRay = new Ray(rayObjectHit.Point, reflectedRayDirection.GetNormalized());

            return reflectedRay;
        }

        private static Vector3f CosineWeightedHemisphere(Vector3f normal, out float pdf)
        {
            float e0 = (float)_random.NextDouble();
            float e1 = (float)_random.NextDouble();

            e0 = MathF.Max(e0, 0.000001f);
            float cosTheta = MathF.Sqrt(e0);
            float sinTheta = MathF.Sqrt(MathF.Max(1 - cosTheta * cosTheta, 0.000001f));
            float phi = 2 * MathF.PI * e1;

            float x = MathF.Cos(phi) * sinTheta;
            float y = MathF.Sin(phi) * sinTheta;
            float z = cosTheta;

            Vector3f t = 1 > MathF.Abs(x) ? new Vector3f(0, 0, 1) : new Vector3f(1, 0, 0);
            Vector3f tangent = Vector3f.Cross(t, normal).GetNormalized();
            Vector3f bitangent = Vector3f.Cross(normal, tangent);

            Vector3f direction = x * tangent + y * bitangent + z * normal;

            pdf = cosTheta / MathF.PI;

            return direction;
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
