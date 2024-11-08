using OpenTK.Mathematics;
using Vildmark.Graphics;
using Vildmark.Graphics.Meshes;
using Vildmark.Helpers;

namespace Vildmark.Qubicle;

public class QubicleMatrix(string name, Vector3i size, Vector3i position, QubicleVoxel[,,] voxels)
{
    public string Name { get; } = name;

    public Vector3i Size { get; } = size;

    public Vector3i Position { get; } = position;

    public QubicleVoxel[,,] Voxels { get; } = voxels;

    public QubicleVoxel this[int x, int y, int z] => Voxels[x, y, z];

    public Mesh<Vertex> CreateMesh(float scale = 1)
    {
        List<Vertex> vertices = [];

        Vector3 size = new(scale);

        for (int z = 0; z < Size.Z; z++)
        {
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    QubicleVoxel voxel = this[x, y, z];
                    Vector3 pos = new Vector3(x, y, z) * scale;

                    if (voxel.LeftVisible)
                    {
                        vertices.AddRange(CubeHelper.GetLeftVertices(pos, size, voxel.Color, Vector2.Zero, Vector2.One));
                    }

                    if (voxel.RightVisible)
                    {
                        vertices.AddRange(CubeHelper.GetRightVertices(pos, size, voxel.Color, Vector2.Zero, Vector2.One));
                    }

                    if (voxel.BottomVisible)
                    {
                        vertices.AddRange(CubeHelper.GetBottomVertices(pos, size, voxel.Color, Vector2.Zero, Vector2.One));
                    }

                    if (voxel.TopVisible)
                    {
                        vertices.AddRange(CubeHelper.GetTopVertices(pos, size, voxel.Color, Vector2.Zero, Vector2.One));
                    }

                    if (voxel.BackVisible)
                    {
                        vertices.AddRange(CubeHelper.GetBackVertices(pos, size, voxel.Color, Vector2.Zero, Vector2.One));
                    }

                    if (voxel.FrontVisible)
                    {
                        vertices.AddRange(CubeHelper.GetFrontVertices(pos, size, voxel.Color, Vector2.Zero, Vector2.One));
                    }
                }
            }
        }

        return new Mesh(vertices.ToArray());
    }
}
