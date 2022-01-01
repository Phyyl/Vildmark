using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Vildmark.Graphics;
using Vildmark.Graphics.Helpers;
using Vildmark.Graphics.Meshes;
using OpenTK.Graphics.ES20;

namespace Vildmark.Qubicle
{
    public class QubicleMatrix
    {
        public string Name { get; }

        public Vector3i Size { get; }

        public Vector3i Position { get; }

        public QubicleVoxel[,,] Voxels { get; }

        public QubicleVoxel this[int x, int y, int z] => Voxels[x, y, z];

        public QubicleMatrix(string name, Vector3i size, Vector3i position, QubicleVoxel[,,] voxels)
        {
            Name = name;
            Size = size;
            Position = position;
            Voxels = voxels;
        }

        public IMesh CreateMesh(float scale = 1)
        {
            List<Vertex> vertices = new();

            Vector3 size = new Vector3(scale);

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

            return new Mesh<Vertex>(vertices.ToArray());
        }
    }
}
