using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace RayTracing
{
    public partial class Form1 : Form
    {
        private Thread _renderStatesThread;

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            rayBouncesNumericUpDown.Maximum = int.MaxValue;
            samplesCountNumericUpDown.Maximum = int.MaxValue;
            imageWidthNumericUpDown.Maximum = int.MaxValue;
            imageHeightNumericUpDown.Maximum = int.MaxValue;

            MainRender.OnEndRendering += OnRenderEnd;
        }

        private Scene CreateAndGetScene()
        {
            Material sphereMaterial = new Material(new VectorColor(1f, 1f, 1f), 2f, 0f, 1000f);
            Material meshMaterial = new Material(new VectorColor(0.2f, 0.2f, 1f), 1f, 0f, 0f);

            CameraRenderObject[] sceneObjects =
            [
                //new Triangle(new Vector3f(0, 0, 5), [new Vector3f(0, 0, 0), new Vector3f(0, 1, 0), new Vector3f(1, 0, 0)], meshMaterial),
                new Sphere(new Vector3f(-2, 3, 0), 1, sphereMaterial),
                new Mesh(new Vector3f(1, 0, 5), @"D:\YLink\RayTracing\RayTracing\src\Monkey180.obj", meshMaterial)
            ];

            //List<CameraRenderObject> sceneObjects = new List<CameraRenderObject>(new Polygon(new Vector3f(0, 0, 5), [new Vector3f(0, 0, 0), new Vector3f(0, 1, 0), new Vector3f(1, 1, 0), new Vector3f(1, 0, 0)], meshMaterial).Triangulate());
            //sceneObjects.Add(new Sphere(new Vector3f(-2, 3, 0), 1, sphereMaterial));

            Camera camera = new Camera(Vector3f.Zero, 60, 1000, (int)imageWidthNumericUpDown.Value, (int)imageHeightNumericUpDown.Value, (int)rayBouncesNumericUpDown.Value);

            return new Scene(sceneObjects, camera);
        }

        private void UpdateRenderStats()
        {
            while (true)
            {
                int allPixelCount = MainRender.ImageWidth * MainRender.ImageHeight;
                PixelsRenderedLabel.SetText("Pixels rendered: " + MainRender.RenderedPixels.ToString() + "/" + allPixelCount.ToString());
                TimeElapsedLabel.SetText("Time elapsed: " + MainRender.RenderTimeElapsed.ToString());
                TimeLeftLabel.SetText("Time left: " + MainRender.RenderTimeLeft.ToString());
                Thread.Sleep(50);
            }
        }

        private void OnRenderEnd(Bitmap image)
        {
            pictureBox1.Image = image;
        }

        private void StartRenderButtonClick(object sender, EventArgs e)
        {
            MainRender.StartRender(1920, 1080, (int)samplesCountNumericUpDown.Value, CreateAndGetScene());

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
