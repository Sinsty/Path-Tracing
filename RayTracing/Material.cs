namespace RayTracing
{
    internal class Material
    {
        public VectorColor Color {  get; private set; }
        public float Specularity;
        public float LightIntencity;

        public Material(VectorColor color, float specularity, float lightIntencity)
        {
            Color = color;
            Specularity = specularity;
            LightIntencity = lightIntencity;
        }
    }
}
