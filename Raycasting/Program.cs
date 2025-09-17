using Raycasting.Core;

namespace Raycasting.Demo;

public class Program
{
    static void Main(string[] args)
    {
        using App game = new(800, 600, "Raycasting");
        game.Run();
    }
}