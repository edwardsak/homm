using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using Heroes.Core.Battle;
using Heroes.Core.Battle.Terrains;

namespace Heroes.Core.Battle.Quick
{
    public class BattleCommander
    {
        Terrains.BattleTerrain _battleTerrain;
        Turn _turn;

        Heroes.Core.Hero _attackHeroOriginal;
        Heroes.Core.Hero _defendHeroOriginal;
        Heroes.Core.Monster _monsterOriginal;

        Heroes.Core.Player _attackPlayer;
        Heroes.Core.Player _defendPlayer;
        public Heroes.Core.Battle.Heros.Hero _attackHero;
        public Heroes.Core.Battle.Heros.Hero _defendHero;
        public Heroes.Core.Battle.Quick.Monster _monster;

        Hashtable _heroes;  // key = heroId
        Hashtable _attackArmies;    // key = slotNo
        Hashtable _defendArmies;    // key = slotNo

        ArrayList _armies;  // order by speed, favour attacker
        Heroes.Core.Battle.Armies.Army _currentArmy;
        Heroes.Core.Hero _currentHero;
        Heroes.Core.Player _currentPlayer;

        bool _quickCombat;

        BattleSideEnum _victory;

        public BattleCommander(Heroes.Core.Player attackPlayer, Heroes.Core.Hero attackHero, 
            Heroes.Core.Player defendPlayer, Heroes.Core.Hero defendHero, Heroes.Core.Monster monster, bool quickCombat)
        {
            _battleTerrain = new BattleTerrain();
            
            _attackPlayer = attackPlayer;
            _defendPlayer = defendPlayer;

            {
                _attackHeroOriginal = attackHero;

                _attackHero = new Heroes.Core.Battle.Heros.Hero();
                _attackHero.CopyFrom(attackHero);
                _attackHero._armySide = Heroes.Core.Battle.Characters.ArmySideEnum.Attacker;
                _attackHero._spells = attackHero._spells;

                foreach (Heroes.Core.Army army in attackHero._armyKSlots.Values)
                {
                    Heroes.Core.Battle.Armies.Army army2 = new Heroes.Core.Battle.Armies.Army();
                    army2.CopyFrom(army);
                    army2._armySide = Heroes.Core.Battle.Characters.ArmySideEnum.Attacker;

                    army2._qtyLeft = army2._qty;
                    army2._healthRemain = army2._health;
                    army2._shotRemain = army2._noOfShot;
                    army2._retaliateRemain = army2._noOfRetaliate;

                    army2._cell = GetCell(army2._armySide, army2._slotNo, army2._isBig);
                    army2._cell._character = army2;

                    _attackHero._armyKSlots.Add(army2._slotNo, army2);
                }

                _attackArmies = _attackHero._armyKSlots;
            }

            _defendHeroOriginal = null;
            _defendHero = null;
            if (defendHero != null)
            {
                _defendHeroOriginal = defendHero;

                _defendHero = new Heroes.Core.Battle.Heros.Hero();
                _defendHero.CopyFrom(defendHero);
                _defendHero._armySide = Heroes.Core.Battle.Characters.ArmySideEnum.Defender;
                _defendHero._spells = attackHero._spells;

                foreach (Heroes.Core.Army army in defendHero._armyKSlots.Values)
                {
                    Heroes.Core.Battle.Armies.Army army2 = new Heroes.Core.Battle.Armies.Army();
                    army2.CopyFrom(army);
                    army2._armySide = Heroes.Core.Battle.Characters.ArmySideEnum.Defender;

                    army2._qtyLeft = army2._qty;
                    army2._healthRemain = army2._health;
                    army2._shotRemain = army2._noOfShot;
                    army2._retaliateRemain = army2._noOfRetaliate;

                    army2._cell = GetCell(army2._armySide, army2._slotNo, army2._isBig);
                    army2._cell._character = army2;

                    _defendHero._armyKSlots.Add(army2._slotNo, army2);
                }

                _defendArmies = _defendHero._armyKSlots;
            }

            _monsterOriginal = null;
            _monster = null;
            if (monster != null)
            {
                _monsterOriginal = monster;

                _monster = new Heroes.Core.Battle.Quick.Monster();
                _monster.CopyFrom(monster);
                _monster._armySide = Heroes.Core.Battle.Characters.ArmySideEnum.Defender;

                foreach (Heroes.Core.Army army in monster._armyKSlots.Values)
                {
                    Heroes.Core.Battle.Armies.Army army2 = new Heroes.Core.Battle.Armies.Army();
                    army2.CopyFrom(army);
                    army2._armySide = Heroes.Core.Battle.Characters.ArmySideEnum.Defender;

                    army2._qtyLeft = army2._qty;
                    army2._healthRemain = army2._health;
                    army2._shotRemain = army2._noOfShot;
                    army2._retaliateRemain = army2._noOfRetaliate;

                    army2._cell = GetCell(army2._armySide, army2._slotNo, army2._isBig);
                    army2._cell._character = army2;

                    _monster._armyKSlots.Add(army2._slotNo, army2);
                }

                _defendArmies = _monster._armyKSlots;
            }

            _heroes = new Hashtable();
            _heroes.Add(_attackHero._id, _attackHero);
            if (_defendHero != null) _heroes.Add(_defendHero._id, _defendHero);

            _armies = new ArrayList();
            foreach (Heroes.Core.Army army in _attackArmies.Values)
            {
                _armies.Add(army);
            }
            foreach (Heroes.Core.Army army in _defendArmies.Values)
            {
                _armies.Add(army);
            }

            BattleEngine.SortBySpeed(_armies);

            _quickCombat = quickCombat;

            _turn = new Turn(_armies, _attackHero, _attackArmies, _defendHero, _monster, _defendArmies);
        }

