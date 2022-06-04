﻿using Vildmark.Graphics.Fonts.Msdf;
using Vildmark.Graphics.Textures;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts.Loaders;

internal class MsdfFontResourceLoader : IResourceLoader<MsdfFont>
{
    public MsdfFont Load(string name, ResourceLoadContext context)
    {
        MsdfFontInfo info = context.Load<MsdfFontInfo>($"{name}.json");
        Texture2D texture = context.Load<Texture2D>($"{name}.png");

        return new MsdfFont(info, texture);
    }
}