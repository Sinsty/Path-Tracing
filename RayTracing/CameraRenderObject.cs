namespace RayTracing
{
    internal interface ICameraRenderObject
    {
        public Vector3f Position { get; set; }
        public Material AppliedMaterial { get; protected set; }
        public bool RayIntersect(Ray ray, out HitInfo hit);
    }
}
