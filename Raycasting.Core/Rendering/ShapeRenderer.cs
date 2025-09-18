using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Raycasting.Core.Buffers;

namespace Raycasting.Core.Rendering;

internal class ShapeRenderer : IDisposable
{
    private const int VertexSize = 6;

    private readonly List<Vertex> _vertices;
    private readonly List<uint> _indices;
    private readonly VertexBufferObject _vbo;
    private ElementBufferObject _ebo;
    private readonly VertexArrayObject _vao;
    private readonly Shader _shader;

    public ShapeRenderer()
    {
        _vertices = [];
        _indices = [];

        _vbo = new VertexBufferObject(BufferUsageHint.DynamicDraw);
        _vao = new VertexArrayObject();
        _ebo = new ElementBufferObject();
        _shader = ShaderFactory.Instance.GetShader("Shaders/shader.vert", "Shaders/shader.frag");

        _vbo.Bind();
        _vao.Bind();
        _ebo.Bind();

        _vao.SetVertexAttribPointer(_shader.GetAttribLocation("aPos"), 2, VertexAttribPointerType.Float, false, sizeof(float) * VertexSize, 0); // Position
        _vao.SetVertexAttribPointer(_shader.GetAttribLocation("aColor"), 4, VertexAttribPointerType.Float, false, sizeof(float) * VertexSize, sizeof(float) * 2); // Color
    }

    public void Begin()
    {
        _vertices.Clear();
        _indices.Clear();
    }

    public void End(Camera2D camera)
    {
        if (_vertices.Count == 0 || _indices.Count == 0)
            return;

        _vbo.UpdateData([
            .. _vertices.SelectMany(v => new float[] { v.Position.X, v.Position.Y, v.Color.R, v.Color.G, v.Color.B, v.Color.A })
        ]);

        _ebo.UpdateData([.. _indices]);
        _ebo.Bind();

        _shader.Use();
        _shader.SetMatrix4("model", Matrix4.Identity);
        _shader.SetMatrix4("viewProj", camera.ViewProj);

        _vao.Bind();
        _ebo.Bind();
        GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void DrawTriangle(float x1, float y1, float x2, float y2, float x3, float y3, Color4 color)
    {
        uint startIndex = (uint)_vertices.Count;
        _vertices.Add(new Vertex(new Vector2(x1, y1), color)); // 0
        _vertices.Add(new Vertex(new Vector2(x2, y2), color)); // 1
        _vertices.Add(new Vertex(new Vector2(x3, y3), color)); // 2

        _indices.Add(startIndex + 0);
        _indices.Add(startIndex + 1);
        _indices.Add(startIndex + 2);
    }

    public void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, Color4 color)
    {
        uint startIndex = (uint)_vertices.Count;
        _vertices.Add(new Vertex(v1, color)); // 0
        _vertices.Add(new Vertex(v2, color)); // 1
        _vertices.Add(new Vertex(v3, color)); // 2

        _indices.Add(startIndex + 0);
        _indices.Add(startIndex + 1);
        _indices.Add(startIndex + 2);
    }

    public void DrawRectangle(float x, float y, float width, float height, Color4 color)
    {
        var topLeft = new Vector2(x, y);
        var topRight = new Vector2(x + width, y);
        var bottomRight = new Vector2(x + width, y + height);
        var bottomLeft = new Vector2(x, y + height);

        uint startIndex = (uint)_vertices.Count;
        _vertices.Add(new Vertex(topLeft, color));     // 0
        _vertices.Add(new Vertex(topRight, color));    // 1
        _vertices.Add(new Vertex(bottomRight, color)); // 2
        _vertices.Add(new Vertex(bottomLeft, color));  // 3

        _indices.Add(startIndex + 0);
        _indices.Add(startIndex + 1);
        _indices.Add(startIndex + 2);
        _indices.Add(startIndex + 0);
        _indices.Add(startIndex + 2);
        _indices.Add(startIndex + 3);
    }

    public void DrawRectangle(Vector2 position, Vector2 size, Color4 color)
    {
        DrawRectangle(position.X, position.Y, size.X, size.Y, color);
    }

    public void DrawCircle(float centerX, float centerY, float radius, Color4 color, int segments = 32)
    {
        if (segments < 3) segments = 3;

        uint startIndex = (uint)_vertices.Count;
        _vertices.Add(new Vertex(new Vector2(centerX, centerY), color)); // Center vertex

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * MathHelper.TwoPi / segments;
            float x = centerX + radius * (float)Math.Cos(angle);
            float y = centerY + radius * (float)Math.Sin(angle);
            _vertices.Add(new Vertex(new Vector2(x, y), color));
        }

        for (uint i = 1; i <= segments; i++)
        {
            _indices.Add(startIndex);         // Center
            _indices.Add(startIndex + i);     // Current perimeter vertex
            _indices.Add(startIndex + i + 1); // Next perimeter vertex
        }
    }

    public void DrawCircle(Vector2 center, float radius, Color4 color, int segments = 32)
    {
        DrawCircle(center.X, center.Y, radius, color, segments);
    }

    public void DrawLine(float x1, float y1, float x2, float y2, Color4 color, float thickness = 1.0f)
    {
        Vector2 direction = new Vector2(x2 - x1, y2 - y1);
        direction.Normalize();
        Vector2 perpendicular = new Vector2(-direction.Y, direction.X) * (thickness / 2);

        var v1 = new Vector2(x1, y1) + perpendicular;
        var v2 = new Vector2(x2, y2) + perpendicular;
        var v3 = new Vector2(x2, y2) - perpendicular;
        var v4 = new Vector2(x1, y1) - perpendicular;

        uint startIndex = (uint)_vertices.Count;
        _vertices.Add(new Vertex(v1, color)); // 0
        _vertices.Add(new Vertex(v2, color)); // 1
        _vertices.Add(new Vertex(v3, color)); // 2
        _vertices.Add(new Vertex(v4, color)); // 3

        _indices.Add(startIndex + 0);
        _indices.Add(startIndex + 1);
        _indices.Add(startIndex + 2);
        _indices.Add(startIndex + 0);
        _indices.Add(startIndex + 2);
        _indices.Add(startIndex + 3);
    }

    public void DrawLine(Vector2 start, Vector2 end, Color4 color, float thickness = 1.0f)
    {
        DrawLine(start.X, start.Y, end.X, end.Y, color, thickness);
    }


    public void DrawShape(IEnumerable<Vector2> positions, Color4 color, IEnumerable<uint> indices)
    {
        uint startIndex = (uint)_vertices.Count;
        foreach (var pos in positions)
        {
            _vertices.Add(new Vertex(pos, color));
        }
        foreach (var idx in indices)
        {
            _indices.Add(startIndex + idx);
        }
    }

    public void Dispose()
    {
        _vbo.Dispose();
        _ebo.Dispose();
        _vao.Dispose();
        _shader.Dispose();
    }

    private struct Vertex(Vector2 position, Color4 color)
    {
        public Vector2 Position = position;
        public Color4 Color = color;
    }
}