        private Cell GetCell(Heroes.Core.Battle.Characters.ArmySideEnum armySide, int slotNo, bool isBig)
        {
            int startCol = 0;
            if (armySide == Heroes.Core.Battle.Characters.ArmySideEnum.Attacker)
                startCol = 0;
            else
                startCol = _battleTerrain._colCount - 1;

            if (slotNo == 0)
                return _battleTerrain._cells[0, startCol];
            else if (slotNo == 1)
                return _battleTerrain._cells[2, startCol];
            else if (slotNo == 2)
                return _battleTerrain._cells[4, startCol];
            else if (slotNo == 3)
                return _battleTerrain._cells[5, startCol];
            else if (slotNo == 4)
                return _battleTerrain._cells[6, startCol];
            else if (slotNo == 5)
                return _battleTerrain._cells[8, startCol];
            else if (slotNo == 6)
                return _battleTerrain._cells[10, startCol];
            else
                return null;
        }

        public BattleSideEnum Start()
        {
            if (_armies.Count < 1) return BattleSideEnum.None;
            if (_turn._currentCharacter == null) return BattleSideEnum.None;

            while (true)
            {
                if (_quickCombat || _currentPlayer._isComputer)
                {
                    if (!ProcessAi(_turn._currentHero, _turn._currentCharacter, _turn._targetHero, _turn._targetArmies))
                    {
                        _turn.Defend();
                    }
                }
                else
                {
                    // wait for user command
                }

                if (CheckVictory()) break;

                _turn.NextTurn();
            }

            return this._victory;
        }

