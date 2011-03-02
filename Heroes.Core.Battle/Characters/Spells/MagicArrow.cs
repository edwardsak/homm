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
    public class MagicArrow : Spell
    {
        public MagicArrow(Controller controller)
        {
            _controller = controller;
            this._imgPath = string.Format(@"{0}\Images\Battle\Sprites\Spells\MagicArrow", Setting._appStartupPath);

            _imgSize = new Size(123, 47);
            _rightPt = new Point(123, 24);
            _leftPt = _rightPt;
        }

        public override void FirstLoad()
        {
            Controller controller = _controller;
            string path = _imgPath;

            this._animations._missileRight = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX02.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX03.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX04.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX05.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX06.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX07.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX08.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX09.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX10.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX11.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX12.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX13.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX14.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Straight\C20spX15.png", path)), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
            );

            _imgSize = new Size(133,116);
            _rightPt = new Point(67, 58);
            _leftPt = _rightPt;

            this._animations._hitRight = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX76.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX77.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX78.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX79.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX80.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX81.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX82.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX83.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX84.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX85.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX86.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX87.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX88.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX89.png", path)), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
            );

            this._animations._hitLeft = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX76f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX77f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX78f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX79f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX80f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX81f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX82f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX83f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX84f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX85f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX86f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX87f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX88f.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\Hit\C20spX89f.png", path)), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
            );

            base.FirstLoad();
        }

    }
}
