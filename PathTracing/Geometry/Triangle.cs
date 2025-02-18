﻿using System;
using System.Diagnostics;
using PathTracing.CameraRendering;
using PathTracing.ThreeDimensionalTree;

namespace PathTracing.Geometry
{
    internal class Triangle : IBoundingBoxable
    {
        public Vector3f point1 => v0p;
        public Vector3f point2 => v1p;
        public Vector3f point3 => v2p;


        private Vector3f _position;
        public Vector3f Position
        {
            get { return _position; }
            set
            {
                _position = value;
                UpdatePositionedVertices();
            }
        }
        public Material AppliedMaterial { get; set; }
        public Vector3f BoundingBoxMax { get; protected set; }
        public Vector3f BoundingBoxMin { get; protected set; }

        private Vector3f v0, v1, v2;
        private Vector3f v0p, v1p, v2p;

        public Triangle(Vector3f position, Vector3f[] vertices, Material material)
        {
            Position = position;
            AppliedMaterial = material;

            v0 = vertices[0];
            v1 = vertices[1];
            v2 = vertices[2];
            UpdatePositionedVertices();

            BoundingBoxMax = new Vector3f(MathF.Max(point1.x, MathF.Max(point2.x, point3.x)),
                                          MathF.Max(point1.y, MathF.Max(point2.y, point3.y)),
                                          MathF.Max(point1.z, MathF.Max(point2.z, point3.z)));

            BoundingBoxMin = new Vector3f(MathF.Min(point1.x, MathF.Min(point2.x, point3.x)),
                                          MathF.Min(point1.y, MathF.Min(point2.y, point3.y)),
                                          MathF.Min(point1.z, MathF.Min(point2.z, point3.z)));
        }

        public bool RayIntersect(Ray ray, out HitInfo hit)
        {
            Vector3f edge1 = v1p - v0p;
            Vector3f edge2 = v2p - v0p;

            Vector3f normal = Vector3f.Cross(edge1, edge2).GetNormalized();

            if (Vector3f.Dot(normal, ray.direction) >= 0)
            {
                hit = new HitInfo();
                return false;
            }

            Vector3f directionCrossEdge2 = Vector3f.Cross(ray.direction, edge2);

            float det = Vector3f.Dot(edge1, directionCrossEdge2);

            if (MathF.Abs(det) < float.Epsilon)
            {
                hit = new HitInfo();
                return false;
            }

            float invDet = 1f / det;
            Vector3f s = ray.origin - v0p;
            float u = invDet * Vector3f.Dot(s, directionCrossEdge2);

            if (u < 0 || u > 1)
            {
                hit = new HitInfo();
                return false;
            }

            Vector3f sCrossEdge1 = Vector3f.Cross(s, edge1);
            float v = invDet * Vector3f.Dot(ray.direction, sCrossEdge1);

            if (v < 0 || u + v > 1)
            {
                hit = new HitInfo();
                return false;
            }

            float t = invDet * Vector3f.Dot(edge2, sCrossEdge1);

            if (t > 0)
            {
                hit = new HitInfo(t, ray.origin + ray.direction * t, normal);
                return true;
            }
            else
            {
                hit = new HitInfo();
                return false;
            }
        }

        private void UpdatePositionedVertices()
        {
            v0p = v0 + Position;
            v1p = v1 + Position;
            v2p = v2 + Position;
        }
    }
}