        private bool ProcessAi(Heroes.Core.Hero currentHero, Heroes.Core.Battle.Armies.Army currentArmy, 
            Heroes.Core.Hero targetHero, Hashtable targetArmies)
        {
            bool hasHandToHandPenalty = false;
            if (currentArmy._hasRangeAttack)
            {
                HasHandToHandPenalty(currentArmy);
            }

            // shoot if has range attack
            if (!hasHandToHandPenalty && currentArmy._shotRemain > 0)
            {
                // find most kill, damage
                Heroes.Core.Battle.Armies.Army mostKillCharacter = null;
                int mostKill = 0;
                int mostDamage = 0;

                foreach (Heroes.Core.Battle.Armies.Army targetArmy in targetArmies.Values)
                {
                    if (targetArmy._isDead) continue;

                    bool hasRangePenalty = false;
                    bool hasObstaclePenalty = false;

                    HasRangeAttackPenalty(_battleTerrain, currentArmy, targetArmy, out hasRangePenalty, out hasObstaclePenalty);

                    int minDamage = 0;
                    int maxDamage = 0;
                    int minKill = 0;
                    int maxKill = 0;
                    int avgKill = 0;

                    CalculateDamage(currentHero, currentArmy, targetHero, targetArmy, 
                        true, hasRangePenalty, hasObstaclePenalty, hasHandToHandPenalty,
                        out minDamage, out maxDamage, out minKill, out maxKill, out avgKill);

                    if (mostKillCharacter == null)
                    {
                        mostKillCharacter = targetArmy;
                        mostKill = maxKill;
                        mostDamage = maxDamage;
                    }
                    else if (maxKill > mostKill
                        || (maxKill == mostKill && maxDamage > mostDamage))
                    {
                        mostKillCharacter = targetArmy;
                        mostKill = maxKill;
                        mostDamage = maxDamage;
                    }
                }

                // attack
                {
                    // reset defend and wait
                    Heroes.Core.Battle.Armies.Army army
                        = (Heroes.Core.Battle.Armies.Army)currentArmy;
                    army._isWait = false;
                    army._isDefend = false;

                    // get attackDirection
                    if (mostKillCharacter == null) return false;
                    CellPartEnum attackDirection
                        = BattleTerrain.FindDirection(currentArmy._cell.GetCenterPoint(), mostKillCharacter._cell.GetCenterPoint());

                    SetRangeAttack(currentHero, currentArmy, targetHero, mostKillCharacter, attackDirection, mostKillCharacter._cell);
                }
            }
            else
            {
                // find enermies distance
                bool hasEnermy = false;
                ArrayList enermies = new ArrayList();
                ArrayList enermyPaths = new ArrayList();
                ArrayList inRangeEnermies = new ArrayList();
                ArrayList inRangePaths = new ArrayList();

                foreach (Heroes.Core.Battle.Armies.Army targetArmy in targetArmies.Values)
                {
                    if (targetArmy._isDead) continue;
                    hasEnermy = true;

                    // get range
                    ArrayList path = null;
                    if (!_battleTerrain.FindPathAdjBest(currentArmy._cell, targetArmy._cell, true, false, out path))
                    {
                        // cannot reach target army
                        continue;
                    }

                    if (path == null) continue;     // cannot reach target army

                    enermies.Add(targetArmy);
                    enermyPaths.Add(path);

                    if (currentArmy._speed >= path.Count - 1)
                    {
                        inRangeEnermies.Add(targetArmy);
                        inRangePaths.Add(path);
                    }
                    else
                        continue;
                }

                // skip if no more enermy
                if (!hasEnermy)
                {
                    // defend

                    return false;
                }

                // has enermy but cannot reach
                if (enermies.Count < 1)
                {
                    // defend

                    return false;
                }

                // if enermy in range, attack most kill, most damage
                if (inRangeEnermies.Count > 0)
                {
                    int mostKillIndex = 0;
                    if (inRangeEnermies.Count > 1)
                    {
                        // find most kill
                        int index = 0;
                        int mostKill = 0;
                        int mostDamage = 0;

                        foreach (Heroes.Core.Battle.Armies.Army targetArmy in inRangeEnermies)
                        {
                            int minDamage = 0;
                            int maxDamage = 0;
                            int minKill = 0;
                            int maxKill = 0;
                            int avgKill = 0;

                            CalculateDamage(currentHero, currentArmy, targetHero, targetArmy, 
                                false, false, false, hasHandToHandPenalty,
                                out minDamage, out maxDamage, out minKill, out maxKill, out avgKill);

                            if (index == 0)
                            {
                                mostKill = maxKill;
                                mostDamage = maxDamage;
                                mostKillIndex = index;
                            }
                            else
                            {
                                if (maxKill > mostKill
                                    || (maxKill == mostKill && maxDamage > mostDamage))
                                {
                                    mostKill = maxKill;
                                    mostDamage = maxDamage;
                                    mostKillIndex = index;
                                }
                            }

                            index += 1;
                        }
                    }

                    // attack most kill enermy
                    {
                        Heroes.Core.Battle.Armies.Army mostKillCharacter
                            = (Heroes.Core.Battle.Armies.Army)inRangeEnermies[mostKillIndex];

                        ArrayList path = (ArrayList)inRangePaths[mostKillIndex];

                        // get attackDirection
                        CellPartEnum attackDirection = CellPartEnum.Center;
                        {
                            Cell cellFrom = null;
                            Cell cellTo = (Cell)path[path.Count - 1];

                            if (path.Count < 2)
                                cellFrom = currentArmy._cell;
                            else
                                cellFrom = (Cell)path[path.Count - 2];

                            attackDirection = BattleTerrain.FindDirection(cellFrom.GetCenterPoint(), cellTo.GetCenterPoint());
                        }

                        // remove last cell because last cell is enermy's cell
                        if (path.Count > 0)
                            path.RemoveAt(path.Count - 1);

                        // damage
                        SetAttack(currentHero, currentArmy, targetHero, mostKillCharacter, attackDirection, path);

                        // set attacker to destination cell
                        if (path.Count > 0)
                            SetDestCell(currentArmy, (Cell)path[path.Count - 1]);
                        else
                            SetDestCell(currentArmy, currentArmy._cell);
                    }
                }
                else
                {
                    // find nearest enermy
                    int index = 0;
                    int leastCount = 0;
                    int leastCountIndex = 0;
                    foreach (ArrayList path in enermyPaths)
                    {
                        if (index == 0)
                        {
                            leastCount = path.Count;
                            leastCountIndex = index;
                        }
                        else
                        {
                            if (path.Count < leastCount)
                            {
                                leastCount = path.Count;
                                leastCountIndex = index;
                            }
                        }

                        index += 1;
                    }

                    // move
                    {
                        ArrayList path2 = (ArrayList)enermyPaths[leastCountIndex];

                        // remove last cell because last cell is enermy's cell
                        if (path2.Count > 0)
                            path2.RemoveAt(path2.Count - 1);

                        // limit path to character's speed
                        while (path2.Count > currentArmy._speed)
                        {
                            path2.RemoveAt(path2.Count - 1);
                        }

                        // reset defend and wait
                        Heroes.Core.Battle.Armies.Army army
                            = (Heroes.Core.Battle.Armies.Army)currentArmy;
                        army._isWait = false;
                        army._isDefend = false;

                        SetDestCell(currentArmy, (Cell)path2[path2.Count - 1]);
                    }
                }
            }   // melee attack

            return true;
        }

