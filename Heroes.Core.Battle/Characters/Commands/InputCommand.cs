using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Diagnostics;

using Heroes.Core.Battle.OtherIO;
using Heroes.Core.Battle.Characters.Graphics;

namespace Heroes.Core.Battle.Characters.Commands
{
    public class InputCommand : Command
    {
        public int _dx;
        public int _dy;
        public int _dz; // wheel
        public byte[] _buttons; // [0] = leftButton, [1] = rightButton, [2] = middleButton

        public InputCommand()
        {
			_dx = 0;
            _dy = 0;
            _dz = 0;
			_buttons = null;
		}

        //public abstract void GetInput();

        public override void InitalizeGraphics(StandardCharacter subject)
        {
            //If HorizontalButtonPressed = HorizontalDirection.Left OrElse HorizontalButtonPressed = HorizontalDirection.Right Then
            //    subject.SetAnimation(AnimationPurpose.Moving, subject.Facing)
            //Else
            //    subject.SetAnimation(AnimationPurpose.StandingStill, subject.Facing)
            //End If
		}

        public override CommandResult Execute(ICharacter subject)
        {
            ICharacter character = subject;

            if (character.CurrentAnimationSeq == null) return new CommandResult(CommandStatusEnum.NotFinished);
            if (character.CurrentAnimationSeq._isEnd) return new CommandResult(CommandStatusEnum.NotFinished);

            switch (character.CurrentAnimationSeq._purpose)
            {
                case AnimationPurposeEnum.Moving:
                    if (!character.CurrentAnimationSeq._isEnd)
                    {
                        PointF point = character.CurrentAnimationSeq._destPoint;
                        character.DestAnimationPt = point;

                        if (Move(character))
                        {
                            character.CurrentAnimationSeq._isEnd = true;
                        }
                    }
                    break;
                default:
                    if (character.CurrentAnimationRunner._isEnd)
                        character.CurrentAnimationSeq._isEnd = true;
                    break;
            }

            return new CommandResult(CommandStatusEnum.NotFinished);
		}

        private bool Move(ICharacter subject)
        {
            // calculate x
            float diffx = subject.DestAnimationPt.X - subject.CurrentAnimationPt.X;
            {
                if (diffx > subject.MoveSpeedX)
                {
                    PointF pt = new PointF(subject.CurrentAnimationPt.X + subject.MoveSpeedX, subject.CurrentAnimationPt.Y);
                    subject.CurrentAnimationPt = pt;
                }
                else if (diffx < -subject.MoveSpeedX)
                {
                    PointF pt = new PointF(subject.CurrentAnimationPt.X - subject.MoveSpeedX, subject.CurrentAnimationPt.Y);
                    subject.CurrentAnimationPt = pt;
                }

                if (Math.Abs(diffx) <= subject.MoveSpeedX)
                {
                    PointF pt = new PointF(subject.DestAnimationPt.X, subject.CurrentAnimationPt.Y);
                    subject.CurrentAnimationPt = pt;
                }
            }

            // Calculate y
            float diffy = subject.DestAnimationPt.Y - subject.CurrentAnimationPt.Y;
            {
                if (diffy > subject.MoveSpeedY)
                {
                    PointF pt = new PointF(subject.CurrentAnimationPt.X, subject.CurrentAnimationPt.Y + subject.MoveSpeedY);
                    subject.CurrentAnimationPt = pt;
                }
                else if (diffy < -subject.MoveSpeedY)
                {
                    PointF pt = new PointF(subject.CurrentAnimationPt.X, subject.CurrentAnimationPt.Y - subject.MoveSpeedY);
                    subject.CurrentAnimationPt = pt;
                }

                if (Math.Abs(diffy) <= subject.MoveSpeedY)
                {
                    PointF pt = new PointF(subject.CurrentAnimationPt.X, subject.DestAnimationPt.Y);
                    subject.CurrentAnimationPt = pt;
                }
            }

            if (diffx == 0 && diffy == 0)
            {
                // reach destination

                // end move
                return true;
            }
            else
            {
            }

            return false;
        }

    }

}
