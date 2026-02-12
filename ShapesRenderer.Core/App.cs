using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ShapesRenderer.Core.Rendering;

namespace ShapesRenderer.Core;

public class App(int width, int height, string title) : GameWindow(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title, Flags = ContextFlags.ForwardCompatible })
{
    private Color _backgroundColor = Color.DimGray;

    public Vector2i WindowSize { get; private set; } = new(width, height);
    public Color BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            GL.ClearColor(value);
            _backgroundColor = value;
        }
    }
    public Camera Camera { get; private set; } = null!;
    public float DeltaTime { get; private set; }
    public double Time { get; private set; }

    public delegate void OnDrawHandler();
    public delegate void OnKeyDownHandler(KeyboardState keyboardState);

    public event OnDrawHandler? OnDraw;
    public new event OnKeyDownHandler? OnKeyDown;

    private void ResizeWindow(int width, int height)
    {
        WindowSize = new(width, height);
        GL.Viewport(0, 0, width, height);
        Camera?.Resize(width, height);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        DeltaTime = (float)e.Time;
        Time += e.Time;

        if (!IsFocused)
        {
            return;
        }

        OnKeyDown?.Invoke(KeyboardState);
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.Enable(EnableCap.DepthTest);
        GL.ClearColor(BackgroundColor);

        Camera = new Camera(WindowSize.X, WindowSize.Y);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Draw.Begin();
        OnDraw?.Invoke();
        Draw.End(Camera);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);

        ResizeWindow(e.Width, e.Height);
    }


    protected override void OnUnload()
    {
        base.OnUnload();

        ShaderFactory.Instance.Dispose();
        Draw.Dispose();
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        base.OnMouseMove(e);

    }
}
