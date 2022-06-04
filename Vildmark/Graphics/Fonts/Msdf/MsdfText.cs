﻿using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts.Msdf;

public class MsdfText
{
    private bool needsUpdate = true;

    private readonly Mesh<Vertex> mesh = new();
    private readonly MsdfFont font;
    private string text;
    private float fontSize;

    public RectangleF Bounds { get; private set; }

    public Vector2 Position => Bounds.Location.ToVector();
    public Vector2 Size => Bounds.Size.ToVector();

    public string Text
    {
        get => text;
        set => SetValue(ref text, value);
    }

    public float FontSize
    {
        get => fontSize;
        set => SetValue(ref fontSize, value);
    }

    public MsdfText(MsdfFont font, string text, float fontSize)
    {
        this.font = font;
        this.text = text;
        this.fontSize = fontSize;
    }

    public void Render(Renderer renderer, Color4 foreground, Color4? background = default, Transform? transform = default)
    {
        if (needsUpdate)
        {
            UpdateMesh();
            needsUpdate = false;
        }

        renderer.Render(mesh, new MsdfMaterial(font.Texture, foreground, background ?? foreground with { A = 0 }, font.Info.Atlas?.DistanceRange ?? 2), MsdfFont.Shader, transform);
    }

    private void UpdateMesh()
    {
        mesh.UpdateVertices(font.CreateMesh(text, fontSize));
        Bounds = font.MeasureString(text, fontSize);
    }

    private void SetValue<T>(ref T field, T value)
    {
        field = value;
        needsUpdate = true;
    }
}