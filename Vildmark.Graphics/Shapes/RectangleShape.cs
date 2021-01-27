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
    //TODO add source rect and tex coord like circle
    public class RectangleShape : Shape
    {
        private float width;
        private float height;

        public RectangleShape(Vector2 position, Vector2 size, Vector4 color)
            : this(position, size, new Material(color))
        {
        }

        public RectangleShape(Vector2 position, Vector2 size, Material material)
             : base(material)
        {
            Transform.Position = new Vector3(position);
            Width = size.X;
            Height = size.Y;
        }

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

        protected override IEnumerable<Vertex> GenerateVertices()
        {
            RectangleF sourceRect = Material.Texture.SourceRectangle;

            yield return new Vertex(new Vector3(0, 0, 0), sourceRect.GetTopLeft());
            yield return new Vertex(new Vector3(0, Height, 0), sourceRect.GetBottomLeft());
            yield return new Vertex(new Vector3(Width, Height, 0), sourceRect.GetBottomRight());
            yield return new Vertex(new Vector3(0, 0, 0), sourceRect.GetTopLeft());
            yield return new Vertex(new Vector3(Width, Height, 0), sourceRect.GetBottomRight());
            yield return new Vertex(new Vector3(Width, 0, 0), sourceRect.GetTopRight());
        }
    }
}
