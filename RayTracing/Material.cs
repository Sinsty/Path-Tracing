namespace RayTracing
{
    internal class Material
    {
        public VectorColor Color {  get; private set; }
        public float Roughness;
        public float Metalness;
        public VectorColor F0;
        public float LightIntencity;

        public Material(VectorColor color, float roughness, float metalness, VectorColor f0, float lightIntencity)
        {
            Color = color;
            Roughness = roughness;
            Metalness = metalness;
            F0 = f0;
            LightIntencity = lightIntencity;
        }
    }
}
