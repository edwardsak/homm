using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Heroes.Core.Battle.Rendering
{
    public class TexturePlus : System.IDisposable
    {
        Texture _texture;
        int _width;
        int _height;
        int _index;

        public TexturePlus(Texture texture, int width, int height, int backIndex)
        {
            this._texture = texture;
            this._width = width;
            this._height = height;
            this._index = backIndex;
        }

        public TexturePlus(Device device, string resName)
        {
            Resource rs = new Resource(resName);
            if (rs.IsFile)
            {
                ImageInformation i = TextureLoader.ImageInformationFromFile(resName);
                _height = i.Height;
                _width = i.Width;

                _texture = TextureLoader.FromFile(device, resName, 0, 0, 0, 0, Format.A8R8G8B8, Pool.Managed, Filter.None, Filter.None, 0);
            }
            else
            {
                using (Stream s = rs.GetStream())
                {
                    ImageInformation i = TextureLoader.ImageInformationFromStream(s);
                    _height = i.Height;
                    _width = i.Width;
                }

                using (Stream s = rs.GetStream())
                {
                    _texture = TextureLoader.FromStream(device, s, 0, 0, 0, 0, Format.A8R8G8B8, Pool.Managed, Filter.None, Filter.None, 0);
                }
            }
        }

        public TexturePlus(Device device, byte[] res)
        {
            using (MemoryStream s = new MemoryStream(res))
            {
                ImageInformation i = TextureLoader.ImageInformationFromStream(s);
                _height = i.Height;
                _width = i.Width;
            }

            using (MemoryStream s = new MemoryStream(res))
            {
                _texture = TextureLoader.FromStream(device, s, 0, 0, 0, 0, Format.A8R8G8B8, Pool.Managed, Filter.None, Filter.None, 0);
            }
        }

        public Texture Texture
        {
            get { return _texture; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_texture != null)
            {
                _texture.Dispose();
                _texture = null;
            }
        }

        #endregion
    }
}
