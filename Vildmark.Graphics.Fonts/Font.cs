using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Fonts
{
	public class Font
	{
		[JsonIgnore]
		public GLTexture2D Texture { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("size")]
		public int Size { get; set; }

		[JsonProperty("bold")]
		public bool Bold { get; set; }

		[JsonProperty("italic")]
		public bool Italic { get; set; }

		[JsonProperty("width")]
		public int Width { get; set; }

		[JsonProperty("height")]
		public int Height { get; set; }

		[JsonProperty("characters")]
		public Dictionary<char, FontChar> Characters { get; set; }
	}
}
