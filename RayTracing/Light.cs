namespace RayTracing
{
    internal class Light : SceneObject
    {
        public override Vector3f Position { get; set; }
        public float Intensity;

        public Light(Vector3f position, float intensity)
        {
            Position = position;
            Intensity = intensity;
        }

    }
}
