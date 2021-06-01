using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;

using PixelGame.Core.Logging;
using PixelGame.Core.Enums;
using PixelGame.Entity_Component;

namespace PixelGame
{
    internal class Game : GameWindow
    {
        Scene gameScene = new Scene();

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            GLLoad();
            gameScene.OnLoad();
        }

        protected override void OnUnload()
        {
            GLUnload();
            gameScene.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GLResize(e);
        }

        protected override void OnMaximized(MaximizedEventArgs e)
        {
            GLMaximized(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            UpdateInputs();
            Update();

            GLUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Draw();

            GLRenderFrame(e);
        }

        protected void UpdateInputs()
        {
            gameScene.OnEvent();
        }

        protected void Update()
        {
            gameScene.OnUpdate();
        }

        protected void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.CullFace(CullFaceMode.Back);

            gameScene.OnDraw();

            SwapBuffers();
        }


        #region OpenGL handles
        private void GLLoad()
        {
            GL.ClearColor(0.392f, 0.584f, 0.929f, 0.0f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

            base.OnLoad();
        }

        private void GLUnload()
        {

            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            base.OnUnload();
        }

        private void GLResize(ResizeEventArgs e)
        {
            unsafe
            {
                GLFW.GetWindowSize(WindowPtr, out int width, out int height);
                GL.Viewport(0, 0, width, height);
            }

            base.OnResize(e);
        }

        private void GLMaximized(MaximizedEventArgs e)
        {
            unsafe
            {
                GLFW.GetWindowSize(WindowPtr, out int width, out int height);
                GL.Viewport(0, 0, width, height);
            }

            base.OnMaximized(e);
        }


        private void GLUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        private void GLRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }

        #endregion
    }
}
