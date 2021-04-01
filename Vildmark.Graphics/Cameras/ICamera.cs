using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Cameras
{
    public interface ICamera : IShaderSetup
    {
        Transform Transform { get; }

        float AspectRatio { get; }

        Matrix4 ProjectionMatrix { get; }
        Matrix4 ViewMatrix { get; }

        RectangleF Viewport { get; }

        int Width { get; set; }
        int Height { get; set; }
        float ZFar { get; set; }
        float ZNear { get; set; }
    }
}
