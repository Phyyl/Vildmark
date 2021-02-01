using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Vildmark.Graphics.Models;

namespace Vildmark.Graphics.Shapes
{
    public class RectangleShape : Shape
    {
        private Vector2 size;

        public float Width
        {
            get => size.X;
            set => SetValue(ref size.X, value);
        }

        public float Height
        {
            get => size.Y;
            set => SetValue(ref size.Y, value);
        }

        public Vector2 Size
        {
            get => size;
            set => SetValue(ref size, value);
        }

        public RectangleShape(Vector2 size)
        {
            Size = size;
        }

        public RectangleShape(float width, float height)
            : this(new Vector2(width, height))
        {
        }

        protected override IEnumerable<Vertex> GenerateVertices()
        {
            yield return new Vertex(new Vector3(0, 0, 0), Vector2.Zero);
            yield return new Vertex(new Vector3(0, Height, 0), Vector2.UnitY);
            yield return new Vertex(new Vector3(Width, Height, 0), Vector2.One);
            yield return new Vertex(new Vector3(0, 0, 0), Vector2.Zero);
            yield return new Vertex(new Vector3(Width, Height, 0), Vector2.One);
            yield return new Vertex(new Vector3(Width, 0, 0), Vector2.UnitX);
        }
    }
}
