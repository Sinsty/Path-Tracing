using System;
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

        public static VectorColor AcesFilmTonemapping(Vector3f color)
        {
            return new VectorColor(
                ParameterAcesFilmTonemapping(color.x), 
                ParameterAcesFilmTonemapping(color.y), 
                ParameterAcesFilmTonemapping(color.z)
                );
        }

        public static VectorColor GammaCorrection(VectorColor color)
        {
            return new VectorColor(GammaCorrection(color.Rgb));
        }

        public static Vector3f GammaCorrection(Vector3f color)
        {
            return new Vector3f(MathF.Pow(color.x, 1 / 2.2f), MathF.Pow(color.y, 1 / 2.2f), MathF.Pow(color.z, 1 / 2.2f));
        }

        private static float ParameterAcesFilmTonemapping(float x)
        {
            float tonemappedColor = (x * (2.51f * x + 0.03f)) / (x * (2.43f * x + 0.59f) + 0.14f);
            //float tonemappedColor = x / (x + 1);
            return MathF.Max(MathF.Min(tonemappedColor, 1), 0);
        }

        public Color ToBaseColor()
		{
			return Color.FromArgb((int)MathF.Round(Rgb.x * 255), (int)MathF.Round(Rgb.y * 255), (int)MathF.Round(Rgb.z * 255));
        }

		private Vector3f SetColor(Vector3f color)
		{
            return Vector3f.ClampValues(color, 0, 1);
		}
	}
}
