using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Heroes.Core.Castle
{
    public class MyButton
    {
        #region
        public delegate void ClickedEventHandler(object sender);

        public event ClickedEventHandler Clicked;

        protected virtual void OnClicked(object sender)
        {
            if (Clicked != null)
            {
                //Invokes the delegates.
                Clicked(sender);
            }
        }
        #endregion

        public Rectangle _rect;

        public MyButton()
        {
            _rect = new Rectangle(0, 0, 20, 20);
        }

        public MyButton(int x, int y, int w, int h)
        {
            _rect = new Rectangle(x, y, w, h);
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(new SolidBrush(Color.Blue), 2), _rect);
        }

        public void Click(int x, int y)
        {
            if (_rect.Contains(x, y))
            {
                // raise event
                OnClicked(this);
            }
        }
    }
}