        private void HasRangeAttackPenalty(BattleTerrain battleTerrain,
            Heroes.Core.Battle.Armies.Army currentArmy, Heroes.Core.Battle.Armies.Army targetArmy,
            out bool hasRangePenalty, out bool hasObstaclePenalty)
        {
            hasRangePenalty = false;
            hasObstaclePenalty = false;

            // Range Penalty
            // Whenever any shooter, except Sharpshooters or those under the command of a hero with the Golden Bow or Bow of the Sharpshooter equipped, attacks a unit standing more than 10(?) hexes away, they will only do half damage. This is cumulative with the Obstacle Penalty.

            // obstacle Penalty
            // Whenever most units tries shooting over Walls into a castle, they will only do half Damage. Certain units don't have this penalty, and neither do units under the command of a hero with the Golden Bow or Bow of the Sharpshooter. This penalty is cumulative with the Range Penalty

            // find attack range
            ArrayList path = new ArrayList();
            battleTerrain.FindPath(currentArmy._cell, targetArmy._cell, path, true, true);

            if (path.Count > 10) hasRangePenalty = true;
        }

        private bool HasHandToHandPenalty(Heroes.Core.Battle.Armies.Army c)
        {
            if (c._handToHandPenalty)
            {
                if (HasAdjacentEnermy(c)) return true;
            }

            return false;
        }

        private bool HasAdjacentEnermy(Heroes.Core.Battle.Armies.Army c)
        {
            foreach (Cell cell in c._cell._adjacentCells.Values)
            {
                if (cell == null) continue;
                if (cell._character == null) continue;
                if (cell._character._playerId != c._playerId)
                {
                    return true;
                }
            }

            return false;
        }

