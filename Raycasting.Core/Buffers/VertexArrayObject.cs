using OpenTK.Graphics.OpenGL4;

namespace Raycasting.Core.Buffers;

internal class VertexArrayObject : IDisposable
{
    private readonly int _handle;

    public VertexArrayObject()
    {
        _handle = GL.GenVertexArray();
    }

    public void Bind()
    {
        GL.BindVertexArray(_handle);
    }

    public void SetVertexAttribPointer(int index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset)
    {
        GL.VertexAttribPointer(index, size, type, normalized, stride, offset);
        GL.EnableVertexAttribArray(index);
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(_handle);
    }
}
