using Newtonsoft.Json;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts
{
    public class FontChar
	{
		[JsonIgnore]
		public Texture2D Texture { get; internal set; }

		[JsonProperty("x")]
		public int X { get; set; }

		[JsonProperty("y")]
		public int Y { get; set; }

		[JsonProperty("width")]
		public int Width { get; set; }

		[JsonProperty("height")]
		public int Height { get; set; }

		[JsonProperty("originX")]
		public int OriginX { get; set; }

		[JsonProperty("originY")]
		public int OriginY { get; set; }

		[JsonProperty("advance")]
		public int Advance { get; set; }
	}
}
