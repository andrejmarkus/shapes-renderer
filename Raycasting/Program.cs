using Raycasting.Core;

namespace Raycasting.Demo;

public class Program
{
    static void Main(string[] args)
    {
        using App game = new(800, 600, "Raycasting");
        game.BackgroundColor = Color.White;
        game.OnDraw += () =>
        {
            Draw.DrawRectangle(100, 100, 200, 250, Color.Red);
            Draw.DrawCircle(400, 300, 100, Color.Blue);
            Draw.DrawLine(600, 100, 700, 500, Color.Green, 5.0f);
        };
        game.Run();
    }
}