using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Diagnostics;

using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.Characters.Graphics;
using Heroes.Core.Battle.Characters.Commands;
using Heroes.Core.Battle.Terrains;

namespace Heroes.Core.Battle.Characters
{
    public enum ActionTypeEnum
    {
        Standing,
        Moving,
        Attack,
        AttackRight,
        AttackLowerRight,
        AttackLowerLeft,
        AttackLeft,
        AttackUpperLeft,
        AttackUpperRight,
        Defend,
        GettingHit,
        Death,
        RangeAttack,
        Spell
    }

    public enum ArmySideEnum
    {
        Attacker,
        Defender
    }

    public class StandardCharacter : Heroes.Core.Battle.Armies.Army, ICharacter
    {
        #region Aniamtion Variables
        public PointF _currentAnimationPt;
        public PointF _destAnimationPt;

        public Heroes.Core.Battle.Characters.Armies.ArmyAnimations _animations;
        public Animation _currentAnimation;
        public AnimationRunner _currentAnimationRunner;

        public HorizontalDirectionEnum _currentFacingDirection;

        public AnimationSequence _currentAnimationSeq;

        protected Size _imgSize;
        protected Point _rightPt;
        protected Point _leftPt;
        protected string _prefix;   // texture file prefix
        #endregion

        protected Controller _controller;
        protected string _imgPath;

        public Command _command;

        public Cell _cell2; // big creature occupy 2 cells

        public ActionTypeEnum _actionType;
        public Cell _destCell;

        public float _moveSpeed;
        public float _moveSpeedX;
        public float _moveSpeedY;
        
        public Action _action;

        public StandardCharacter()
        {
            _moveSpeed = 5;
            _moveSpeedX = _moveSpeed;     // animation move speed
            _moveSpeedY = _moveSpeed;     // animation move speed\

            _currentFacingDirection = HorizontalDirectionEnum.Right;

			_command = new Idle();

            _animations = new Heroes.Core.Battle.Characters.Armies.ArmyAnimations();

            _isBeginTurn = false;
            _isEndTurn = false;

            _isDead = false;

            _currentAnimationSeq = null;
        }

        public virtual void FirstLoad()
        {
            _action = CreateStandingAction(this, _currentFacingDirection, _isBeginTurn);
            SetAnimation(_action._currentAnimationSeq);
        }

        public void Initalize()
        {
            _action = CreateFirstStandingAction(this, _currentFacingDirection);
            SetAnimation(_action._currentAnimationSeq);

			//_currentAnimationRunner = _currentAnimation.CreateRunner();
			//StoredEffectColor = Color.White

            if (_command == null)
            {
                _command = new Idle();
            }
        }

        public TexturePlus GetTexture()
        {
            return null;
        }

        public static void SetAnimation(ICharacter character, AnimationSequence seq)
        {
            if (character.CurrentAnimation != seq._animation)
            {
                character.CurrentAnimation = seq._animation;
                character.CurrentAnimationRunner = character.CurrentAnimation.CreateRunner();
            }
        }

        public void SetAnimation(AnimationSequence seq)
        {
            SetAnimation(this, seq);
        }

        public void SetAnimation(AnimationPurposeEnum purpose, HorizontalDirectionEnum facing)
        {
            if (purpose == AnimationPurposeEnum.StandingStill)
            {
                if (facing == HorizontalDirectionEnum.Right)
                {
                    Animation animation = null;
                    if (this._isBeginTurn)
                        animation = this._animations._standingRightActive;
                    else
                        animation = this._animations._standingRight;

                    if (this._currentAnimation != animation)
                    {
                        this._currentAnimation = animation;
                        this._currentAnimationRunner = this._currentAnimation.CreateRunner();
                    }
                }
                else if (facing == HorizontalDirectionEnum.Left)
                {
                    Animation animation = null;
                    if (this._isBeginTurn)
                        animation = this._animations._standingLeftActive;
                    else
                        animation = this._animations._standingLeft;

                    if (this._currentAnimation != animation)
                    {
                        this._currentAnimation = animation;
                        this._currentAnimationRunner = this._currentAnimation.CreateRunner();
                    }
                }
            }
            else if (purpose == AnimationPurposeEnum.Moving)
            {
                if (facing == HorizontalDirectionEnum.Right)
                {
                    if (this._currentAnimation != this._animations._movingRight)
                    {
                        this._currentAnimation = this._animations._movingRight;
                        this._currentAnimationRunner = this._currentAnimation.CreateRunner();
                    }
                }
                else if (facing == HorizontalDirectionEnum.Left)
                {
                    if (this._currentAnimation != this._animations._movingLeft)
                    {
                        this._currentAnimation = this._animations._movingLeft;
                        this._currentAnimationRunner = this._currentAnimation.CreateRunner();
                    }
                }
            }
            else if (purpose == AnimationPurposeEnum.Hover)
            {
                if (facing == HorizontalDirectionEnum.Right)
                {
                    if (this._currentAnimation != this._animations._standingRightHover)
                    {
                        this._currentAnimation = this._animations._standingRightHover;
                        this._currentAnimationRunner = this._currentAnimation.CreateRunner();
                    }
                }
                else if (facing == HorizontalDirectionEnum.Left)
                {
                    if (this._currentAnimation != this._animations._standingLeftHover)
                    {
                        this._currentAnimation = this._animations._standingLeftHover;
                        this._currentAnimationRunner = this._currentAnimation.CreateRunner();
                    }
                }
            }
        }

