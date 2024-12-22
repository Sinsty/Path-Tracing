using System;
using System.Windows.Forms;

namespace RayTracing
{
    internal struct Vector3f
    {
        public enum Axis
        {
            X,
            Y,
            Z,
        }

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

        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ")";
        }

        public static Vector3f Max(Vector3f a, Vector3f b)
        {
            if (a.GetLength() > b.GetLength())
                return a;

            return b;
        }

        public static Vector3f Min(Vector3f a, Vector3f b)
        {
            if (a.GetLength() < b.GetLength())
                return a;

            return b;
        }

        public static Vector3f Cross(Vector3f a, Vector3f b)
        {
            float cx = a.y * b.z - a.z * b.y;
            float cy = a.z * b.x - a.x * b.z;
            float cz = a.x * b.y - a.y * b.x;

            return new Vector3f(cx, cy, cz);
        }

        public static Vector3f MultiplyByElements(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static float Dot(Vector3f a, Vector3f b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector3f Reflect(Vector3f vector, Vector3f normal)
        {
            return vector - 2 * Dot(vector, normal) * normal;
        }

        public static Vector3f AxisToVector(Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    return Right;
                case Axis.Y:
                    return Up;
                case Axis.Z:
                    return Forward;
            }

            throw new ArgumentException(nameof(axis));
        }

        public static Vector3f operator * (Vector3f a, float b)
        {
            return new Vector3f(a.x * b, a.y * b, a.z * b);
        }

        public static Vector3f operator + (Vector3f a, float b)
        {
            return new Vector3f(a.x + b, a.y + b, a.z + b);
        }

        public static Vector3f operator * (float a, Vector3f b)
        {
            return new Vector3f(b.x * a, b.y * a, b.z * a);
        }

        public static bool operator ==(Vector3f a, Vector3f b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(Vector3f a, Vector3f b)
        {
            return a.x != b.x || a.y != b.y || a.z != b.z;
        }

        public static Vector3f operator / (Vector3f a, float b)
        {
            return new Vector3f(a.x / b, a.y / b, a.z / b);
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
            return Cross(a, b);
        }

        public static Vector3f operator / (Vector3f a, Vector3f b)
        {
            return MultiplyByElements(a, new Vector3f(1 / b.x, 1 / b.y, 1 / b.z));
        }

        public static float DegreesToRadians(float degrees)
        {
            return degrees * MathF.PI / 180f;
        }

        public static float Distance(Vector3f a, Vector3f b)
        {
            return MathF.Sqrt((a.x - b.x)*(a.x - b.x) + (a.y - b.y)*(a.y - b.y) + (a.z - b.z)*(a.z - b.z));
        }

        //Vector length (magnitude)
        public float GetLength()
        {
            return MathF.Sqrt(x*x + y*y + z*z);
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

        public float GetAxis(Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    return x;
                case Axis.Y:
                    return y;
                case Axis.Z:
                    return z;
            }

            throw new ArgumentException(nameof(axis));
        }

        public void SetAxis(Axis axis, float value)
        {
            switch (axis)
            {
                case Axis.X:
                    x = value;
                    return;
                case Axis.Y:
                    y = value;
                    return;
                case Axis.Z:
                    z = value;
                    return;
            }

            throw new ArgumentException(nameof(axis));
        }

        public static Vector3f Abs(Vector3f a)
        {
            return new Vector3f(MathF.Abs(a.x), MathF.Abs(a.y), MathF.Abs(a.z));
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

        public void Rotate(Vector3f angle, Vector3f aroundPoint)
        {
            angle.x = DegreesToRadians(angle.x);
            angle.y = DegreesToRadians(angle.y);
            angle.z = DegreesToRadians(angle.z);

            if (angle.x != 0)
            {
                float tempY = (y - aroundPoint.y) * MathF.Cos(angle.x) - (z - aroundPoint.z) * MathF.Sin(angle.x);
                z = (y - aroundPoint.y) * MathF.Sin(angle.x) + (z - aroundPoint.z) * MathF.Cos(angle.x);

                y = tempY;
            }
            if (angle.y != 0)
            {
                float tempX = (x - aroundPoint.x) * MathF.Cos(angle.y) + (z - aroundPoint.z) * MathF.Sin(angle.y);
                z = -(x - aroundPoint.x) * MathF.Sin(angle.y) + (z - aroundPoint.z) * MathF.Cos(angle.y);

                x = tempX;
            }
            if (angle.z != 0)
            {
                float tempX = (x - aroundPoint.x) * MathF.Cos(angle.z) - (y - aroundPoint.y) * MathF.Sin(angle.z);
                y = (x - aroundPoint.x) * MathF.Sin(angle.z) + (y - aroundPoint.y) * MathF.Cos(angle.z);

                x = tempX;
            }
        }

        public static Vector3f ClampValues(Vector3f vector, float min, float max)
        {
            vector.x = MathF.Max(MathF.Min(vector.x, max), min);
            vector.y = MathF.Max(MathF.Min(vector.y, max), min);
            vector.z = MathF.Max(MathF.Min(vector.z, max), min);

            return vector;
        }

        public static Vector3f ClampValuesFromVector(Vector3f vector, Vector3f minVector, Vector3f maxVector)
        {
            float x = MathF.Max(MathF.Min(vector.x, maxVector.x), minVector.x);
            float y = MathF.Max(MathF.Min(vector.y, maxVector.y), minVector.y);
            float z = MathF.Max(MathF.Min(vector.z, maxVector.z), minVector.z);

            return new Vector3f(x, y, z);
        }
    }
}
