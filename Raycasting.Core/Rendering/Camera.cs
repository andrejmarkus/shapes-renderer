using OpenTK.Mathematics;

namespace Raycasting.Core.Rendering;

public class Camera
{
    private int _width;
    private int _height;

    public Matrix4 ViewProj { get; private set; }
    public Vector2 Position { get; private set; } = Vector2.Zero;
    public float Zoom { get; set; } = 1.0f;

    public Camera(int width, int height)
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
        var hw = _width / Zoom;
        var hh = _height / Zoom;

        var proj = Matrix4.CreateOrthographicOffCenter(0, hw, 0, hh, -1.0f, 1.0f);
        var view = Matrix4.CreateTranslation(-Position.X, -Position.Y, 0.0f);

        ViewProj = view * proj;
    }
}
