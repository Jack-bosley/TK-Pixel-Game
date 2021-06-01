using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace PixelGame.Foundations
{
    public static class ShaderManager
    {
        public static Shader GetShader(string _name, ShaderType _shaderType)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.GetFullPath(@$"{dir}/PixelGame/Shaders/{_name}.{GetExtension(_shaderType)}");

            if (File.Exists(path))
                return new Shader(_shaderType, path);

            return null;
        }


        private static string GetExtension(ShaderType _shaderType)
        {
            switch (_shaderType)
            {
                case ShaderType.FragmentShader:
                    return "frag";
                case ShaderType.VertexShader:
                    return "vert";
                case ShaderType.ComputeShader:
                    return "comp";
                case ShaderType.GeometryShader:
                    return "geom";
                default:
                    return "";
            }
        }
    }
}
