using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace RayTracing
{
    public partial class Form1 : Form
    {
        private int _renderThreadCountToUse;

        private int _maxThreadsForRendering;

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

            ThreadPool.GetMaxThreads(out int worker, out int completion);
            _maxThreadsForRendering = worker - 10;

            ThreadCountNumeric.Maximum = _maxThreadsForRendering;

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
            Material sphereMaterial1 = new Material(new VectorColor(1f, 1f, 1f), 1f, 0f, new VectorColor(0.05f, 0.05f, 0.05f), 5f);
            Material sphereMaterial2 = new Material(new VectorColor(0.2f, 0.7f, 0.4f), 0.25f, 0.25f, new VectorColor(0.1f, 0.1f, 0.1f), 0f); // rought-metallic
            Material sphereMaterial3 = new Material(new VectorColor(0.35f, 0.55f, 0.75f), 0.25f, 1f, new VectorColor(0.95f, 0.95f, 0.95f), 0f); // metallic
            Material sphereMaterial4 = new Material(new VectorColor(0.5f, 0.5f, 0.5f), 1f, 0f, new VectorColor(0.05f, 0.05f, 0.05f), 0f); // rough
            Material sphereMaterial6 = new Material(new VectorColor(1f, 1f, 1f), 0.5f, 0f, new VectorColor(0.5f, 0.5f, 0.5f), 0f);

            Material sphereMaterial5 = new Material(new VectorColor(1, 1f, 1f), 1f, 0f, new VectorColor(0.05f, 0.05f, 0.05f), 1);

            Sphere[] spheres =
            [
                new Sphere(new Vector3f(0, 5, 3), 2, sphereMaterial1),
                new Sphere(new Vector3f(2, 0, 7), 1, sphereMaterial2),
                new Sphere(new Vector3f(-2, 0, 7), 1, sphereMaterial3),
                new Sphere(new Vector3f(0, -31, 7), 30, sphereMaterial4),
                new Sphere(new Vector3f(4, 0, 7), 1, sphereMaterial6),

                new Sphere(new Vector3f(5, 5, 7), 1, sphereMaterial5),
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

        private void OnThreadCountNumericValueChange(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(MathF.Min(_maxThreadsForRendering, Convert.ToInt32(ThreadCountNumeric.Value)));
            ThreadCountNumeric.Value = value;

            _renderThreadCountToUse = value;
        }
    }
}
