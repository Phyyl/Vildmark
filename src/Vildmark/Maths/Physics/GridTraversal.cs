namespace Vildmark.Maths.Physics;

public static class GridTraversal
{
    public static IEnumerable<Vector3i> LineOfSight(LineSegment3 line, bool includeCurrent = false)
    {
        if (line.Direction.Length == 0)
        {
            yield break;
        }

        Vector3 d = line.Direction.Abs();

        int x = (int)MathF.Floor(line.Start.X);
        int y = (int)MathF.Floor(line.Start.Y);
        int z = (int)MathF.Floor(line.Start.Z);

        Vector3 dt = new(1 / d.X, 1 / d.Y, 1 / d.Z);

        float t = 0;

        int n = 1;
        int x_inc = 0;
        int y_inc = 0;
        int z_inc = 0;
        float t_next_x = 0;
        float t_next_y = 0;
        float t_next_z = 0;

        if (d.X == 0)
        {
            x_inc = 0;
            t_next_x = dt.X;
        }
        else if (line.End.X > line.Start.X)
        {
            x_inc = 1;
            n += (int)line.End.X - x;
            t_next_x = (float)(MathF.Floor(line.Start.X) + 1 - line.Start.X) * dt.X;
        }
        else
        {
            x_inc = -1;
            n += x - (int)line.End.X;
            t_next_x = (float)(line.Start.X - MathF.Floor(line.Start.X)) * dt.X;
        }

        if (d.Y == 0)
        {
            y_inc = 0;
            t_next_y = dt.Y;
        }
        else if (line.End.Y > line.Start.Y)
        {
            y_inc = 1;
            n += (int)line.End.Y - y;
            t_next_y = (float)(MathF.Floor(line.Start.Y) + 1 - line.Start.Y) * dt.Y;
        }
        else
        {
            y_inc = -1;
            n += y - (int)line.End.Y;
            t_next_y = (float)(line.Start.Y - MathF.Floor(line.Start.Y)) * dt.Y;
        }

        if (d.Z == 0)
        {
            z_inc = 0;
            t_next_z = dt.Z;
        }
        else if (line.End.Z > line.Start.Z)
        {
            z_inc = 1;
            n += (int)line.End.Z - z;
            t_next_z = (float)(MathF.Floor(line.Start.Z) + 1 - line.Start.Z) * dt.Z;
        }
        else
        {
            z_inc = -1;
            n += z - (int)MathF.Floor(line.End.Z);
            t_next_z = (float)(line.Start.Z - MathF.Floor(line.Start.Z)) * dt.Z;
        }

        for (int i = 0; i < n; i++)
        {
            if (i > 0 || includeCurrent)
            {
                yield return new(x, y, z);
            }

            if (t_next_x < t_next_y && t_next_x < t_next_z)
            {
                x += x_inc;
                t = t_next_x;
                t_next_x += dt.X;
            }
            else if (t_next_y < t_next_z)
            {
                y += y_inc;
                t = t_next_y;
                t_next_y += dt.Y;
            }
            else
            {
                z += z_inc;
                t = t_next_z;
                t_next_z += dt.Z;
            }
        }
    }

}
