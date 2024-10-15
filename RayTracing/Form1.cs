using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RayTracing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(1920, 1080);

            image = Render(image);

            pictureBox1.Image = image;
        }

        private Bitmap Render(Bitmap renderMap)
        {
            #region Scene Objects

            Light[] light =
            [
                new Light(new Vector3f(0, 7, 5), 1),
            ];

            Material sphereMaterial1 = new Material(new VectorColor(1, 1, 1), 1, 1);
            Material sphereMaterial2 = new Material(new VectorColor(0.8f, 0, 0.8f), 1, 0);

            Sphere[] spheres =
            [
                new Sphere(new Vector3f(0, 20, 7), 15, sphereMaterial1),
                new Sphere(new Vector3f(0, 1, 7), 1, sphereMaterial2),
                new Sphere(new Vector3f(0, -10, 7), 10, sphereMaterial2),
            ];

            Camera camera = new Camera(Vector3f.Zero, 60, 1000, spheres, light);

            #endregion Scene Objects

            for (int i = 0; i < renderMap.Height; i++)
            {
                for (int j = 0; j < renderMap.Width; j++)
                {
                    renderMap.SetPixel(j, i, camera.GetPixelColor(j, i, renderMap.Width, renderMap.Height));
                }
            }

            return renderMap;
        }
    }
}
