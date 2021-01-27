using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Shapes
{
    // Add texture coordinate to the vertex data, checking source rect from Texture2D
    public class CircleShape : Shape
    {
        private int sides;
        private float radius;

        public float Radius
        {
            get => radius;
            set => SetValue(ref radius, value);
        }

        public int Sides
        {
            get => sides;
            set => SetValue(ref sides, value);
        }

        public Vector4 Color
        {
            get => Material.Tint;
            set => Material.Tint = value;
        }

        public CircleShape(Vector2 position, float radius, Vector4 color, int sides = 36)
            : base(new Material(Textures.WhitePixel, color))
        {
            Transform.Position = new Vector3(position);
            Radius = radius;
            Sides = sides;
        }

        protected override IEnumerable<Vertex> GenerateVertices()
        {
            IEnumerable<Vertex> GetCircleVertices(int i)
            {
                float angle = i / (float)Sides * MathHelper.TwoPi;
                float angle2 = (i + 1) / (float)Sides * MathHelper.TwoPi;

                if (i == 0)
                {
                    yield return new Vertex(new Vector3(0, 0, 0));
                }

                yield return new Vertex(new Vector3((float)Math.Cos(angle), (float)Math.Sin(angle), 0) * Radius);
            }

            return Enumerable.Range(0, Sides + 1).SelectMany(GetCircleVertices);
        }
    }
}
