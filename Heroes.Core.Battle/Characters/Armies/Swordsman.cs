using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.Characters;
using Heroes.Core.Battle.Characters.Graphics;
using Heroes.Core.Battle.Terrains;

namespace Heroes.Core.Battle.Characters.Armies
{
    public class Swordsman : Army
    {
        public Swordsman(Controller controller, ArmySideEnum armySide)
            : base(armySide)
        {
            _controller = controller;
            _imgPath = string.Format(@"{0}\Images\Battle\Sprites\Armies\Swordsman", Setting._appStartupPath);
            _prefix = "csword";

            this._animations._firstStandingRight = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\csword54.png", _imgPath)), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
            );

            this._animations._firstStandingLeft = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\csword54f.png", _imgPath)), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
            );
        }

        public override void FirstLoad()
        {
            #region Standing
            this._animations._standingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "54", "30", "31", "32", "33", "32", "31", "30" }, _rightPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 3);

            this._animations._standingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "54", "30", "31", "32", "33", "32", "31", "30" }, _leftPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 3);

            this._animations._standingRightActive = Character.CreateAnimation(_controller, _imgPath, _prefix, "s",
                new string[] { "54", "30", "31", "32", "33", "32", "31", "30" }, _rightPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 3);

            this._animations._standingLeftActive = Character.CreateAnimation(_controller, _imgPath, _prefix, "sf",
                new string[] { "54", "30", "31", "32", "33", "32", "31", "30" }, _leftPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 3);
            #endregion

            #region Moving
            this._animations._movingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "36", "37", "38", "39", "40", "41", "42", "43" }, _rightPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 2);

            this._animations._movingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "36", "37", "38", "39", "40", "41", "42", "43" }, _leftPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 2);
            #endregion

            #region Start Moving
            this._animations._startMovingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "34", "35" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._startMovingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "34", "35" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            #region Stop Moving
            this._animations._stopMovingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "44", "45" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._stopMovingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "44", "45" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            #region Attack
            this._animations._attackStraightRightBegin = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "01", "02", "03", "04" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._attackStraightLeftBegin = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "01", "02", "03", "04" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._attackStraightRightEnd = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "05", "06", "07" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._attackStraightLeftEnd = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "05", "06", "07" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            #region Defend
            this._animations._defendRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "22", "23", "24", "25", "25", "25", "25", "25", "24", "23", "22" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._defendLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "22", "23", "24", "25", "25", "25", "25", "25", "24", "23", "22" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            #region Getting Hit
            this._animations._gettingHitRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "46", "47", "48", "49", "50", "51" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._gettingHitLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "46", "47", "48", "49", "50", "51" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            #region Death
            this._animations._deathRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "16", "17", "18", "19", "20", "21" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._deathLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "16", "17", "18", "19", "20", "21" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            base.FirstLoad();
        }

    }
}
