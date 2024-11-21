using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace RayTracing
{
    public partial class Form1 : Form
    {
        private int _imageWidth = 1920;
        private int _imageHeight = 1080;

        private float _renderingTimeLeft;
        private float _renderTimeElapsed;
        private long _pixelsRendered;

        private Bitmap _image;
        private Camera _camera;

        private Thread _renderThread;
        private Thread _renderStatesThread;

        public Form1()
        {

            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ObjReader.Parse(@"D:\YLink\RayTracing\RayTracing\src\WavefontMonkey.obj");

            //Vector3f vec = Vector3f.One - new Vector3f(0.341231f, 0.012331f, 0.5432124f);
            //Debug.WriteLine(vec.x + " " + vec.y + " " + vec.z);

            InitializeCamera();
        }

        private void UpdateRenderStats()
        {
            while (true)
            {
                PixelsRenderedLabel.SetText("Pixels rendered: " + _pixelsRendered.ToString() + "/" + (_imageWidth * _imageHeight).ToString());
                TimeElapsedLabel.SetText("Time elapsed: " + _renderTimeElapsed.ToString());
                TimeLeftLabel.SetText("Time left: " + _renderingTimeLeft.ToString());

                Thread.Sleep(100);
            }
        }

        private void InitializeCamera()
        {
            //Material sphereMaterial1 = new Material(new VectorColor(1f, 1f, 1f), 1f, 0f, new VectorColor(0.05f, 0.05f, 0.05f), 5f);
            //Material sphereMaterial2 = new Material(new VectorColor(0.2f, 0.7f, 0.4f), 0.1f, 0, new VectorColor(0.04f, 0.04f, 0.04f), 0f);
            //Material sphereMaterial3 = new Material(new VectorColor(0.35f, 0.55f, 0.75f), 0.25f, 0.75f, new VectorColor(0.55f, 0.75f, 0.95f), 0f);
            //Material sphereMaterial4 = new Material(new VectorColor(0.5f, 0.5f, 0.5f), 1f, 0f, new VectorColor(0.05f, 0.05f, 0.05f), 0f);
            //Material sphereMaterial6 = new Material(new VectorColor(1f, 1f, 1f), 0.5f, 0f, new VectorColor(0.5f, 0.5f, 0.5f), 0f);

            //Material sphereMaterial5 = new Material(new VectorColor(1, 1f, 1f), 1f, 0f, new VectorColor(0.05f, 0.05f, 0.05f), 1);

            //Sphere[] spheres =
            //[
            //    new Sphere(new Vector3f(-2, 7, 0), 1, sphereMaterial1),
            //    new Sphere(new Vector3f(2, 0, 7), 1, sphereMaterial2),
            //    new Sphere(new Vector3f(-2, 0, 7), 1, sphereMaterial3),
            //    new Sphere(new Vector3f(0, -31, 7), 30, sphereMaterial4),
            //    new Sphere(new Vector3f(4, 0, 7), 1, sphereMaterial6),

            //    //new Sphere(new Vector3f(3, 5, -2), 1, sphereMaterial5),
            //];

            Material sphereLight = new Material(new VectorColor(1, 1f, 1f), 1, 0f, new VectorColor(0.05f, 0.05f, 0.05f), 10f);
            Material sphereMaterial = new Material(new VectorColor(1f, 0.2f, 0.2f), 0.5f, 0.5f, new VectorColor(0.04f, 0.04f, 0.04f), 0f);


            Sphere[] spheres =
            [
                new Sphere(new Vector3f(5, 7, 0), 1, sphereLight),
                new Sphere(new Vector3f(0, 0, 7), 1, sphereMaterial),
            ];

            _camera = new Camera(Vector3f.Zero, 60, 1000, spheres);
        }

        private void Render()
        {
            _image = new Bitmap(_imageWidth, _imageHeight);

            float renderTime = 0f;

            Stopwatch watch = new Stopwatch();
            Stopwatch allRenderingWatch = new Stopwatch();
            allRenderingWatch.Start();

            for (int i = 0; i < _image.Height; i++)
            {
                watch.Restart();
                for (int j = 0; j < _image.Width; j++)
                {
                    _image.SetPixel(j, i, _camera.GetPixelColor(j, i, _image.Width, _image.Height));

                    _pixelsRendered++;
                }
                watch.Stop();

                renderTime = (renderTime * i + (float)watch.Elapsed.TotalSeconds) / (i + 1);
                _renderingTimeLeft = renderTime * (_image.Height - i);

                _renderTimeElapsed = (float)allRenderingWatch.Elapsed.TotalSeconds;
            }


            _renderingTimeLeft = 0;

            allRenderingWatch.Stop();

            pictureBox1.Image = _image;
        }

        private void SetPixel(int x, int y, Color color, byte[] image, BitmapData imageData)
        {
            int pixel = (Math.Abs(imageData.Stride) * y + x * image.Length / (imageData.Width * imageData.Height));

            image[pixel] = color.B;
            image[pixel + 1] = color.G;
            image[pixel + 2] = color.R;

            if (pixel > 3000000)
            {
                Debug.WriteLine(pixel);
            }
        }

        private void StartRenderButtonClick(object sender, EventArgs e)
        {
            if (_renderThread == null)
            {
                _renderThread = new Thread(Render);
                _renderThread.IsBackground = true;
                _renderThread.Priority = ThreadPriority.AboveNormal;
                _renderThread.Start();
            }

            if (_renderStatesThread == null)
            {
                _renderStatesThread = new Thread(UpdateRenderStats);
                _renderStatesThread.IsBackground = true;
                _renderStatesThread.Priority = ThreadPriority.Lowest;
                _renderStatesThread.Start();
            }
        }
    }
}
