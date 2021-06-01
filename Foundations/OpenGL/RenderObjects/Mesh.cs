using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace PixelGame.Foundations
{
    public class Mesh
    {
        #region Private variables

        private int indexBufferObject;
        private int vertexBufferObject;
        private int texCoordBufferObject;
        private int vertexArrayObject;

        private BufferUsageHint indexBufferUsageHint; 
        private BufferUsageHint vertexBufferUsageHint;
        private BufferUsageHint texCoordBufferUsageHint;

        private float[] vertices;
        private float[] textureCoordinates;
        private uint[] indices;

        #endregion

        #region Constructor

        public Mesh()
        {
            VertexBufferObject = GL.GenBuffer();
            TexCoordBufferObject = GL.GenBuffer();
            IndexBufferObject = GL.GenBuffer();
            VertexArrayObject = GL.GenVertexArray();

            indexBufferUsageHint = BufferUsageHint.StaticDraw;
            vertexBufferUsageHint = BufferUsageHint.StaticDraw;
            texCoordBufferUsageHint = BufferUsageHint.StaticDraw;

            SetVertices(new float[]
            {
                -1f, -1f,   //Bottom-left
                 1f, -1f,   //Bottom-right
                -1f,  1f,   //Top-left
                 1f,  1f,   //Top-right
            }, skipBufferData: true);
            SetTextureCoordinates(new float[]
            {
                1f,  1f,   //Bottom-left
                0f,  1f,   //Bottom-right
                1f,  0f,   //Top-left
                0f,  0f,   //Top-right
            }, skipBufferData: true);
            SetIndices(new uint[]
            {
                0, 1, 2,
                2, 3, 1,
            }, skipBufferData: true);

            BufferData();
        }
        ~Mesh()
        {
            Dispose();
        }
        public void Dispose()
        {
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteBuffer(TexCoordBufferObject);
            GL.DeleteBuffer(IndexBufferObject);

            GL.DeleteVertexArray(VertexArrayObject);

            GC.SuppressFinalize(this);
        }

        #endregion

        #region Public Properties

        public int IndexBufferObject
        {
            get => indexBufferObject;
            private set => indexBufferObject = value;
        }
        public int VertexBufferObject
        {
            get => vertexBufferObject;
            private set => vertexBufferObject = value;
        }
        public int TexCoordBufferObject
        {
            get => texCoordBufferObject;
            private set => texCoordBufferObject = value;
        }
        public int VertexArrayObject
        {
            get => vertexArrayObject;
            private set => vertexArrayObject = value;
        }

        public float[] Vertices
        {
            get => vertices;
            private set => vertices = value;
        }
        public float[] TextureCoordinates
        {
            get => textureCoordinates;
            private set => textureCoordinates = value;
        }
        public uint[] Indices
        {
            get => indices;
            private set => indices = value;
        }

        private BufferUsageHint IndexBufferUsageHint
        {
            get => indexBufferUsageHint;
            set => indexBufferUsageHint = value;
        }
        private BufferUsageHint VertexBufferUsageHint
        {
            get => vertexBufferUsageHint;
            set => vertexBufferUsageHint = value;
        }
        private BufferUsageHint TexCoordBufferUsageHint
        {
            get => texCoordBufferUsageHint;
            set => texCoordBufferUsageHint = value;
        }

        #endregion

        #region Public methods

        public void SetVertices(float[] vertices, BufferUsageHint usageHint = BufferUsageHint.StaticDraw, bool skipBufferData = false)
        {
            Vertices = vertices;
            VertexBufferUsageHint = usageHint;

            if (!skipBufferData)
                BufferVertexData();
        }
        public void SetTextureCoordinates(float[] textureCoords, BufferUsageHint usageHint = BufferUsageHint.StaticDraw, bool skipBufferData = false)
        {
            TextureCoordinates = textureCoords;
            TexCoordBufferUsageHint = usageHint;

            if (!skipBufferData)
                BufferVertexData();
        }
        public void SetIndices(uint[] indices, BufferUsageHint usageHint = BufferUsageHint.StaticDraw, bool skipBufferData = false)
        {
            Indices = indices;
            IndexBufferUsageHint = usageHint;

            if (!skipBufferData)
                BufferIndexData();
        }

        public void BufferData()
        {
            BufferVertexData();
            BufferIndexData();
        }
        public void BufferVertexData()
        {
            GL.BindVertexArray(VertexArrayObject);

            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, VertexBufferUsageHint);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, TexCoordBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, TextureCoordinates.Length * sizeof(float), TextureCoordinates, TexCoordBufferUsageHint);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        public void BufferIndexData()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(uint), Indices, IndexBufferUsageHint);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        #endregion
    }
}
