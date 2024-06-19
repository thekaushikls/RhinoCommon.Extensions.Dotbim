namespace Rhino.Extensions.Dotbim.UnitTests
{
    public class MeshExtensionsTests
    {
        [Fact]
        public void GenerateSingleFaceTriangulatedMesh()
        {
            // Arrange
            double
                expectedArea = 5000,
                expectedHypotenuseLength = 141.421,
                tolerance = 0.001;
            // Act
            Rhino.Geometry.Mesh rhinoMesh = Utility.GetSimpleMeshSurface(100, 100);
            // Assert
            Assert.True(rhinoMesh.IsValid);
            Assert.True(rhinoMesh.Vertices[rhinoMesh.Faces[0].A].DistanceTo(rhinoMesh.Vertices[rhinoMesh.Faces[0].C]) - expectedHypotenuseLength < tolerance);
            Assert.True(Rhino.Geometry.AreaMassProperties.Compute(rhinoMesh).Area - expectedArea < tolerance);
        }

        [Fact]
        public void GetCoordinatesFromRhinoMesh()
        {
            // Arrange
            Rhino.Geometry.Mesh rhinoMesh = Utility.GetSimpleMeshSurface(100, 100);
            List<List<double>> expectedCoordinates =
            [
                [0  , 0  , 0],    // {0} Bottom-Left
                [100, 0  , 0],    // {1} Bottom-Right
                [100, 100, 0],    // {2} Top-Right
                [0  , 100, 0],    // {3} Top-Left
            ];
            // Act
            List<List<double>> resultCoordinates = rhinoMesh.GetAllVerticesByFaceAsCoordinates();
            // Assert
            Assert.Equal(expectedCoordinates, resultCoordinates);
        }

        [Fact]
        public void RhinoMeshToDotbimMesh()
        {
            // Arrange
            Rhino.Geometry.Mesh rhinoMesh = Utility.GetSimpleMeshSurface(100, 100);
            
            dotbim.Mesh dotbimMesh = new dotbim.Mesh();
            dotbimMesh.MeshId = 0;

            // Add Coordinates
            List<List<double>> coordinates = rhinoMesh.GetAllVerticesByFaceAsCoordinates();

            dotbimMesh.Coordinates = new List<double>();
            dotbimMesh.Indices = new List<int>();
            for (int faceIndex = 0; faceIndex < coordinates.Count; faceIndex++)
            {
                dotbimMesh.Indices.Add(faceIndex);
                dotbimMesh.Coordinates.AddRange(coordinates[faceIndex]);
            }

            // Act
            dotbim.Mesh resultMesh = rhinoMesh.ConvertToDotbimMesh();

            // Assert
            throw new NotImplementedException();
        }

        [Fact]
        public void RhinoMeshFromDotbimMesh()
        {
            throw new NotImplementedException();
        }
    }
}