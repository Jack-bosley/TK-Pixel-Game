using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace PixelGame.Foundations
{
    public class Shader : IDisposable
    {
        private ShaderType type;
        private int handle;
        private string path;
        private string source;
        private bool isCompiled = false;

        public Shader(ShaderType _shaderType, string _path)
        {
            Path = _path;
            Type = _shaderType;

            Compile();
        }
        ~Shader()
        {
            Dispose();
        }
        public void Dispose()
        {
            GL.DeleteShader(this);
        }

        internal void Compile()
        {
            if (!isCompiled)
            {
                if (File.Exists(Path))
                {
                    using StreamReader sr = new StreamReader(Path, Encoding.UTF8);
                    Source = sr.ReadToEnd();
                }

                Handle = GL.CreateShader(Type);
                GL.ShaderSource(this, Source);
                GL.CompileShader(this);

                string infoLogVert = GL.GetShaderInfoLog(this);
                if (infoLogVert != String.Empty)
                    Console.WriteLine(infoLogVert);
                else
                    isCompiled = true;
            }
        }

        public ShaderType Type
        {
            get => type;
            protected set => type = value;
        }
        public int Handle
        {
            get => handle;
            protected set => handle = value;
        }
        public string Path
        {
            get => path;
            protected set => path = value;
        }
        public string Source
        {
            get => source;
            protected set => source = value;
        }


        public static implicit operator int(Shader _shader)
        {
            return _shader.Handle;
        }
    }
}
