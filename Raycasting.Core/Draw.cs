using Raycasting.Core.Rendering;
using OpenTK.Mathematics;

namespace Raycasting.Core;

public static class Draw
{
    private static ShapeRenderer? _renderer;
    private static bool _begun = false;

    private static ShapeRenderer Renderer
    {
        get
        {
            _renderer ??= new ShapeRenderer();
            return _renderer;
        }
    }

    internal static void Begin()
    {
        Renderer.Begin();
        _begun = true;
    }

    internal static void End(Camera camera)
    {
        if (!_begun) return;
        Renderer.End(camera);
        _begun = false;
    }

    public static void DrawTriangle(float ax, float ay, float bx, float by, float cx, float cy, Color color)
        => Renderer.DrawTriangle(ax, ay, bx, by, cx, cy, color);

    public static void DrawTriangle(Vector2 a, Vector2 b, Vector2 c, Color color)
        => Renderer.DrawTriangle(a.X, a.Y, b.X, b.Y, c.X, c.Y, color);

    public static void DrawRectangle(float x, float y, float width, float height, Color color)
        => Renderer.DrawRectangle(x, y, width, height, color);

    public static void DrawRectangle(Vector2 position, Vector2 size, Color color)
        => Renderer.DrawRectangle(position, size, color);

    public static void DrawCircle(float centerX, float centerY, float radius, Color color, int segments = 32)
        => Renderer.DrawCircle(centerX, centerY, radius, color, segments);

    public static void DrawCircle(Vector2 center, float radius, Color color, int segments = 32)
        => Renderer.DrawCircle(center, radius, color, segments);

    public static void DrawLine(float x1, float y1, float x2, float y2, Color color, float thickness = 1.0f)
        => Renderer.DrawLine(x1, y1, x2, y2, color, thickness);

    public static void DrawLine(Vector2 start, Vector2 end, Color color, float thickness = 1.0f)
        => Renderer.DrawLine(start, end, color, thickness);

    public static void DrawShape(IEnumerable<Vector2> positions, Color color, IEnumerable<uint> indices)
        => Renderer.DrawShape(positions, color, indices);

    internal static void DrawTriangle(float ax, float ay, float bx, float by, float cx, float cy, Color color, Matrix4 model)
        => Renderer.DrawTriangle(ax, ay, bx, by, cx, cy, color, model);

    internal static void DrawTriangle(Vector2 a, Vector2 b, Vector2 c, Color color, Matrix4 model)
        => Renderer.DrawTriangle(a.X, a.Y, b.X, b.Y, c.X, c.Y, color, model);

    internal static void DrawRectangle(float x, float y, float width, float height, Color color, Matrix4 model)
        => Renderer.DrawRectangle(x, y, width, height, color, model);

    internal static void DrawRectangle(Vector2 position, Vector2 size, Color color, Matrix4 model)
        => Renderer.DrawRectangle(position, size, color, model);

    internal static void DrawCircle(float centerX, float centerY, float radius, Color color, Matrix4 model, int segments = 32)
        => Renderer.DrawCircle(centerX, centerY, radius, color, segments, model);

    internal static void DrawCircle(Vector2 center, float radius, Color color, Matrix4 model, int segments = 32)
        => Renderer.DrawCircle(center, radius, color, segments, model);

    internal static void DrawLine(float x1, float y1, float x2, float y2, Color color, Matrix4 model, float thickness = 1.0f)
        => Renderer.DrawLine(x1, y1, x2, y2, color, thickness, model);

    internal static void DrawLine(Vector2 start, Vector2 end, Color color, Matrix4 model, float thickness = 1.0f)
        => Renderer.DrawLine(start, end, color, thickness, model);

    internal static void DrawShape(IEnumerable<Vector2> positions, Color color, IEnumerable<uint> indices, Matrix4 model)
        => Renderer.DrawShape(positions, color, indices, model);

    public static void Dispose()
    {
        _renderer?.Dispose();
        _renderer = null;
        _begun = false;
    }
}
