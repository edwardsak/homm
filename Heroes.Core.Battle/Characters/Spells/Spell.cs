using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.Terrains;
using Heroes.Core.Battle.Characters.Graphics;
using Heroes.Core.Battle.Characters.Commands;

namespace Heroes.Core.Battle.Characters.Spells
{
    public class Spell : Heroes.Core.Spell, ICharacter
    {
        #region Aniamtion Variables
        public PointF _currentAnimationPt;
        public PointF _destAnimationPt;

        public SpellAnimations _animations;
        public Animation _currentAnimation;
        public AnimationRunner _currentAnimationRunner;

        public HorizontalDirectionEnum _currentFacingDirection;

        public AnimationSequence _currentAnimationSeq;

        protected Point _rightPt;
        protected Point _leftPt;
        protected Size _imgSize;
        #endregion

        protected Controller _controller;
        protected string _imgPath;

        public Cell _cell;
        public Cell _destCell;

        public ActionTypeEnum _actionType;

        public Command _command;

        public float _defaultMoveSpeed;
        public float _moveSpeedX;
        public float _moveSpeedY;

        Hashtable _animationDataKPurs;

        public Spell()
        {
            Init();
        }

        public Spell(Controller controller, int id)
        {
            Init();

            _controller = controller;

            if (!Heroes.Core.Setting._spellAnimationKIdKPurs.ContainsKey(id)) return;
            _animationDataKPurs = Heroes.Core.Setting._spellAnimationKIdKPurs[id];

            if (!_animationDataKPurs.ContainsKey(Heroes.Core.Heros.SpellAnimationData.PURPOSE_ON_ARMY)) return;
            AnimationData animationStanding = (AnimationData)_animationDataKPurs[Heroes.Core.Heros.SpellAnimationData.PURPOSE_ON_ARMY];
        }

        private void Init()
        {
            _animations = new SpellAnimations();

            _command = new Idle();

            _defaultMoveSpeed = 40;
            _moveSpeedX = _defaultMoveSpeed;
            _moveSpeedY = _defaultMoveSpeed;
        }

        public virtual void FirstLoad()
        {
            if (_animationDataKPurs.ContainsKey(Heroes.Core.Heros.SpellAnimationData.PURPOSE_ON_ARMY))
            {
                Heroes.Core.Heros.SpellAnimationData animateData 
                    = (Heroes.Core.Heros.SpellAnimationData)_animationDataKPurs[Heroes.Core.Heros.SpellAnimationData.PURPOSE_ON_ARMY];

                string path = string.Format(@"{0}\Images\Battle\Sprites\Spells\{1}", Setting._appStartupPath, animateData._folder);

                this._animations._onArmy = Character.CreateAnimation(_controller, path, animateData._prefix, "",
                    animateData._fileNos, animateData._point, animateData._size, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }

            if (_animationDataKPurs.ContainsKey(Heroes.Core.Heros.SpellAnimationData.PURPOSE_MISSILE))
            {
                Heroes.Core.Heros.SpellAnimationData animateData
                    = (Heroes.Core.Heros.SpellAnimationData)_animationDataKPurs[Heroes.Core.Heros.SpellAnimationData.PURPOSE_MISSILE];

                _defaultMoveSpeed = animateData._moveSpeed;
                _moveSpeedX = _defaultMoveSpeed;
                _moveSpeedY = _defaultMoveSpeed;

                string path = string.Format(@"{0}\Images\Battle\Sprites\Spells\{1}", Setting._appStartupPath, animateData._folder);

                this._animations._missileRight = Character.CreateAnimation(_controller, path, animateData._prefix, "",
                    animateData._fileNos, animateData._point, animateData._size, AnimationCueDirectionEnum.MoveToBeginning, animateData._turnPerFrame);

                this._animations._missileLeft = Character.CreateAnimation(_controller, path, animateData._prefix, "",
                    animateData._fileNos, animateData._point, animateData._size, AnimationCueDirectionEnum.MoveToBeginning, animateData._turnPerFrame);
            }

            if (_animationDataKPurs.ContainsKey(Heroes.Core.Heros.SpellAnimationData.PURPOSE_HIT))
            {
                Heroes.Core.Heros.SpellAnimationData animateData
                    = (Heroes.Core.Heros.SpellAnimationData)_animationDataKPurs[Heroes.Core.Heros.SpellAnimationData.PURPOSE_HIT];

                string path = string.Format(@"{0}\Images\Battle\Sprites\Spells\{1}", Setting._appStartupPath, animateData._folder);

                this._animations._hitRight = Character.CreateAnimation(_controller, path, animateData._prefix, "",
                    animateData._fileNos, animateData._point, animateData._size, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);

                this._animations._hitLeft = Character.CreateAnimation(_controller, path, animateData._prefix, "",
                    animateData._fileNos, animateData._point, animateData._size, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }
        }

        public void SetDefaultAnimation()
        {
        }

        public static Spell CreateSpell(Heroes.Core.Spell spell, Controller controller)
        {
            Heroes.Core.Battle.Characters.Spells.Spell spell2
                = new Spell(controller, spell._id);

            spell2.CopyFrom(spell);

            return spell2;
        }

        #region ICharacter Members

        public PointF CurrentAnimationPt
        {
            get
            {
                return this._currentAnimationPt;
            }
            set
            {
                this._currentAnimationPt = value;
            }
        }

        public PointF DestAnimationPt
        {
            get
            {
                return this._destAnimationPt;
            }
            set
            {
                this._destAnimationPt = value;
            }
        }

        public Animations Animations
        {
            get
            {
                return _animations;
            }
        }

        public Animation CurrentAnimation
        {
            get
            {
                return _currentAnimation;
            }
            set
            {
                _currentAnimation = value;
            }
        }

        public AnimationRunner CurrentAnimationRunner
        {
            get
            {
                return _currentAnimationRunner;
            }
            set
            {
                _currentAnimationRunner = value;
            }
        }

        public HorizontalDirectionEnum CurrentFacingDirection
        {
            get
            {
                return _currentFacingDirection;
            }
            set
            {
                _currentFacingDirection = value;
            }
        }

        public AnimationSequence CurrentAnimationSeq
        {
            get
            {
                return this._currentAnimationSeq;
            }
            set
            {
                this._currentAnimationSeq = value;
            }
        }

        public void SetAnimation(AnimationSequence seq)
        {
            StandardCharacter.SetAnimation(this, seq);
        }

        public Point TextureRightPoint
        {
            get { return _rightPt; }
            set { _rightPt = value; }
        }

        public Point TextureLeftPoint
        {
            get { return _leftPt; }
            set { _leftPt = value; }
        }

        public Size TextureSize
        {
            get { return _imgSize; }
            set { _imgSize = value; }
        }

        public float MoveSpeedX
        {
            get { return _moveSpeedX; }
            set { _moveSpeedX = value; }
        }

        public float MoveSpeedY
        {
            get { return _moveSpeedY; }
            set { _moveSpeedY = value; }
        }

        #endregion

    }
}
