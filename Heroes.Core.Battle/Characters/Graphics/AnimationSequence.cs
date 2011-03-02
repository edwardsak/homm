using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Terrains;

namespace Heroes.Core.Battle.Characters.Graphics
{
    public class AnimationSequence
    {
        public Animation _animation;
        public AnimationPurposeEnum _purpose;
        public HorizontalDirectionEnum _facing;

        public PointF _destPoint;

        public ArrayList _triggerAnimationSeqs;     // animation seqs to trigger
        public bool _waitToTrigger;
        public bool _isStarted;
        public bool _isEnd;

        public bool _triggerWhenBegin;
        public bool _triggerWhenEnd;

        public AnimationSequence(Animation animation, AnimationPurposeEnum purpose, HorizontalDirectionEnum facing)
        {
            _animation = animation;
            _purpose = purpose;
            _facing = facing;

            _destPoint = new PointF(0f, 0f);

            _triggerAnimationSeqs = new ArrayList();
            _waitToTrigger = false;
            _isStarted = false;
            _isEnd = false;

            _triggerWhenBegin = false;
            _triggerWhenEnd = false; 
        }

        public void Trigger()
        {
            if (_triggerAnimationSeqs == null) return;

            foreach (AnimationSequence seq in _triggerAnimationSeqs)
            {
                seq._waitToTrigger = false;
            }
        }

    }
}
