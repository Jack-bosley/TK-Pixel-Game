using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace PixelGame.Foundations
{
    public class Material
    {
        private ShaderPipeline pipeline;


        static Material()
        {
            Shader defaultVertex = new Shader(ShaderType.VertexShader, @"
#version 330 core
layout(location = 0) in vec2 position;
layout(location = 1) in vec2 texCoord;

out vec2 v_texCoord;

void main()
{
    gl_Position = vec4(position, 0, 1.0f);
    v_texCoord = texCoord;
}
            ");
            Shader defaultFrag = new Shader(ShaderType.FragmentShader, @"
#version 330 core
uniform sampler2D u_texture;
in vec2 v_texCoord;

layout(location = 0) out vec4 color;

void main()
{
    color = texture(u_texture, v_texCoord);
} 
            ");

            DefaultMaterial = new Material()
            {
                Pipeline = new ShaderPipeline(defaultVertex, defaultFrag)
            };
        }

        public static Material DefaultMaterial { get; private set; }
        public ShaderPipeline Pipeline
        {
            get => pipeline;
            set => pipeline = value;
        }
        
        public static implicit operator int(Material _material)
        {
            return _material.Pipeline.Handle;
        }
    }
}
