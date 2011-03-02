using System;
using System.Collections.Generic;
using System.Text;

using Heroes.Core.Battle.Characters.Graphics;

namespace Heroes.Core.Battle.Characters.Commands
{
    public class Idle : Command
    {
        public Idle()
        {
        }

        public override void InitalizeGraphics(StandardCharacter subject)
        {
			subject.SetAnimation(AnimationPurposeEnum.StandingStill, subject.CurrentFacingDirection);
		}

        public override CommandResult Execute(ICharacter subject)
        {
            return new CommandResult(CommandStatusEnum.Finished);
        }

    }
}
