namespace Vildmark.Graphics.Shaders
{
	public abstract class ShaderVariable
	{
		protected ShaderVariable(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public int Location { get; internal set; } = -1;

		public bool Defined => Location >= 0;

		public bool Enabled { get; set; } = true;

		public override string ToString()
		{
			return $"Name: {Name}, Location: {Location}";
		}
	}
}
