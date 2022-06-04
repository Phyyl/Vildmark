using OpenTK.Mathematics;

namespace Vildmark.Graphics.Cameras;

public class PerspectiveCamera : Camera
{
    private float fovY;
    private float aspectRatio;

    public float FovY
    {
        get => fovY;
        set => SetValue(ref fovY, value);
    }

    public float AspectRatio
    {
        get => aspectRatio;
        set => SetValue(ref aspectRatio, value);
    }

    public PerspectiveCamera(float aspectRatio, float fovY = MathF.PI / 3f, float zNear = 0.01f, float zFar = 1000)
        : base(zNear, zFar)
    {
        FovY = fovY;
        AspectRatio = aspectRatio;
    }

    protected override Matrix4 CreateProjectionMatrix()
    {
        return Matrix4.CreatePerspectiveFieldOfView(FovY, AspectRatio, ZNear, ZFar);
    }
}
