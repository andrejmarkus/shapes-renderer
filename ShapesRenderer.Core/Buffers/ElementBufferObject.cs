using OpenTK.Graphics.OpenGL4;

namespace ShapesRenderer.Core.Buffers;

internal class ElementBufferObject : IDisposable
{
    private const int MaxBufferSize = 4 * 1024 * 1024; // 4 MB

    private readonly int _handle;

    public ElementBufferObject()
    {
        _handle = GL.GenBuffer();
    }

    public void UpdateData(uint[] indices, BufferUsageHint usage = BufferUsageHint.StaticDraw)
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, usage);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_handle);
    }
}
