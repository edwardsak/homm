using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Characters;
using Heroes.Core.Battle.Terrains;
using Heroes.Core.Battle.Characters.Commands;

namespace Heroes.Core.Battle
{
    public class Turn
    {
        #region
        public delegate void NextTurnedEventHandler();

        public event NextTurnedEventHandler NextTurned;

        protected virtual void OnNextTurned()
        {
            if (NextTurned != null)
            {
                //Invokes the delegates.
                NextTurned();
            }
        }
        #endregion

        public ArrayList _characters;  // sort by speed, favour to attacker
        public StandardCharacter _currentCharacter;    // current controlling character

        Heroes.Core.Battle.Characters.Hero _attackHero;
        Heroes.Core.Battle.Characters.Hero _defendHero;

        public Turn(ArrayList characters, 
            Heroes.Core.Battle.Characters.Hero attackHero, Heroes.Core.Battle.Characters.Hero defendHero)
        {
            _characters = characters;

            if (_characters.Count > 0)
            {
                _currentCharacter = (StandardCharacter)_characters[0];
                _currentCharacter._isBeginTurn = true;
                //_currentCharacter._action = _currentCharacter.CreateStandingAction(_currentCharacter, _currentCharacter.CurrentFacingDirection, _currentCharacter._isBeginTurn);
                //_currentCharacter.SetAnimation(_currentCharacter._action._currentAnimationSeq);
            }
            else
                _currentCharacter = null;

            _attackHero = attackHero;
            _defendHero = defendHero;
        }

        public void NextTurn()
        {
            // end current character
            {
                Heroes.Core.Battle.Characters.Armies.Army army = (Heroes.Core.Battle.Characters.Armies.Army)_currentCharacter;

                if (!army._isWait)
                {
                    _currentCharacter._isBeginTurn = false;
                    _currentCharacter._isEndTurn = true;
                }

                if (!_currentCharacter._isDead)
                {
                    Action action = _currentCharacter.CreateStandingAction(_currentCharacter, _currentCharacter.CurrentFacingDirection, false);
                    _currentCharacter._action = action;
                    _currentCharacter.SetAnimation(action._currentAnimationSeq);
                }
            }

            // next character
            {
                Heroes.Core.Battle.Characters.Armies.Army army = (Heroes.Core.Battle.Characters.Armies.Army)_currentCharacter;

                // get next character
                _currentCharacter = GetNextCharacter();

                // get character start from 0, because of waiting
                if (_currentCharacter == null) _currentCharacter = GetNextCharacter(0);

                // reset turn
                if (_currentCharacter == null)
                {
                    ResetTurn();
                    _currentCharacter = GetNextCharacter(0);
                }

                _currentCharacter._isBeginTurn = true;

                // reset defend
                army._isDefend = false;

                if (!_currentCharacter._isDead)
                {
                    Action action = _currentCharacter.CreateStandingAction(_currentCharacter, _currentCharacter.CurrentFacingDirection, true);
                    _currentCharacter._action = action;
                    _currentCharacter.SetAnimation(action._currentAnimationSeq);
                }
            }

            OnNextTurned();
        }

        private StandardCharacter GetNextCharacter()
        {
            if (_characters.Count < 1) return null;

            // get currentCharacter index
            int index = 0;
            if (_currentCharacter != null)
            {
                index = _characters.IndexOf(_currentCharacter);
                index += 1;
            }

            return GetNextCharacter(index);
        }

        private StandardCharacter GetNextCharacter(int index)
        {
            if (_characters.Count < 1) return null;

            // if end of index, start from 0
            if (index >= _characters.Count) index = 0;

            while (index < _characters.Count)
            {
                StandardCharacter character = (StandardCharacter)_characters[index];

                if (character._isDead)
                {
                    index += 1;
                    continue;    // dead
                }

                if (character._isEndTurn)
                {
                    index += 1;
                    continue;
                }

                return character;
            }

            return null;
        }

        private void ResetTurn()
        {
            foreach (StandardCharacter character in _characters)
            {
                character._retaliateRemain = character._noOfRetaliate;  // reset noOfRetaliate

                character._isBeginTurn = false;
                character._isEndTurn = false;

                Heroes.Core.Battle.Characters.Armies.Army army = (Heroes.Core.Battle.Characters.Armies.Army)character;
                army._isWait = false;
            }

            _currentCharacter = null;

            // hero can cast spell per whole turn
            _attackHero._canCastSpell = true;
            if (_defendHero != null)
                _defendHero._canCastSpell = true;
        }

        public void Wait()
        {
            if (_currentCharacter == null) return;
            
            Heroes.Core.Battle.Characters.Armies.Army army = (Heroes.Core.Battle.Characters.Armies.Army)_currentCharacter;

            if (army._isWait) return;

            army._isWait = true;
            army._isDefend = false;

            NextTurn();
        }

        public void Defend()
        {
            if (_currentCharacter == null) return;

            Heroes.Core.Battle.Characters.Armies.Army army = (Heroes.Core.Battle.Characters.Armies.Army)_currentCharacter;

            army._isWait = false;
            army._isDefend = true;

            NextTurn();
        }

    }
}
