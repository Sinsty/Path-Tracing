namespace RayTracing.CameraRendering
{
    internal class Material
    {
        public VectorColor Color { get; private set; }
        public float Roughness;
        public float Metalness;
        public VectorColor F0 = new VectorColor(0.04f, 0.04f, 0.04f);
        public float LightIntencity;

        public Material(VectorColor color, float roughness, float metalness, float lightIntencity)
        {
            Color = color;
            Roughness = roughness;
            Metalness = metalness;
            LightIntencity = lightIntencity;
        }

        public Material(VectorColor color, float roughness, float metalness, float lightIntencity, VectorColor F0)
        {
            Color = color;
            Roughness = roughness;
            Metalness = metalness;
            LightIntencity = lightIntencity;
            this.F0 = F0;
        }
    }
}
