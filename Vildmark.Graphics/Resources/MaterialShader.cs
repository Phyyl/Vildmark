﻿using OpenToolkit.Mathematics;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Resources
{
	internal class MaterialShader : EmbeddedShader
	{
		public MaterialShader()
			: base("material")
		{
			ProjectionMatrix = new Uniform<Matrix4>(this, "projection_matrix");
			Tex0 = new Uniform<GLSLSampler2D>(this, "tex0");
			Tint = new Uniform<Vector4>(this, "tint");

			Position = new Attrib<Vertex>(this, "vert_position", 3);
			TexCoord = new Attrib<Vertex>(this, "vert_texcoord", 2);
			Color = new Attrib<Vertex>(this, "vert_color", 4);
			Normal = new Attrib<Vertex>(this, "vert_normal", 3);
		}

		public Attrib<Vertex> Position { get; }

		public Attrib<Vertex> TexCoord { get; }

		public Attrib<Vertex> Color { get; }

		public Attrib<Vertex> Normal { get; }

		public Uniform<Matrix4> ProjectionMatrix { get; }

		public Uniform<GLSLSampler2D> Tex0 { get; }

		public Uniform<Vector4> Tint { get; }
	}
}