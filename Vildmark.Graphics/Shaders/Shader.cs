using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Shaders
{
    public abstract class Shader<TVertex, TMaterial> : IShader<TMaterial>
        where TVertex : unmanaged
    {
        private record class Attrib(int Size, VertexAttribPointerType Type);

        private readonly Dictionary<string, Attrib> attribs = new();
        private readonly GLShaderProgram shaderProgram;

        protected Shader(string vertexShaderSource, string fragmentShaderSource, string? geometryShaderSource = default)
        {
            shaderProgram = GLShaderProgram.Create(
                GLShader.CreateVertex(vertexShaderSource),
                GLShader.CreateFragment(fragmentShaderSource),
                GLShader.CreateGeometry(geometryShaderSource)) ??
                throw new Exception("Could not create shader program");

            InitializeAttribs();
        }

        public void Use() => shaderProgram?.Use();

        public void Uniform<T>(string name, T value) => shaderProgram.Uniform(name, value);
        public void Uniform<T>(int location, T value) => shaderProgram.Uniform(location, value);

        public int GetAttribLocation(string name) => shaderProgram?.GetAttribLocation(name) ?? -1;
        public int GetUniformLocation(string name) => shaderProgram?.GetUniformLocation(name) ?? -1;

        protected abstract void SetupUniforms(TMaterial material, Camera camera, Transform transform);
        protected abstract void SetupAttribs();

        public void Begin(TMaterial material, Camera camera, Transform transform)
        {
            Use();
            SetupUniforms(material, camera, transform);
        }

        public void Initialize(GLVertexArray vertexArray)
        {
            Use();
            vertexArray.Bind();
            SetupAttribs();
        }

        public VertexAttribPointerType GetAttribType(string name)
        {
            return attribs.GetValueOrDefault(name)?.Type ?? VertexAttribPointerType.Float;
        }

        public int GetAttribSize(string name)
        {
            return attribs.GetValueOrDefault(name)?.Size ?? 1;
        }

        protected unsafe void AttribPointer(string attribName, string fieldName)
        {
            VertexAttribPointerType type = GetAttribType(attribName);
            int size = GetAttribSize(attribName);

            AttribPointer(GetAttribLocation(attribName), size, type, sizeof(TVertex), (int)Marshal.OffsetOf<TVertex>(fieldName));
        }

        protected void AttribPointer(string attribName, int size, VertexAttribPointerType type, int stride, int offset) => AttribPointer(GetAttribLocation(attribName), size, type, stride, offset);
        protected void AttribPointer(int index, int size, VertexAttribPointerType type, int stride, int offset)
        {
            GL.VertexAttribPointer(index, size, type, false, stride, offset);
            GL.EnableVertexAttribArray(index);
        }

        protected void AttribPointer<T>(string attribName, int stride, int offset) where T : unmanaged => AttribPointer<T>(GetAttribLocation(attribName), stride, offset);
        protected void AttribPointer<T>(int index, int stride, int offset)
            where T : unmanaged
        {
            AttribPointer(index, StructTypeInfo.GetAttribSize<T>(), StructTypeInfo.GetAttribType<T>(), stride, offset);
        }

        private void InitializeAttribs()
        {
            GL.GetProgram(shaderProgram.ID, GetProgramParameterName.ActiveAttributes, out int count);

            for (int i = 0; i < count; i++)
            {
                GL.GetActiveAttrib(shaderProgram.ID, i, 256, out int length, out int size, out ActiveAttribType activeAttribType, out string name);

                attribs[name] = activeAttribType switch
                {
                    ActiveAttribType.FloatVec2 => new(2, VertexAttribPointerType.Float),
                    ActiveAttribType.FloatVec3 => new(3, VertexAttribPointerType.Float),
                    ActiveAttribType.FloatVec4 => new(4, VertexAttribPointerType.Float),
                    ActiveAttribType.Int => new(1, VertexAttribPointerType.Int),
                    ActiveAttribType.UnsignedInt => new(1, VertexAttribPointerType.UnsignedInt),
                    _ => new(1, VertexAttribPointerType.Float),
                };
            }
        }
    }
}
