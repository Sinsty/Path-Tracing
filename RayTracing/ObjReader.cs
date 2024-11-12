using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RayTracing
{
    internal class ObjReader
    {
        static public (Vector3f[], Vector3f[]) Parse(string path)
        {
            string[] objLines = File.ReadAllLines(path);

            string file = File.ReadAllText(path);

            List<Vector3f> vertices = new List<Vector3f>();
            List<Vector3f> faces = new List<Vector3f>();

            foreach (string line in objLines)
            {
                if (line.StartsWith("v "))
                {
                    vertices.Add(ParseVertices(line));
                }

                if (line.StartsWith("f "))
                {
                    faces.Add(ParseFaces(line));
                }
            }

            return (vertices.ToArray(), faces.ToArray());
        }

        private static Vector3f ParseVertices(string line)
        {
            if (line.StartsWith("v ") == false)
            {
                throw new ArgumentException("Line doesn't contain vertices");
            }

            line = line.Remove(0, 2);
            string[] coords = line.Split(" ");

            float x = float.Parse(coords[0], CultureInfo.InvariantCulture.NumberFormat);
            float y = float.Parse(coords[1], CultureInfo.InvariantCulture.NumberFormat);
            float z = float.Parse(coords[2], CultureInfo.InvariantCulture.NumberFormat);

            return new Vector3f(x, y, z);
        }

        private static Vector3f ParseFaces(string line)
        {
            if (line.StartsWith("f ") == false)
            {
                throw new ArgumentException("Line doesn't contain faces");
            }

            line = line.Remove(0, 2);
            string[] vertices = line.Split(" ");

            float x = int.Parse(vertices[0].Split("/")[0], CultureInfo.InvariantCulture.NumberFormat);
            float y = int.Parse(vertices[1].Split("/")[0], CultureInfo.InvariantCulture.NumberFormat);
            float z = int.Parse(vertices[2].Split("/")[0], CultureInfo.InvariantCulture.NumberFormat);

            return new Vector3f(x, y, z);
        }
    }
}
