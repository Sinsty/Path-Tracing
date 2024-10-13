namespace RayTracing
{
    internal class Material
    {
        public Vector3f Color {  get; private set; }

        public Material(Vector3f color)
        {
            Color = color;
        }
    }
}
