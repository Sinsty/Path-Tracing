namespace RayTracing
{
    internal abstract class CameraRenderObject
    {
        public abstract Vector3f Position { get; set; }
        public abstract Material AppliedMaterial { get; protected set; }

        public abstract bool RayIntersect(Ray ray, out HitInfo hit);
    }
}
