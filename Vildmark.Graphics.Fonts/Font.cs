using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts
{
	//Generator: https://evanw.github.io/font-texture-generator/
	//Chars (include space): !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~ ¡¢£¥▌"cª«¬-r_°±²3'µ·,1º»¼½_¿AAAAÄÅÆÇEÉEEIIIIDÑOOOOÖxOUUUÜY_ßàáâaäåæçèéêëìíîïdñòóôoö÷oùúûüy_ÿ
	public class Font
	{
		private static Font arial;

		public static Font Arial => arial ??= EmbeddedResources.Get<Font>("arial", typeof(Font).Assembly);

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
