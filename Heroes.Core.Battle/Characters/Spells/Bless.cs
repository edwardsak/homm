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
    public class Bless: Spell
    {
        public Bless(Controller controller)
        {
            _controller = controller;
            this._imgPath = string.Format(@"{0}\Images\Battle\Sprites\Spells\Bless", Setting._appStartupPath);

            _imgSize = new Size(43, 123);
            _rightPt = new Point(22, 123);
            _leftPt = _rightPt;
        }

        public override void FirstLoad()
        {
            Controller controller = _controller;
            string path = _imgPath;

            this._animations._animationRight = new Animation(
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW01.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW02.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW03.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW04.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW05.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW06.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW07.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW08.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW09.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW10.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW11.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW12.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW13.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW14.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW15.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW16.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW17.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW18.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW19.png", path)), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
                new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\C01spW20.png", path)), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
            );

            _animations._animationLeft = _animations._animationRight;

            base.FirstLoad();
        }

    }
}
