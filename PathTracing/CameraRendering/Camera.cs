using System;
using System.Drawing;

namespace PathTracing.CameraRendering
{
    internal class Camera
    {
        public Vector3f Position { get; set; }
        public float Fov { get; private set; }
        public float MaxViewDistance { get; private set; }

        public int CurrentImageWidth => _currentImageWidth;
        public int CurrentImageHeight => _currentImageHeight;
        public int CurrentRayBouncesCount => _currentRayBouncesCount;
        public int CurrentSamplesCount => _samplesCount;

        private int _currentImageWidth;
        private int _currentImageHeight;
        private int _currentRayBouncesCount;
        private int _currentSamplesCount;

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
            set
            {
                if (value < 0)
                { throw new ArgumentOutOfRangeException(nameof(value)); }
                _rayBouncesCount = value;
            }
        }

        private int _samplesCount;
        public int SamplesCount
        {
            get { return _samplesCount; }
            set
            {
                if (value < 0)
                { throw new ArgumentOutOfRangeException(nameof(value)); }
                _samplesCount = value;
            }
        }

        #endregion RaycastParameters

        private Random _random = new Random();

        public Camera(Vector3f position, float fov, float maxViewDistance, int imageWidth, int imageHeight, int rayBouncesCount, int samplesCount)
        {
            Position = position;
            Fov = fov;
            MaxViewDistance = maxViewDistance;
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
            RayBouncesCount = rayBouncesCount;
            SamplesCount = samplesCount;

            UpdateValues();
            MainRender.OnEndRendering += (image) => UpdateValues();
        }

        public Color CalculatePixelColor(int x, int y)
        {
            return CameraRaycaster.PathTraceColor(CreateRayFromPixel(x, y), _currentRayBouncesCount, _currentSamplesCount);
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
            _currentSamplesCount = SamplesCount;
        }
    }
}
