using OpenTK.Mathematics;

namespace ShapesRenderer.Core.Drawing;

public class Line(Vector2 start, Vector2 end, Color color, float thickness = 1.0f) : Shape(start, color)
{
    public Vector2 Start { get; set; } = start;
    public Vector2 End { get; set; } = end;
    public float Thickness { get; set; } = thickness;

    protected override void DrawShape(Matrix4 model)
    {
        Core.Draw.DrawLine(Vector2.Zero, End - Start, Color, model, Thickness);
    }
}
