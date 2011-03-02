using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Characters;
using Heroes.Core.Battle.Characters.Graphics;
using Heroes.Core.Battle.Characters.Commands;
using Heroes.Core.Battle.Terrains;

namespace Heroes.Core.Battle.Characters.Commands
{
    public class Action
    {
        public ArrayList _animationSeqs;
        public AnimationSequence _currentAnimationSeq;
        public ArrayList _path;     // moving path
        public ActionTypeEnum _actionType;
        public bool _isEnd;
        public StandardCharacter _targetCharacter;  // attack target

        public ArrayList _subActions;

        public Action(ActionTypeEnum actionType)
        {
            _actionType = actionType;
            _animationSeqs = new ArrayList();
            _currentAnimationSeq = null;
            _path = null;
            _isEnd = false;
            _targetCharacter = null;

            _subActions = new ArrayList();
        }

        public bool NextAnimationSeq()
        {
            if (_currentAnimationSeq == null) return false;
            if (!_animationSeqs.Contains(_currentAnimationSeq))
            {
                _currentAnimationSeq = null;
                return false;
            }

            int index = _animationSeqs.IndexOf(_currentAnimationSeq);
            index += 1;
            if (index >= _animationSeqs.Count)
            {
                // end of seq
                _isEnd = true;
                return true;
            }
            else
            {
                _currentAnimationSeq = (AnimationSequence)_animationSeqs[index];
            }

            return false;
        }

        public void Run()
        {
            if (_isEnd) return;

            int endCount = 0;

            foreach (SubAction subAction in _subActions)
            {
                if (!subAction._isEnd)
                {
                    // trigger at begining of action
                    if (subAction._currentAnimationSeq._triggerWhenBegin)
                    {
                        subAction._currentAnimationSeq._triggerWhenBegin = false;
                        subAction._currentAnimationSeq.Trigger();
                    }

                    if (subAction._currentAnimationSeq._isEnd)
                    {
                        // trigger at end of action
                        if (subAction._currentAnimationSeq._triggerWhenEnd)
                        {
                            subAction._currentAnimationSeq._triggerWhenEnd = false;
                            subAction._currentAnimationSeq.Trigger();
                        }
                       
                        subAction.NextAnimationSeq();
                    }
                }

                if (subAction._isEnd)
                {
                    endCount += 1;
                    continue;
                }

                if (!subAction._currentAnimationSeq._isStarted)
                {
                    if (subAction._currentAnimationSeq._waitToTrigger)
                        continue;
                    else
                    {
                        subAction._currentAnimationSeq._isStarted = true;
                        subAction._character.CurrentAnimationSeq = subAction._currentAnimationSeq;
                        subAction._character.SetAnimation(subAction._currentAnimationSeq);
                    }
                }
            }

            if (endCount == _subActions.Count)
                _isEnd = true;
        }

        public void SetDefaultAnimation()
        {
            foreach (SubAction subAction in _subActions)
            {
                subAction._character.SetDefaultAnimation();
            }
        }

        public static Action CreateMoveAction(StandardCharacter character, ArrayList path)
        {
            Action action = new Action(ActionTypeEnum.Moving);
            
            SubAction subAction = new SubAction();
            subAction._character = character;
            subAction._path = path;

            subAction.AddStartMoving();
            subAction.AddMoving();
            subAction.AddStopMoving();

            subAction._currentAnimationSeq = (AnimationSequence)subAction._animationSeqs[0];

            action._subActions.Add(subAction);

            return action;
        }

        public static Action CreateAttackAction(StandardCharacter character, CellPartEnum attackDirection,
            ArrayList path, ArrayList targets)
        {
            Action action = new Action(ActionTypeEnum.Attack);

            SubAction subAction = new SubAction();
            action._subActions.Add(subAction);
            subAction._character = character;
            subAction._path = path;

            if (path != null && path.Count > 0)
            {
                subAction.AddStartMoving();
                subAction.AddMoving();
                subAction.AddStopMoving();
            }

            // target getting hit or death
            ArrayList defendTriggers = new ArrayList();
            ArrayList hitTriggers = new ArrayList();
            foreach (Heroes.Core.Battle.Characters.Armies.Army target in targets)
            {
                SubAction subAction2 = new SubAction();
                action._subActions.Add(subAction2);
                subAction2._character = target;

                AnimationSequence animationSeq = null;
                if (target._isDead)
                    animationSeq = subAction2.AddDeath(attackDirection);
                else if (target._isDefend)
                    animationSeq = subAction2.AddDefend(attackDirection);
                else
                    animationSeq = subAction2.AddGettingHit(attackDirection);

                subAction2._currentAnimationSeq = animationSeq;

                if (target._isDefend)
                    defendTriggers.Add(animationSeq);
                else
                    hitTriggers.Add(animationSeq);
            }

            subAction.AddStartAttack(attackDirection, defendTriggers);
            subAction.AddStopAttack(attackDirection, hitTriggers);

            subAction._currentAnimationSeq = (AnimationSequence)subAction._animationSeqs[0];

            return action;
        }

