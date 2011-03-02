using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Heroes.Core.Battle.Characters.Graphics
{
    public class AnimationRunner
    {
        private TimeSpan _remainingTimeAtThisCue;
        private Animation _subject;
        public AnimationCue _currentCue;
        public bool _isEnd;

        public AnimationRunner(Animation subject)
        {
            this._subject = subject;
            _currentCue = subject.First;
            _remainingTimeAtThisCue = _currentCue.Duration;
            _isEnd = false;
        }

        public Point Run(TimeSpan duration)
        {
            AnimationCueDirectionEnum dir = _currentCue.Next;
            // if this is the case then we never have to do time calcs
            if (dir == AnimationCueDirectionEnum.StayHere)
            {
                _isEnd = true;
                return _currentCue.Point;
            }

            while (duration.Ticks >= _remainingTimeAtThisCue.Ticks)
            {
                duration = duration.Subtract(_remainingTimeAtThisCue);
                // move to next				
                if (dir == AnimationCueDirectionEnum.MoveToNext)
                {
                    AnimationCue cue = _subject.GetNextCue(_currentCue);
                    _currentCue = cue;
                }
                else if (dir == AnimationCueDirectionEnum.MoveToBeginning)
                {
                    _currentCue = _subject.First;
                }
                _remainingTimeAtThisCue = _currentCue.Duration;
                dir = _currentCue.Next;
            }

            _remainingTimeAtThisCue = _remainingTimeAtThisCue.Subtract(duration);
            return _currentCue.Point;
        }

        public Point CurrentPoint
        {
            get
            {
                return _currentCue.Point;
            }
        }

    }
}
