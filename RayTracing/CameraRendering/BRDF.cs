using System;
using System.Diagnostics;

namespace RayTracing.CameraRendering
{
    internal static class BRDF
    {
        public static VectorColor Brdf(Vector3f normal, Vector3f viewVector, Vector3f lightVector, Material material)
        {
            VectorColor F0 = material.F0;

            Vector3f fresnel = FresnelFunction(normal, lightVector, viewVector, F0);
            Vector3f diffuseK = (Vector3f.One - fresnel) * (1 - material.Metalness);

            VectorColor lambertFunction = LambertDiffuseFunction(material.Color, lightVector, normal);
            VectorColor diffuseLight = new VectorColor(Vector3f.MultiplyByElements(lambertFunction.Rgb, diffuseK));

            Vector3f specularLight = CookTorranceSpecularFunction(normal, viewVector, lightVector, fresnel, material.Roughness);

            VectorColor brdf = new VectorColor(diffuseLight.Rgb + specularLight);

            return brdf;
        }

        private static VectorColor LambertDiffuseFunction(VectorColor objectColor, Vector3f lightVector, Vector3f normal)
        {
            return objectColor / MathF.PI;
        }

        private static Vector3f CookTorranceSpecularFunction(Vector3f normal, Vector3f viewVector, Vector3f lightVector, Vector3f fresnel, float roughness)
        {
            Vector3f halfWayVector = (viewVector + lightVector).GetNormalized();
            float nDotV = MathF.Max(Vector3f.Dot(normal, viewVector), 0);
            float nDotL = MathF.Max(Vector3f.Dot(normal, lightVector), 0);
            float nDotH = MathF.Max(Vector3f.Dot(normal, halfWayVector), 0);
            float alpha = roughness * roughness;
            float D = NormalDistribution(normal, nDotH, alpha);
            float G = SmithGeometryShadowing(normal, nDotV, nDotL, alpha);

            Vector3f numerator = D * G * fresnel;
            float denominator = 4 * nDotV * nDotL;

            return numerator / MathF.Max(denominator, 0.00001f);
        }

        // GGX/Trowbridge-reitz model
        private static float NormalDistribution(Vector3f normal, float nDotH, float alpha)
        {
            float d = nDotH * nDotH * (alpha * alpha - 1) + 1;

            float numerator = alpha * alpha;
            float denominator = MathF.PI * d * d;

            return numerator / MathF.Max(denominator, 0.00001f);
        }

        //Schlick-GGX model (Smith model + Shlick-Beckman model)
        // "x" variable is view or light vector (to calculate for both sides)
        private static float GeometryShadowing(Vector3f normal, float nDotX, float alpha)
        {
            float k = alpha / 2;
            float numerator = nDotX;
            float denominator = nDotX * (1 - k) + k;

            return numerator / MathF.Max(denominator, 0.00001f);
        }

        private static float SmithGeometryShadowing(Vector3f normal, float nDotV, float nDotL, float alpha)
        {
            return GeometryShadowing(normal, nDotV, alpha) * GeometryShadowing(normal, nDotL, alpha);
        }

        //Shlick's approximation
        private static Vector3f FresnelFunction(Vector3f normal, Vector3f lightVector, Vector3f viewVector, VectorColor F0)
        {
            float cosTheta = MathF.Max(Vector3f.Dot(lightVector, normal), 0);

            return F0.Rgb + (Vector3f.One - F0.Rgb) * MathF.Pow(1 - cosTheta, 5);
        }
    }
}
