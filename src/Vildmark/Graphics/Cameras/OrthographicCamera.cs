using OpenTK.Mathematics;

namespace Vildmark.Graphics.Cameras;

public class OrthographicCamera : Camera
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

    public OrthographicCamera(float width, float height, float zNear = 1, float zFar = -1)
        : base(zNear, zFar)
    {
        Width = width;
        Height = height;
    }

    protected override Matrix4 CreateProjectionMatrix()
    {
        return Matrix4.CreateOrthographic(width, height, ZNear, ZFar);
    }
}
