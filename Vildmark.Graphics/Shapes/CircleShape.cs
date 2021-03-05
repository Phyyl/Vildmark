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

        public CircleShape(float radius, int sides = 36)
        {
            Radius = radius;
            Sides = sides;
        }

        protected override IEnumerable<Vertex> GenerateVertices()
        {
            IEnumerable<Vertex> GetCircleVertices(int i)
            {
                float angle = i / (float)Sides * MathHelper.TwoPi;
                float angle2 = (i + 1) / (float)Sides * MathHelper.TwoPi;

                Vertex GenerateVertex(float angle)
                {
                    float cos = (float)Math.Cos(angle);
                    float sin = (float)Math.Sin(angle);

                    Vector2 pos = new(cos, sin);

                    return new Vertex(new Vector3(pos) * Radius, pos);
                }

                yield return new Vertex(new Vector3(0, 0, 0), new Vector2(0.5f));
                yield return GenerateVertex(i / (float)Sides * MathHelper.TwoPi);
                yield return GenerateVertex((i + 1) / (float)Sides * MathHelper.TwoPi);
            }

            return Enumerable.Range(0, Sides).SelectMany(GetCircleVertices);
        }
    }
}
