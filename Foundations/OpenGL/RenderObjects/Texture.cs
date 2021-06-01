using System;
using System.Collections.Generic;
using SysDraw = System.Drawing;
using SysDrawImg = System.Drawing.Imaging;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace PixelGame.Foundations
{
    public class Texture
    {
        private int textureId;
        private Material material;

        public Texture()
        {
            TextureId = GL.GenTexture();
            MainMaterial = Material.DefaultMaterial;
        }
        ~Texture()
        {
            Dispose();
        }
        public void Dispose()
        {
            GL.DeleteTexture(TextureId);
            GL.DeleteProgram(material);

        }

        public void SetTexture(IntPtr _texture, int _width, int _height, PixelInternalFormat _internalPixelFormat = PixelInternalFormat.Rgba, PixelFormat _pixelFormat = PixelFormat.Bgra)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureId);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, _internalPixelFormat, _width, _height, 0, _pixelFormat, PixelType.UnsignedByte, _texture);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            Width = _width;
            Height = _height;
            PixelFormat = _pixelFormat;
            PixelInternalFormat = _internalPixelFormat;
        }
        public void SetTexture(SysDraw.Bitmap bitmap)
        {
            SysDrawImg.BitmapData data = bitmap.LockBits(new SysDraw.Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                SysDrawImg.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            SetTexture(data.Scan0, bitmap.Width, bitmap.Height);

            bitmap.UnlockBits(data);
        }

        public override string ToString()
        {
            return $"Texture ({Width}, {Height}) [{PixelFormat}; {PixelInternalFormat}]";
        }

        public int Width
        {
            get; private set;
        }
        public int Height
        {
            get; private set;
        }
        public PixelFormat PixelFormat
        {
            get; private set;
        }
        public PixelInternalFormat PixelInternalFormat
        {
            get; private set;
        }

        public int TextureId
        {
            get => textureId;
            private set => textureId = value;
        }

        public Material MainMaterial
        {
            get => material;
            set => material = value;
        }
    }
}
