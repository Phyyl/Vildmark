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
        private float width;
        private float height;

        public float Width
        {
            get => width;
            set => SetValue(ref width, value);
        }

        public float Height
        {
            get => height;
            set => SetValue(ref height, value);
        }

        public RectangleShape(float width, float height)
        {
            Width = width;
            Height = height;
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
