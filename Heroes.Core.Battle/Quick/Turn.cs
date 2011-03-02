using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Characters;
using Heroes.Core.Battle.Terrains;
using Heroes.Core.Battle.Characters.Commands;

namespace Heroes.Core.Battle.Quick
{
    public class Turn
    {
        #region Event
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

        public Heroes.Core.Battle.Heros.Hero _currentHero;
        public Heroes.Core.Battle.Heros.Hero _targetHero;
        public Heroes.Core.Battle.Armies.Army _currentCharacter;    // current controlling character
        
        Heroes.Core.Battle.Heros.Hero _attackHero;
        Heroes.Core.Battle.Heros.Hero _defendHero;
        Heroes.Core.Battle.Quick.Monster _monster;

        Hashtable _attackArmies;
        Hashtable _defendArmies;
        public Hashtable _targetArmies;

        public Turn(ArrayList characters,
            Heroes.Core.Battle.Heros.Hero attackHero, Hashtable attackArmies,
            Heroes.Core.Battle.Heros.Hero defendHero, Heroes.Core.Battle.Quick.Monster monster, Hashtable defendArmies)
        {
            _characters = characters;
            _attackArmies = attackArmies;
            _defendArmies = defendArmies;

            if (_characters.Count > 0)
            {
                _currentCharacter = (Heroes.Core.Battle.Armies.Army)_characters[0];
                _currentCharacter._isBeginTurn = true;
            }
            else
                _currentCharacter = null;

            _attackHero = attackHero;
            _defendHero = defendHero;
            _monster = monster;

            FindHero();
        }

        private void FindHero()
        {
            if (_currentCharacter != null)
            {
                if (_attackHero != null && _currentCharacter._heroId == _attackHero._id)
                {
                    _currentHero = _attackHero;
                    _targetHero = _defendHero;
                    _targetArmies = _defendArmies;
                }
                else if (_defendHero != null && _currentCharacter._heroId == _defendHero._id)
                {
                    _currentHero = _defendHero;
                    _targetHero = _attackHero;
                    _targetArmies = _attackArmies;
                }
                else if (_monster != null)
                {
                    _currentHero = null;
                    _targetHero = _attackHero;
                    _targetArmies = _attackArmies;
                }
            }
        }

        public void NextTurn()
        {
            // end current character
            {
                Heroes.Core.Battle.Armies.Army army = (Heroes.Core.Battle.Armies.Army)_currentCharacter;

                if (!army._isWait)
                {
                    _currentCharacter._isBeginTurn = false;
                    _currentCharacter._isEndTurn = true;
                }
            }

            // next character
            {
                Heroes.Core.Battle.Armies.Army army = (Heroes.Core.Battle.Armies.Army)_currentCharacter;

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
            }

            FindHero();

            OnNextTurned();
        }

        private Heroes.Core.Battle.Armies.Army GetNextCharacter()
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

        private Heroes.Core.Battle.Armies.Army GetNextCharacter(int index)
        {
            if (_characters.Count < 1) return null;

            // if end of index, start from 0
            if (index >= _characters.Count) index = 0;

            while (index < _characters.Count)
            {
                Heroes.Core.Battle.Armies.Army character = (Heroes.Core.Battle.Armies.Army)_characters[index];

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
            foreach (Heroes.Core.Battle.Armies.Army character in _characters)
            {
                character._retaliateRemain = character._noOfRetaliate;  // reset noOfRetaliate

                character._isBeginTurn = false;
                character._isEndTurn = false;

                Heroes.Core.Battle.Armies.Army army = (Heroes.Core.Battle.Armies.Army)character;
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
