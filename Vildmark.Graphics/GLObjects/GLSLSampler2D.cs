namespace Vildmark.Graphics.GLObjects
{
	public struct GLSLSampler2D
	{
		public GLTexture2D Texture;
		public int Index;

		public GLSLSampler2D(GLTexture2D texture, int index = 0)
		{
			Index = index;
			Texture = texture;
		}
	}
}
