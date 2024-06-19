using Rhino.Geometry;

namespace Rhino.Extensions.Dotbim
{
    public class Utility
    {
        public static Mesh GetSimpleMeshSurface(double width = 100, double height = 100)
        {
            Mesh mesh = new Mesh();
            int v1 = mesh.Vertices.Add(0, 0, 0);
            int v2 = mesh.Vertices.Add(width, 0, 0);
            int v3 = mesh.Vertices.Add(width, height, 0);

            mesh.Faces.AddFace(v1, v2, v3);
            mesh.Normals.ComputeNormals();
            mesh.Compact();

            return mesh;
        }
    }
}
