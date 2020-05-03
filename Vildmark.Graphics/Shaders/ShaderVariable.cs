namespace Vildmark.Graphics.Shaders
{
	public abstract class ShaderVariable
	{
		protected ShaderVariable(IShader shader, string name)
		{
			Name = name;
			Shader = shader;
			Location = GetLocation();
		}

		public string Name { get; }

		public IShader Shader { get; }

		public int Location { get; }

		public bool Defined => Location >= 0;

		protected abstract int GetLocation();
	}
}
