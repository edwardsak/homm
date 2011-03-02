using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Heroes.Core.Battle.Rendering;

namespace Heroes.Core.Battle.Characters.Graphics
{
    public class AnimationCue
    {
        private Point _corner;
        private AnimationCueDirectionEnum _next;
        private TimeSpan _duration;
        public TexturePlus _tex;
        public PointF _point;
        public Size _size;


        internal int _arrayIndex;

        public AnimationCue(Point corner, AnimationCueDirectionEnum direction, TimeSpan duration)
        {
            this._corner = corner;
            this._next = direction;
            this._duration = duration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="corner"></param>
        /// <param name="direction"></param>
        /// <param name="ticks">Duration of this cue in ticks.  A tick here is 
        /// the same as for the TimeSpan class.</param>
        public AnimationCue(Point corner, AnimationCueDirectionEnum direction, long ticks)
        {
            this._corner = corner;
            this._next = direction;
            this._duration = TimeSpan.FromTicks(ticks);
        }

        public AnimationCue(TexturePlus tex, AnimationCueDirectionEnum direction, long ticks)
            : this(tex, direction, ticks, new Point(0,0), new Size(800, 600))
        {
        }

        public AnimationCue(TexturePlus tex, AnimationCueDirectionEnum direction, long ticks, Point point, Size size)
        {
            this._tex = tex;
            this._next = direction;
            this._duration = TimeSpan.FromTicks(ticks);

            _point = point;
            _size = size;
        }

        public Point Point
        {
            get
            {
                return _corner;
            }
        }

        public AnimationCueDirectionEnum Next
        {
            get
            {
                return _next;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
            }
        }

        

    }
}
