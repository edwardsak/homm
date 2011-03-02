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
    public class Cavalier : Army
    {
        public Cavalier(Controller controller, ArmySideEnum armySide)
            : base(armySide)
        {
            _controller = controller;
            _imgPath = string.Format(@"{0}\Images\Battle\Sprites\Armies\Cavalier", Setting._appStartupPath);
            _prefix = "ccavlr";

            this._animations._firstStandingRight = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\ccavlr45.png", _imgPath)), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
            );

            this._animations._firstStandingLeft = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\ccavlr45f.png", _imgPath)), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
            );
        }

        public override void FirstLoad()
        {
            #region Standing
            this._animations._standingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "45", "46", "47", "48" }, _rightPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 3);

            this._animations._standingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "45", "46", "47", "48" }, _leftPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 3);

            this._animations._standingRightActive = Character.CreateAnimation(_controller, _imgPath, _prefix, "s",
                new string[] { "45", "46", "47", "48" }, _rightPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 3);

            this._animations._standingLeftActive = Character.CreateAnimation(_controller, _imgPath, _prefix, "sf",
                new string[] { "45", "46", "47", "48" }, _leftPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 3);
            #endregion

            #region Moving
            this._animations._movingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "52", "53", "54", "55", "56", "57", "58", "59" }, _rightPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 2);

            this._animations._movingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "52", "53", "54", "55", "56", "57", "58", "59" }, _leftPt, _imgSize, AnimationCueDirectionEnum.MoveToBeginning, 2);
            #endregion

            #region Start Moving
            this._animations._startMovingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "49", "50", "51" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._startMovingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "49", "50", "51" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            #region Stop Moving
            this._animations._stopMovingRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "60", "61", "62", "63" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._stopMovingLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "60", "61", "62", "63" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            #region Attack
            this._animations._attackStraightRightBegin = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "01", "02", "03", "04", "05", "06", "07" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._attackStraightLeftBegin = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "01", "02", "03", "04", "05", "06", "07" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._attackStraightRightEnd = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "08", "09", "10" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._attackStraightLeftEnd = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "08", "09", "10" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            #region Defend
            this._animations._defendRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "34", "35", "36", "37", "38", "39" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._defendLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "34", "35", "36", "37", "38", "39" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            #region Getting Hit
            this._animations._gettingHitRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "67", "70", "69", "68", "67", "66", "65", "64" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._gettingHitLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "67", "70", "69", "68", "67", "66", "65", "64" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            #region Death
            this._animations._deathRight = Character.CreateAnimation(_controller, _imgPath, _prefix, "",
                new string[] { "26", "27", "28", "29", "30", "31", "32", "33" }, _rightPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);

            this._animations._deathLeft = Character.CreateAnimation(_controller, _imgPath, _prefix, "f",
                new string[] { "26", "27", "28", "29", "30", "31", "32", "33" }, _leftPt, _imgSize, AnimationCueDirectionEnum.StayHere, 2);
            #endregion

            base.FirstLoad();
        }

    }
}