        public static Action CreateShootAction(StandardCharacter character, CellPartEnum attackDirection,
            ArrayList targets, Cell targetCell)
        {
            Action action = new Action(ActionTypeEnum.RangeAttack);

            SubAction subAction = new SubAction();
            action._subActions.Add(subAction);
            subAction._character = character;

            // add getting hit or death
            ArrayList defendTriggers = new ArrayList();
            ArrayList hitTriggers = new ArrayList();
            foreach (Heroes.Core.Battle.Characters.Armies.Army target in targets)
            {
                SubAction subAction2 = new SubAction();
                action._subActions.Add(subAction2);
                subAction2._character = target;

                AnimationSequence animationSeq = null;
                if (target._isDead)
                    animationSeq = subAction2.AddDeath(attackDirection);
                else if (target._isDefend)
                    animationSeq = subAction2.AddDefend(attackDirection);
                else
                    animationSeq = subAction2.AddGettingHit(attackDirection);

                subAction2._currentAnimationSeq = animationSeq;

                if (target._isDefend)
                    defendTriggers.Add(animationSeq);
                else
                    hitTriggers.Add(animationSeq);
            }

            subAction.AddStartShoot(attackDirection, defendTriggers);
            subAction.AddStopShoot(attackDirection, hitTriggers);

            subAction._currentAnimationSeq = (AnimationSequence)subAction._animationSeqs[0];

            return action;
        }

        public static Action CreateCastSpellAction(Heroes.Core.Battle.Characters.Hero hero, 
            Heroes.Core.Battle.Characters.Spells.Spell spell,
            CellPartEnum attackDirection,
            ArrayList targets, Cell targetCell)
        {
            Action action = new Action(ActionTypeEnum.Spell);

            SubAction subAction = new SubAction();
            action._subActions.Add(subAction);
            subAction._character = hero;

            // hero casting animation
            AnimationSequence startCastSpellSeq = subAction.AddStartCasting(attackDirection);
            
            AnimationSequence stopCastSpellSeq = subAction.AddStopCasting(attackDirection);
            stopCastSpellSeq._waitToTrigger = true;

            // add casting
            AnimationSequence spellSeq = null;
            AnimationSequence spellHitSeq = null;
            AnimationSequence hitSeq = null;
            foreach (Heroes.Core.Battle.Characters.ICharacter target in targets)
            {
                // spell animate
                {
                    SubAction subAction2 = new SubAction();
                    action._subActions.Add(subAction2);
                    subAction2._character = spell;

                    PointF destPoint = spell._destCell.GetStandingPoint();

                    if (spell._isMissile)
                    {
                        // move missile
                        PointF srcPoint = hero._castingPointRight;
                        spell._currentAnimationPt = srcPoint;

                        destPoint.Y -= hero._castingHeight;

                        Character.CalculateFlySpeed(srcPoint, destPoint,
                            spell._defaultMoveSpeed,
                        out spell._moveSpeedX, out spell._moveSpeedY);

                        spellSeq = subAction2.AddMissileSpell(destPoint, attackDirection, AnimationPurposeEnum.Moving);
                        subAction2._currentAnimationSeq = spellSeq;
                    }
                    else
                    {
                        spell._currentAnimationPt = destPoint;

                        spellSeq = subAction2.AddOnArmySpell(destPoint, attackDirection, AnimationPurposeEnum.CastSpell);
                        subAction2._currentAnimationSeq = spellSeq;
                    }

                    // hit
                    if (spell._isHit)
                    {
                        spellHitSeq = subAction2.AddSpellHit(destPoint, attackDirection);
                        subAction2._currentAnimationSeq = spellSeq;
                    }
                }

                // target
                if (spell._isDamage)
                {
                    Heroes.Core.Battle.Characters.Armies.Army army
                        = (Heroes.Core.Battle.Characters.Armies.Army)target;

                    SubAction subAction2 = new SubAction();
                    action._subActions.Add(subAction2);
                    subAction2._character = target;

                    if (army._isDead)
                        hitSeq = subAction2.AddDeath(attackDirection);
                    else if (army._isDefend)
                        hitSeq = subAction2.AddDefend(attackDirection);
                    else
                        hitSeq = subAction2.AddGettingHit(attackDirection);

                    hitSeq._waitToTrigger = true;

                    subAction2._currentAnimationSeq = hitSeq;
                }
            }

            startCastSpellSeq._waitToTrigger = false;
            startCastSpellSeq._triggerWhenEnd = true;
            startCastSpellSeq._triggerAnimationSeqs.Add(spellSeq);

            spellSeq._waitToTrigger = true;
            spellSeq._triggerWhenEnd = true;

            if (spell._isHit)
            {
                spellHitSeq._waitToTrigger = true;
                spellSeq._triggerAnimationSeqs.Add(spellHitSeq);
            }

            if (spell._isDamage)
            {
                hitSeq._waitToTrigger = true;
                spellSeq._triggerAnimationSeqs.Add(hitSeq);
            }

            stopCastSpellSeq._waitToTrigger = true;
            spellSeq._triggerAnimationSeqs.Add(stopCastSpellSeq);

            subAction._currentAnimationSeq = (AnimationSequence)subAction._animationSeqs[0];

            return action;
        }

    }

}
