using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace PixelGame.Foundations
{
    public class ShaderPipeline : IDisposable
    {
        private List<Shader> shaders;
        private int handle;
        private bool isCompiled = false;

        public ShaderPipeline(IEnumerable<Shader> _shaders)
        {
            shaders = new List<Shader>(_shaders);

            Compile();
        }
        public ShaderPipeline(params Shader[] _shaders)
        {
            shaders = new List<Shader>(_shaders);

            Compile();
        }
        ~ShaderPipeline()
        {
            Dispose();
        }
        public void Dispose()
        {
            GL.DeleteProgram(this);
        }

        public void Compile()
        {
            if (!isCompiled)
            {
                Handle = GL.CreateProgram();

                foreach (Shader shader in shaders)
                {
                    shader.Compile();
                    GL.AttachShader(this, shader);
                }

                GL.LinkProgram(this);

                foreach (Shader shader in shaders)
                {
                    GL.DetachShader(this, shader);
                    GL.DeleteShader(shader);
                }
                isCompiled = true;
            }
        }
        public void UseProgram()
        {
            GL.UseProgram(this);
        }

        public List<Shader> Shaders
        {
            get => shaders;
        }
        public int Handle
        {
            get => handle;
            private set => handle = value;
        }

        public static implicit operator int(ShaderPipeline _shader)
        {
            return _shader.Handle;
        }

    }
}
