using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Battle.Characters.Commands
{
    public abstract class Command
    {
        public abstract void InitalizeGraphics(StandardCharacter subject);
        public abstract CommandResult Execute(ICharacter subject);

    }
}
