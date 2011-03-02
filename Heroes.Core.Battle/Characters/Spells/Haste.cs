using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.Characters.Graphics;
using Heroes.Core.Battle.Characters.Commands;
using Heroes.Core.Battle.Characters.Heros;

namespace Heroes.Core.Battle.Characters.Spells
{
    public class Haste : Spell
    {
        public Haste(Controller controller)
        {
            _controller = controller;
            this._imgPath = string.Format(@"{0}\Images\Battle\Sprites\Spells\Haste", Setting._appStartupPath);

            _imgSize = new Size(113, 106);
            _rightPt = new Point(57, 106);
            _leftPt = _rightPt;
        }

        public override void FirstLoad()
        {
            Controller controller = _controller;
            string path = _imgPath;

            this._animations._missileRight = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA01.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA02.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA03.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA04.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA05.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA06.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA07.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA08.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA09.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA10.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA11.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA12.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA13.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA14.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C15spA15.png", path)), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
            );

            this._animations._missileLeft = this._animations._missileRight;

            base.FirstLoad();
        }

    }
}
