namespace Vildmark.Graphics.Cameras;

public class OrthographicOffCenterCamera : Camera
{
    private float left;
    private float right;
    private float bottom;
    private float top;

    public float Left
    {
        get => left;
        set => SetValue(ref left, value);
    }

    public float Right
    {
        get => right;
        set => SetValue(ref right, value);
    }

    public float Bottom
    {
        get => bottom;
        set => SetValue(ref bottom, value);
    }

    public float Top
    {
        get => top;
        set => SetValue(ref top, value);
    }

    public OrthographicOffCenterCamera(float left, float right, float bottom, float top, float zNear = 1, float zFar = -1)
        : base(zNear, zFar)
    {
        Left = left;
        Right = right;
        Bottom = bottom;
        Top = top;
    }

    protected override Matrix4 CreateProjectionMatrix()
    {
        return Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, ZNear, ZFar);
    }
}
