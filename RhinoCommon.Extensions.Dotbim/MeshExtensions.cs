using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace Rhino.Extensions.Dotbim
{
    /// <summary>
    /// <see cref="https://github.com/paireks/dotbimGH/blob/master/dotbimGH/Tools.cs" />
    /// </summary>
    public static class MeshExtensions
    {
        #region Dotbim Extensions
        public static Rhino.Geometry.Mesh ConvertToRhinoMesh(this dotbim.Mesh dotbimMesh)
        {
            Rhino.Geometry.Mesh mesh = new Rhino.Geometry.Mesh();

            for (int i = 0; i < dotbimMesh.Coordinates.Count; i += 3)
            {
                mesh.Vertices.Add(dotbimMesh.Coordinates[i], dotbimMesh.Coordinates[i + 1], dotbimMesh.Coordinates[i + 2]);
            }
            for (int i = 0; i < dotbimMesh.Indices.Count; i += 3)
            {
                mesh.Faces.AddFace(dotbimMesh.Indices[i], dotbimMesh.Indices[i + 1], dotbimMesh.Indices[i + 2]);
            }

            mesh.Normals.ComputeNormals();
            mesh.Compact();

            return mesh;
        }
        #endregion Dotbim Extensions

        #region Rhino Extensions
        public static List<List<double>> GetAllVerticesByFaceAsCoordinates(this Rhino.Geometry.Mesh mesh)
        {
            List<List<double>> coordinatesByFace = new List<List<double>>();

            for (int faceIndex = 0; faceIndex < mesh.Faces.Count; faceIndex++)
            {
                MeshFace face = mesh.Faces[faceIndex];             
                List<double> coordinates = new List<double>()
                {
                    mesh.Vertices[face.A].X,
                    mesh.Vertices[face.A].Y,
                    mesh.Vertices[face.A].Z,

                    mesh.Vertices[face.B].X,
                    mesh.Vertices[face.B].Y,
                    mesh.Vertices[face.B].Z,

                    mesh.Vertices[face.C].X,
                    mesh.Vertices[face.C].Y,
                    mesh.Vertices[face.C].Z,
                };
                coordinatesByFace[faceIndex] = coordinates;
            }

            return coordinatesByFace;
        }

        public static dotbim.Mesh ConvertToDotbimMesh(this Rhino.Geometry.Mesh mesh, int meshId = 0)
        {
            dotbim.Mesh dotbimMesh = new dotbim.Mesh();
            dotbimMesh.MeshId = meshId;
            
            // Add Face Indices
            dotbimMesh.Indices = new List<int>();
            for (int i = 0; i < mesh.Faces.Count; i++)
            {
                MeshFace face = mesh.Faces[i];
                if (face.IsQuad) throw new NotImplementedException("Quad Meshes are not supported.");

                dotbimMesh.Indices.AddRange(new List<int>()
                    {
                        face.A,
                        face.B,
                        face.C,
                    });
            }

            // Add Coordinates
            dotbimMesh.Coordinates = new List<double>();
            foreach (Point3f vertex in mesh.Vertices)
            {
                dotbimMesh.Coordinates.AddRange(new List<double>()
                    {
                        vertex.X,
                        vertex.Y,
                        vertex.Z,
                    });
            }

            return dotbimMesh;
        }
        #endregion Rhino Extensions
    }
}
