//using System;
//using System.Collections.Generic;

//namespace RayTracing
//{
//    internal class Polygon
//    {
//        private Vector3f[] _points = new Vector3f[];

//        public Vector3f Position { get; set; }
//        public Material AppliedMaterial { get; protected set; }

//        public Polygon(Vector3f position, Vector3f[] points, Material material)
//        {
//            Position = position;
//            AppliedMaterial = material;

//            _points = points;
//        }

//        //compare with previous and next vertex
//        //  1. is angle acute
//        //  2. is there is no points in triangle that vertices creates
//        //if true: remove point from list and then start over
//        //if false: continue with next vertex
//        public Triangle[] Triangulate()
//        {
//            List<Vector3f> points = new List<Vector3f>(_points);

//            List<Triangle> triangles = new List<Triangle>();

//            int triangleIndexCount = 0;
//            while (points.Count > 3)
//            {
//                for (int i = 0; i < points.Count; i++)
//                {
//                    int a = i;
//                    int b = i == 0 ? points.Count - 1 : (i - 1) % points.Count;
//                    int c = (i + 1) % points.Count;
//                    if (IsPolygonPointsInTriangle(points, [b, a, c]) == false && IsAcute(points[b], points[a], points[c]))
//                    {
//                        triangles.Add(new Triangle(Position, [points[b], points[a], points[c]], AppliedMaterial));
//                        points.RemoveAt(a);
//                        continue;
//                    }
//                }
//            }

//            return triangles.ToArray();
//        }

//        private bool IsAcute(Vector3f b, Vector3f a, Vector3f c)
//        {
//            Vector3f ab = b - a;
//            Vector3f ac = a - c;

//            Vector3f normal = Vector3f.Cross(ab, ac);

//            throw new NotImplementedException();
//            return true;
//        }

//        private float CalculateTriangleSquare(Vector3f a, Vector3f b, Vector3f c)
//        {
//            return Vector3f.Cross(a - b, a - c).GetLength() / 2;
//        }

//        private bool IsPointInTriangle(Vector3f a, Vector3f b, Vector3f c, Vector3f p)
//        {
//            float square1 = CalculateTriangleSquare(a, b, c);
//            float square2 = CalculateTriangleSquare(a, b, p) + CalculateTriangleSquare(a, c, p) + CalculateTriangleSquare(b, c, p);

//            return square1 > square2 - 0.001f && square1 < square2 + 0.001f;
//        }

//        private bool IsPolygonPointsInTriangle(Vector3f[] points, int[] triangle)
//        {
//            for (int i = 0; i < points.Length; i++)
//            {
//                if (i == triangle[0] || i == triangle[1] || i == triangle[2])
//                    continue;

//                if (IsPointInTriangle(points[triangle[0]], points[triangle[1]], points[triangle[2]], points[i]))
//                {
//                    return true;
//                }
//            }

//            return false;
//        }

//        private Vector3f CalculateGlobalNormal()
//        {
//            Vector3f[] normals = new Vector3f[_points.Length];

//            for (int i = 0; i < _points.Length; i++)
//            {
//                int a = i;
//                int b = i == 0 ? _points.Length - 1 : (i - 1) % _points.Length;
//                int c = (i + 1) % _points.Length;

//                Vector3f ab = _points[a] - _points[b];
//                Vector3f cb = _points[c] - _points[b];

//                normals[i] = Vector3f.Cross(ab, cb);
//            }

//            for (int i = 0; i < _points.Length; i++)
//            {
//                int a = i;
//                int b = i == 0 ? _points.Length - 1 : (i - 1) % _points.Length;
//                int c = (i + 1) % _points.Length;

//                Vector3f ab = _points[a] - _points[b];
//                Vector3f cb = _points[c] - _points[b];

//                normals[i] = Vector3f.Cross(ab, cb);
//            }
//        }
//    }
//}