        private void CalculateDamage(Heroes.Core.Hero attackHero, Heroes.Core.Battle.Armies.Army attacker,
            Heroes.Core.Hero defendHero, Heroes.Core.Battle.Armies.Army defender,
            bool isRangeAttack, bool hasRangePenalty, bool hasObstaclePenalty, bool hasHandToHandPenalty,
            out int minDamage, out int maxDamage, out int minKill, out int maxKill, out int avgKill)
        {
            minDamage = 0;
            maxDamage = 0;
            minKill = 0;
            maxKill = 0;
            avgKill = 0;

            // totalDamage = baseDamage * (1+a+b+c+...) * (1-q) * (1-r) * (1-s) * ...
            // a, b, c,... are damage bonus
            // q, r, s,... are damage reductions (both as decimal numbers)

            // Damage bonuese
            // Attacker's Attack Skill > Defender's Defense Skill
            // 0.05 * (A-D), capped at 3

            // Attacker is a shooter
            // Archery Skill bonus, 
            // additive with artifact bonuses if Archery skill present

            // Damage Reductions
            // Defender's Defense > Attacker's Attack
            // 0,025 * (D – A), up to a maximum of 0,7 

            // if got hero
            // attack + 1 + hero's attack

            decimal attackBonus = 0m;
            if (attacker._attack > defender._defense)
            {
                attackBonus = 0.05m * (decimal)(attacker._attack - defender._defense);
                if (attackBonus > 3m) attackBonus = 3m;
            }

            decimal offenceBonus = 0m;
            decimal archeryBonus = 0m;
            {
                Hero hero = FindHero(attackHero, defendHero, attacker._heroId);
                if (hero != null)
                {
                    if (isRangeAttack)
                    {
                        if (hero._skills.ContainsKey((int)SkillIdEnum.Archery))
                        {
                            Skill skill = (Skill)hero._skills[(int)SkillIdEnum.Archery];
                            archeryBonus = (decimal)skill._effects[skill._level] / 100m;
                        }
                    }
                    else
                    {
                        if (hero._skills.ContainsKey((int)SkillIdEnum.Offence))
                        {
                            Skill skill = (Skill)hero._skills[(int)SkillIdEnum.Offence];
                            offenceBonus = (decimal)skill._effects[skill._level] / 100m;
                        }
                    }
                }
            }

            decimal defenseBonus = 0m;
            if (defender._defense > attacker._attack)
            {
                defenseBonus = 0.025m * (decimal)(defender._defense - attacker._attack);
                if (defenseBonus > 0.7m) defenseBonus = 0.7m;
            }

            decimal armourerBonus = 0m;
            {
                Hero hero = FindHero(attackHero, defendHero, defender._heroId);
                if (hero != null)
                {
                    if (hero._skills.ContainsKey((int)SkillIdEnum.Armourer))
                    {
                        Skill skill = (Skill)hero._skills[(int)SkillIdEnum.Armourer];
                        armourerBonus = (decimal)skill._effects[skill._level] / 100m;
                    }
                }
            }

            decimal rangePenalty = 0m;
            if (hasRangePenalty) rangePenalty = 0.5m;

            decimal obstaclePenalty = 0m;
            if (hasObstaclePenalty) obstaclePenalty = 0.5m;

            decimal handToHandPenalty = 0m;
            if (hasHandToHandPenalty) handToHandPenalty = 0.5m;

            decimal defending = 0m;
            Heroes.Core.Battle.Armies.Army defendArmy = (Heroes.Core.Battle.Armies.Army)defender;
            if (defendArmy._isDefend) defending = 0.25m;

            minDamage = (int)decimal.Truncate((decimal)attacker._qtyLeft * (decimal)attacker._minDamage
                * (1m + attackBonus) * (1m + offenceBonus) * (1m + archeryBonus)
                * (1m - defenseBonus) * (1m - armourerBonus) * (1m - rangePenalty) * (1m - obstaclePenalty) * (1m - handToHandPenalty)
                * (1m - defending));
            maxDamage = (int)decimal.Truncate((decimal)attacker._qtyLeft * (decimal)attacker._maxDamage
                * (1m + attackBonus) * (1m + offenceBonus) * (1m + archeryBonus)
                * (1m - defenseBonus) * (1m - armourerBonus) * (1m - rangePenalty) * (1m - obstaclePenalty) * (1m - handToHandPenalty)
                * (1m - defending));

            // calculate kill
            if (defender._healthRemain > minDamage)
                minKill = 0;
            else if (defender._healthRemain == minDamage)
                minKill = 1;
            else
                minKill = 1 + (int)decimal.Truncate((decimal)(minDamage - defender._healthRemain) / (decimal)defender._health);

            if (defender._healthRemain > maxDamage)
                maxKill = 0;
            else if (defender._healthRemain == maxDamage)
                maxKill = 1;
            else
                maxKill = 1 + (int)decimal.Truncate((decimal)(maxDamage - defender._healthRemain) / (decimal)defender._health);

            avgKill = (minKill + maxKill) / 2;
        }

