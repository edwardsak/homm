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
    public class SubAction
    {
        public ICharacter _character;
        public ArrayList _animationSeqs;
        public AnimationSequence _currentAnimationSeq;
        public ArrayList _path;     // moving path
        public bool _isEnd;

        public SubAction()
        {
            _character = null;
            _animationSeqs = new ArrayList();
            _currentAnimationSeq = null;
            _path = null;
            _isEnd = false;
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

        public void AddStartMoving()
        {
            Heroes.Core.Battle.Characters.Armies.Army currentArmy 
                = (Heroes.Core.Battle.Characters.Armies.Army)this._character;

            Animation animation = null;
            if (_character.CurrentFacingDirection == HorizontalDirectionEnum.Right)
                animation = currentArmy._animations._startMovingRight;
            else
                animation = currentArmy._animations._startMovingLeft;

            // Not all army has start moving animation
            if (animation == null) return;

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.StartMoving, _character.CurrentFacingDirection);
            animationSeq._waitToTrigger = false;
            animationSeq._destPoint = currentArmy._cell.GetStandingPoint();

            this._animationSeqs.Add(animationSeq);
        }

        public void AddMoving()
        {
            Heroes.Core.Battle.Characters.Armies.Army currentArmy = (Heroes.Core.Battle.Characters.Armies.Army)this._character;

            // moving seqs
            Cell prevCell = currentArmy._cell;
            HorizontalDirectionEnum facing = this._character.CurrentFacingDirection;

            Heroes.Core.Battle.Characters.Armies.Army army = (Heroes.Core.Battle.Characters.Armies.Army)this._character;
            if (army._moveType == MoveTypeEnum.Ground)
            {
                foreach (Cell cell in this._path)
                {
                    // get direction
                    Point prevPoint = prevCell.GetCenterPoint();
                    Point point = cell.GetCenterPoint();
                    CellPartEnum direction = BattleTerrain.FindDirection(prevPoint, point);

                    Animation animation = null;
                    facing = HorizontalDirectionEnum.None;
                    switch (direction)
                    {
                        case CellPartEnum.CenterRight:
                        case CellPartEnum.UpperRight:
                        case CellPartEnum.LowerRight:
                            animation = army._animations._movingRight;
                            facing = HorizontalDirectionEnum.Right;
                            break;
                        default:
                            animation = army._animations._movingLeft;
                            facing = HorizontalDirectionEnum.Left;
                            break;
                    }

                    AnimationSequence seq = new AnimationSequence(animation, AnimationPurposeEnum.Moving, facing);
                    seq._waitToTrigger = false;
                    seq._destPoint = cell.GetStandingPoint();
                    this._animationSeqs.Add(seq);

                    prevCell = cell;
                }
            }
            else
            {
                // flying, telepote
                Point prevPoint = prevCell.GetCenterPoint();
                Cell cell = (Cell)this._path[this._path.Count - 1];
                Point point = cell.GetCenterPoint();
                CellPartEnum direction = BattleTerrain.FindDirection(prevPoint, point);

                Animation animation = null;
                facing = HorizontalDirectionEnum.None;
                switch (direction)
                {
                    case CellPartEnum.CenterRight:
                    case CellPartEnum.UpperRight:
                    case CellPartEnum.LowerRight:
                        animation = army._animations._movingRight;
                        facing = HorizontalDirectionEnum.Right;
                        break;
                    default:
                        animation = army._animations._movingLeft;
                        facing = HorizontalDirectionEnum.Left;
                        break;
                }

                AnimationSequence seq = new AnimationSequence(animation, AnimationPurposeEnum.Moving, facing);
                seq._waitToTrigger = false;
                seq._destPoint = cell.GetStandingPoint();

                this._animationSeqs.Add(seq);

                // calculate move speed, because move straight line
                Character.CalculateFlySpeed(prevPoint, point, currentArmy._moveSpeed,
                    out currentArmy._moveSpeedX, out currentArmy._moveSpeedY);
            }
        }

        public void AddStopMoving()
        {
            Heroes.Core.Battle.Characters.Armies.Army army
                = (Heroes.Core.Battle.Characters.Armies.Army)this._character;

            Animation animation = null;
            if (this._character.CurrentFacingDirection == HorizontalDirectionEnum.Right)
                animation = army._animations._stopMovingRight;
            else
                animation = army._animations._stopMovingLeft;

            // Not all army has stop moving animation
            if (animation == null) return;

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.StopMoving, this._character.CurrentFacingDirection);
            animationSeq._waitToTrigger = false;
            animationSeq._destPoint = ((Cell)this._path[this._path.Count - 1]).GetStandingPoint();
            this._animationSeqs.Add(animationSeq);
        }

        public void AddStartAttack(CellPartEnum attackDirection, ArrayList triggerSeqs)
        {
            Heroes.Core.Battle.Characters.Armies.Army currentArmy = (Heroes.Core.Battle.Characters.Armies.Army)this._character;

            Animation animation = null;
            HorizontalDirectionEnum facing = HorizontalDirectionEnum.None;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    animation = currentArmy._animations._attackStraightRightBegin;
                    facing = HorizontalDirectionEnum.Right;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    animation = currentArmy._animations._attackStraightLeftBegin;
                    facing = HorizontalDirectionEnum.Left;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.AttackBegin, facing);
            animationSeq._waitToTrigger = false;

            if (this._path != null && this._path.Count > 0)
                animationSeq._destPoint = ((Cell)this._path[this._path.Count - 1]).GetStandingPoint();
            else
                animationSeq._destPoint = currentArmy._cell.GetStandingPoint();

            animationSeq._triggerWhenBegin = true;
            animationSeq._triggerAnimationSeqs = triggerSeqs;

            this._animationSeqs.Add(animationSeq);
        }

        public void AddStopAttack(CellPartEnum attackDirection, ArrayList triggerSeqs)
        {
            Heroes.Core.Battle.Characters.Armies.Army currentArmy = (Heroes.Core.Battle.Characters.Armies.Army)this._character;

            Animation animation = null;
            HorizontalDirectionEnum facing = HorizontalDirectionEnum.None;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    animation = currentArmy._animations._attackStraightRightEnd;
                    facing = HorizontalDirectionEnum.Right;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    animation = currentArmy._animations._attackStraightLeftEnd;
                    facing = HorizontalDirectionEnum.Left;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.AttackEnd, facing);
            animationSeq._waitToTrigger = false;

            if (this._path != null && this._path.Count > 0)
                animationSeq._destPoint = ((Cell)this._path[this._path.Count - 1]).GetStandingPoint();
            else
                animationSeq._destPoint = currentArmy._cell.GetStandingPoint();

            animationSeq._triggerWhenBegin = true;
            animationSeq._triggerAnimationSeqs = triggerSeqs;

            this._animationSeqs.Add(animationSeq);
        }

        public void AddStartShoot(CellPartEnum attackDirection, ArrayList triggerSeqs)
        {
            Heroes.Core.Battle.Characters.Armies.Army currentArmy = (Heroes.Core.Battle.Characters.Armies.Army)this._character;

            Animation animation = null;
            HorizontalDirectionEnum facing = HorizontalDirectionEnum.None;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    animation = currentArmy._animations._shootStraightRightBegin;
                    facing = HorizontalDirectionEnum.Right;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    animation = currentArmy._animations._shootStraightLeftBegin;
                    facing = HorizontalDirectionEnum.Left;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.AttackBegin, facing);
            animationSeq._waitToTrigger = false;
            animationSeq._destPoint = currentArmy._cell.GetStandingPoint();

            animationSeq._triggerWhenBegin = true;
            animationSeq._triggerAnimationSeqs = triggerSeqs;

            this._animationSeqs.Add(animationSeq);
        }

        public void AddStopShoot(CellPartEnum attackDirection, ArrayList triggerSeqs)
        {
            Heroes.Core.Battle.Characters.Armies.Army currentArmy 
                = (Heroes.Core.Battle.Characters.Armies.Army)this._character;

            Animation animation = null;
            HorizontalDirectionEnum facing = HorizontalDirectionEnum.None;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    animation = currentArmy._animations._shootStraightRightEnd;
                    facing = HorizontalDirectionEnum.Right;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    animation = currentArmy._animations._shootStraightLeftEnd;
                    facing = HorizontalDirectionEnum.Left;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.AttackEnd, facing);
            animationSeq._waitToTrigger = false;
            animationSeq._destPoint = currentArmy._cell.GetStandingPoint();

            animationSeq._triggerWhenBegin = true;
            animationSeq._triggerAnimationSeqs = triggerSeqs;

            this._animationSeqs.Add(animationSeq);
        }

        public AnimationSequence AddDefend(CellPartEnum attackDirection)
        {
            Heroes.Core.Battle.Characters.Armies.Army currentArmy 
                = (Heroes.Core.Battle.Characters.Armies.Army)this._character;

            // get opposite direction
            Animation animation = null;
            HorizontalDirectionEnum facing = HorizontalDirectionEnum.None;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    animation = currentArmy._animations._defendLeft;
                    facing = HorizontalDirectionEnum.Left;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    animation = currentArmy._animations._defendRight;
                    facing = HorizontalDirectionEnum.Right;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.Defend, facing);
            animationSeq._waitToTrigger = true;
            animationSeq._destPoint = currentArmy._cell.GetStandingPoint();

            this._animationSeqs.Add(animationSeq);

            return animationSeq;
        }

        public AnimationSequence AddGettingHit(CellPartEnum attackDirection)
        {
            Heroes.Core.Battle.Characters.Armies.Army currentArmy 
                = (Heroes.Core.Battle.Characters.Armies.Army)this._character;

            // get opposite direction
            Animation animation = null;
            HorizontalDirectionEnum facing = HorizontalDirectionEnum.None;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    animation = currentArmy._animations._gettingHitLeft;
                    facing = HorizontalDirectionEnum.Left;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    animation = currentArmy._animations._gettingHitRight;
                    facing = HorizontalDirectionEnum.Right;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.GettingHit, facing);
            animationSeq._waitToTrigger = true;
            animationSeq._destPoint = currentArmy._cell.GetStandingPoint();

            this._animationSeqs.Add(animationSeq);

            return animationSeq;
        }

        public AnimationSequence AddDeath(CellPartEnum attackDirection)
        {
            Heroes.Core.Battle.Characters.Armies.Army currentArmy 
                = (Heroes.Core.Battle.Characters.Armies.Army)this._character;

            // get opposite direction
            Animation animation = null;
            HorizontalDirectionEnum facing = HorizontalDirectionEnum.None;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    animation = currentArmy._animations._deathLeft;
                    facing = HorizontalDirectionEnum.Left;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    animation = currentArmy._animations._deathRight;
                    facing = HorizontalDirectionEnum.Right;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.GettingHit, facing);
            animationSeq._waitToTrigger = true;
            animationSeq._destPoint = currentArmy._cell.GetStandingPoint();

            this._animationSeqs.Add(animationSeq);

            return animationSeq;
        }

        public AnimationSequence AddStartCasting(CellPartEnum attackDirection)
        {
            Heroes.Core.Battle.Characters.Hero currentHero 
                = (Heroes.Core.Battle.Characters.Hero)this._character;

            Animation animation = null;
            HorizontalDirectionEnum facing = HorizontalDirectionEnum.Right;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    if (currentHero._sex == SexEnum.Female)
                        animation = currentHero._animations._startCastSpellRightFemale;
                    else
                        animation = currentHero._animations._startCastSpellRightMale;
                    facing = HorizontalDirectionEnum.Right;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    if (currentHero._sex == SexEnum.Female)
                        animation = currentHero._animations._startCastSpellLeftFemale;
                    else
                        animation = currentHero._animations._startCastSpellLeftMale;
                    facing = HorizontalDirectionEnum.Left;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.CastSpell, facing);
            animationSeq._waitToTrigger = false;

            this._animationSeqs.Add(animationSeq);

            return animationSeq;
        }

        public AnimationSequence AddStopCasting(CellPartEnum attackDirection)
        {
            Heroes.Core.Battle.Characters.Hero currentHero
                = (Heroes.Core.Battle.Characters.Hero)this._character;

            Animation animation = null;
            HorizontalDirectionEnum facing = HorizontalDirectionEnum.Right;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    if (currentHero._sex == SexEnum.Female)
                        animation = currentHero._animations._stopCastSpellRightFemale;
                    else
                        animation = currentHero._animations._stopCastSpellRightMale;
                    facing = HorizontalDirectionEnum.Right;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    if (currentHero._sex == SexEnum.Female)
                        animation = currentHero._animations._stopCastSpellLeftFemale;
                    else
                        animation = currentHero._animations._stopCastSpellLeftMale;
                    facing = HorizontalDirectionEnum.Left;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.CastSpell, facing);
            animationSeq._waitToTrigger = true;
            
            this._animationSeqs.Add(animationSeq);

            return animationSeq;
        }

        public AnimationSequence AddOnArmySpell(PointF destPoint, CellPartEnum attackDirection, AnimationPurposeEnum purpose)
        {
            Heroes.Core.Battle.Characters.Spells.Spell currentSpell
                = (Heroes.Core.Battle.Characters.Spells.Spell)this._character;

            Animation animation = null;
            animation = currentSpell._animations._onArmy;

            HorizontalDirectionEnum facing = HorizontalDirectionEnum.Right;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    facing = HorizontalDirectionEnum.Right;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    facing = HorizontalDirectionEnum.Left;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, purpose, facing);
            animationSeq._waitToTrigger = true;
            animationSeq._destPoint = destPoint;

            this._animationSeqs.Add(animationSeq);

            return animationSeq;
        }

        public AnimationSequence AddMissileSpell(PointF destPoint, CellPartEnum attackDirection, AnimationPurposeEnum purpose)
        {
            Heroes.Core.Battle.Characters.Spells.Spell currentSpell
                = (Heroes.Core.Battle.Characters.Spells.Spell)this._character;

            Animation animation = null;
            
            HorizontalDirectionEnum facing = HorizontalDirectionEnum.Right;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    animation = currentSpell._animations._missileRight;
                    facing = HorizontalDirectionEnum.Right;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    animation = currentSpell._animations._missileLeft;
                    facing = HorizontalDirectionEnum.Left;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, purpose, facing);
            animationSeq._waitToTrigger = true;
            animationSeq._destPoint = destPoint;

            this._animationSeqs.Add(animationSeq);

            return animationSeq;
        }

        public AnimationSequence AddSpellHit(PointF destPoint, CellPartEnum attackDirection)
        {
            Heroes.Core.Battle.Characters.Spells.Spell currentSpell
                = (Heroes.Core.Battle.Characters.Spells.Spell)this._character;

            Animation animation = null;
            animation = currentSpell._animations._hitRight;

            HorizontalDirectionEnum facing = HorizontalDirectionEnum.Right;
            switch (attackDirection)
            {
                case CellPartEnum.CenterRight:
                case CellPartEnum.LowerRight:
                case CellPartEnum.UpperRight:
                    animation = currentSpell._animations._hitRight;
                    facing = HorizontalDirectionEnum.Right;
                    break;
                case CellPartEnum.CenterLeft:
                case CellPartEnum.LowerLeft:
                case CellPartEnum.UpperLeft:
                    animation = currentSpell._animations._hitLeft;
                    facing = HorizontalDirectionEnum.Left;
                    break;
            }

            AnimationSequence animationSeq = new AnimationSequence(animation, AnimationPurposeEnum.Moving, facing);
            animationSeq._waitToTrigger = true;
            animationSeq._destPoint = destPoint;

            this._animationSeqs.Add(animationSeq);

            return animationSeq;
        }

    }
}
