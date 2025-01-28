namespace PathTracing.CameraRendering
{
    internal struct HitInfo
    {
        public readonly float Distance;
        public readonly Vector3f Point;
        public readonly Vector3f Normal;

        public HitInfo(float distance)
        {
            Distance = distance;
            Point = Vector3f.Zero;
            Normal = Vector3f.Zero;
        }

        public HitInfo()
        {
            Distance = 0;
            Point = Vector3f.Zero;
            Normal = Vector3f.Zero;
        }

        public HitInfo(float distance, Vector3f point, Vector3f normal)
        {
            Distance = distance;
            Point = point;
            Normal = normal.GetNormalized();
        }
    }
}
