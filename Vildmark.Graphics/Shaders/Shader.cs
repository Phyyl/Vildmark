using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Models;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
    public abstract class Shader
    {
        private GLShaderProgram shaderProgram;

        public GLShaderProgram ShaderProgram
        {
            get => shaderProgram;
            internal set
            {
                shaderProgram = value;
                InitializeUniforms();
                InitializeAttributes();
            }
        }

        public Shader()
        {
        }

        public int GetAttribLocation(string name)
        {
            return ShaderProgram?.GetAttribLocation(name) ?? -1;
        }

        public int GetUniformLocation(string name)
        {
            return ShaderProgram?.GetUniformLocation(name) ?? -1;
        }

        public IDisposable Use()
        {
            if (ShaderProgram is null)
            {
                return null;
            }

            IDisposable result = ShaderProgram.Use();

            return result;
        }

        private void InitializeUniforms()
        {
            IEnumerable<PropertyInfo> properties = GetType().GetProperties().Where(p => typeof(Uniform).IsAssignableFrom(p.PropertyType));

            foreach (var prop in properties)
            {
                Uniform uniform = prop.GetValue(this) as Uniform;

                if (uniform is null || string.IsNullOrWhiteSpace(uniform.Name))
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
