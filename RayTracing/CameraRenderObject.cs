namespace RayTracing
{
    internal abstract class CameraRenderObject : SceneObject
    {
        public abstract Material AppliedMaterial { get; set; }

        public abstract bool RayIntersect(Ray ray, out RaycastHit hit);
    }
}
