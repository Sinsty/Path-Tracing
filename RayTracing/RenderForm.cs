using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using RayTracing.CameraRendering;
using RayTracing.Geometry;

namespace RayTracing
{
    public partial class RenderForm : Form
    {
        private Thread _renderStatesThread;

        public RenderForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            MainRender.OnEndRendering += OnRenderEnd;
        }

        private Scene CreateAndGetScene()
        {
            Material lightMaterial = new Material(new VectorColor(1f, 1f, 1f), 1f, 0f, 1000f);
            Material sphereMaterial2 = new Material(new VectorColor(1f, 1f, 1f), 0.5f, 0.75f, 0f);
            Material meshMaterial = new Material(new VectorColor(0.2f, 0.2f, 1f), 1f, 0f, 0f);

            Material cornellLeft = new Material(new VectorColor(1f, 0f, 0f), 1f, 0f, 0f);
            Material cornellRight = new Material(new VectorColor(0f, 1f, 0f), 1f, 0f, 0f);
            Material cornellMain = new Material(new VectorColor(1f, 1f, 1f), 1f, 0f, 0f);

            ICameraRenderObject[] sceneObjects =
            [
                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 0), new Vector3f(0, 5, 0), new Vector3f(0, 5, 5)], cornellLeft),
                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 0), new Vector3f(0, 5, 5), new Vector3f(0, 0, 5)], cornellLeft),

                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(5, 5, 5), new Vector3f(5, 5, 0), new Vector3f(5, 0, 0)], cornellRight),
                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(5, 0, 5), new Vector3f(5, 5, 5), new Vector3f(5, 0, 0)], cornellRight),

                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 0), new Vector3f(0, 0, 5), new Vector3f(5, 0, 5)], cornellMain),
                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 0), new Vector3f(5, 0, 5), new Vector3f(5, 0, 0)], cornellMain),

                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(5, 5, 5), new Vector3f(0, 5, 5), new Vector3f(0, 5, 0)], cornellMain),
                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(5, 5, 0), new Vector3f(5, 5, 5), new Vector3f(0, 5, 0)], cornellMain),

                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 5), new Vector3f(0, 5, 5), new Vector3f(5, 5, 5)], cornellMain),
                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 5), new Vector3f(5, 5, 5), new Vector3f(5, 0, 5)], cornellMain),

                new Sphere(new Vector3f(0, 5.25f, 0), 3, lightMaterial),
                new Mesh(new Vector3f(0, -2.5f, 0), @"D:\YLink\RayTracing\RayTracing\src\Dragon871414.obj", meshMaterial)
            ];

            Camera camera = new Camera(new Vector3f(0f, 0f, -5f), 60, 1000, (int)imageWidthNumericUpDown.Value, (int)imageHeightNumericUpDown.Value, (int)rayBouncesNumericUpDown.Value);

            Scene scene = new Scene(sceneObjects, camera);
            if (useKDTreeCheckBox.Checked)
            {
                scene.GenerateKDTree((int)kdTreeMaxDepthNumericUpDown.Value,
                                     (int)sahPlanesDivisionsNumericUpDown.Value,
                                     (int)minObjectsInBoundingBoxNumericUpDown.Value);
            }

            return scene;
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
