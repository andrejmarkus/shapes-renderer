using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Raycasting.Core;
using Raycasting.Core.Drawing;

namespace Raycasting.Demo;

public class Program
{
    static void Main(string[] args)
    {
        using App game = new(800, 600, "Raycasting");

        // var shape = new Triangle(new Vector2(400, 300), new Vector2(500, 450), new Vector2(300, 450), Color.Red);
        var shape = new Rectangle(350, 250, 100, 100, Color.Red);
        // var shape = new Circle(new Vector2(400, 300), 50, Color.Red);
        // var shape = new Line(new Vector2(300, 300), new Vector2(500, 400), Color.Red, 10.0f);

        game.BackgroundColor = Color.White;

        game.OnKeyDown += state =>
        {
            if (state.IsKeyDown(Keys.Escape))
            {
                game.Close();
            }
            if (state.IsKeyDown(Keys.Left))
            {
                shape.Rotation += new Vector3(0, 0, 45) * game.DeltaTime;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                shape.Rotation -= new Vector3(0, 0, 45) * game.DeltaTime;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                shape.Scale += new Vector2(1, 1) * game.DeltaTime;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                shape.Scale -= new Vector2(1, 1) * game.DeltaTime;
            }
        };

        game.OnDraw += () =>
        {
            Draw.DrawCircle(200, 200, 100, Color.Blue, 64);
            shape.Draw();
            Draw.DrawLine(600, 100, 700, 500, Color.Green, 5.0f);
        };

        game.Run();
    }
}