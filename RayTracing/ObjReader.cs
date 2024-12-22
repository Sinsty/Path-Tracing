using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RayTracing
{
    internal class ObjReader
    {
        //<summary>
        //1 - vertices
        //2 - faces (Vertices indexes)
        //</summary>
        static public (Vector3f[], int[]) Parse(string path)
        {
            string[] objLines = File.ReadAllLines(path);

            string file = File.ReadAllText(path);

            List<Vector3f> vertices = new List<Vector3f>();
            List<int> faces = new List<int>();

            foreach (string line in objLines)
            {
                if (line.StartsWith("v "))
                {
                    vertices.Add(ParseVertices(line));
                }

                if (line.StartsWith("f "))
                {
                    int[] face = ParseFaces(line);
                    faces.Add(face[0]);
                    faces.Add(face[1]);
                    faces.Add(face[2]);
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

        private static Vector3f ParseNormals(string line)
        {
            if (line.StartsWith("vn ") == false)
            {
                throw new ArgumentException("Line doesn't contain normals");
            }

            line = line.Remove(0, 3);
            string[] direction = line.Split(" ");

            float x = float.Parse(direction[0], CultureInfo.InvariantCulture.NumberFormat);
            float y = float.Parse(direction[1], CultureInfo.InvariantCulture.NumberFormat);
            float z = float.Parse(direction[2], CultureInfo.InvariantCulture.NumberFormat);

            return new Vector3f(x, y, z);
        }

        //<summary>
        //1 - faces
        //2 - normals
        //</summary>
        private static int[] ParseFaces(string line)
        {
            if (line.StartsWith("f ") == false)
            {
                throw new ArgumentException("Line doesn't contain faces");
            }

            line = line.Remove(0, 2);
            string[] vertices = line.Split(" ");

            int x = int.Parse(vertices[0].Split("/")[0], CultureInfo.InvariantCulture.NumberFormat);
            int y = int.Parse(vertices[1].Split("/")[0], CultureInfo.InvariantCulture.NumberFormat);
            int z = int.Parse(vertices[2].Split("/")[0], CultureInfo.InvariantCulture.NumberFormat);

            return [x, y, z];
        }
    }
}
