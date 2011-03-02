using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.Characters.Graphics;
using Heroes.Core.Battle.Characters.Commands;
using Heroes.Core.Battle.Characters.Heros;

namespace Heroes.Core.Battle.Characters
{
    public class Hero : Heroes.Core.Hero, ICharacter
    {
        #region Aniamtion Variables
        public PointF _currentAnimationPt;
        public PointF _destAnimationPt;

        public HeroAnimations _animations;
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

        public ArmySideEnum _armySide;

        public Rectangle _rect;     // hero size, click it to show hero info

        public Heroes.Core.Hero _originalHero;

        public Heroes.Core.Spell _currentSpell;
        public bool _canCastSpell;

        float _moveSpeedX;
        float _moveSpeedY;

        public PointF _standingPointRight;
        public PointF _standingPointLeft;

        public PointF _castingPointRight;
        public PointF _castingPointLeft;
        public float _castingHeight;        // position of hero's casting hand from standing point

        public Hero()
        {

            _animations = new HeroAnimations();

            _standingPointRight = new PointF(27f, 127f);
            _standingPointLeft = new PointF(800f - 27f, 127f);

            _castingHeight = 64f;
            _castingPointRight = new PointF(27f + 54f, // plus 54f to make it cast in front of hero
                127f - _castingHeight);
            _castingPointLeft = new PointF(800f - 27f - 54f, 127f - _castingHeight);

            _imgSize = new Size(150, 175);
            _rightPt = new Point(43, 146 - 127);
            _leftPt = new Point(48, 146 - 127);
            _rect = new Rectangle(0, 16, 54, 111);

            _currentSpell = null;
            _canCastSpell = true;

            _currentAnimationSeq = null;
        }

        public Hero(int id)
            : base(id)
        { 
        }

        public virtual void FirstLoad()
        {
            
        }

        public void Initalize()
        {
            if (_armySide == ArmySideEnum.Attacker)
            {
                _currentFacingDirection = HorizontalDirectionEnum.Right;
                _currentAnimationPt = new Point(0, 0);
            }
            else
            {
                _currentFacingDirection = HorizontalDirectionEnum.Left;
                _currentAnimationPt = new Point(800 - _rect.Width, 0);
                _rect.X = (int)_currentAnimationPt.X;
            }

            Action action = CreateStandingAction(this, _currentFacingDirection);
            SetAnimation(action._currentAnimationSeq);
        }

        public void SetAnimation(AnimationSequence seq)
        {
            StandardCharacter.SetAnimation(this, seq);
        }

        public void SetDefaultAnimation()
        {
            Heroes.Core.Battle.Characters.Hero hero = (Heroes.Core.Battle.Characters.Hero)this;
            
            Action action = hero.CreateStandingAction(hero, hero.CurrentFacingDirection);
            hero.SetAnimation(action._currentAnimationSeq);
        }

        public Action CreateStandingAction(Hero character, HorizontalDirectionEnum facing)
        {
            Action action = new Action(ActionTypeEnum.Standing);

            Animation animation = null;
            if (facing == HorizontalDirectionEnum.Right)
            {
                if (this._sex == SexEnum.Male)
                    animation = this._animations._standingRightMale;
                else
                    animation = this._animations._standingRightFemale;
            }
            else
            {
                if (this._sex == SexEnum.Male)
                    animation = this._animations._standingLeftMale;
                else
                    animation = this._animations._standingLeftFemale;
            }

            AnimationSequence seq = new AnimationSequence(animation, AnimationPurposeEnum.StandingStill, facing);
            action._animationSeqs.Add(seq);
            action._currentAnimationSeq = seq;

            return action;
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
