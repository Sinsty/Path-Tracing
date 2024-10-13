namespace RayTracing
{
    internal class Ray
    {
        public Vector3f origin { get; private set; }
        public Vector3f direction { get; private set; }

        public Ray(Vector3f origin, Vector3f direction)
        {
            this.origin = origin;
            this.direction = direction.GetNormalized();
        }
    }
}
