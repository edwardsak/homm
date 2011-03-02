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
    public class Crusader : Army
    {
        public Crusader(Controller controller, ArmySideEnum armySide)
            : base(armySide)
        {
        //    #region Standing
        //    _standingRight = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd54.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd30.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd31.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd32.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd33.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd32.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd31.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd30.dds")), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
        //    );

        //    _standingRightActive = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd54s.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd30s.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd31s.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd32s.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd33s.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd32s.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd31s.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd30s.dds")), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
        //    );

        //    _standingLeft = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd54f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd30f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd31f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd32f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd33f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd32f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd31f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd30f.dds")), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
        //    );

        //    _standingLeftActive = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd54sf.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd30sf.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd31sf.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd32sf.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd33sf.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd32sf.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd31sf.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Standing.ccrusd30sf.dds")), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
        //    );
        //    #endregion

        //    #region Moving
        //    _startMovingRight = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.StartMoving.ccrusd34.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.StartMoving.ccrusd35.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
        //    );

        //    _startMovingLeft = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.StartMoving.ccrusd34f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.StartMoving.ccrusd35f.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
        //    );

        //    _stopMovingRight = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.StopMoving.ccrusd44.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.StopMoving.ccrusd45.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
        //    );

        //    _stopMovingLeft = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.StopMoving.ccrusd44f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.StopMoving.ccrusd45f.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
        //    );

        //    _movingRight = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd36.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd37.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd38.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd39.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd40.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd41.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd42.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd43.dds")), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
        //    );

        //    _movingLeft = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd36f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd37f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd38f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd39f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd40f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd41f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd42f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Moving.ccrusd43f.dds")), AnimationCueDirectionEnum.MoveToBeginning, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
        //    );
        //    #endregion

        //    #region Attack
        //    _attackStraightRightBegin = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd01.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd02.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd03.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd04.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
        //    );

        //    _attackStraightRightEnd = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd05.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd06.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd07.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
        //    );

        //    _attackStraightLeftBegin = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd01f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd02f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd03f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd04f.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
        //    );

        //    _attackStraightLeftEnd = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd05f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd06f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.AttackStraight.ccrusd07f.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
        //    );
        //    #endregion

        //    #region Hit
        //    _gettingHitRight = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd46.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd47.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd48.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd49.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd50.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd51.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
        //    );

        //    _gettingHitLeft = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd46f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd47f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd48f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd49f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd50f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.GettingHit.ccrusd51f.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
        //    );

        //    _deathRight = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd16.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd17.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd18.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd19.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd20.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd21.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _rightPt, _imgSize)
        //    );

        //    _deathLeft = new Animation(
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd16f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd17f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd18f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd19f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd20f.dds")), AnimationCueDirectionEnum.MoveToNext, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize),
        //        new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"Heroes.Core.Battle.Images.Sprites.Armies.Crusader.Death.ccrusd21f.dds")), AnimationCueDirectionEnum.StayHere, BasicEngine.TurnTimeSpan.Ticks * 2, _leftPt, _imgSize)
        //    );
        //    #endregion

        //    _animations = new ArmyAnimations(_standingRight, _standingLeft, 
        //        _standingRightActive, _standingLeftActive,
        //        _standingRight, _standingLeft, 
        //        _startMovingRight, _startMovingLeft,
        //        _startMovingRight, _stopMovingLeft,
        //        _movingRight, _movingLeft,
        //        _attackStraightRightBegin, _attackStraightRightEnd,
        //        _attackStraightLeftBegin, _attackStraightLeftEnd,
        //        null, null,
        //        null, null,
        //        null, null,
        //        _gettingHitRight, _gettingHitLeft,
        //        _deathRight, _deathLeft);
        }

    }
}
