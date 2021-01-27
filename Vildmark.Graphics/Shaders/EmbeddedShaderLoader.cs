using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
    [Register(typeof(EmbeddedShaderLoader))]
    public class EmbeddedShaderLoader
    {
        public T Load<T>(string name, Assembly assembly = null) where T : Shader, new()
        {
            assembly ??= Assembly.GetCallingAssembly();

            T result = new T();

            string vertSource = ResourceLoader.LoadEmbedded<string>($"{name}.vert", assembly);
            string fragSource = ResourceLoader.LoadEmbedded<string>($"{name}.frag", assembly);
            string geomSource = ResourceLoader.LoadEmbedded<string>($"{name}.geom", assembly);

            result.ShaderProgram = GLShaderProgram.Create(GLShader.CreateVertex(vertSource), GLShader.CreateFragment(fragSource), GLShader.CreateGeometry(geomSource));

            return result;
        }
    }
}
