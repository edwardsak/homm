using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Map
{
    class gv
    {
        // gv = Gloval Variable

        public static int _bigMapPtX = 0;
        public static int _bigMapPtY = 0;
        public static int _bigMapPtX_eX = 0;
        public static int _bigMapPtY_eY = 0;
        public static int _cellDrawPointX = 71;     // 0
        public static int _cellDrawPointY = 71;     // 0
        public static int _row = 0;
        public static int _col = 0;
        public static int _turnCount = 0;

        public static int bottomCenterRow = 0;
        public static int RightCenterCol = 0;

        public static int cellRecordRow = 0;
        public static int cellRecordCol = 0;

        // Checkbox Variable from frmMap3.cs
        public static int _viewGridLine = 1;
        public static int _viewPassibilityCell = 1;

        // Cell Passibility Recording
        public static int StartRecordCellPassibility = 0;
    }
}