        public void SetDefaultAnimation()
        {
            Heroes.Core.Battle.Characters.Armies.Army army = (Heroes.Core.Battle.Characters.Armies.Army)this;
            if (army._isDead) return;

            StandardCharacter character = army;

            Action action = character.CreateStandingAction(character, character.CurrentFacingDirection, character._isBeginTurn);
            character.SetAnimation(action._currentAnimationSeq);
        }

        public Command Command
        {
            get { return _command; }
            set { _command = value; }
        }

        public void InitCell(Cell cell)
        {
            _cell = cell;
            Point pt = cell.GetStandingPoint();
            this._currentAnimationPt = pt;
            this._destAnimationPt = new Point(pt.X, pt.Y);
        }

        public void SetCell(Cell cell)
        {
            _cell._character = null;
            
            _cell = cell;
            _cell._character = this;

            Point pt = cell.GetStandingPoint();
            this._destAnimationPt = pt;
        }

        public Action CreateFirstStandingAction(StandardCharacter character, HorizontalDirectionEnum facing)
        {
            Action action = new Action(ActionTypeEnum.Standing);

            Animation animation = null;
            if (facing == HorizontalDirectionEnum.Right)
            {
                animation = _animations._firstStandingRight;    
            }
            else
            {
                animation = _animations._firstStandingLeft;
            }

            AnimationSequence seq = new AnimationSequence(animation, AnimationPurposeEnum.StandingStill, facing);
            action._animationSeqs.Add(seq);
            action._currentAnimationSeq = seq;

            return action;
        }

        public Action CreateStandingAction(StandardCharacter character, HorizontalDirectionEnum facing, bool isActive)
        {
            // no standing for dead character
            if (character._isDead) return null;

            Action action = new Action(ActionTypeEnum.Standing);

            Animation animation = null;
            if (facing == HorizontalDirectionEnum.Right)
            {
                if (isActive)
                    animation = _animations._standingRightActive;
                else
                    animation = _animations._standingRight;
            }
            else
            {
                if (isActive)
                    animation = _animations._standingLeftActive;
                else
                    animation = _animations._standingLeft;
            }

            if (animation == null)
            {
                Debug.WriteLine("");
            }

            AnimationSequence seq = new AnimationSequence(animation, AnimationPurposeEnum.StandingStill, facing);
            action._animationSeqs.Add(seq);
            action._currentAnimationSeq = seq;

            return action;
        }

        private void SetMovingAction(Action action, ArrayList path)
        {
            action._path = path;

            // moving seqs
            Cell prevCell = this._cell;
            HorizontalDirectionEnum facing = this.CurrentFacingDirection;

            // start moving
            {
                Animation animation = null;
                if (facing == HorizontalDirectionEnum.Right)
                    animation = _animations._startMovingRight;
                else
                    animation = _animations._startMovingLeft;

                // not all army has start moving
                if (animation != null)
                {
                    AnimationSequence seq = new AnimationSequence(animation, AnimationPurposeEnum.StartMoving, facing);
                    seq._destPoint = prevCell.GetStandingPoint();

                    action._animationSeqs.Add(seq);
                }
            }

            foreach (Cell cell in path)
            {
                // get direction
                Point prevPoint = prevCell.GetCenterPoint();
                Point point = cell.GetCenterPoint();
                CellPartEnum direction = BattleTerrain.FindDirection(prevPoint, point);

                Animation animation = null;
                facing = HorizontalDirectionEnum.None;
                switch (direction)
                {
                    case CellPartEnum.CenterRight:
                    case CellPartEnum.UpperRight:
                    case CellPartEnum.LowerRight:
                        animation = _animations._movingRight;
                        facing = HorizontalDirectionEnum.Right;
                        break;
                    default:
                        animation = _animations._movingLeft;
                        facing = HorizontalDirectionEnum.Left;
                        break;
                }

                if (animation == null)
                {
                    Debug.WriteLine("");
                }

                AnimationSequence seq = new AnimationSequence(animation, AnimationPurposeEnum.Moving, facing);
                seq._destPoint = cell.GetStandingPoint();

                action._animationSeqs.Add(seq);

                prevCell = cell;
            }

            // stop moving
            {
                Animation animation = null;
                if (facing == HorizontalDirectionEnum.Right)
                    animation = _animations._stopMovingRight;
                else
                    animation = _animations._stopMovingLeft;

                // not all army has stop moving
                if (animation != null)
                {
                    AnimationSequence seq = new AnimationSequence(animation, AnimationPurposeEnum.StopMoving, facing);
                    seq._destPoint = prevCell.GetStandingPoint();

                    action._animationSeqs.Add(seq);
                }
            }
        }

