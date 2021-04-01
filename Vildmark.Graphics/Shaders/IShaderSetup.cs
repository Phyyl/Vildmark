using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Graphics.Shaders
{
    public interface IShaderSetup
    {
        void SetupShader(IShader shader);
    }
}
