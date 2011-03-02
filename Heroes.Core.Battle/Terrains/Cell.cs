using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.Characters;

namespace Heroes.Core.Battle.Terrains
{
    public class Cell
    {
        public const int HEIGHT_HEAD = 10;
        public const int HEIGHT_BODY = 32;
        public const int WIDTH_BODY = 45;
        public const int WIDTH_MIDDLE = 23;
        public const int HEIGHT = 52;
        public const int WIDTH_BODY_LEFT = 10;
        public const int WIDTH_BODY_CENTER = 25;
        public const int WIDTH_BODY_RIGHT = 10;

        public int _row;
        public int _col;
        public Rectangle _rect;
        public Point[] _points;
        public TexturePlus _tex;
        public TexturePlus _texShd;

        public Dictionary<CellPartEnum, Cell> _adjacentCells;

        public bool _isHover;

        public Heroes.Core.Battle.Armies.Army _character;

        public Cell()
        {
            _rect = new Rectangle(50, 50, 50, 50);

            _adjacentCells = new Dictionary<CellPartEnum, Cell>();
        }

        public Cell(int row, int col, int x, int y)
        {
            _row = row;
            _col = col;
            _rect = new Rectangle(x, y, 45, 52);

            _adjacentCells = new Dictionary<CellPartEnum, Cell>();
        }

        public Cell(int row, int col, int x, int y, TexturePlus tex, TexturePlus texShd)
        {
            _row = row;
            _col = col;
            _rect = new Rectangle(x, y, 45, 52);
            _tex = tex;
            _texShd = texShd;

            _adjacentCells = new Dictionary<CellPartEnum, Cell>();
        }

        public void Draw(Sprite sprite)
        {
            if (_isHover)
                sprite.Draw(_texShd.Texture, new Rectangle(3, 0, 45, 52), new Vector3(0f, 0f, 0f), new Vector3((Single)_rect.X, (Single)_rect.Y, 0f), Color.White);
            
            sprite.Draw(_tex.Texture, new Rectangle(3, 0, 45, 52), new Vector3(0f, 0f, 0f), new Vector3((Single)_rect.X, (Single)_rect.Y, 0f), Color.White);
        }

        public Point GetCenterPoint()
        {
            return new Point(_rect.Left + _rect.Width / 2, _rect.Top + _rect.Height / 2);
        }

        public Point GetStandingPoint()
        {
            return new Point(_rect.Left + _rect.Width / 2, _rect.Top + HEIGHT_HEAD + HEIGHT_BODY);
        }

        public bool HasPassability()
        {
            if (this._character != null)
            {
                return this._character._isDead;
            }

            return true;
        }

    }
}
