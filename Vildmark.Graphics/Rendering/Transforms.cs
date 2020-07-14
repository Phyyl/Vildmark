using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Graphics.Rendering
{
	public class Transforms
	{
		private Vector3 translation;
		private Vector3 origin;
		private Vector3 rotation;
		private Vector3 scale = new Vector3(1);
		private Vector3 translationScale = new Vector3(1);

		public ref Vector3 Translation => ref translation;
		public ref Vector3 Origin => ref origin;
		public ref Vector3 Rotation => ref rotation;
		public ref Vector3 Scale => ref scale;
		public ref Vector3 TranslationScale => ref translationScale;

		public Vector3 ForwardVector => new Vector3((float)Math.Sin(rotation.Y), 0, (float)-Math.Cos(rotation.Y)).Normalized();
		public Vector3 BackwardVector => -ForwardVector;

		public Vector3 RightVector => new Vector3((float)Math.Cos(rotation.Y), 0, (float)Math.Sin(rotation.Y));
		public Vector3 LeftVector => -RightVector;

		public Matrix4 Matrix =>
			Matrix4.CreateScale(Scale)
			* Matrix4.CreateTranslation(Translation * TranslationScale)
			* Matrix4.CreateTranslation(-Origin * TranslationScale)
			* Matrix4.CreateRotationY(Rotation.Y)
			* Matrix4.CreateRotationX(Rotation.X)
			* Matrix4.CreateRotationX(Rotation.Z)
			* Matrix4.CreateTranslation(Origin * TranslationScale);
	}
}
