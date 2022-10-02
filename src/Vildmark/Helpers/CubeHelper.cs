using Vildmark.Graphics;

namespace Vildmark.Helpers;

public static class CubeHelper
{
    public static IEnumerable<Vertex> GetVertices(Vector3 offset = default) => GetVertices(offset, Vector3.One, Vector4.One, Vector2.Zero, Vector2.One);
    public static IEnumerable<Vertex> GetVertices(Vector3 offset, Vector3 size, Vector4 color, Vector2 texCoord, Vector2 texSize, int texCoordRotation = 0)
    {
        foreach (var vertex in GetRightVertices(offset, size, color, texCoord, texSize, texCoordRotation))
        {
            yield return vertex;
        }

        foreach (var vertex in GetLeftVertices(offset, size, color, texCoord, texSize, texCoordRotation))
        {
            yield return vertex;
        }

        foreach (var vertex in GetTopVertices(offset, size, color, texCoord, texSize, texCoordRotation))
        {
            yield return vertex;
        }

        foreach (var vertex in GetBottomVertices(offset, size, color, texCoord, texSize, texCoordRotation))
        {
            yield return vertex;
        }

        foreach (var vertex in GetFrontVertices(offset, size, color, texCoord, texSize, texCoordRotation))
        {
            yield return vertex;
        }

        foreach (var vertex in GetBackVertices(offset, size, color, texCoord, texSize, texCoordRotation))
        {
            yield return vertex;
        }
    }

    public static IEnumerable<Vertex> GetRightVertices(Vector3 offset, Vector3 size, Vector4 color, Vector2 texCoord, Vector2 texSize, int texCoordRotation = 0)
    {
        Vector2[] texCoords = new Vector2[]
        {
            texCoord + new Vector2(0, 0) * texSize,
            texCoord + new Vector2(0, 1) * texSize,
            texCoord + new Vector2(1, 1) * texSize,
            texCoord + new Vector2(1, 0) * texSize
        };

        yield return new Vertex(new Vector3(1, 1, 1) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(1, 0, 0));
        yield return new Vertex(new Vector3(1, 0, 1) * size + offset, texCoords[(1 + texCoordRotation) % texCoords.Length], color, new Vector3(1, 0, 0));
        yield return new Vertex(new Vector3(1, 0, 0) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(1, 0, 0));
        yield return new Vertex(new Vector3(1, 1, 1) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(1, 0, 0));
        yield return new Vertex(new Vector3(1, 0, 0) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(1, 0, 0));
        yield return new Vertex(new Vector3(1, 1, 0) * size + offset, texCoords[(3 + texCoordRotation) % texCoords.Length], color, new Vector3(1, 0, 0));
    }

    public static IEnumerable<Vertex> GetLeftVertices(Vector3 offset, Vector3 size, Vector4 color, Vector2 texCoord, Vector2 texSize, int texCoordRotation = 0)
    {
        Vector2[] texCoords = new Vector2[]
        {
            texCoord + new Vector2(0, 0) * texSize,
            texCoord + new Vector2(0, 1) * texSize,
            texCoord + new Vector2(1, 1) * texSize,
            texCoord + new Vector2(1, 0) * texSize
        };

        yield return new Vertex(new Vector3(0, 1, 0) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(-1, 0, 0));
        yield return new Vertex(new Vector3(0, 0, 0) * size + offset, texCoords[(1 + texCoordRotation) % texCoords.Length], color, new Vector3(-1, 0, 0));
        yield return new Vertex(new Vector3(0, 0, 1) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(-1, 0, 0));
        yield return new Vertex(new Vector3(0, 1, 0) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(-1, 0, 0));
        yield return new Vertex(new Vector3(0, 0, 1) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(-1, 0, 0));
        yield return new Vertex(new Vector3(0, 1, 1) * size + offset, texCoords[(3 + texCoordRotation) % texCoords.Length], color, new Vector3(-1, 0, 0));
    }

