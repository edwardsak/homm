using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;

namespace Heroes.Core.Battle.Rendering
{
    public class SpashScreenRenderer : Renderer
    {
        public SpashScreenRenderer()
        {
        }

        public override void Render(Controller controller)
        {
            //TexturePlus bkgd = controller.TextureStore.GetTexture(controller.Device, _imageId);

            //controller.Sprite.Draw(bkgd.Texture, new Rectangle(0, 0, 800, 600), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), Color.White);

            //if (i > 3) i = 0;

            //TexturePlus tex = null;
            //if (i == 0) tex = controller.TextureStore._tex1;
            //else if (i == 1) tex = controller.TextureStore._tex2;
            //else if (i == 2) tex = controller.TextureStore._tex3;
            //else if (i == 3) tex = controller.TextureStore._tex4;

            //controller.Sprite.Draw(tex.Texture, new Rectangle(0, 0, 800, 600), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), Color.White);
            //i += 1;
        }
    }
}
