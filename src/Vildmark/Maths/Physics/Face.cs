namespace Vildmark.Maths.Physics;

[Flags]
public enum Face2
{
    None = 0,
    Left = 1,
    Right = 2,
    Top = 4,
    Bottom = 8,
    All = Left | Right | Bottom | Top
}

[Flags]
public enum Face3
{
    None = 0,
    Left = 1,
    Right = 2,
    Bottom = 4,
    Top = 8,
    Back = 16,
    Front = 32,
    All = Left | Right | Bottom | Top | Back | Front
}
