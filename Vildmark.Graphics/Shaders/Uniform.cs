using OpenToolkit.Graphics.OpenGL;
using OpenToolkit.Mathematics;
using System;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Shaders
{
	public class Uniform<T> : ShaderVariable
	{
		private Action<T> setterAction;

		public Uniform(IShader shader, string name)
			: base(shader, name)
		{
			InitializeSetterAction();
		}

		public void SetValue(T value)
		{
			if (!Defined)
			{
				return;
			}

			setterAction?.Invoke(value);
		}

		protected override int GetLocation() => Shader.GetUniformLocation(Name);

		private void InitializeSetterAction()
		{
			setterAction = default(T) switch
			{
				bool _ => CreateAction<bool>(x => GL.Uniform1(Location, x ? 1 : 0)),
				byte _ => CreateAction<byte>(x => GL.Uniform1(Location, x)),
				sbyte _ => CreateAction<sbyte>(x => GL.Uniform1(Location, x)),
				short _ => CreateAction<short>(x => GL.Uniform1(Location, x)),
				ushort _ => CreateAction<ushort>(x => GL.Uniform1(Location, x)),
				int _ => CreateAction<int>(x => GL.Uniform1(Location, x)),
				uint _ => CreateAction<uint>(x => GL.Uniform1(Location, x)),
				double _ => CreateAction<double>(x => GL.Uniform1(Location, x)),
				Vector2 _ => CreateAction<Vector2>(x => GL.Uniform2(Location, x)),
				Vector3 _ => CreateAction<Vector3>(x => GL.Uniform3(Location, x)),
				Vector4 _ => CreateAction<Vector4>(x => GL.Uniform4(Location, x)),
				Matrix2 _ => CreateAction<Matrix2>(x => GL.UniformMatrix2(Location, false, ref x)),
				Matrix3 _ => CreateAction<Matrix3>(x => GL.UniformMatrix3(Location, false, ref x)),
				Matrix4 _ => CreateAction<Matrix4>(x => GL.UniformMatrix4(Location, false, ref x)),
				GLTexture2D _ => CreateAction<GLTexture2D>(x =>
				{
					GL.Uniform1(Location, Index);
					x.Bind(Index);
				}),
				null => GetReferenceTypeSetterAction(),
				_ => null
			} ?? (x => { });
		}

		private Action<T> GetReferenceTypeSetterAction()
		{
			if (typeof(T).IsArray)
			{
				Type elementType = typeof(T).GetElementType();

				if (elementType == typeof(int))
				{
					return CreateAction<int[]>(x => GL.Uniform1(Location, x.Length, x));
				}

				if (elementType == typeof(uint))
				{
					return CreateAction<uint[]>(x => GL.Uniform1(Location, x.Length, x));
				}

				if (elementType == typeof(double))
				{
					return CreateAction<double[]>(x => GL.Uniform1(Location, x.Length, x));
				}

				if (elementType == typeof(float))
				{
					return CreateAction<float[]>(x => GL.Uniform1(Location, x.Length, x));
				}

				if (elementType == typeof(GLTexture2D))
				{
					return CreateAction<GLTexture2D[]>(x =>
					{
						foreach (var texture in x)
						{
							texture.Bind(Index);

							GL.Uniform1(Location + Index, Index);
						}
					});
				}
			}
			else if (typeof(T) == typeof(GLTexture2D))
			{
				return CreateAction<GLTexture2D>(x =>
				{
					x.Bind(Index);

					GL.Uniform1(Location, Index);
				});
			}

			return null;
		}

		private Action<T> CreateAction<T2>(Action<T2> action)
		{
			return action as Action<T>;
		}
	}
}
