using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Remoting
{
    [Serializable]
    public class GameCell
    {
        public int _row;
        public int _col;

        public GameCell(int row, int col)
        {
            _row = row;
            _col = col;
        }

    }
}
