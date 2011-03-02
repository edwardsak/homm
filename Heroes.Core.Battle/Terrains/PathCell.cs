using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Battle.Terrains
{
    public class PathCell 
    {
        public Cell _cell;
        public CellPartEnum _prevDirection;
        public CellPartEnum _nextDirection;
        public bool _isAvoidObstracle;

        public PathCell(Cell cell, CellPartEnum prevDirection, CellPartEnum nextDirection)
        {
            _cell = cell;
            _prevDirection = prevDirection;
            _nextDirection = nextDirection;
            _isAvoidObstracle = false;
        }

    }
}
