using System;
using System.Drawing;

namespace RayTracing
{
    internal struct VectorColor
    {
        private const float _acesA = 2.51f;
        private const float _acesB = 0.03f;
        private const float _acesC = 2.43f;
        private const float _acesD = 0.59f;
        private const float _acesE = 0.14f;

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

        public static VectorColor operator /(VectorColor a, float b)
        {
            return new VectorColor(a.Rgb.x / b, a.Rgb.y / b, a.Rgb.z / b);
        }

        public static VectorColor operator +(VectorColor a, VectorColor b)
        {
            return new VectorColor(a.Rgb.x + b.Rgb.x, a.Rgb.y + b.Rgb.y, a.Rgb.z + b.Rgb.z);
        }

        public VectorColor(float red, float green, float blue)
        {
            Rgb = new Vector3f(red, green, blue);
        }

        public static VectorColor AcesFilmTonemapping(Vector3f x)
        {
            Vector3f tonemappedColor = (x * (_acesA * x + _acesB)) / (x * (_acesC * x + _acesA) + _acesE);
            tonemappedColor.ClampValues(0, 1);

            return new VectorColor(tonemappedColor);
        }

        public Color ToBaseColor()
		{
			return Color.FromArgb((int)(MathF.Pow(Rgb.x, 1/2.2f) * 255), (int)(MathF.Pow(Rgb.y, 1 / 2.2f) * 255), (int)(MathF.Pow(Rgb.z, 1 / 2.2f) * 255));
        }

		private Vector3f SetColor(Vector3f color)
		{
			color.ClampValues(0, 1);
            return color;
		}
	}
}
