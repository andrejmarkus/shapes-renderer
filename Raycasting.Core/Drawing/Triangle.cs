using OpenTK.Mathematics;

namespace Raycasting.Core.Drawing;

public class Triangle(Vector2 a, Vector2 b, Vector2 c, Color color) : Shape(a, color)
{
    public Vector2 A { get; set; } = a;
    public Vector2 B { get; set; } = b;
    public Vector2 C { get; set; } = c;

    public Triangle(float ax, float ay, float bx, float by, float cx, float cy, Color color)
        : this(new Vector2(ax, ay), new Vector2(bx, by), new Vector2(cx, cy), color) { }

    protected override void DrawShape(Matrix4 model)
    {
        Core.Draw.DrawTriangle(Vector2.Zero, B - A, C - A, Color, model);
    }
}
