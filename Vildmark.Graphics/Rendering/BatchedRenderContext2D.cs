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

        public override void Render(Mesh mesh, Material material, Transform transform, MaterialShader shader)
        {
            Batch lastBatch = batches.LastOrDefault();
            Batch batch = new Batch(material.Texture.GLTexture, shader);
            BatchItem item = new BatchItem(mesh, material, transform);

            batch.Items.Add(item);

            if (lastBatch == batch)
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
                        batch.Shader.Render(item.Mesh, item.Material, item.Transform, Camera);
                    }
                }
            }
        }

        private record BatchItem(Mesh Mesh, Material Material, Transform Transform);

        private record Batch(GLTexture2D Texture, MaterialShader Shader)
        {
            public List<BatchItem> Items { get; } = new List<BatchItem>();
        }
    }
}
