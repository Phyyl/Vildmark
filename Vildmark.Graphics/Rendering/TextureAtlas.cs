using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Models;
using Vildmark.Maths;

namespace Vildmark.Graphics.Rendering
{
    public class TextureAtlas
    {
        public Vector2 TextureTileSize { get; }

        public Vector2 TileSize { get; }

        public Texture2D Texture { get; }

        public TextureAtlas(Texture2D texture, int tileWidth, int tileHeight)
        {
            Texture = texture;

            TileSize = new Vector2(tileWidth, tileHeight);
            TextureTileSize = new Vector2(tileWidth / (float)texture.Width, tileHeight / (float)texture.Height);
        }

        public Texture2D this[int x, int y] => Texture.CreateSubTexture(new RectangleF(x * TextureTileSize.X, y * TextureTileSize.Y, TextureTileSize.X, TextureTileSize.Y));
    }
}
