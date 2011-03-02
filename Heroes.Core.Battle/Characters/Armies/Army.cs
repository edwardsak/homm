using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.Characters;
using Heroes.Core.Battle.Characters.Graphics;
using Heroes.Core.Battle.Terrains;

namespace Heroes.Core.Battle.Characters.Armies
{
    public class Army : StandardCharacter
    {
        public Image _portrait;
        
        Hashtable _animationDataKPurs;

        public Army(ArmySideEnum armySide)
        {
            Init(armySide);
        }

        public Army(ArmySideEnum armySide, Controller controller, int id)
        {
            Init(armySide);

            _controller = controller;

            if (!Heroes.Core.Setting._armyAnimationKIdKPurs.ContainsKey(id)) return;
            _animationDataKPurs = Heroes.Core.Setting._armyAnimationKIdKPurs[id];

            if (!_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_STANDING)) return;
            AnimationData animationStanding = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_STANDING];

            _imgPath = string.Format(@"{0}\Images\Battle\Sprites\Armies\{1}", Setting._appStartupPath, animationStanding._folder);
            _prefix = animationStanding._prefix;
            _moveSpeed = animationStanding._moveSpeed;

            this._animations._firstStandingRight = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\{1}{2}.{3}", _imgPath, _prefix, animationStanding._fileNos[0], animationStanding._ext)), 
                    AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * animationStanding._turnPerFrame, _rightPt, _imgSize)
            );

            this._animations._firstStandingLeft = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\{1}{2}f.{3}", _imgPath, _prefix, animationStanding._fileNos[0], animationStanding._ext)),
                    AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * animationStanding._turnPerFrame, _leftPt, _imgSize)
            );
        }

        private void Init(ArmySideEnum armySide)
        {
            _armySide = armySide;
            if (armySide == ArmySideEnum.Attacker)
                base._currentFacingDirection = HorizontalDirectionEnum.Right;
            else
                base._currentFacingDirection = HorizontalDirectionEnum.Left;

            _imgSize = new Size(450, 400);
            _rightPt = new Point(197, 267);
            _leftPt = new Point(253, 267);

            _portrait = null;

            _isWait = false;
            _isDefend = false;
        }

        public static Army CreateArmy(Heroes.Core.Army army, Controller controller, ArmySideEnum armySide)
        {
            Heroes.Core.Battle.Characters.Armies.Army army2
                = new Army(armySide, controller, army._id);

            army2.CopyFrom(army);

            return army2;
        }

        public override void FirstLoad()
        {
            #region Standing
            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_STANDING))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_STANDING];

                this._animations._standingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, animateData._turnPerFrame);

                this._animations._standingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, animateData._turnPerFrame);

                this._animations._standingRightActive = Character.CreateAnimation(_controller, _imgPath, _prefix, "s",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, animateData._turnPerFrame);

                this._animations._standingLeftActive = Character.CreateAnimation(_controller, _imgPath, _prefix, "sf",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, animateData._turnPerFrame);
            }
            #endregion

            #region Moving
            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_MOVING))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_MOVING];

                this._animations._movingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, animateData._turnPerFrame);

                this._animations._movingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, animateData._turnPerFrame);
            }
            #endregion

            #region Start Moving
            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_START_MOVING))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_START_MOVING];

                this._animations._startMovingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);

                this._animations._startMovingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }
            #endregion

            #region Stop Moving
            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_STOP_MOVING))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_STOP_MOVING];

                this._animations._stopMovingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);

                this._animations._stopMovingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }
            #endregion

            #region Attack
            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_ATTACK_STRAIGHT_BEGIN))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_ATTACK_STRAIGHT_BEGIN];

                this._animations._attackStraightRightBegin = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);

                this._animations._attackStraightLeftBegin = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }

            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_ATTACK_STRAIGHT_END))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_ATTACK_STRAIGHT_END];

                this._animations._attackStraightRightEnd = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);

                this._animations._attackStraightLeftEnd = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }
            #endregion

            #region Shoot
            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_SHOOT_STRAIGHT_BEGIN))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_SHOOT_STRAIGHT_BEGIN];

                this._animations._shootStraightRightBegin = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);

                this._animations._shootStraightLeftBegin = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }

            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_SHOOT_STRAIGHT_END))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_SHOOT_STRAIGHT_END];

                this._animations._shootStraightRightEnd = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);

                this._animations._shootStraightLeftEnd = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }
            #endregion

            #region Defend
            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_DEFEND))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_DEFEND];

                this._animations._defendRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);

                this._animations._defendLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }
            #endregion

            #region Getting Hit
            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_GETTING_HIT))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_GETTING_HIT];

                this._animations._gettingHitRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);

                this._animations._gettingHitLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }
            #endregion

            #region Death
            if (_animationDataKPurs.ContainsKey(Heroes.Core.Armies.ArmyAnimationData.PURPOSE_DEATH))
            {
                AnimationData animateData = (AnimationData)_animationDataKPurs[Heroes.Core.Armies.ArmyAnimationData.PURPOSE_DEATH];

                this._animations._deathRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                    animateData._fileNos, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);

                this._animations._deathLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                    animateData._fileNos, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, animateData._turnPerFrame);
            }
            #endregion

            base.FirstLoad();
        }

    }
}
