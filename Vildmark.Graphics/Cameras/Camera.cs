using OpenTK.Mathematics;
using System;
using System.Drawing;
using System.Net.Http.Json;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Transactions;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Resources;
using Vildmark.Maths;

namespace Vildmark.Graphics.Cameras
{
    public abstract class Camera
    {
        private Matrix4? projectionMatrix;

        private int width;
        private int height;
        private float zNear;
        private float zFar;

        private Transform transform = new Transform
        {
            Inverse = true
        };

        public ref Transform Transform => ref transform;

        public int Width
        {
            get => width;
            set => SetValue(ref width, value);
        }

        public int Height
        {
            get => height;
            set => SetValue(ref height, value);
        }

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

        public RectangleF Viewport => new RectangleF(transform.X * transform.Scale, transform.Y * transform.Scale, Width / transform.Scale, Height / transform.Scale);

        public Matrix4 ProjectionMatrix => projectionMatrix ??= CreateProjectionMatrix();

        public Matrix4 ViewMatrix => Transform.Matrix;

        public float AspectRatio => width / (float)height;

        protected Camera(int width, int height, float zNear, float zFar)
        {
            Width = width;
            Height = height;
            ZNear = zNear;
            ZFar = zFar;
        }

        protected abstract Matrix4 CreateProjectionMatrix();

        protected void SetValue<T>(ref T field, T value)
        {
            field = value;
            projectionMatrix = null;
        }
    }
}
