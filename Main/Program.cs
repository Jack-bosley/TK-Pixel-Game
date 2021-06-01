using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;

using PixelGame.Core.Logging;

namespace PixelGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings gameWindowSettings = new GameWindowSettings()
            {
                IsMultiThreaded = true,
                RenderFrequency = 60,
                UpdateFrequency = double.MaxValue,
            };
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "PixelGame",
            };

            // To create a new window, create a class that extends GameWindow, then call Run() on it.
            using Game gameWindow = new Game(gameWindowSettings, nativeWindowSettings);

            try
            {
                gameWindow.Run();
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
        }
    }
}
