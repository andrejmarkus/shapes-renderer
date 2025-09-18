using OpenTK.Mathematics;

namespace Raycasting.Core.Drawing;

public class Rectangle(Vector2 position, Vector2 size, Color color) : Shape(position, color)
{
    public Vector2 Size { get; set; } = size;

    public Rectangle(float x, float y, float width, float height, Color color) : this(new Vector2(x, y), new Vector2(width, height), color) { }

    protected override void DrawShape(Matrix4 model)
    {
        Core.Draw.DrawRectangle(Vector2.Zero, Size, Color, model);
    }
}
