using System;

namespace RayTracing
{
    internal struct Vector3f
    {
        private static readonly Vector3f _zeroVector = new Vector3f(0f, 0f, 0f);
        private static readonly Vector3f _oneVector = new Vector3f(1f, 1f, 1f);
        private static readonly Vector3f _upVector = new Vector3f(0f, 1f, 0f);
        private static readonly Vector3f _rightVector = new Vector3f(1f, 0f, 0f);
        private static readonly Vector3f _leftVector = new Vector3f(-1f, 0f, 0f);
        private static readonly Vector3f _forwardVector = new Vector3f(0f, 0f, 1f);
        private static readonly Vector3f _backwardVector = new Vector3f(0f, 0f, -1f);

        public static Vector3f Zero => _zeroVector;
        public static Vector3f One => _oneVector;
        public static Vector3f Up => _upVector;
        public static Vector3f Right => _rightVector;
        public static Vector3f Left => _leftVector;
        public static Vector3f Forward => _forwardVector;
        public static Vector3f Backward => _backwardVector;

        public float x;
        public float y;
        public float z;

        public Vector3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3f Multiply(Vector3f a, Vector3f b)
        {
            float xc = a.y * b.z - b.y * a.z;
            float yc = a.z * b.x - b.z * a.x;
            float zc = a.x * b.y - b.x * a.y;

            return new Vector3f(xc, yc, zc);
        }

        public static float Dot(Vector3f a, Vector3f b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector3f operator * (Vector3f a, float b)
        {
            return new Vector3f(a.x * b, a.y * b, a.z * b);
        }

        public static Vector3f operator * (float a, Vector3f b)
        {
            return new Vector3f(b.x * a, b.y * a, b.z * a);
        }

        public static Vector3f operator + (Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3f operator - (Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3f operator - (Vector3f a)
        {
            return new Vector3f(-a.x, -a.y, -a.z);
        }

        public static Vector3f operator * (Vector3f a, Vector3f b)
        {
            return Multiply(a, b);
        }

        public static float DegreesToRadians(float degrees)
        {
            return degrees * MathF.PI / 180f;
        }

        //Vector length (magnitude)
        public float GetLength()
        {
            return MathF.Sqrt(MathF.Pow(x, 2) + MathF.Pow(y, 2) + MathF.Pow(z, 2));
        }

        public Vector3f GetNormalized()
        {
            float length = GetLength();
            return new Vector3f(x / length, y / length, z / length);
        }

        public void Normalize()
        {
            Vector3f normalized = GetNormalized();
            x = normalized.x;
            y = normalized.y;
            z = normalized.z;
        }

        public void Add(float b)
        {
            x += b;
            y += b;
            z += b;
        }

        public void Rotate(Vector3f angle)
        {
            angle.x = DegreesToRadians(angle.x);
            angle.y = DegreesToRadians(angle.y);
            angle.z = DegreesToRadians(angle.z);

            if (angle.x != 0)
            {
                float tempY = y * MathF.Cos(angle.x) - z * MathF.Sin(angle.x);
                z = y * MathF.Sin(angle.x) + z * MathF.Cos(angle.x);

                y = tempY;
            }
            if (angle.y != 0)
            {
                float tempX = x * MathF.Cos(angle.y) + z * MathF.Sin(angle.y);
                z = -x * MathF.Sin(angle.y) + z * MathF.Cos(angle.y);

                x = tempX;
            }
            if (angle.z != 0)
            {
                float tempX = x * MathF.Cos(angle.z) - y * MathF.Sin(angle.z);
                y = x * MathF.Sin(angle.z) + y * MathF.Cos(angle.z);

                x = tempX;
            }
        }

        public void ClampValues(float min, float max)
        {
            x = MathF.Max(MathF.Min(x, max), min);
            y = MathF.Max(MathF.Min(y, max), min);
            z = MathF.Max(MathF.Min(z, max), min);
        }
    }
}
