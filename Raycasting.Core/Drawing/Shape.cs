using OpenTK.Mathematics;

namespace Raycasting.Core.Drawing;

public abstract class Shape(Vector2 position, Color color)
{
    public Vector2 Position { get; set; } = position;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public Vector2 Scale { get; set; } = Vector2.One;
    public Color Color { get; set; } = color;

    public void Draw()
    {
        var scale = Matrix4.CreateScale(new Vector3(Scale.X, Scale.Y, 1.0f));
        var rotate = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation.Z));
        var translation = Matrix4.CreateTranslation(new Vector3(Position.X, Position.Y, 0.0f));

        var model = scale * rotate * translation;

        DrawShape(model);
    }

    protected abstract void DrawShape(Matrix4 model);
}
