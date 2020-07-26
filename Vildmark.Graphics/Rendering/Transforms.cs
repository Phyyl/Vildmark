using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Vildmark.Graphics.Rendering
{
	public class Transforms
	{
		private Matrix4 matrix;
		private Vector3 translation;
		private Vector3 origin;
		private Vector3 rotation;
		private Vector3 scale= new Vector3(1);
		private Vector3 translationScale = new Vector3(1);

		public Transforms()
		{
			Recalculate();
		}

		public Vector3 Translation
		{
			get => translation;
			set
			{
				translation = value;
				Recalculate();
			}
		}

		public Vector3 Origin
		{
			get => origin;
			set
			{
				origin = value;
				Recalculate();
			}
		}

		public Vector3 Rotation
		{
			get => rotation;
			set
			{
				rotation = value;
				Recalculate();
			}
		}

		public Vector3 Scale
		{
			get => scale;
			set
			{
				scale = value;
				Recalculate();
			}
		}

		public Vector3 TranslationScale
		{
			get => translationScale;
			set
			{
				translationScale = value;
				Recalculate();
			}
		}

		public Vector3 ForwardVector => new Vector3((float)Math.Sin(rotation.Y), 0, (float)-Math.Cos(rotation.Y)).Normalized();
		public Vector3 BackwardVector => -ForwardVector;

		public Vector3 RightVector => new Vector3((float)Math.Cos(rotation.Y), 0, (float)Math.Sin(rotation.Y));
		public Vector3 LeftVector => -RightVector;

		public Matrix4 Matrix => matrix;

		private void Recalculate()
		{
			matrix = Matrix4.CreateScale(Scale)
			* Matrix4.CreateTranslation(Translation * TranslationScale)
			* Matrix4.CreateTranslation(-Origin * TranslationScale)
			* Matrix4.CreateRotationY(Rotation.Y)
			* Matrix4.CreateRotationX(Rotation.X)
			* Matrix4.CreateRotationX(Rotation.Z)
			* Matrix4.CreateTranslation(Origin * TranslationScale);
		}
	}
}
