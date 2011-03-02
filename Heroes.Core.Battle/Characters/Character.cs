using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.Characters.Graphics;

namespace Heroes.Core.Battle.Characters
{
    public interface ICharacter
    {
        PointF CurrentAnimationPt { get; set; }
        PointF DestAnimationPt { get; set; }

        Animations Animations { get; }
        Animation CurrentAnimation { get; set; }
        AnimationRunner CurrentAnimationRunner { get; set; }

        HorizontalDirectionEnum CurrentFacingDirection { get; set; }

        AnimationSequence CurrentAnimationSeq { get; set; }

        void SetAnimation(AnimationSequence seq);

        Point TextureRightPoint { get; set; }
        Point TextureLeftPoint { get; set; }
        Size TextureSize { get; set; }

        void FirstLoad();

        void SetDefaultAnimation();

        float MoveSpeedX { get; set; }
        float MoveSpeedY { get; set; }
    }

    public class Character
    {
        public static void CalculateFlySpeed(PointF srcPoint, PointF destPoint,
            float stdMoveSpeed,
            out float moveSpeedX, out float moveSpeedY)
        {
            moveSpeedX = stdMoveSpeed;
            moveSpeedY = stdMoveSpeed;

            float dx = Math.Abs(destPoint.X - srcPoint.X);
            float dy = Math.Abs(destPoint.Y - srcPoint.Y);

            if (dx < dy)
            {
                moveSpeedX = dx / dy * stdMoveSpeed;
            }
            else
            {
                moveSpeedY = dy / dx * stdMoveSpeed;
            }
        }

        public static Animation CreateAnimation(Controller controller, string path, string prefix, string suffix,
            string[] nos, Point pt, Size sz,
            AnimationCueDirectionEnum lastCueDirection, int duration)
        {
            AnimationCue[] cues = new AnimationCue[nos.Length];
            for (int i = 0; i < nos.Length; i++)
            {
                AnimationCueDirectionEnum cueDirection = AnimationCueDirectionEnum.MoveToNext;
                if (i == nos.Length - 1) cueDirection = lastCueDirection;

                cues[i] = new AnimationCue(controller.TextureStore.GetTexture(controller.Device, string.Format(@"{0}\{1}{2}{3}.png", path, prefix, nos[i], suffix)),
                    cueDirection, BasicEngine.TurnTimeSpan.Ticks * duration, pt, sz);
            }

            Animation animation = new Animation(cues);
            return animation;
        }

    }

}
