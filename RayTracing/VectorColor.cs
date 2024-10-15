using System.Drawing;

namespace RayTracing
{
    internal struct VectorColor
    {
		private Vector3f _color;

		public Vector3f Rgb
		{
			get { return _color; }
			set { _color = SetColor(value); }
		}

        public VectorColor(Vector3f rgbColor)
        {
			Rgb = rgbColor;
        }

        public static VectorColor operator *(VectorColor a, VectorColor b)
        {
            return new VectorColor(a.Rgb.x * b.Rgb.x, a.Rgb.y * b.Rgb.y, a.Rgb.z * b.Rgb.z);
        }

        public static VectorColor operator *(VectorColor a, float b)
        {
            return new VectorColor(a.Rgb.x * b, a.Rgb.y * b, a.Rgb.z * b);
        }

        public static VectorColor operator +(VectorColor a, VectorColor b)
        {
            return new VectorColor(a.Rgb.x + b.Rgb.x, a.Rgb.y + b.Rgb.y, a.Rgb.z + b.Rgb.z);
        }

        public VectorColor(float red, float green, float blue)
        {
            Rgb = new Vector3f(red, green, blue);
        }

        public Color ToBaseColor()
		{
			return Color.FromArgb((int)(Rgb.x * 255), (int)(Rgb.y * 255), (int)(Rgb.z * 255));
        }

		private Vector3f SetColor(Vector3f color)
		{
			color.ClampValues(0, 1);
            return color;
		}
	}
}