        public Action CreateMovingAction(ArrayList path)
        {
            Action action = new Action(ActionTypeEnum.Moving);

            SetMovingAction(action, path);

            action._currentAnimationSeq = (AnimationSequence)action._animationSeqs[0];

            return action;
        }

        public Action CreateAttackAction(ArrayList path, CellPartEnum attackDirection, StandardCharacter target)
        {
            Action action = new Action(ActionTypeEnum.Attack);
            action._targetCharacter = target;

            SetMovingAction(action, path);
            
            // attack
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                    {
                        // attack begin
                        AnimationSequence seq = new AnimationSequence(_animations._attackStraightRightBegin, AnimationPurposeEnum.AttackBegin, HorizontalDirectionEnum.Right);
                        action._animationSeqs.Add(seq);

                        // attack end
                        seq = new AnimationSequence(_animations._attackStraightRightEnd, AnimationPurposeEnum.AttackEnd, HorizontalDirectionEnum.Right);
                        action._animationSeqs.Add(seq);
                    }
                    break;
                case CellPartEnum.CenterLeft:
                    {
                        // attack begin
                        AnimationSequence seq = new AnimationSequence(_animations._attackStraightLeftBegin, AnimationPurposeEnum.AttackBegin, HorizontalDirectionEnum.Left);
                        action._animationSeqs.Add(seq);

                        // attack end
                        seq = new AnimationSequence(_animations._attackStraightLeftEnd, AnimationPurposeEnum.AttackEnd, HorizontalDirectionEnum.Left);
                        action._animationSeqs.Add(seq);
                    }
                    break;
                case CellPartEnum.LowerRight:
                    {
                        // attack begin
                        AnimationSequence seq = new AnimationSequence(_animations._attackStraightRightBegin, AnimationPurposeEnum.AttackBegin, HorizontalDirectionEnum.Right);
                        action._animationSeqs.Add(seq);

                        // attack end
                        seq = new AnimationSequence(_animations._attackStraightRightEnd, AnimationPurposeEnum.AttackEnd, HorizontalDirectionEnum.Right);
                        action._animationSeqs.Add(seq);
                    }
                    break;
                case CellPartEnum.LowerLeft:
                    {
                        // attack begin
                        AnimationSequence seq = new AnimationSequence(_animations._attackStraightLeftBegin, AnimationPurposeEnum.AttackBegin, HorizontalDirectionEnum.Left);
                        action._animationSeqs.Add(seq);

                        // attack end
                        seq = new AnimationSequence(_animations._attackStraightLeftEnd, AnimationPurposeEnum.AttackEnd, HorizontalDirectionEnum.Left);
                        action._animationSeqs.Add(seq);
                    }
                    break;
                case CellPartEnum.UpperRight:
                    {
                        // attack begin
                        AnimationSequence seq = new AnimationSequence(_animations._attackStraightRightBegin, AnimationPurposeEnum.AttackBegin, HorizontalDirectionEnum.Right);
                        action._animationSeqs.Add(seq);

                        // attack end
                        seq = new AnimationSequence(_animations._attackStraightRightEnd, AnimationPurposeEnum.AttackEnd, HorizontalDirectionEnum.Right);
                        action._animationSeqs.Add(seq);
                    }
                    break;
                case CellPartEnum.UpperLeft:
                    {
                        // attack begin
                        AnimationSequence seq = new AnimationSequence(_animations._attackStraightLeftBegin, AnimationPurposeEnum.AttackBegin, HorizontalDirectionEnum.Left);
                        action._animationSeqs.Add(seq);

                        // attack end
                        seq = new AnimationSequence(_animations._attackStraightLeftEnd, AnimationPurposeEnum.AttackEnd, HorizontalDirectionEnum.Left);
                        action._animationSeqs.Add(seq);
                    }
                    break;
            }

            action._currentAnimationSeq = (AnimationSequence)action._animationSeqs[0];

            return action;
        }

        public Action CreateGettingHitAction(HorizontalDirectionEnum facing)
        {
            Action action = new Action(ActionTypeEnum.GettingHit);

            Animation animation = null;
            if (facing == HorizontalDirectionEnum.Right)
            {
                animation = _animations._gettingHitRight;
            }
            else
            {
                animation = _animations._gettingHitLeft;
            }

            AnimationSequence seq = new AnimationSequence(animation, AnimationPurposeEnum.GettingHit, facing);
            action._animationSeqs.Add(seq);
            action._currentAnimationSeq = seq;

            return action;
        }

        public Action CreateDeathAction(HorizontalDirectionEnum facing)
        {
            Action action = new Action(ActionTypeEnum.Death);

            Animation animation = null;
            if (facing == HorizontalDirectionEnum.Right)
            {
                animation = _animations._deathRight;
            }
            else
            {
                animation = _animations._deathLeft;
            }

            AnimationSequence seq = new AnimationSequence(animation, AnimationPurposeEnum.Death, facing);
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
                return _currentAnimationSeq;
            }
            set
            {
                _currentAnimationSeq = value;
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
