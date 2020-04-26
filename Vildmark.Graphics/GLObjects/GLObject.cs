using System;

namespace Vildmark.Graphics.GLObjects
{
	public abstract class GLObject : IDisposable
	{
		protected GLObject(int id)
		{
			ID = id;
		}

		protected int ID { get; }

		public void Dispose()
		{
			DisposeOpenGL();
		}

		protected abstract void DisposeOpenGL();

		public static implicit operator int(GLObject obj)
		{
			return obj?.ID ?? 0;
		}
	}
}
