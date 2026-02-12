# 🎨 ShapesRenderer

[![.NET 9](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![OpenTK](https://img.shields.io/badge/OpenGL-OpenTK-red.svg)](https://opentk.net/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A lightweight, hardware-accelerated 2D shape rendering engine built with **C#** and **OpenTK**. Designed for simplicity and performance, **ShapesRenderer** provides a clean API for drawing geometric primitives directly on an OpenGL context.

---

## 🚀 Features

- **🎯 Simple Primitives**: Easily render Triangles, Rectangles, Circles, and Lines.
- **⚡ Hardware Accelerated**: Leverages OpenGL 4.x for efficient rendering.
- **🛠 Easy Integration**: Designed as a library (`ShapesRenderer.Core`) that can be plugged into any .NET 9 project.
- **🎮 Interactive**: Built-in event system for handling keyboard input and frame updates.
- **🎥 Dynamic Camera**: Built-in `Camera` class for managing orthographic views and window resizing.
- **🎨 Flexible Styling**: Full control over colors, scaling, rotation, and camera views.

## 📦 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A GPU supporting OpenGL 4.x or higher

### Installation

Clone the repository:

```bash
git clone https://github.com/andrejmarkus/raycasting.git
cd ShapesRenderer
```

Run the demo project:

```bash
dotnet run --project ShapesRenderer.Demo
```

## 💻 Usage Example

Creating a simple interactive scene is straightforward. Check out how you can draw a rotating rectangle:

```csharp
using ShapesRenderer.Core;
using ShapesRenderer.Core.Drawing;
using OpenTK.Mathematics;

using App game = new(800, 600, "My Shapes App");

var shape = new Rectangle(350, 250, 100, 100, Color.Red);

game.OnKeyDown += state => {
    if (state.IsKeyDown(Keys.Left)) 
        shape.Rotation += new Vector3(0, 0, 45) * game.DeltaTime;
};

game.OnDraw += () => {
    shape.Draw();
    Draw.DrawCircle(200, 200, 50, Color.Blue, 64);
};

game.Run();
```

## 🏗 Project Structure

- **`ShapesRenderer.Core`**: The engine core containing the OpenGL wrapper, shaders, and shape abstractions.
- **`ShapesRenderer.Demo`**: A sample implementation showing how to use the core library for interactive rendering.

## 🛠 Built With

- **[OpenTK](https://opentk.net/)**: Low-level C# bindings for OpenGL.
- **[.NET 9](https://dotnet.microsoft.com/)**: The latest and greatest from the .NET ecosystem.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---
<p align="center">Made with ❤️ for the C# community</p>
