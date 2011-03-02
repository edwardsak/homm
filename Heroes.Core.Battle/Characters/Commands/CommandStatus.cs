using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Battle.Characters.Commands
{
    public enum CommandStatusEnum
    {
		/// <summary>
		/// The command is not finished.
		/// </summary>
		NotFinished,
		/// <summary>
		/// The command is finished, the character should think to obtain a new
		/// command.  It can be dangerous, though not necessarily so, to let a 
		/// Finished command continue running.
		/// </summary>
		Finished,
		/// <summary>
		/// The command is not finished per se, but the character should think 
		/// and possibly decide on a new command.
        /// </summary>
		CharacterShouldThink
    }
}
