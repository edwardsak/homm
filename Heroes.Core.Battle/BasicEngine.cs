using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using Heroes.Core.Battle.Characters;

namespace Heroes.Core.Battle
{
    public class BasicEngine
    {
        public const int TURNS_PER_SECOND = 32;
        public static TimeSpan TurnTimeSpan = TimeSpan.FromSeconds(1.0 / TURNS_PER_SECOND);

        protected TimeSpan _remainingTurnTime;

        protected ArrayList _activeCharacters;
        private uint _turnCount;

        /// <summary>
        /// Creates a turn engine for the area and that will process the 
        /// characters in the passed list each turn.  The list should be 
        /// updated by other objects to add or remove characters that need
        /// processing.
        /// </summary>
        /// <param name="newLevel"></param>
        /// <param name="activeCharactersList"></param>
        public BasicEngine()
        {
            //Level = newLevel;
            _activeCharacters = new ArrayList();
            _remainingTurnTime = TurnTimeSpan;
            _turnCount = 0;
        }

        /// <summary>
        /// Call this to run the engine for the specified amount of time.
        /// </summary>
        /// <param name="elapsedTime"></param>
        public virtual void Run(TimeSpan elapsedTime)
        {
            while (elapsedTime.Ticks >= _remainingTurnTime.Ticks)
            {
                elapsedTime = elapsedTime.Subtract(_remainingTurnTime);
                //_turnCount += (uint)(1);
                RunTurn();

                _remainingTurnTime = TurnTimeSpan;
            }
            _remainingTurnTime = _remainingTurnTime.Subtract(elapsedTime);
        }

        /// <summary>
        /// Does the work for an entire turn  This one just runs process for 
        /// each character in the active character list.
        /// </summary>
        /// <remarks>
        /// The lenght of a turn is determined by the TurnTimeSpan constant 
        /// defined for this class (1/32 seconds currently).  This method can 
        /// be overriden in derived classes to do more important work for a 
        /// turn, but should probably call the base class implementation to do
        /// the individual work on characters.  To override the work that is 
        /// done the Process method should be overriden.
        /// </remarks>
        protected virtual void RunTurn()
        {
            //EnviromentEffects turnEffects = new EnviromentEffects();
            //foreach (StandardCharacter c in ActiveCharacters)
            //{
            //    ProcessCharacter(c);
            //}
            //ProcessEnviromentEffects(turnEffects);
        }

        /// <summary>
        /// Takes care of all turn processing for a character.  In this class 
        /// it just runs that characters animation.  
        /// </summary>
        /// <remarks>
        /// This should be overridden in derived classes to do more important
        /// work for each character.
        /// </remarks>
        /// <param name="c"></param>
        //protected virtual void ProcessCharacter(StandardCharacter c)
        //{
        //    c.CurrentAnimationRunner.Run(TurnTimeSpan);
        //}

        #region Properties
        public ArrayList ActiveCharacters
        {
            get { return _activeCharacters; }
        }
        public uint TurnCount
        {
            get { return _turnCount; }
        }
        #endregion

    }
}