        private void SetAttackDamage(BattleTerrain battleTerrain,
            Heroes.Core.Hero currentHero, Heroes.Core.Battle.Armies.Army currentArmy,
            Heroes.Core.Hero targetHero, Heroes.Core.Battle.Armies.Army targetArmy, bool isRangeAttack)
        {
            bool hasRangePenalty = false;
            bool hasObstaclePenalty = false;
            HasRangeAttackPenalty(battleTerrain, currentArmy, targetArmy, out hasRangePenalty, out hasObstaclePenalty);

            bool hasHandToHandPenalty = HasHandToHandPenalty(currentArmy);

            int minDamage = 0;
            int maxDamage = 0;
            int minKill = 0;
            int maxKill = 0;
            int avgKill = 0;

            CalculateDamage(currentHero, currentArmy, targetHero, targetArmy,
                isRangeAttack, hasRangePenalty, hasObstaclePenalty, hasHandToHandPenalty,
                out minDamage, out maxDamage, out minKill, out maxKill, out avgKill);

            // random from minDamage to maxDamage
            //Random rnd = new Random();
            //int rndDamage = rnd.Next(minDamage, maxDamage);
            int rndDamage = (minDamage + maxDamage) / 2;        // use average damage because rnd Damage need to inform other player

            SetDamage(rndDamage, targetArmy);
        }

        private void SetDamage(int damage, Heroes.Core.Battle.Armies.Army targetArmy)
        {
            // defender's health
            int totalHealthRemain = targetArmy._healthRemain + (targetArmy._qtyLeft - 1) * targetArmy._health;
            totalHealthRemain -= damage;

            if (totalHealthRemain <= 0)
            {
                targetArmy._qtyLeft = 0;
                targetArmy._healthRemain = 0;
                targetArmy._isDead = true;
            }
            else
            {
                decimal a = (decimal)totalHealthRemain / (decimal)targetArmy._health;
                decimal qtyRemain = decimal.Truncate(a);
                decimal healthRemain2 = decimal.Truncate((a - qtyRemain) * (decimal)targetArmy._health);

                targetArmy._qtyLeft = (int)(qtyRemain + 1);     // extra 1 is not full health
                targetArmy._healthRemain = (int)healthRemain2;  // remaining health
                if (targetArmy._healthRemain == 0)
                {
                    targetArmy._qtyLeft -= 1;    // reduce 1 if no health
                    targetArmy._healthRemain = targetArmy._health;  // full health
                }
            }
        }

        private void SetAttack(Heroes.Core.Hero attackHero, Heroes.Core.Battle.Armies.Army attacker,
            Heroes.Core.Hero defendHero, Heroes.Core.Battle.Armies.Army defender,
            CellPartEnum attackDirection, ArrayList path)
        {
            // reset defend and wait
            Heroes.Core.Battle.Armies.Army army
                = (Heroes.Core.Battle.Armies.Army)attacker;
            army._isWait = false;
            army._isDefend = false;

            // attack
            SetAttackDamage(this._battleTerrain, attackHero, attacker, defendHero, defender, false);

            if (!defender._isDead)
            {
                // retaliate
                if (defender._retaliateRemain > 0)
                {
                    SetAttackDamage(this._battleTerrain, defendHero, defender, attackHero, attacker, false);
                    defender._retaliateRemain -= 1;
                }

                // attack more
                {
                    ArrayList targets = new ArrayList();
                    targets.Add(defender);

                    for (int i = 1; i < attacker._noOfAttack; i++)
                    {
                        SetAttackDamage(this._battleTerrain, attackHero, attacker, defendHero, defender, false);
                    }
                }
            }
        }

        private void SetRangeAttack(Heroes.Core.Hero attackHero, Heroes.Core.Battle.Armies.Army attacker,
            Heroes.Core.Hero defendHero, Heroes.Core.Battle.Armies.Army defender,
            CellPartEnum attackDirection, Cell targetCell)
        {
            // reset defend and wait
            Heroes.Core.Battle.Armies.Army army
                = (Heroes.Core.Battle.Armies.Army)attacker;
            army._isWait = false;
            army._isDefend = false;

            // attack
            SetAttackDamage(this._battleTerrain, attackHero, attacker, defendHero, defender, true);
            attacker._shotRemain -= 1;

            if (!defender._isDead)
            {
                // retaliate
                //if (defender._canRangeRetaliate && defender._retaliateRemain > 0 && defender._shotRemain > 0)
                //{
                //    SetAttackDamage(defender, attacker, true);
                //    defender._retaliateRemain -= 1;

                //    // retaliate action
                //    {
                //        CellPartEnum oppAttackDirection = BattleTerrain.GetOppositeDirection(attackDirection);

                //        ArrayList targets = new ArrayList();
                //        targets.Add(attacker);

                //        Action action = Action.CreateShootAction(defender, oppAttackDirection, targets, attacker._cell);
                //        _actions.Add(action);
                //    }
                //}

                // attack more
                {
                    ArrayList targets = new ArrayList();
                    targets.Add(defender);

                    for (int i = 1; i < attacker._noOfAttack; i++)
                    {
                        SetAttackDamage(this._battleTerrain, attackHero, attacker, defendHero, defender, true);
                        attacker._shotRemain -= 1;
                    }
                }
            }
        }

