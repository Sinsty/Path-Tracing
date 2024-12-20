namespace RayTracing.CameraRendering
{
    internal class Material
    {
        public VectorColor Color { get; private set; }
        public float Roughness;
        public float Metalness;
        public float LightIntencity;

        public Material(VectorColor color, float roughness, float metalness, float lightIntencity)
        {
            Color = color;
            Roughness = roughness;
            Metalness = metalness;
            LightIntencity = lightIntencity;
        }
    }
}
