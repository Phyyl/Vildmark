using OpenTK.Mathematics;
using System.Reflection;
using System.Text;
using Vildmark.Resources;

namespace Vildmark.Qubicle
{
    [Register(typeof(IResourceLoader<QubicleMatrixCollection>))]
	public class QubicleMatrixLoader : IResourceLoader<QubicleMatrixCollection>
	{
		public QubicleMatrixCollection Load(Stream stream, Assembly? assembly, string? resourceName)
		{
			QubicleReader reader = new(stream);

			return new QubicleMatrixCollection(reader.ReadMatrices());
		}

		private class QubicleReader
		{
			private readonly BinaryReader reader;

			public Version? Version { get; private set; }

			public bool Bgra { get; private set; }

			public bool RightHandedZAxis { get; private set; }

			public bool Compressed { get; private set; }

			public bool VisibilityMaskEncoded { get; private set; }

			public uint NumMatrices { get; private set; }

			public QubicleReader(Stream stream)
			{
				reader = new BinaryReader(stream);

				ReadHeader();
			}

			public IEnumerable<QubicleMatrix> ReadMatrices()
			{
				for (int i = 0; i < NumMatrices; i++)
				{
					yield return ReadMatrix();
				}
			}

			private uint ReadUInt() => reader.ReadUInt32();
			private int ReadInt() => reader.ReadInt32();
			private byte ReadByte() => reader.ReadByte();
			private byte[] ReadBytes(int length) => reader.ReadBytes(length);
			private Version ReadVersion() => new(ReadByte(), ReadByte(), ReadByte(), ReadByte());
			private QubicleVoxel GetVoxel(uint value) => Bgra ? QubicleVoxel.FromBGRA(value) : QubicleVoxel.FromRGBA(value);
			private QubicleVoxel ReadVoxel() => GetVoxel(ReadUInt());

			private string ReadString()
			{
				byte length = ReadByte();
				byte[] data = ReadBytes(length);

				return Encoding.UTF8.GetString(data);
			}

			private QubicleMatrix ReadMatrix()
			{
				string name = ReadString();
				Vector3i size = new((int)ReadUInt(), (int)ReadUInt(), (int)ReadUInt());
				Vector3i position = new(ReadInt(), ReadInt(), ReadInt());
				QubicleVoxel[,,] voxels = Compressed ? ReadCompressedVoxels(size) : ReadUncompressed(size);

				return new QubicleMatrix(name, size, position, voxels);
			}

			private QubicleVoxel[,,] ReadCompressedVoxels(Vector3i size)
			{
				//throw new NotImplementedException();

				const uint CODEFLAG = 2;
				const uint NEXTSLICEFLAG = 6;

				QubicleVoxel[,,] voxels = new QubicleVoxel[size.X, size.Y, size.Z];

				//TODO: Check if we should start it 1 (https://getqubicle.com/learn/article.php?id=22&oid=34)
				for (int z = 0; z < size.Z; z++)
				{
					int index = 0;

					while (true)
					{
						uint data = ReadUInt();

						if (data == NEXTSLICEFLAG)
						{
							break;
						}

						if (data == CODEFLAG)
						{
							uint count = ReadUInt();
							QubicleVoxel voxel = ReadVoxel();

							for (int i = 0; i < count; i++)
							{
								int x = index % size.X;
								int y = index / size.X;

								index++;

								voxels[x, y, z] = voxel;
							}
						}
						else
						{
							int x = index % size.X;
							int y = index / size.X;

							index++;

							voxels[x, y, z] = GetVoxel(data);
						}
					}
				}

				return voxels;
			}

			private QubicleVoxel[,,] ReadUncompressed(Vector3i size)
			{
				QubicleVoxel[,,] voxels = new QubicleVoxel[size.X, size.Y, size.Z];

				for (int z = 0; z < size.Z; z++)
				{
					for (int y = 0; y < size.Y; y++)
					{
						for (int x = 0; x < size.X; x++)
						{
							voxels[x, y, z] = ReadVoxel();
						}
					}
				}

				return voxels;
			}

			private void ReadHeader()
			{
				Version = ReadVersion();
				Bgra = ReadUInt() == 1;
				RightHandedZAxis = ReadUInt() == 1;
				Compressed = ReadUInt() == 1;
				VisibilityMaskEncoded = ReadUInt() == 1;
				NumMatrices = ReadUInt();
			}
		}
	}
}