        private void SetDestCell(Heroes.Core.Battle.Armies.Army c, Cell cell)
        {
            c._cell._character = null;
            c._cell = cell;
            c._cell._character = c;
        }

        private Heroes.Core.Hero FindHero(Heroes.Core.Hero attacker, Heroes.Core.Hero defender, int heroId)
        {
            if (attacker != null && attacker._id == heroId) return attacker;
            else if (defender != null && defender._id == heroId) return defender;
            else return null;
        }

        private bool CheckVictory()
        {
            bool hasArmy = false;
            if (_attackHero != null)
            {
                foreach (Heroes.Core.Battle.Armies.Army c in _attackHero._armyKSlots.Values)
                {
                    if (c._qtyLeft > 0)
                    {
                        hasArmy = true;
                        break;
                    }
                }

                if (!hasArmy)
                {
                    // attacker loss
                    _victory = BattleSideEnum.Defender;

                    if (_defendHero != null)
                        EndBattle(_attackHero, _defendHero, _monster, null, _victory);
                    else if (_monster != null)
                        EndBattle(_attackHero, _defendHero, _monster, null, _victory);

                    return true;
                }
            }

            hasArmy = false;
            if (_defendHero != null)
            {
                foreach (Heroes.Core.Battle.Armies.Army c in _defendHero._armyKSlots.Values)
                {
                    if (c._qtyLeft > 0)
                    {
                        hasArmy = true;
                        break;
                    }
                }

                if (!hasArmy)
                {
                    _victory = BattleSideEnum.Attacker;
                    EndBattle(_attackHero, _defendHero, _monster, null, _victory);

                    return true;
                }
            }

            hasArmy = false;
            if (_monster != null)
            {
                foreach (Heroes.Core.Battle.Armies.Army c in _monster._armyKSlots.Values)
                {
                    if (c._qtyLeft > 0)
                    {
                        hasArmy = true;
                        break;
                    }
                }

                if (!hasArmy)
                {
                    _victory = BattleSideEnum.Attacker;
                    EndBattle(_attackHero, _defendHero, _monster, null, _victory);

                    return true;
                }
            }

            return false;
        }

        private void EndBattle(Heroes.Core.Battle.Heros.Hero attackHero, Heroes.Core.Battle.Heros.Hero defendHero,
            Heroes.Core.Battle.Quick.Monster defendMonster, Heroes.Core.Town defendCastle,
            BattleSideEnum victory)
        {
            SetQtyLeft(attackHero, _attackHeroOriginal);

            if (defendHero != null)
            {
                SetQtyLeft(defendHero, _defendHeroOriginal);
            }

            if (defendMonster != null)
            {
                SetQtyLeft(defendMonster, _monsterOriginal);
            }
        }

        private void SetQtyLeft(Heroes.Core.Battle.Heros.Hero hero, Heroes.Core.Hero originalHero)
        {
            foreach (Heroes.Core.Army army in hero._armyKSlots.Values)
            {
                if (army._qtyLeft <= 0)
                    originalHero._armyKSlots.Remove(army._slotNo);
                else
                {
                    Heroes.Core.Army originalArmy = (Heroes.Core.Army)originalHero._armyKSlots[army._slotNo];
                    originalArmy._qty = army._qtyLeft;
                }
            }
        }

        private void SetQtyLeft(Heroes.Core.Battle.Quick.Monster monster, Heroes.Core.Monster originalMonster)
        {
            foreach (Heroes.Core.Army army in monster._armyKSlots.Values)
            {
                if (army._qtyLeft <= 0)
                    originalMonster._armyKSlots.Remove(army._slotNo);
                else
                {
                    Heroes.Core.Army originalArmy = (Heroes.Core.Army)originalMonster._armyKSlots[army._slotNo];
                    originalArmy._qty = army._qtyLeft;
                }
            }
        }

    }
}
