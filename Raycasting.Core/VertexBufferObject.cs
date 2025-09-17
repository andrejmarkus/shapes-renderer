using OpenTK.Graphics.OpenGL4;

namespace Raycasting.Core;

public class VertexBufferObject : IDisposable
{
    private const int MaxBufferSize = 4 * 1024 * 1024; // 4 MB

    private readonly int _handle;
    private bool _disposed = false;

    public VertexBufferObject(BufferUsageHint usage = BufferUsageHint.StaticDraw)
    {
        _handle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
        GL.BufferData(BufferTarget.ArrayBuffer, MaxBufferSize, IntPtr.Zero, usage);
    }

    public void UpdateData(float[] data)
    {
        int dataSize = data.Length * sizeof(float);
        if (dataSize > MaxBufferSize)
            throw new ArgumentException($"Data size ({dataSize} bytes) exceeds buffer size ({MaxBufferSize} bytes)");
        GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
        GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, dataSize, data);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            GL.DeleteBuffer(_handle);
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
