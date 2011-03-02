using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.Characters.Graphics;

namespace Heroes.Core.Battle.Characters.Heros
{
    public class Knight : Hero
    {
        public Knight(Controller controller)
        {
            this._controller = controller;
            this._imgPath = string.Format(@"{0}\Images\Battle\Sprites\Heros\Knight", Setting._appStartupPath);

            this._animations._standingRightMale = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\Standing\ch00_01.png", _imgPath)), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
            );

            this._animations._standingLeftMale = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\Standing\ch00_01f.png", _imgPath)), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
            );

            this._animations._standingRightFemale = this._animations._standingRightMale;
            this._animations._standingLeftFemale = this._animations._standingLeftMale;
        }

        public override void FirstLoad()
        {
            Controller controller = _controller;
            string path = _imgPath;

            #region Casting
            this._animations._startCastSpellRightMale = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_13.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_14.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_15.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_16.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_17.png", path)), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
            );

            this._animations._stopCastSpellRightMale = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_17.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_18.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_19.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_20.png", path)), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
            );

            this._animations._startCastSpellLeftMale = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_13.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_14.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_15.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_16.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_17.png", path)), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
            );

            this._animations._stopCastSpellLeftMale = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_17.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_18.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_19.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Male\CastSpell\ch00_20.png", path)), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
            );

            this._animations._startCastSpellRightFemale = this._animations._startCastSpellRightMale;
            this._animations._stopCastSpellRightFemale = this._animations._startCastSpellRightMale;
            this._animations._startCastSpellLeftFemale = this._animations._startCastSpellLeftMale;
            this._animations._stopCastSpellLeftFemale = this._animations._stopCastSpellLeftMale;
            #endregion

            base.FirstLoad();
        }

    }
}
