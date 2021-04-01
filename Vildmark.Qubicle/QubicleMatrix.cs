using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Vildmark.Graphics;
using Vildmark.Graphics.Helpers;

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

		public Mesh<ColorVertex> CreateMesh(float scale = 1)
		{
			List<ColorVertex> vertices = new();

            ColorVertex ConvertToColorVertex(Vertex vertex) => new ColorVertex(vertex.Position * scale, vertex.Color);

            for (int z = 0; z < Size.Z; z++)
			{
				for (int y = 0; y < Size.Y; y++)
				{
					for (int x = 0; x < Size.X; x++)
					{
						QubicleVoxel voxel = this[x, y, z];

						if (voxel.LeftVisible)
						{
							vertices.AddRange(CubeHelper.GetLeftVertices(new Vector3(x, y, z), Vector3.One, voxel.Color, Vector2.Zero, Vector2.One).Select(ConvertToColorVertex));
						}

						if (voxel.RightVisible)
						{
							vertices.AddRange(CubeHelper.GetRightVertices(new Vector3(x, y, z), Vector3.One, voxel.Color, Vector2.Zero, Vector2.One).Select(ConvertToColorVertex));
						}

						if (voxel.BottomVisible)
						{
							vertices.AddRange(CubeHelper.GetBottomVertices(new Vector3(x, y, z), Vector3.One, voxel.Color, Vector2.Zero, Vector2.One).Select(ConvertToColorVertex));
						}

						if (voxel.TopVisible)
						{
							vertices.AddRange(CubeHelper.GetTopVertices(new Vector3(x, y, z), Vector3.One, voxel.Color, Vector2.Zero, Vector2.One).Select(ConvertToColorVertex));
						}

						if (voxel.BackVisible)
						{
							vertices.AddRange(CubeHelper.GetBackVertices(new Vector3(x, y, z), Vector3.One, voxel.Color, Vector2.Zero, Vector2.One).Select(ConvertToColorVertex));
						}

						if (voxel.FrontVisible)
						{
							vertices.AddRange(CubeHelper.GetFrontVertices(new Vector3(x, y, z), Vector3.One, voxel.Color, Vector2.Zero, Vector2.One).Select(ConvertToColorVertex));
						}
					}
				}
			}

			return new Mesh<ColorVertex>(vertices.ToArray());
		}
	}
}
