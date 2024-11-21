using System;
using System.Diagnostics;

namespace RayTracing
{
    internal static class BRDF
    {
        public static VectorColor Brdf(Vector3f normal, Vector3f viewVector, Vector3f lightVector, Material material)
        {
            float objectMetalness = material.Metalness;
            float objectRoughness = material.Roughness;
            VectorColor objectColor = material.Color;
            VectorColor F0 = material.Color * material.Metalness + new VectorColor(0.04f, 0.04f, 0.04f);

            //F0 = objectColor * F0;
            //F0 = new VectorColor(0.04f, 0.04f, 0.04f);

            Vector3f fresnel = FresnelFunction(normal, lightVector, viewVector, F0);
            Vector3f diffuseK = (Vector3f.One - fresnel) * (1 - objectMetalness);

            VectorColor lambert = LambertDiffuseFunction(objectColor, lightVector, normal);
            Vector3f cookTorrance = CookTorranceSpecularFunction(normal, viewVector, lightVector, fresnel, objectRoughness);

            VectorColor diffuseLight = new VectorColor(Vector3f.MultiplyByElements(lambert.Rgb, diffuseK));
            //VectorColor diffuseLight = new VectorColor(lambert.Rgb * diffuseK);

            VectorColor brdf = new VectorColor(diffuseLight.Rgb + cookTorrance);

            //if (/*cookTorrance.GetLength() > 0.01f && */ material.LightIntencity == 0)
            //{
            //    float normalDistribution = NormalDistribution(normal, viewVector, lightVector, objectRoughness);
            //    float geometryShadowing = SmithGeometryShadowing(normal, viewVector, lightVector, objectRoughness);
            //    Debug.WriteLine("BaseColor: " + objectColor.Rgb);
            //    Debug.WriteLine("Roughness: " + objectRoughness);
            //    Debug.WriteLine("Metalness: " + objectMetalness);
            //    Debug.WriteLine("CookTorrance: " + cookTorrance);
            //    Debug.WriteLine("LambertLight: " + lambert.Rgb);
            //    Debug.WriteLine("Distribution: " + normalDistribution);
            //    Debug.WriteLine("GeometryShad: " + geometryShadowing);
            //    Debug.WriteLine("FresnelFunct: " + fresnel);
            //    Debug.WriteLine("K diffuse: " + diffuseK);
            //    Debug.WriteLine("Diffuse light" + diffuseLight.Rgb);
            //    Debug.WriteLine("BRDF: " + brdf.Rgb);
            //    Debug.WriteLine("=============================================");
            //}

            return brdf;
        }

        private static VectorColor LambertDiffuseFunction(VectorColor objectColor, Vector3f lightVector, Vector3f normal)
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

            float d = (cosThetaH * cosThetaH * (alpha * alpha - 1)) + 1;

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

        private static Vector3f FresnelFunction(Vector3f normal, Vector3f lightVector, Vector3f viewVector, VectorColor F0)
        {
            //Vector3f halfWayVector = (viewVector + lightVector).GetNormalized();
            float cosTheta = MathF.Max(Vector3f.Dot(lightVector, normal), 0);
            //float hDotV = MathF.Max(Vector3f.Dot(halfWayVector, viewVector), 0);

            return F0.Rgb + (Vector3f.One - F0.Rgb) * MathF.Pow(1 - cosTheta, 5);
        }
    }
}
