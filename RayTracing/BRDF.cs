using System;
using System.Diagnostics;

namespace RayTracing
{
    internal static class BRDF
    {
        public static VectorColor Brdf(Vector3f normal, Vector3f viewVector, Vector3f lightVector, VectorColor objectColor, VectorColor F0, float objectRoughtness, float objectMetalness)
        {
            Vector3f cookTorrance = CookTorranceSpecularFunction(normal, viewVector, lightVector, F0, objectRoughtness);
            VectorColor lambert = LambertDiffuseFunction(normal, lightVector, objectColor);
            Vector3f fresnel = FresnelFunction(normal, viewVector, lightVector, F0);
            //float normalDistribution = NormalDistribution(normal, viewVector, lightVector, objectRoughtness);
            //float geometryShadowing = GeometryShadowing(normal, viewVector, objectRoughtness) * GeometryShadowing(normal, lightVector, objectRoughtness);

            Debug.WriteLine("CookTorrance: " + cookTorrance.x + " " + cookTorrance.y + " " + cookTorrance.z);
            //Debug.WriteLine("LambertLight: " + lambert.Rgb.x + " " + lambert.Rgb.y + " " + lambert.Rgb.z);
            //Debug.WriteLine("Distribution: " + normalDistribution);
            //Debug.WriteLine("GeometryShad: " + geometryShadowing);
            //Debug.WriteLine("FresnelFunct: " + fresnel.x + " " + fresnel.y + " " + fresnel.z);
            //Debug.WriteLine("=============================================");

            Vector3f diffuseK = (Vector3f.Zero - fresnel) * (1 - objectMetalness);
            return new VectorColor(lambert.Rgb * diffuseK + cookTorrance);
        }

        private static VectorColor LambertDiffuseFunction(Vector3f normal, Vector3f lightVector, VectorColor objectColor)
        {
            VectorColor numerator = objectColor;
            float denominator = MathF.PI * MathF.Max(Vector3f.Dot(normal, lightVector), 0);

            return numerator / MathF.Max(denominator, 0.00001f);
        }

        private static Vector3f CookTorranceSpecularFunction(Vector3f normal, Vector3f viewVector, Vector3f lightVector, VectorColor F0, float roughtness)
        {
            Vector3f numerator = NormalDistribution(normal, viewVector, lightVector, roughtness) * GeometryShadowing(normal, viewVector, roughtness) * GeometryShadowing(normal, lightVector, roughtness) * FresnelFunction(normal, viewVector, lightVector, F0);
            float denominator = 4 * MathF.Max(Vector3f.Dot(viewVector, normal), 0) * MathF.Max(Vector3f.Dot(lightVector, normal), 0);

            return numerator / MathF.Max(denominator, 0.00001f);
        }

        // GGX/Trowbridge-reitz model
        private static float NormalDistribution(Vector3f normal, Vector3f viewVector, Vector3f lightVector, float roughtness)
        {
            float alpha = roughtness * roughtness;
            Vector3f halfWayVector = (viewVector + lightVector).GetNormalized();

            float numerator = alpha * alpha;
            float d = MathF.Pow(MathF.Max(Vector3f.Dot(normal, halfWayVector), 0), 2) * (alpha * alpha - 1) + 1;
            float denominator = MathF.PI * d * d;

            return numerator / MathF.Max(denominator, 0.00001f);
        }

        //Schlick-GGX model (Smith model + Shlick-Beckman model)
        // "x" variable is view or light vector (to calculate for both sides)
        private static float GeometryShadowing(Vector3f normal, Vector3f x, float roughtness)
        {
            float alpha = roughtness * roughtness;
            float k = alpha / 2;
            float numerator = MathF.Max(Vector3f.Dot(normal, x), 0);
            float denominator = (MathF.Max(Vector3f.Dot(normal, x), 0) * (1 - k)) + k;

            return numerator / MathF.Max(denominator, 0.00001f);
        }

        private static Vector3f FresnelFunction(Vector3f normal, Vector3f viewVector, Vector3f ligthVector, VectorColor F0)
        {
            Vector3f halfWayVector = (viewVector + ligthVector).GetNormalized();
            return F0.Rgb + (Vector3f.One - F0.Rgb) * MathF.Pow(MathF.Min(MathF.Max(1 - MathF.Max(Vector3f.Dot(viewVector, halfWayVector), 0), 0), 1), 5);
        }
    }
}
