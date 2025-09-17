using OpenTK.Mathematics;

namespace Raycasting.Core;

public class Camera2D
{
    private int _width;
    private int _height;

    public Matrix4 ViewProj { get; private set; }
    public Vector2 Position { get; private set; } = Vector2.Zero;
    public float Zoom { get; set; } = 1.0f;

    public Camera2D(int width, int height)
    {
        Resize(width, height);
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public void SetPosition(float x, float y)
    {
        Position = new Vector2(x, y);
    }

    public void Resize(int width, int height)
    {
        _width = width;
        _height = height;
        Rebuild();
    }

    private void Rebuild()
    {
        var hw = _width * 0.5f / Zoom;
        var hh = _height * 0.5f / Zoom;

        var proj = Matrix4.CreateOrthographicOffCenter(-hw, hw, -hh, hh, -1.0f, 1.0f);
        var view = Matrix4.CreateTranslation(-Position.X, -Position.Y, 0.0f);

        ViewProj = view * proj;
    }
}
