using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Raycasting.Core;

public class App(int width, int height, string title) : GameWindow(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title, Flags = ContextFlags.ForwardCompatible })
{
    public Vector2i WindowSize { get; private set; } = new(width, height);
    public Camera2D Camera { get; private set; } = null!;
    public ShapeRenderer ShapeRenderer { get; private set; } = null!;

    public float DeltaTime { get; private set; }
    public double Time { get; private set; }

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

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
        if (KeyboardState.IsKeyDown(Keys.W))
        {
            Camera.SetPosition(Camera.Position + new Vector2(0, 1f) * DeltaTime);
        }
        if (KeyboardState.IsKeyDown(Keys.S))
        {
            Camera.SetPosition(Camera.Position + new Vector2(0, -1f) * DeltaTime);
        }
        if (KeyboardState.IsKeyDown(Keys.A))
        {
            Camera.SetPosition(Camera.Position + new Vector2(-1f, 0) * DeltaTime);
        }
        if (KeyboardState.IsKeyDown(Keys.D))
        {
            Camera.SetPosition(Camera.Position + new Vector2(1f, 0) * DeltaTime);
        }
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.Enable(EnableCap.DepthTest);
        GL.ClearColor(0.1f, 0.2f, 0.3f, 1.0f);

        Camera = new Camera2D(WindowSize.X, WindowSize.Y);
        ShapeRenderer = new ShapeRenderer();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        ShapeRenderer.Begin();
        ShapeRenderer.DrawRectangle(0, 0, 100, 100, Color4.DarkGreen);
        ShapeRenderer.DrawRectangle(50, 50, 100, 100, Color4.CornflowerBlue);
        ShapeRenderer.DrawCircle(200, 200, 50, Color4.OrangeRed);
        ShapeRenderer.DrawLine(300, 300, 400, 400, Color4.Red, 5.0f);
        ShapeRenderer.End(Camera);

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
        ShapeRenderer.Dispose();
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        base.OnMouseMove(e);

    }
}
