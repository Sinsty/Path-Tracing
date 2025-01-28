using System;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using PathTracing.Geometry;


namespace PathTracing
{
    internal static class MainRender
    {
        public static Action<Bitmap> OnEndRendering;

        public static int ImageWidth => _currentScene.Camera.ImageWidth;
        public static int ImageHeight => _currentScene.Camera.ImageHeight;
        public static Scene Scene => _currentScene;
        public static bool IsRendering { get; private set; }
        public static float RenderingTime { get; private set; }
        public static int RenderedPixels { get; private set; }
        public static float RenderTimeLeft { get; private set; }
        public static float RenderTimeElapsed { get; private set; }

        private static Thread _renderThread;

        private static Scene _currentScene;

        public static void StartRender(int imageWidth, int imageHeight, Scene scene)
        {
            if (IsRendering)
                return;

            if (imageWidth < 0)
                throw new ArgumentOutOfRangeException(nameof(imageWidth));
            if (imageHeight < 0)
                throw new ArgumentOutOfRangeException(nameof(imageHeight));
            if (scene == null)
                throw new NullReferenceException(nameof(scene));

            _currentScene = scene;

            _renderThread = new Thread(Render);
            _renderThread.IsBackground = true;
            _renderThread.Priority = ThreadPriority.AboveNormal;
            _renderThread.Start();
        }
        private static async void Render()
        {
            IsRendering = true;

            Bitmap image = new Bitmap(_currentScene.Camera.ImageWidth, _currentScene.Camera.ImageHeight, PixelFormat.Format24bppRgb);

            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData imageData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);
            IntPtr pointer = imageData.Scan0;

            int bytes = Math.Abs(imageData.Stride) * image.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(pointer, rgbValues, 0, bytes);

            float renderTime = 0f;

            RenderedPixels = 0;
            RenderTimeElapsed = 0;
            RenderTimeLeft = 0;

            Stopwatch rowRenderWatch = new Stopwatch();
            Stopwatch allRenderingWatch = new Stopwatch();
            allRenderingWatch.Start();

            for (int y = 0; y < image.Height; y++)
            {
                rowRenderWatch.Restart();

                Parallel.For(0, image.Width, (int x) =>
                {
                    SetPixel(x, y, rgbValues, imageData, _currentScene.Camera.CalculatePixelColor(x, y));
                    RenderedPixels++;
                });

                rowRenderWatch.Stop();

                renderTime = (renderTime * y + (float)rowRenderWatch.Elapsed.TotalSeconds) / (y + 1);

                RenderTimeLeft = renderTime * (image.Height - y);
                RenderTimeElapsed = (float)allRenderingWatch.Elapsed.TotalSeconds;
            }

            RenderTimeLeft = 0;
            allRenderingWatch.Stop();
            RenderedPixels = image.Width * image.Height;

            IsRendering = false;

            Marshal.Copy(rgbValues, 0, pointer, bytes);
            image.UnlockBits(imageData);

            OnEndRendering?.Invoke(image);
        }

        private static void SetPixel(int x, int y, byte[] rgbValues, BitmapData data, Color color)
        {
            int pixel = x * 3 + y * data.Stride;
            rgbValues[pixel] = color.B;
            rgbValues[pixel + 1] = color.G;
            rgbValues[pixel + 2] = color.R;
        }
    }
}
