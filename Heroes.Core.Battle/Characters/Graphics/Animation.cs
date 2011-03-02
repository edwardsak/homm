using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Heroes.Core.Battle.Characters.Graphics
{
    public class Animation : IEnumerable
    {
        public AnimationCue[] _cues;

        public Animation(params AnimationCue[] cues)
        {
            if (cues == null || cues.Length == 0)
            {
                throw new ArgumentException("Animations must have at least one cue.", "cues");
            }
            this._cues = cues;

            int a = 0;
            foreach (AnimationCue c in this._cues)
            {
                c._arrayIndex = a;
                a += 1;
            }
        }

        public AnimationCue First
        {
            get
            {
                return _cues[0];
            }
        }

        public AnimationCue Last
        {
            get
            {
                return _cues[_cues.Length - 1];
            }
        }

        public AnimationCue this[int index]
        {
            get
            {
                return _cues[index];
            }
        }

        public AnimationRunner CreateRunner()
        {
            return new AnimationRunner(this);
        }

        public AnimationCue GetNextCue(AnimationCue cue)
        {
            AnimationCue cue2 = _cues[cue._arrayIndex + 1];
            return cue2;
        }


        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return _cues.GetEnumerator();
        }

        #endregion
    }
}
