using OpenTK.Mathematics;

namespace Raycasting.Core.Drawing;

public class Circle(Vector2 position, float radius, Color color) : Shape(position, color)
{
    public float Radius { get; set; } = radius;

    protected override void DrawShape(Matrix4 model)
    {
        Core.Draw.DrawCircle(Vector2.Zero, Radius, Color, model);
    }
}
