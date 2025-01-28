using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using PathTracing.CameraRendering;
using PathTracing.Geometry;

namespace PathTracing
{
    public partial class RenderForm : Form
    {
        private Thread _renderStatsThread;

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
            Material lightMaterial = new Material(new VectorColor(1f, 1f, 1f), 1f, 0f, 25f);
            Material sphereMaterial2 = new Material(new VectorColor(0.7f, 0.5f, 1f), 0.5f, 0.8f, 0f, new VectorColor(0.0337f, 0.0337f, 0.0337f));
            Material meshMaterial = new Material(new VectorColor(1f, 1f, 1f), 0.5f, 0.5f, 0f);
            Material teapotMaterial = new Material(new VectorColor(0f, 1f, 1f), 0.5f, 0.5f, 0f);

            Material cornellLeft = new Material(new VectorColor(1f, 0f, 0f), 1f, 0f, 0f);
            Material cornellRight = new Material(new VectorColor(0f, 1f, 0f), 1f, 0f, 0f);
            Material cornellMain = new Material(new VectorColor(1f, 1f, 1f), 1f, 0f, 0f, new VectorColor(0.0337f, 0.0337f, 0.0337f));

            ICameraRenderObject[] sceneObjects =
            [
                //new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 0), new Vector3f(0, 5, 0), new Vector3f(0, 5, 5)], cornellLeft),
                //new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 0), new Vector3f(0, 5, 5), new Vector3f(0, 0, 5)], cornellLeft),

                //new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(5, 5, 5), new Vector3f(5, 5, 0), new Vector3f(5, 0, 0)], cornellRight),
                //new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(5, 0, 5), new Vector3f(5, 5, 5), new Vector3f(5, 0, 0)], cornellRight),

                //new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 0), new Vector3f(0, 0, 5), new Vector3f(5, 0, 5)], cornellMain),
                //new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 0), new Vector3f(5, 0, 5), new Vector3f(5, 0, 0)], cornellMain),

                //new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(5, 5, 5), new Vector3f(0, 5, 5), new Vector3f(0, 5, 0)], cornellMain),
                //new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(5, 5, 0), new Vector3f(5, 5, 5), new Vector3f(0, 5, 0)], cornellMain),

                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(3f, 4.99f, 3f), new Vector3f(2f, 4.99f, 3f), new Vector3f(2f, 4.99f, 2f)], lightMaterial),
                new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(3f, 4.99f, 2f), new Vector3f(3f, 4.99f, 3f), new Vector3f(2f, 4.99f, 2f)], lightMaterial),

                //new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 5), new Vector3f(0, 5, 5f), new Vector3f(5, 5, 5f)], cornellMain),
                //new Triangle(new Vector3f(-2.5f, -2.5f, -2.5f), [new Vector3f(0, 0, 5), new Vector3f(5, 5, 5f), new Vector3f(5, 0, 5)], cornellMain),

                //new Sphere(new Vector3f(0, 3.25f, 0), 1f, lightMaterial),

                //new Sphere(new Vector3f(0, 0, 0), 1f, sphereMaterial2),
                new Mesh(new Vector3f(0, 0, 2f), @"D:\YLink\RayTracing\RayTracing\src\Skull.obj", teapotMaterial)
            ];

            Camera camera = new Camera(new Vector3f(0f, 0f, -5f), 60, 1000, 
                                       (int)imageWidthNumericUpDown.Value, 
                                       (int)imageHeightNumericUpDown.Value, 
                                       (int)rayBouncesNumericUpDown.Value, 
                                       (int)samplesCountNumericUpDown.Value);

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
            MainRender.StartRender(1920, 1080, CreateAndGetScene());

            if (_renderStatsThread == null)
            {
                _renderStatsThread = new Thread(UpdateRenderStats);
                _renderStatsThread.IsBackground = true;
                _renderStatsThread.Priority = ThreadPriority.Lowest;
                _renderStatsThread.Start();
            }
        }

        private void saveImageButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPG Image|*.jpg";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }
    }
}
