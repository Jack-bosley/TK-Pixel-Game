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
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "LearnOpenTK - Creating a Window",
            };

            // To create a new window, create a class that extends GameWindow, then call Run() on it.
            using Game gameWindow = new Game(GameWindowSettings.Default, nativeWindowSettings);

            try
            {
                gameWindow.Run();
            }
            catch (Exception e)
            {
                Logger.LogException(e);
            }
        }
    }
}
