namespace RayTracing
{
    internal struct RaycastHit
    {
        public readonly float Distance;
        public readonly Vector3f Point;
        public readonly Vector3f Normal;

        public RaycastHit(float distance, Vector3f point, Vector3f normal)
        {
            Distance = distance;
            Point = point;
            Normal = normal;
        }
    }
}
