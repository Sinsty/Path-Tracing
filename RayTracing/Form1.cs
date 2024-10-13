using System;
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

            image =  Render(image);

            pictureBox1.Image = image;
        }

        private Bitmap Render(Bitmap renderMap)
        {
            #region Scene Objects

            Material sphereMaterial1 = new Material(new Vector3f(100, 0, 0));
            Material sphereMaterial2 = new Material(new Vector3f(0, 0, 100));

            Light[] light = 
            [
                new Light(new Vector3f(0, 3, 5), 1)
            ];

            Sphere[] spheres =
            [
                new Sphere(new Vector3f(0, 0, 5), 1, sphereMaterial1),
                new Sphere(new Vector3f(1.5f, 0.5f, 5), 1, sphereMaterial2),
            ];

            Camera camera = new Camera(Vector3f.Zero, 60, 1000, spheres, light);

            #endregion Scene Objects

            for (int i = 0; i < renderMap.Height; i++)
            {
                for (int j = 0; j < renderMap.Width; j++)
                {
                    renderMap.SetPixel(j, i, camera.GetPixelColor(j, i, Width, Height));
                }
            }

            return renderMap;
        }
    }
}
