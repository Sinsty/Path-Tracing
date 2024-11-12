using System;
using System.Diagnostics;
using static System.Windows.Forms.DataFormats;

namespace RayTracing
{
    internal static class BRDF
    {
        public static VectorColor Brdf(Vector3f normal, Vector3f viewVector, Vector3f lightVector, VectorColor objectColor, VectorColor F0, float objectRoughtness, float objectMetalness)
        {
            F0 = new VectorColor(Vector3f.One * 0.05f) + objectColor * F0 * objectMetalness;
            //F0 = new VectorColor(0.04f, 0.04f, 0.04f);

            Vector3f fresnel = FresnelFunction(lightVector, viewVector, F0);
            Vector3f diffuseK = (Vector3f.One - fresnel) * (1 - objectMetalness);

            VectorColor lambert = LambertDiffuseFunction(objectColor);
            Vector3f cookTorrance = CookTorranceSpecularFunction(normal, viewVector, lightVector, fresnel, objectRoughtness);

            VectorColor brdf = new VectorColor(lambert.Rgb * diffuseK + cookTorrance);

            //float normalDistribution = NormalDistribution(normal, viewVector, lightVector, objectRoughness);
            //float geometryShadowing = SmithGeometryShadowing(normal, viewVector, lightVector, objectRoughness);
            //Debug.WriteLine("BaseColor: " + objectColor.Rgb.x + " " + objectColor.Rgb.y + " " + objectColor.Rgb.z);
            //Debug.WriteLine("Roughness: " + objectRoughness);
            //Debug.WriteLine("Metalness: " + objectMetalness);
            //Debug.WriteLine("CookTorrance: " + cookTorrance.x + " " + cookTorrance.y + " " + cookTorrance.z);
            //Debug.WriteLine("LambertLight: " + lambert.Rgb.x + " " + lambert.Rgb.y + " " + lambert.Rgb.z);
            //Debug.WriteLine("Distribution: " + normalDistribution);
            //Debug.WriteLine("GeometryShad: " + geometryShadowing);
            //Debug.WriteLine("FresnelFunct: " + fresnel.x + " " + fresnel.y + " " + fresnel.z);
            //Debug.WriteLine("BRDF: " + brdf.Rgb.x + " " + brdf.Rgb.y + " " + brdf.Rgb.z);
            //Debug.WriteLine("K diffuse: " + diffuseK.x + " " + diffuseK.y + " " + diffuseK.z);
            //Debug.WriteLine("K diffuse * lambert: " + (lambert.Rgb * diffuseK).x + " " + (lambert.Rgb * diffuseK).y + " " + (lambert.Rgb * diffuseK).z);
            //Debug.WriteLine("=============================================");

            return brdf;
        }

        private static VectorColor LambertDiffuseFunction(VectorColor objectColor)
        {
            return objectColor / MathF.PI;
        }


        private static Vector3f CookTorranceSpecularFunction(Vector3f normal, Vector3f viewVector, Vector3f lightVector, Vector3f fresnel, float roughness)
        {
            float D = NormalDistribution(normal, viewVector, lightVector, roughness);
            float G = SmithGeometryShadowing(normal, viewVector, lightVector, roughness);
            Vector3f F = fresnel;

            float nDotV = MathF.Max(Vector3f.Dot(normal, viewVector), 0);
            float nDotL = MathF.Max(Vector3f.Dot(normal, lightVector), 0);

            Vector3f numerator = D * G * F;
            float denominator = 4 * nDotV * nDotL;

            return numerator / MathF.Max(denominator, 0.00001f);
        }

        // GGX/Trowbridge-reitz model
        private static float NormalDistribution(Vector3f normal, Vector3f viewVector, Vector3f lightVector, float roughness)
        {
            float alpha = roughness * roughness;
            Vector3f halfWayVector = (viewVector + lightVector).GetNormalized();


            float cosThetaH = MathF.Max(Vector3f.Dot(normal, halfWayVector), 0);

            float d = cosThetaH * cosThetaH * (alpha * alpha - 1) + 1;

            float numerator = alpha * alpha;
            float denominator = MathF.PI * d * d;

            return numerator / MathF.Max(denominator, 0.00001f);
        }

        //Schlick-GGX model (Smith model + Shlick-Beckman model)
        // "x" variable is view or light vector (to calculate for both sides)
        private static float GeometryShadowing(Vector3f normal, Vector3f x, float roughness)
        {
            float alpha = roughness * roughness;
            float k = alpha / 2;
            float nDotX = MathF.Max(Vector3f.Dot(normal, x), 0);

            float numerator = nDotX;
            float denominator = (nDotX * (1 - k)) + k;

            return numerator / MathF.Max(denominator, 0.00001f);
        }

        private static float SmithGeometryShadowing(Vector3f normal, Vector3f viewVector, Vector3f lightVector, float roughness)
        {
            return GeometryShadowing(normal, viewVector, roughness) * GeometryShadowing(normal, lightVector, roughness);
        }

        private static Vector3f FresnelFunction(Vector3f lightVector, Vector3f viewVector, VectorColor F0)
        {
            Vector3f halfWayVector = (viewVector + lightVector).GetNormalized();
            float hDotV = MathF.Max(Vector3f.Dot(halfWayVector, viewVector), 0);

            return F0.Rgb + (Vector3f.One - F0.Rgb) * MathF.Pow(1 - hDotV, 5);
        }
    }
}