    public static IEnumerable<Vertex> GetTopVertices(Vector3 offset, Vector3 size, Vector4 color, Vector2 texCoord, Vector2 texSize, int texCoordRotation = 0)
    {
        Vector2[] texCoords = new Vector2[]
        {
            texCoord + new Vector2(0, 0) * texSize,
            texCoord + new Vector2(0, 1) * texSize,
            texCoord + new Vector2(1, 1) * texSize,
            texCoord + new Vector2(1, 0) * texSize
        };

        yield return new(new Vector3(0, 1, 0) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 1, 0));
        yield return new(new Vector3(0, 1, 1) * size + offset, texCoords[(1 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 1, 0));
        yield return new(new Vector3(1, 1, 1) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 1, 0));
        yield return new(new Vector3(0, 1, 0) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 1, 0));
        yield return new(new Vector3(1, 1, 1) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 1, 0));
        yield return new(new Vector3(1, 1, 0) * size + offset, texCoords[(3 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 1, 0));
    }

    public static IEnumerable<Vertex> GetBottomVertices(Vector3 offset, Vector3 size, Vector4 color, Vector2 texCoord, Vector2 texSize, int texCoordRotation = 0)
    {
        Vector2[] texCoords = new Vector2[]
        {
            texCoord + new Vector2(0, 0) * texSize,
            texCoord + new Vector2(0, 1) * texSize,
            texCoord + new Vector2(1, 1) * texSize,
            texCoord + new Vector2(1, 0) * texSize
        };

        yield return new Vertex(new Vector3(0, 0, 1) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(0, -1, 0));
        yield return new Vertex(new Vector3(0, 0, 0) * size + offset, texCoords[(1 + texCoordRotation) % texCoords.Length], color, new Vector3(0, -1, 0));
        yield return new Vertex(new Vector3(1, 0, 0) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(0, -1, 0));
        yield return new Vertex(new Vector3(0, 0, 1) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(0, -1, 0));
        yield return new Vertex(new Vector3(1, 0, 0) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(0, -1, 0));
        yield return new Vertex(new Vector3(1, 0, 1) * size + offset, texCoords[(3 + texCoordRotation) % texCoords.Length], color, new Vector3(0, -1, 0));
    }

    public static IEnumerable<Vertex> GetFrontVertices(Vector3 offset, Vector3 size, Vector4 color, Vector2 texCoord, Vector2 texSize, int texCoordRotation = 0)
    {
        Vector2[] texCoords = new Vector2[]
        {
            texCoord + new Vector2(0, 0) * texSize,
            texCoord + new Vector2(0, 1) * texSize,
            texCoord + new Vector2(1, 1) * texSize,
            texCoord + new Vector2(1, 0) * texSize
        };

        yield return new Vertex(new Vector3(0, 1, 1) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, 1));
        yield return new Vertex(new Vector3(0, 0, 1) * size + offset, texCoords[(1 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, 1));
        yield return new Vertex(new Vector3(1, 0, 1) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, 1));
        yield return new Vertex(new Vector3(0, 1, 1) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, 1));
        yield return new Vertex(new Vector3(1, 0, 1) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, 1));
        yield return new Vertex(new Vector3(1, 1, 1) * size + offset, texCoords[(3 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, 1));
    }

    public static IEnumerable<Vertex> GetBackVertices(Vector3 offset, Vector3 size, Vector4 color, Vector2 texCoord, Vector2 texSize, int texCoordRotation = 0)
    {
        Vector2[] texCoords = new Vector2[]
        {
            texCoord + new Vector2(0, 0) * texSize,
            texCoord + new Vector2(0, 1) * texSize,
            texCoord + new Vector2(1, 1) * texSize,
            texCoord + new Vector2(1, 0) * texSize
        };

        yield return new Vertex(new Vector3(1, 1, 0) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, -1));
        yield return new Vertex(new Vector3(1, 0, 0) * size + offset, texCoords[(1 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, -1));
        yield return new Vertex(new Vector3(0, 0, 0) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, -1));
        yield return new Vertex(new Vector3(1, 1, 0) * size + offset, texCoords[(0 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, -1));
        yield return new Vertex(new Vector3(0, 0, 0) * size + offset, texCoords[(2 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, -1));
        yield return new Vertex(new Vector3(0, 1, 0) * size + offset, texCoords[(3 + texCoordRotation) % texCoords.Length], color, new Vector3(0, 0, -1));
    }
}
