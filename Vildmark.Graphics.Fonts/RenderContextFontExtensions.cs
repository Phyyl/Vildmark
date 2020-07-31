using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts
{
	public static class RenderContextFontExtensions
	{
		private static Mesh stringMesh;

		public static void RenderString(this RenderContext renderContext, string str, Font font, Vector2 position, Camera camera, float scale = 12)
		{
			Vertex[] vertices = CreateStringVertices(str, font, position, scale);

			if (stringMesh is null)
			{
				stringMesh = new Mesh(vertices);
			}
			else
			{
				stringMesh.UpdateVertices(vertices);
			}

			renderContext.Render(stringMesh, camera, new Material(font.Texture));
		}

		private static Vertex[] CreateStringVertices(string str, Font font, Vector2 position, float scale = 12)
		{
			List<Vertex> vertices = new List<Vertex>();

			Vector2 cursor = new Vector2(0, font.Size);

			foreach (var chr in str)
			{
				if (!font.Characters.TryGetValue(chr, out FontChar fontChar))
				{
					continue;
				}

				Vector2 glyphSize = new Vector2(fontChar.Width, fontChar.Height) / font.Size * scale;
				Vector2 glyphOrigin = new Vector2(fontChar.OriginX, fontChar.OriginY) / font.Size * scale;

				Vector3 vtl = new Vector3(position - glyphOrigin + cursor);
				Vector3 vtr = vtl + new Vector3(glyphSize.X, 0, 0);
				Vector3 vbl = vtl + new Vector3(0, glyphSize.Y, 0);
				Vector3 vbr = vtl + new Vector3(glyphSize.X, glyphSize.Y, 0);

				Vector2 ts = new Vector2(fontChar.Width / (float)font.Width, fontChar.Height / (float)font.Height);
				Vector2 ttl = new Vector2(fontChar.X / (float)font.Width, fontChar.Y / (float)font.Height);
				Vector2 ttr = ttl + new Vector2(ts.X, 0);
				Vector2 tbl = ttl + new Vector2(0, ts.Y);
				Vector2 tbr = ttl + ts;

				vertices.Add(new Vertex(vtl, ttl));
				vertices.Add(new Vertex(vbl, tbl));
				vertices.Add(new Vertex(vbr, tbr));
				vertices.Add(new Vertex(vtl, ttl));
				vertices.Add(new Vertex(vbr, tbr));
				vertices.Add(new Vertex(vtr, ttr));

				cursor.X += fontChar.Advance / (float)font.Size * scale;
			}

			return vertices.ToArray();
		}
	}
}
