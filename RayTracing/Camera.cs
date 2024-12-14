using System;
using System.Drawing;

namespace RayTracing
{
    internal class Camera
    {
        public Vector3f Position { get; set; }
        public float Fov { get; private set; }
        public float MaxViewDistance { get; private set; }

        private int _currentImageWidth;
        private int _currentImageHeight;
        private int _currentRayBouncesCount;

        #region RaycastParameters

        private int _imageWidth;
        public int ImageWidth
        {
            get { return _imageWidth; }
            set
            {
                if (value < 0)
                { throw new ArgumentOutOfRangeException(nameof(value)); }
                _imageWidth = value;
            }
        }

        private int _imageHeight;
        public int ImageHeight
        {
            get { return _imageHeight; }
            set
            {
                if (value < 0)
                { throw new ArgumentOutOfRangeException(nameof(value)); }
                _imageHeight = value;
            }
        }

        private int _rayBouncesCount;
        public int RayBouncesCount
        {
            get { return _rayBouncesCount; }
            set { if (value < 0 ) 
                {   throw new ArgumentOutOfRangeException(nameof(value)); } 
                _rayBouncesCount = value; }
        }

        #endregion RaycastParameters

        private Random _random = new Random();

        public Camera(Vector3f position, float fov, float maxViewDistance, int imageWidth, int imageHeight, int rayBouncesCount)
        {
            Position = position;
            Fov = fov;
            MaxViewDistance = maxViewDistance;
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
            RayBouncesCount = rayBouncesCount;

            UpdateValues();
            MainRender.OnEndRendering += (Bitmap image) => UpdateValues();
        }

        public CameraRaycastInfo TraceRay(int x, int y)
        {
            return CameraRaycaster.CastRay(CreateRayFromPixel(x, y), _currentRayBouncesCount);
        }

        private Ray CreateRayFromPixel(int x, int y)
        {
            float directionX = (2 * (x + 0.5f) / _currentImageWidth - 1) * MathF.Tan(Vector3f.DegreesToRadians(Fov / 2)) * _currentImageWidth / _currentImageHeight;
            float directionY = (1 - 2 * (y + 0.5f) / _currentImageHeight) * MathF.Tan(Vector3f.DegreesToRadians(Fov / 2));

            return new Ray(Position, new Vector3f(directionX, directionY, 1));
        }

        private void UpdateValues()
        {
            _currentImageWidth = ImageWidth;
            _currentImageHeight = ImageHeight;
            _currentRayBouncesCount = RayBouncesCount;
        }
    }
}
