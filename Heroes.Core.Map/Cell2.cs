//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Heroes.Core.Map
{
    public class Cell2 
    {
        public int _row;
        public int _col;
        public Hero _hero;
        public Town _town;
        public Mine _mine;
        public bool _passability;

        public Rectangle _rect;
    }
}
