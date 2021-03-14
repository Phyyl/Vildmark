using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Shaders
{
    public abstract class Shader
    {
        public GLShaderProgram ShaderProgram { get; }

        protected Shader()
        {
            ShaderProgram = GLShaderProgram.Create(GLShader.CreateVertex(VertexShaderSource), GLShader.CreateFragment(FragmentShaderSource), GLShader.CreateGeometry(GeometryShaderSource));

            if (ShaderProgram is not null)
            {
                InitializeUniforms();
                InitializeAttributes();
            }
        }

        protected abstract string VertexShaderSource { get; }
        protected abstract string FragmentShaderSource { get; }
        protected virtual string GeometryShaderSource => null;

        public int GetAttribLocation(string name)
        {
            return ShaderProgram?.GetAttribLocation(name) ?? -1;
        }

        public int GetUniformLocation(string name)
        {
            return ShaderProgram?.GetUniformLocation(name) ?? -1;
        }

        public void Use()
        {
            if (ShaderProgram is null)
            {
                return;
            }

            ShaderProgram.Use();
        }

        private void InitializeUniforms()
        {
            IEnumerable<PropertyInfo> properties = GetType().GetProperties().Where(p => typeof(Uniform).IsAssignableFrom(p.PropertyType));

            foreach (var prop in properties)
            {
                if (prop.GetValue(this) is not Uniform uniform || string.IsNullOrWhiteSpace(uniform.Name))
                {
                    continue;
                }

                uniform.Location = GetUniformLocation(uniform.Name);
            }
        }

        private void InitializeAttributes()
        {
            IEnumerable<PropertyInfo> properties = GetType().GetProperties().Where(p => typeof(Attrib).IsAssignableFrom(p.PropertyType));

            foreach (var prop in properties)
            {
                Attrib attrib = prop.GetValue(this) as Attrib;

                if (attrib is null || string.IsNullOrWhiteSpace(attrib.Name))
                {
                    continue;
                }

                attrib.Location = GetAttribLocation(attrib.Name);
            }
        }
    }
}
