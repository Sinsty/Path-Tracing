namespace RayTracing
{
    internal class Material
    {
        public VectorColor Color {  get; private set; }
        public float Roughtness;
        public float Metalness;
        public VectorColor F0;
        public float LightIntencity;

        public Material(VectorColor color, float roughness, float metalness, VectorColor f0, float lightIntencity)
        {
            Color = color;
            Roughtness = roughness;
            Metalness = metalness;
            F0 = f0;
            LightIntencity = lightIntencity;
        }
    }
}
