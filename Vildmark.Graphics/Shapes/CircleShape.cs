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

        public CircleShape(Vector2 position, float radius, Material material, int sides = 36)
            : base(material, new Vector3(position))
        {
            Radius = radius;
            Sides = sides;
        }

        protected override IEnumerable<Vertex> GenerateVertices()
        {
            Vector2 texSize = Material.Texture.SourceRectangle.Size.ToVector();
            Vector2 texPos = Material.Texture.SourceRectangle.Location.ToVector();

            IEnumerable<Vertex> GetCircleVertices(int i)
            {
                float angle = i / (float)Sides * MathHelper.TwoPi;
                float angle2 = (i + 1) / (float)Sides * MathHelper.TwoPi;

                Vertex GenerateVertex(float angle)
                {
                    float cos = (float)Math.Cos(angle);
                    float sin = (float)Math.Sin(angle);

                    Vector2 pos = new Vector2(cos, sin);

                    return new Vertex(new Vector3(pos) * Radius, ((pos + Vector2.One) / 2) * texSize + texPos);
                }

                yield return new Vertex(new Vector3(0, 0, 0), texSize / 2 + texPos);
                yield return GenerateVertex(i / (float)Sides * MathHelper.TwoPi);
                yield return GenerateVertex((i + 1) / (float)Sides * MathHelper.TwoPi);
            }

            return Enumerable.Range(0, Sides).SelectMany(GetCircleVertices);
        }
    }
}
