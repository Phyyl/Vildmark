using OpenTK.Mathematics;
using Vildmark.Maths;

namespace Vildmark.Graphics.Cameras;

public abstract class Camera
{
    private Matrix4? projectionMatrix;

    private float zNear;
    private float zFar;

    public Transform Transform { get; } = new CameraTransform();

    public float ZNear
    {
        get => zNear;
        set => SetValue(ref zNear, value);
    }

    public float ZFar
    {
        get => zFar;
        set => SetValue(ref zFar, value);
    }

    public Matrix4 ProjectionMatrix => projectionMatrix ??= CreateProjectionMatrix();
    public Matrix4 ViewMatrix => Transform.Matrix;
    public Matrix4 Matrix => ViewMatrix * ProjectionMatrix;

    protected Camera(float zNear, float zFar)
    {
        ZNear = zNear;
        ZFar = zFar;
    }

    protected abstract Matrix4 CreateProjectionMatrix();

    protected void SetValue<T>(ref T field, T value)
    {
        if (Equals(field, value))
        {
            return;
        }

        field = value;
        projectionMatrix = null;
    }

    private class CameraTransform : Transform
    {
        protected override Matrix4 CreateMatrix()
        {
            return MatrixHelper.CreateMatrix(-Position, Rotation, Origin + Position, Scale);
        }
    }
}
