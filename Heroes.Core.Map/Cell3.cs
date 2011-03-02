using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Heroes.Core.Map
{
    public class Cell3
    {
        public Cell3(int x, int y, int width, int height)
        {
            _rect = new Rectangle(x, y, width, height);
            _passability = false;
        }

        public delegate void ClickedEventHandler(object sender);
        public event ClickedEventHandler Clicked;
        protected virtual void OnClicked(object sender)
        {
            if (Clicked != null)
            {
                Clicked(sender);
            }
        }

        public int _id;
        public int _row;
        public int _col;
        public Hero _hero;
        public Town _town;
        public Mine _mine;
        public bool _passability;
        public Rectangle _rect;
        public ArrayList _neighbourCell;

        public void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(new SolidBrush(Color.Blue), 2), _rect);
        }

        public void Click(int x, int y)
        {
            if (_rect.Contains(x, y))
            {
                OnClicked(this);
            }
        }

        public Cell3 Clone()
        {
            Cell3 c = new Cell3(this._rect.X, this._rect.Y, this._rect.Width, this._rect.Height);
            c._id = this._id;
            c._row = this._row;
            c._col = this._col;
            c._neighbourCell = this._neighbourCell;

            return c;
        }

    }
}
