using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Heroes.Core.Battle.Rendering
{
    public class TextureStore : System.IDisposable
    {
        public Hashtable _texs;
        public TexturePlus _texBg;
        public TexturePlus _texBar;

        public TexturePlus _texCell;
        public TexturePlus _texCellShd;

        public TextureStore(Device device, EventHandler progressReport)
        {
            _texs = new Hashtable();

            LoadTextures(device, progressReport);
        }

        ~TextureStore()
        {
            Dispose();
        }

        #region IDisposable Members

        public void Dispose()
        {
            //if (m_background != null)
            //{
            //    m_background.Dispose();
            //    //back.Dispose();
            //    //main.Dispose();
            //    //fore.Dispose();
            //    //backanim.Dispose();
            //    //mainanim.Dispose();
            //    //foreanim.Dispose();
            //    //m_textbox.Dispose();
            //    m_swordMan.Dispose();
            //    //m_items.Dispose();
            //    //m_frabbit.Dispose();
            //    //m_turtle.Dispose();
            //    //m_enemies1.Dispose();
            //    //m_ice.Dispose();
            //    //m_whale.Dispose();
            //    //m_ito.Dispose();
            //    foreach (TexturePlus t in otherTextures.Values)
            //    {
            //        t.Dispose();
            //    }

            //    m_background = null;
            //}
        }

        #endregion

        /// <summary>
        /// Loads new all textures in the settings file that need to be loaded.
        /// This can be called multiple times to load different files by 
        /// changing the paths in the settings object.
        /// 
        /// Probably should make sure textures are not being used at this point ;)
        /// </summary>
        /// <param name="device"></param>
        /// <param name="progressReport"></param>
        public void LoadTextures(Device device, EventHandler progressReport)
        {
            _texBg = new TexturePlus(device, string.Format(@"Heroes.Core.Battle.Images.Battle.Bitmaps.CmBkMag.dds"));
            _texBar = new TexturePlus(device, string.Format(@"Heroes.Core.Battle.Images.Battle.Bitmaps.cbar.dds"));

            _texCell = new TexturePlus(device, string.Format(@"Heroes.Core.Battle.Images.Battle.Bitmaps.CCellGrd.dds"));
            _texCellShd = new TexturePlus(device, string.Format(@"Heroes.Core.Battle.Images.Battle.Bitmaps.CCellShd.dds"));

            if (progressReport != null)
            {
                progressReport(this, EventArgs.Empty);
            }
        }

        //public TexturePlus GetExtraTexture(Device device, Settings.ImageTypeEnum imageId)
        //{
        //    if (_texs.ContainsKey(imageId))
        //    {
        //        return (TexturePlus)(_texs[imageId]);
        //    }

        //    TexturePlus tex = null;
        //    tex = new TexturePlus(device, GetImage(imageId));
        //    _texs.Add(imageId, tex);
        //    return tex;
        //}

        public TexturePlus GetTexture(Device device, string resName)
        {
            if (_texs.ContainsKey(resName))
            {
                return (TexturePlus)(_texs[resName]);
            }

            TexturePlus tex = null;
            tex = new TexturePlus(device, resName);
            _texs.Add(resName, tex);
            return tex;
        }

        public TexturePlus GetTexture(Device device, string resName, byte[] res)
        {
            if (_texs.ContainsKey(resName))
            {
                return (TexturePlus)(_texs[resName]);
            }

            TexturePlus tex = null;
            tex = new TexturePlus(device, res);
            _texs.Add(resName, tex);
            return tex;
        }

    }
}
