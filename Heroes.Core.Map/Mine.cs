using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Heroes.Core.Map
{
    public class Mine : Heroes.Core.Mine
    {
        public Image _image;

        public Mine(Image img, int id)
            : base(id)
        {
            _image = img;
        }

    }
}
