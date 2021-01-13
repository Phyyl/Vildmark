using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Cameras
{
	public class OrthographicCamera : Camera
	{
		public float Width { get; set; }

		public float Height { get; set; }

		public float ZNear { get; }

		public float ZFar { get;}

		public override Matrix4 ProjectionMatrix => Matrix4.CreateOrthographic(Width, Height, ZNear, ZFar);

		public OrthographicCamera(float width, float height, float zNear = 1, float zFar = -1)
		{
			Width = width;
			Height = height;
			ZNear = zNear;
			ZFar = zFar;
		}

		public override void Resize(int width, int height)
		{
			Width = width;
			Height = height;
		}
	}
}
