using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Rendering
{
    public class BatchedRenderContext2D : RenderContext2D
    {
        private readonly List<Batch> batches = new List<Batch>();

        public override void Render(Mesh mesh, Material material, MaterialShader shader, Matrix4 modelMatrix, Vector3 offset)
        {
            Batch lastBatch = batches.LastOrDefault();
            Batch batch = new Batch(material.Texture.GLTexture, shader);
            BatchItem item = new BatchItem(mesh, material, modelMatrix, offset);

            batch.Items.Add(item);

            if (batch.Equals(lastBatch))
            {
                lastBatch.Items.Add(item);
            }
            else
            {
                batches.Add(batch);
            }
        }

        public override IDisposable Begin()
        {
            batches.Clear();

            return base.Begin();
        }

        public override void End()
        {
            foreach (var batch in batches)
            {
                using (batch.Shader.Use())
                {
                    foreach (var item in batch.Items)
                    {
                        batch.Shader.Render(item.Mesh, item.Material, Camera, item.ModelMatrix, item.Offset);
                    }
                }
            }
        }

        private record BatchItem(Mesh Mesh, Material Material, Matrix4 ModelMatrix, Vector3 Offset);

        private class Batch
        {
            public GLTexture2D Texture { get; }

            public MaterialShader Shader { get; }

            public List<BatchItem> Items { get; } = new List<BatchItem>();

            public Batch(GLTexture2D texture, MaterialShader shader)
            {
                Texture = texture;
                Shader = shader;
            }

            public override bool Equals(object obj)
            {
                return obj is Batch other
                    && other.Shader == Shader
                    && other.Texture == Texture;
            }
        }
    }
}
