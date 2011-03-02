using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Battle.Characters.Commands
{
    public class CommandResult
    {
        public CommandStatusEnum _status;

        public CommandResult(CommandStatusEnum status)
        {
            _status = status;
        }

    }
}
