using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

using Heroes.Core.Battle.Characters;
using Heroes.Core.Battle.Characters.Armies;
using Heroes.Core.Battle.Characters.Commands;
using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.Terrains;

namespace Heroes.Core.Battle
{
    public class BattleEngine : BasicEngine
    {
        #region Events
        public delegate void BattleEndedEventHandler(BattleEndedEventArg e);
        public delegate void DamageCalculatedEventHandler(DamageCalculatedEventArg e);
        

        public event BattleEndedEventHandler BattleEnded;
        public event DamageCalculatedEventHandler DamageCalculated;

        public class BattleEndedEventArg
        {
            public BattleSideEnum _victory;

            public BattleEndedEventArg(BattleSideEnum victory)
            {
                _victory = victory;
            }
        }

        public class DamageCalculatedEventArg
        {
            public int _minDamage;
            public int _maxDamage;
            public int _minKill;
            public int _maxKill;
            public int _avgKill;

            public DamageCalculatedEventArg(int minDamage, int maxDamage, int minKill, int maxKill, int avgKill)
            {
                _minDamage = minDamage;
                _maxDamage = maxDamage;
                _minKill = minKill;
                _maxKill = maxKill;
                _avgKill = avgKill;
            }
        }

        public class ViewingArmyInfoEventArg
        {
            public Heroes.Core.Army _army;
            public Heroes.Core.Battle.frmArmyInfo _frmArmyInfo;

            public ViewingArmyInfoEventArg(Heroes.Core.Army army, Heroes.Core.Battle.frmArmyInfo frmArmyInfo)
            {
                _army = army;
                _frmArmyInfo = frmArmyInfo;
            }
        }

        protected virtual void OnBattleEnded(BattleEndedEventArg e)
        {
            if (BattleEnded != null)
            {
                //Invokes the delegates.
                BattleEnded(e);
            }
        }

        protected virtual void OnDamageCalculated(DamageCalculatedEventArg e)
        {
            if (DamageCalculated != null)
            {
                //Invokes the delegates.
                DamageCalculated(e);
            }
        }
        #endregion

        Controller _controller;
        InputCommand _inputCommand;
        public BattleTerrain _battleTerrain;
        public Turn _turn;

        bool _isFirstRun;
        public bool _disableControl;

        public Heroes.Core.Battle.Characters.Hero _attacker;
        public Heroes.Core.Battle.Characters.Hero _defender;
        public Heroes.Core.Battle.Characters.Monster _monster;

        ArrayList _actions;

        // status message
        public Rectangle _rectStatusMsg;
        public string _statusMsg;

        // spells
        public Hashtable _spells;
        public ArrayList _spellActions;

        public BattleEngine(Controller controller, Heroes.Core.Hero attacker, 
            Heroes.Core.Hero defender, Heroes.Core.Monster monster, Heroes.Core.Town castle)
            : base()
        {
            _isFirstRun = true;
            _disableControl = false;

            _controller = controller;
            _inputCommand = new InputCommand();

            _battleTerrain = new Heroes.Core.Battle.Terrains.BattleTerrain(controller);

            _spells = new Hashtable();
            _spellActions = new ArrayList();

            // init hero and armies
            {
                _activeCharacters = new ArrayList();

                InitHero(attacker, ArmySideEnum.Attacker, controller, out _attacker);

                if (defender != null)
                {
                    InitHero(defender, ArmySideEnum.Defender, controller, out _defender);
                }

                if (monster != null)
                {
                    InitMonster(monster, ArmySideEnum.Defender, controller, out _monster);
                }

                // sort armies by speed
                SortBySpeed(_activeCharacters);
            }

            // initialize graphics
            {
                foreach (StandardCharacter c in _activeCharacters)
                {
                    c.Initalize();
                    c.Command = _inputCommand;
                    c.Command.InitalizeGraphics(c);
                }

                _attacker.Initalize();
                if (_defender != null) _defender.Initalize();
            }

            _turn = new Turn(_activeCharacters, _attacker, _defender);
            _turn.NextTurned += new Turn.NextTurnedEventHandler(_turn_NextTurned);

            _actions = new ArrayList();
        }

        void _turn_NextTurned()
        {
            ProcessAi();
        }

        private void InitHero(Heroes.Core.Hero hero, ArmySideEnum armySide, 
            Controller controller,
            out Heroes.Core.Battle.Characters.Hero hero2)
        {
            hero2 = null;

            switch (hero._heroType)
            {
                case HeroTypeEnum.Knight:
                    hero2 = new Heroes.Core.Battle.Characters.Heros.Knight(controller);
                    break;
                case HeroTypeEnum.Cleric:
                    hero2 = new Heroes.Core.Battle.Characters.Heros.Knight(controller);
                    break;
                default:
                    return;
            }

            hero2.CopyFrom(hero);
            hero2._originalHero = hero;
            hero2._player = hero._player;
            hero2._armySide = armySide;
            hero2._spells = hero._spells;

            // add armies
            {
                foreach (Heroes.Core.Army army in hero._armyKSlots.Values)
                {
                    Heroes.Core.Battle.Characters.Armies.Army c
                        = Heroes.Core.Battle.Characters.Armies.Army.CreateArmy(army, controller, hero2._armySide);
                    if (c == null) continue;

                    c._qtyLeft = c._qty;
                    c._healthRemain = c._health;
                    c._shotRemain = c._noOfShot;
                    c._retaliateRemain = c._noOfRetaliate;

                    c.InitCell(GetCell(c._armySide, c._slotNo, c._isBig));
                    c._cell._character = c;

                    hero2._armyKSlots.Add(c._slotNo, c);
                    _activeCharacters.Add(c);
                }
            }

            // add spells
            {
                foreach (Heroes.Core.Spell spell in hero._spells.Values)
                {
                    if (_spells.ContainsKey(spell._id)) continue;

                    Heroes.Core.Battle.Characters.Spells.Spell c
                        = Heroes.Core.Battle.Characters.Spells.Spell.CreateSpell(spell, controller);
                    if (c == null) continue;

                    _spells.Add(c._id, c);
                }
            }
        }

        private void InitMonster(Heroes.Core.Monster monster, ArmySideEnum armySide, 
            Controller controller,
            out Heroes.Core.Battle.Characters.Monster monster2)
        {
            monster2 = new Heroes.Core.Battle.Characters.Monster();
            monster2.CopyFrom(monster);
            monster2._originalMonster = monster;
            monster2._armySide = armySide;

            // add armies
            {
                foreach (Heroes.Core.Army army in monster._armyKSlots.Values)
                {
                    Heroes.Core.Battle.Characters.Armies.Army c
                        = Heroes.Core.Battle.Characters.Armies.Army.CreateArmy(army, controller, monster2._armySide);
                    if (c == null) continue;

                    c._qtyLeft = c._qty;
                    c._healthRemain = c._health;
                    c._shotRemain = c._noOfShot;
                    c._retaliateRemain = c._noOfRetaliate;

                    if (c != null)
                    {
                        c.InitCell(GetCell(c._armySide, c._slotNo, c._isBig));

                        c._cell._character = c;

                        monster2._armyKSlots.Add(c._slotNo, c);
                        _activeCharacters.Add(c);
                    }
                }
            }
        }

        private Cell GetCell(ArmySideEnum armySide, int slotNo, bool isBig)
        {
            //if (armySide == ArmySideEnum.Attacker)
            //{
            //    if (slotNo == 0)
            //        return _battleTerrain._cells[6, 13];
            //    else if (slotNo == 1)
            //        return _battleTerrain._cells[7, 13];
            //    else if (slotNo == 2)
            //        return _battleTerrain._cells[8, 12];
            //    else if (slotNo == 3)
            //        return _battleTerrain._cells[5, 6];
            //}
            //else
            //{
            //    if (slotNo == 0)
            //        return _battleTerrain._cells[9, 12];
            //    else if (slotNo == 1)
            //        return _battleTerrain._cells[10, 11];
            //    else if (slotNo == 2)
            //        return _battleTerrain._cells[4, 11];
            //    else if (slotNo == 3)
            //        return _battleTerrain._cells[5, 14];
            //}

            //return null;

            int startCol = 0;
            if (armySide == ArmySideEnum.Attacker)
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

        #region Action, Turn
        protected override void RunTurn()
        {
            if (_isFirstRun)
            {
                _isFirstRun = false;
                
                FirstLoad();

                // if first character is computer, process ai
                ProcessAi();

                return;
            }

            ProcessAction();

            // process army
            foreach (StandardCharacter c in _activeCharacters)
            {
                ProcessCharacter(c);
            }

            // process hero
            ProcessHero(_attacker);

            if (_defender != null)
                ProcessHero(_defender);

            // process spells
            foreach (Heroes.Core.Battle.Characters.Spells.Spell c in _spellActions)
            {
                ProcessSpell(c);
            }

            base.RunTurn();
        }

        private void FirstLoad()
        {
            _attacker.FirstLoad();
            if (_defender != null) _defender.FirstLoad();

            foreach (Heroes.Core.Battle.Characters.ICharacter c in this._activeCharacters)
            {
                c.FirstLoad();
            }

            foreach (Heroes.Core.Battle.Characters.Spells.Spell c in this._spells.Values)
            {
                c.FirstLoad();
            }
        }

        private void ProcessAction()
        {
            if (_actions.Count < 1) return;
            
            Action action = (Action)_actions[0];

            // skip if last action already end
            if (_actions.Count == 1 && action._isEnd)
            {
                _actions.Remove(action);
                ClearSpells();
                return;
            }

            action.Run();

            if (action._isEnd)
            {
                // end of last action
                if (_actions.Count == 1)
                {
                    // set standing for each hero and army
                    action.SetDefaultAnimation();

                    _disableControl = false;
                    _battleTerrain.ClearHover();
                    
                    // check victory first before next turn
                    CheckVictory();

                    // cast spell not next turn
                    if (action._actionType != ActionTypeEnum.Spell)
                        _turn.NextTurn();
                }
                else
                {
                    // process next action
                    _actions.Remove(action);
                    ClearSpells();
                }
            }
        }

        private bool ProcessAi()
        {
            StandardCharacter currentCharacter = _turn._currentCharacter;

            if (_attacker._armyKSlots.ContainsValue(currentCharacter)) return false;

            if (_defender != null)
            {
                if (_defender._armyKSlots.ContainsValue(currentCharacter))
                {
                    if (!_defender._player._isComputer) return false;
                }
            }

            bool hasHandToHandPenalty = false;
            if (currentCharacter._hasRangeAttack)
            {
                HasHandToHandPenalty(currentCharacter);
            }

            // shoot if has range attack
            if (!hasHandToHandPenalty && currentCharacter._shotRemain > 0)
            {
                // find most kill, damage
                StandardCharacter mostKillCharacter = null;
                int mostKill = 0;
                int mostDamage = 0;

                foreach (StandardCharacter c in _attacker._armyKSlots.Values)
                {
                    if (c._isDead) continue;

                    bool hasRangePenalty = false;
                    bool hasObstaclePenalty = false;

                    HasRangeAttackPenalty(currentCharacter, c, out hasRangePenalty, out hasObstaclePenalty);

                    int minDamage = 0;
                    int maxDamage = 0;
                    int minKill = 0;
                    int maxKill = 0;
                    int avgKill = 0;

                    CalculateDamage(currentCharacter, c, true, hasRangePenalty, hasObstaclePenalty, hasHandToHandPenalty,
                        out minDamage, out maxDamage, out minKill, out maxKill, out avgKill);

                    if (mostKillCharacter == null)
                    {
                        mostKillCharacter = c;
                        mostKill = maxKill;
                        mostDamage = maxDamage;
                    }
                    else if (maxKill > mostKill
                        || (maxKill == mostKill && maxDamage > mostDamage))
                    {
                        mostKillCharacter = c;
                        mostKill = maxKill;
                        mostDamage = maxDamage;
                    }
                }

                // attack
                {
                    // reset defend and wait
                    Heroes.Core.Battle.Characters.Armies.Army army
                        = (Heroes.Core.Battle.Characters.Armies.Army)currentCharacter;
                    army._isWait = false;
                    army._isDefend = false;

                    // get attackDirection
                    CellPartEnum attackDirection
                        = BattleTerrain.FindDirection(currentCharacter._cell.GetCenterPoint(), mostKillCharacter._cell.GetCenterPoint());

                    SetRangeAttackNAnimate(currentCharacter, mostKillCharacter, attackDirection, mostKillCharacter._cell);

                    //// damage
                    //SetAttackDamage(currentCharacter, mostKillCharacter, true);
                    //currentCharacter._shotRemain -= 1;

                    //// set action
                    //{
                    //    ArrayList targets = new ArrayList();
                    //    targets.Add(mostKillCharacter);

                    //    Action action = Action.CreateShootAction(currentCharacter, attackDirection, targets, mostKillCharacter._cell);
                    //    _actions.Add(action);
                    //}
                }
            }
            else
            {
                // find enermies
                ArrayList enermies = new ArrayList();
                ArrayList enermyPaths = new ArrayList();
                ArrayList inRangeEnermies = new ArrayList();
                ArrayList inRangePaths = new ArrayList();
                foreach (StandardCharacter c in _attacker._armyKSlots.Values)
                {
                    if (c._isDead) continue;

                    // get range
                    ArrayList path = null;
                    if (!_battleTerrain.FindPathAdjBest(currentCharacter._cell, c._cell, true, false, out path))
                    {
                        // enermy cannot reach
                        continue;
                    }

                    if (path == null) continue;

                    enermies.Add(c);
                    enermyPaths.Add(path);

                    if (currentCharacter._speed >= path.Count - 1)
                    {
                        inRangeEnermies.Add(c);
                        inRangePaths.Add(path);
                    }
                    else
                        continue;
                }

                // no more enermy
                if (enermies.Count < 1) return false;

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

                        foreach (StandardCharacter c in inRangeEnermies)
                        {
                            int minDamage = 0;
                            int maxDamage = 0;
                            int minKill = 0;
                            int maxKill = 0;
                            int avgKill = 0;

                            CalculateDamage(currentCharacter, c, false, false, false, hasHandToHandPenalty,
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
                        StandardCharacter mostKillCharacter = (StandardCharacter)inRangeEnermies[mostKillIndex];

                        ArrayList path = (ArrayList)inRangePaths[mostKillIndex];

                        // get attackDirection
                        CellPartEnum attackDirection = CellPartEnum.Center;
                        {
                            Cell cellFrom = null;
                            Cell cellTo = (Cell)path[path.Count - 1];

                            if (path.Count < 2)
                                cellFrom = currentCharacter._cell;
                            else
                                cellFrom = (Cell)path[path.Count - 2];

                            attackDirection = BattleTerrain.FindDirection(cellFrom.GetCenterPoint(), cellTo.GetCenterPoint());
                        }

                        // remove last cell because last cell is enermy's cell
                        if (path.Count > 0)
                            path.RemoveAt(path.Count - 1);

                        // damage
                        SetAttackNAnimate(currentCharacter, mostKillCharacter, attackDirection, path);

                        // set attacker to destination cell
                        if (path.Count > 0)
                            SetDestCell(currentCharacter, (Cell)path[path.Count - 1]);
                        else
                            SetDestCell(currentCharacter, currentCharacter._cell);
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
                        while (path2.Count > currentCharacter._speed)
                        {
                            path2.RemoveAt(path2.Count - 1);
                        }

                        // reset defend and wait
                        Heroes.Core.Battle.Characters.Armies.Army army
                            = (Heroes.Core.Battle.Characters.Armies.Army)currentCharacter;
                        army._isWait = false;
                        army._isDefend = false;

                        Action action = Action.CreateMoveAction(currentCharacter, path2);
                        _actions.Add(action);

                        SetDestCell(currentCharacter, (Cell)path2[path2.Count - 1]);
                    }
                }
            }

            return true;
        }

        private void ProcessCharacter(StandardCharacter c)
        {
            // set currentAnimationSeq._isEnd = true;
            CommandResult result = c.Command.Execute(c);

            // just runs the animation
            c.CurrentAnimationRunner.Run(TurnTimeSpan);
        }

        private void ProcessHero(Heroes.Core.Battle.Characters.Hero hero)
        {
            if (hero.CurrentAnimationRunner._isEnd)
                hero._currentAnimationSeq._isEnd = true;

            hero.CurrentAnimationRunner.Run(TurnTimeSpan);
        }

        private void ProcessSpell(Heroes.Core.Battle.Characters.Spells.Spell c)
        {
            if (c.CurrentAnimationRunner == null) return;

            // set currentAnimationSeq._isEnd = true;
            CommandResult result = c._command.Execute(c);

            c.CurrentAnimationRunner.Run(TurnTimeSpan);
        }

        private void ClearSpells()
        {
            foreach (Heroes.Core.Battle.Characters.Spells.Spell spell in _spellActions)
            {
                spell._currentAnimationSeq = null;
                spell._currentAnimation = null;
                spell._currentAnimationRunner = null;
            }

            _spellActions.Clear();
        }

        private void CheckVictory()
        {
            bool hasArmy = false;
            if (_attacker != null)
            {
                foreach (StandardCharacter c in _attacker._armyKSlots.Values)
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
                    if (_defender != null)
                        EndBattle(_attacker, _defender, _monster, null, BattleSideEnum.Defender);
                    else if (_monster != null)
                        EndBattle(_attacker, _defender, _monster, null, BattleSideEnum.Defender);
                }
            }

            hasArmy = false;
            if (_defender != null)
            {
                foreach (StandardCharacter c in _defender._armyKSlots.Values)
                {
                    if (c._qtyLeft > 0)
                    {
                        hasArmy = true;
                        break;
                    }
                }

                if (!hasArmy)
                {
                    EndBattle(_attacker, _defender, _monster, null, BattleSideEnum.Attacker);
                }
            }

            hasArmy = false;
            if (_monster != null)
            {
                foreach (StandardCharacter c in _monster._armyKSlots.Values)
                {
                    if (c._qtyLeft > 0)
                    {
                        hasArmy = true;
                        break;
                    }
                }

                if (!hasArmy)
                {
                    EndBattle(_attacker, _defender, _monster, null, BattleSideEnum.Attacker);
                }
            }
        }

        private void EndBattle(Heroes.Core.Battle.Characters.Hero attackHero, Heroes.Core.Battle.Characters.Hero defendHero,
            Heroes.Core.Battle.Characters.Monster defendMonster, Heroes.Core.Town defendCastle,
            BattleSideEnum victory)
        {
            SetQtyLeft(attackHero);

            if (defendHero != null)
            {
                SetQtyLeft(defendHero);
            }

            if (defendMonster != null)
            {
                SetQtyLeft(defendMonster);
            }

            // raise victory event
            BattleEndedEventArg eventArg = new BattleEndedEventArg(victory);
            OnBattleEnded(eventArg);
        }
        
        private void SetQtyLeft(Heroes.Core.Battle.Characters.Hero hero)
        {
            foreach (Heroes.Core.Army army in hero._armyKSlots.Values)
            {
                if (army._qtyLeft <= 0)
                    hero._originalHero._armyKSlots.Remove(army._slotNo);
                else
                {
                    Heroes.Core.Army originalArmy = (Heroes.Core.Army)hero._originalHero._armyKSlots[army._slotNo];
                    originalArmy._qty = army._qtyLeft;
                }
            }
        }

        private void SetQtyLeft(Heroes.Core.Battle.Characters.Monster monster)
        {
            foreach (Heroes.Core.Army army in monster._armyKSlots.Values)
            {
                if (army._qtyLeft <= 0)
                    monster._originalMonster._armyKSlots.Remove(army._slotNo);
                else
                {
                    Heroes.Core.Army originalArmy = (Heroes.Core.Army)monster._originalMonster._armyKSlots[army._slotNo];
                    originalArmy._qty = army._qtyLeft;
                }
            }
        }
        #endregion

        public void ProcessMouseMove(int x, int y, Form target)
        {
            target.Cursor = Cursors.Default;

            _statusMsg = "";

            Cell cell = _battleTerrain.FindCell(x, y);
            if (cell == null)
            {
                return;
            }

            if (cell._character == null)
            {
                return;
            }

            // dead
            if (cell._character._isDead)
            {
                return;
            }

            // self
            if (_turn._currentCharacter._cell.Equals(cell))
            {
                target.Cursor = Cursors.Help;
                return;
            }

            // ally
            if (_turn._currentCharacter._playerId == cell._character._playerId)
            {
                target.Cursor = Cursors.Help;
                return;
            }

            // attack
            if (cell._character._playerId != _turn._currentCharacter._playerId)
            {
                if (CanRangeAttack(_turn._currentCharacter))
                {
                    // range attack

                    bool hasRangePenalty = false;
                    bool hasObstaclePenalty = false;

                    HasRangeAttackPenalty(_turn._currentCharacter, (StandardCharacter)cell._character, 
                        out hasRangePenalty, out hasObstaclePenalty);

                    if (hasRangePenalty || hasObstaclePenalty)
                        target.Cursor = Cursors.SizeAll;
                    else
                        target.Cursor = Cursors.Cross;

                    CalculateDamage(_turn._currentCharacter, (StandardCharacter)cell._character, true, hasRangePenalty, hasObstaclePenalty, false);
                }
                else
                {
                    // melee attack

                    CellPartEnum cellPart = _battleTerrain.FindCellPart(x, y, cell);

                    if (cellPart == CellPartEnum.Center)
                    {
                        target.Cursor = Cursors.Help;
                    }
                    else
                    {
                        Cell cell2 = cell._adjacentCells[cellPart];

                        switch (cellPart)
                        {
                            case CellPartEnum.UpperLeft:
                                {
                                    if (cell2 == null 
                                        || (cell2._character != null && !cell2._character.Equals(_turn._currentCharacter)))
                                        break;
                                    else
                                        target.Cursor = Cursors.PanSE;
                                }
                                break;
                            case CellPartEnum.UpperRight:
                                {
                                    if (cell2 == null 
                                        || (cell2._character != null && !cell2._character.Equals(_turn._currentCharacter)))
                                        break;
                                    else
                                        target.Cursor = Cursors.PanSW;
                                }
                                break;
                            case CellPartEnum.CenterLeft:
                                {
                                    if (cell2 == null 
                                        || (cell2._character != null && !cell2._character.Equals(_turn._currentCharacter)))
                                        break;
                                    else
                                        target.Cursor = Cursors.PanEast;
                                        
                                }
                                break;
                            case CellPartEnum.CenterRight:
                                {
                                    if (cell2 == null 
                                        || (cell2._character != null && !cell2._character.Equals(_turn._currentCharacter)))
                                        break;
                                    else
                                        target.Cursor = Cursors.PanWest;
                                }
                                break;
                            case CellPartEnum.LowerLeft:
                                {
                                    if (cell2 == null 
                                        || (cell2._character != null && !cell2._character.Equals(_turn._currentCharacter)))
                                        break;
                                    else
                                        target.Cursor = Cursors.PanNE;
                                }
                                break;
                            case CellPartEnum.LowerRight:
                                {
                                    if (cell2 == null 
                                        || (cell2._character != null && !cell2._character.Equals(_turn._currentCharacter)))
                                        break;
                                    else
                                        target.Cursor = Cursors.PanNW;
                                }
                                break;
                        }

                        bool hasDamagePenalty = HasHandToHandPenalty(_turn._currentCharacter);

                        CalculateDamage(_turn._currentCharacter, (StandardCharacter)cell._character, false, false, false, hasDamagePenalty);
                    }
                }
            }
        }

        public void ProcessMouseClick(int x, int y, MouseButtons buttons, bool isDoubleClick, CommandTypeEnum cmdType,
            Heroes.Core.Spell currentSpell)
        {
            if (buttons == MouseButtons.Left)
            {
                if (isDoubleClick)
                {
                    #region Mouse Double Click
                    if (_disableControl) return;

                    Cell cell = _battleTerrain.FindCell(x, y);
                    if (cell == null) return;

                    if (cmdType == CommandTypeEnum.Move)
                    {
                        // move
                        if (cell.Equals(_turn._currentCharacter._cell)) return;     // cannot move to self
                        if (!cell.HasPassability()) return;    // cannot move to cell if has life character

                        // get path
                        ArrayList path = new ArrayList();
                        _battleTerrain.FindPathAdjBest(_turn._currentCharacter._cell, cell, false, false, out path);

                        // cannot move if destination is more than movement range
                        if (path.Count > _turn._currentCharacter._speed) return;

                        // set flag to disable control
                        _disableControl = true;

                        // reset defend and wait
                        Heroes.Core.Battle.Characters.Armies.Army army 
                            = (Heroes.Core.Battle.Characters.Armies.Army)_turn._currentCharacter;
                        army._isWait = false;
                        army._isDefend = false;

                        // set movement animation
                        Action action = Action.CreateMoveAction(_turn._currentCharacter, path);
                        _actions.Add(action);

                        // set dest cell
                        SetDestCell(_turn._currentCharacter, cell);
                    }
                    else if (cmdType == CommandTypeEnum.AttackLeft
                        || cmdType == CommandTypeEnum.AttackRight
                        || cmdType == CommandTypeEnum.AttackLowerLeft
                        || cmdType == CommandTypeEnum.AttackLowerRight
                        || cmdType == CommandTypeEnum.AttackUpperLeft
                        || cmdType == CommandTypeEnum.AttackUpperRight)
                    {
                        // attack
                        if (cell.Equals(_turn._currentCharacter._cell)) return;     // cannot attack self
                        if (cell._character == null) return;    // cannot attack if don't has character
                        if (cell._character._isDead) return;    // cannot attack dead character

                        StandardCharacter targetCharacter = (StandardCharacter)cell._character;

                        // get next cell
                        {
                            CellPartEnum direction = GetAttackCellDirection(cmdType);

                            cell = _battleTerrain.GetNextCell(cell, direction);
                            if (cell == null) return;
                        }

                        // get path
                        ArrayList path = new ArrayList();
                        _battleTerrain.FindPathAdjBest(_turn._currentCharacter._cell, cell, false, false, out path);
                        // cannot move if destination is more than movement range
                        if (path != null && path.Count > _turn._currentCharacter._speed) return;

                        // set flag to disable control
                        _disableControl = true;

                        // commit damage and do animation
                        CellPartEnum attackDirection = GetAttackCommandDirection(cmdType);
                        SetAttackNAnimate(_turn._currentCharacter, targetCharacter, attackDirection, path);
                        
                        // set dest cell
                        SetDestCell(_turn._currentCharacter, cell);
                    }
                    else if (cmdType == CommandTypeEnum.RangeAttack)
                    {
                        // range attack
                        if (cell._character == null) return;    // cannot attack if don't has character
                        if (cell._character._isDead) return;    // cannot attack dead character

                        StandardCharacter targetCharacter = (StandardCharacter)cell._character;

                        // set flag to disable control
                        _disableControl = true;

                        CellPartEnum attackDirection = BattleTerrain.FindDirection(_turn._currentCharacter._cell.GetCenterPoint(),
                            cell._character._cell.GetCenterPoint());

                        SetRangeAttackNAnimate(_turn._currentCharacter, targetCharacter, attackDirection, cell);
                    }
                    else if (cmdType == CommandTypeEnum.Spell)
                    {
                        StandardCharacter targetCharacter = (StandardCharacter)cell._character;

                        Heroes.Core.Battle.Characters.Hero hero = FindHero(_turn._currentCharacter._heroId);
                        if (hero == null) return;
                        if (!hero._canCastSpell) return;

                        // set spell
                        hero._currentSpell = currentSpell;

                        if (hero._spellPointLeft < hero._currentSpell._cost)
                        {
                            // no enough spell points
                            return;
                        }

                        if (hero._currentSpell._targetType == SpellTargetTypeEnum.Enermy)
                        {
                            // cannot damage ally
                            if (targetCharacter._playerId == hero._playerId) return;
                        }
                        else if (hero._currentSpell._targetType == SpellTargetTypeEnum.Ally)
                        {
                            // cannot cast on enermy
                            if (targetCharacter._playerId != hero._playerId) return;
                        }

                        // reduce sp
                        hero._spellPointLeft -= hero._currentSpell._cost;
                        hero._originalHero._spellPointLeft = hero._spellPointLeft;

                        // can only cast once per round
                        hero._canCastSpell = false;

                        // set damage
                        if (hero._currentSpell._isDamage)
                        {
                            SetSpellDamage(hero._currentSpell, targetCharacter);
                        }
                        else
                        {
                            SetSpellEffect(hero._currentSpell, targetCharacter);
                        }

                        // set movement animation
                        {
                            ArrayList targets = new ArrayList();
                            targets.Add(cell._character);

                            Heroes.Core.Battle.Characters.Spells.Spell spell =
                                (Heroes.Core.Battle.Characters.Spells.Spell)_spells[hero._currentSpell._id];
                            spell._cell = _battleTerrain._cells[0, 0];
                            spell._destCell = cell;
                            spell._command = _inputCommand;
                            _spellActions.Add(spell);

                            Action action = Action.CreateCastSpellAction(hero, spell,
                                CellPartEnum.CenterRight, targets, cell);
                            _actions.Add(action);
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Mouse Click
                    _battleTerrain.ClearHover();

                    Cell cell = _battleTerrain.FindCell(x, y);
                    if (cell == null) return;
                    if (cell.Equals(_turn._currentCharacter._cell)) return;     // click on self

                    if (cmdType == CommandTypeEnum.Move)
                    {
                        // move
                        if (_disableControl) return;
                        if (!cell.HasPassability()) return;    // cannot move to cell if has character

                        // get path
                        ArrayList path = new ArrayList();
                        _battleTerrain.FindPathAdjBest(_turn._currentCharacter._cell, cell, false, false, out path);
                        //_battleTerrain.FindPath(_turn._currentCharacter._cell, cell, path);

                        // Show path
                        if (path != null)
                        {
                            foreach (Cell cellPath in path)
                            {
                                cellPath._isHover = true;
                            }
                        }
                    }
                    else if (cmdType == CommandTypeEnum.AttackLeft
                        || cmdType == CommandTypeEnum.AttackRight
                        || cmdType == CommandTypeEnum.AttackLowerLeft
                        || cmdType == CommandTypeEnum.AttackLowerRight
                        || cmdType == CommandTypeEnum.AttackUpperLeft
                        || cmdType == CommandTypeEnum.AttackUpperRight)
                    {
                        // attack
                        if (_disableControl) return;
                        if (cell._character == null) return;    // cannot move to cell if has character
                        if (cell._character._isDead) return;    // cannot attack dead character

                        StandardCharacter targetCharacter = (StandardCharacter)cell._character;

                        // get next cell
                        {
                            CellPartEnum direction = GetAttackCellDirection(cmdType);

                            cell = _battleTerrain.GetNextCell(cell, direction);
                            if (cell == null) return;
                        }

                        // get path
                        ArrayList path = new ArrayList();
                        _battleTerrain.FindPathAdjBest(_turn._currentCharacter._cell, cell, false, false, out path);

                        // Show path
                        if (path != null)
                        {
                            foreach (Cell cellPath in path)
                            {
                                cellPath._isHover = true;
                            }
                        }
                    }
                    #endregion
                }
            }
            else if (buttons == MouseButtons.Right)
            {
            }
        }

        public Heroes.Core.Battle.Characters.Armies.Army GetArmy(int x, int y)
        {
            Cell cell = _battleTerrain.FindCell(x, y);
            if (cell == null) return null;
            if (cell._character == null) return null;

            return (Heroes.Core.Battle.Characters.Armies.Army)cell._character;
        }

        private bool CanRangeAttack(StandardCharacter c)
        {
            if (c._hasRangeAttack && c._shotRemain > 0)
            {
                // check surrounding cell has enermy or not
                foreach (Cell cell2 in c._cell._adjacentCells.Values)
                {
                    if (cell2 != null
                        && cell2._character != null
                        && !cell2._character._isDead
                        && cell2._character._playerId != _turn._currentCharacter._playerId)
                    {
                        // has enermy, no range attack
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private void HasRangeAttackPenalty(StandardCharacter c, StandardCharacter target,
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
            _battleTerrain.FindPath(c._cell, target._cell, path, true, true);
            //_battleTerrain.FindPath(c._cell, target._cell, path, true, true, false);

            if (path.Count > 10) hasRangePenalty = true;
        }

        private bool HasHandToHandPenalty(StandardCharacter c)
        {
            if (c._handToHandPenalty)
            {
                if (HasAdjacentEnermy(c)) return true;
            }

            return false;
        }

        private bool HasAdjacentEnermy(StandardCharacter c)
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

        private void SetDestCell(StandardCharacter c, Cell cell)
        {
            c._cell._character = null;
            c._cell = cell;
            c._cell._character = c;
        }

        #region Calculate Damage, Commit Damage
        private void SetKill(StandardCharacter c, StandardCharacter target)
        {
            target._qtyLeft -= c._qtyLeft;
            if (target._qtyLeft <= 0)
            {
                target._qtyLeft = 0;
                target._isDead = true;
            }
        }

        private int CalculateKill(StandardCharacter c, StandardCharacter target)
        {
            int kill = 0;
            if (c._qtyLeft >= target._qtyLeft)
                kill = target._qtyLeft;
            else
                kill = c._qtyLeft;

            return kill;
        }

        private void SetAttackDamage(StandardCharacter c, StandardCharacter target, bool isRangeAttack)
        {
            bool hasRangePenalty = false;
            bool hasObstaclePenalty = false;
            HasRangeAttackPenalty(c, target, out hasRangePenalty, out hasObstaclePenalty);

            bool hasHandToHandPenalty = HasHandToHandPenalty(c);

            int minDamage = 0;
            int maxDamage = 0;
            int minKill = 0;
            int maxKill = 0;
            int avgKill = 0;

            CalculateDamage(c, target, isRangeAttack, hasRangePenalty, hasObstaclePenalty, hasHandToHandPenalty,
                out minDamage, out maxDamage, out minKill, out maxKill, out avgKill);

            // random from minDamage to maxDamage
            //Random rnd = new Random();
            //int rndDamage = rnd.Next(minDamage, maxDamage);
            int rndDamage = (minDamage + maxDamage) / 2;        // use average damage because rnd Damage need to inform other player

            SetDamage(rndDamage, target);
        }

        private void SetAttackNAnimate(StandardCharacter attacker, StandardCharacter defender,
            CellPartEnum attackDirection, ArrayList path)
        {
            // reset defend and wait
            Heroes.Core.Battle.Characters.Armies.Army army
                = (Heroes.Core.Battle.Characters.Armies.Army)attacker;
            army._isWait = false;
            army._isDefend = false;

            // attack
            SetAttackDamage(attacker, defender, false);

            // attack Action
            {
                ArrayList targets = new ArrayList();
                targets.Add(defender);

                Action action = Action.CreateAttackAction(attacker, attackDirection, path, targets);
                _actions.Add(action);
            }

            if (!defender._isDead)
            {
                // retaliate
                if (defender._retaliateRemain > 0)
                {
                    SetAttackDamage(defender, attacker, false);
                    defender._retaliateRemain -= 1;

                    // retaliate action
                    {
                        CellPartEnum oppAttackDirection = BattleTerrain.GetOppositeDirection(attackDirection);

                        ArrayList targets = new ArrayList();
                        targets.Add(attacker);

                        Action action = Action.CreateAttackAction(defender, oppAttackDirection, null, targets);
                        _actions.Add(action);
                    }
                }

                // attack more
                {
                    ArrayList targets = new ArrayList();
                    targets.Add(defender);

                    for (int i = 1; i < attacker._noOfAttack; i++)
                    {
                        SetAttackDamage(attacker, defender, false);

                        // attack more action
                        {
                            Action action = Action.CreateAttackAction(attacker, attackDirection, null, targets);
                            _actions.Add(action);
                        }
                    }
                }
            }
        }

        private void SetRangeAttackNAnimate(StandardCharacter attacker, StandardCharacter defender,
            CellPartEnum attackDirection, Cell targetCell)
        {
            // reset defend and wait
            Heroes.Core.Battle.Characters.Armies.Army army
                = (Heroes.Core.Battle.Characters.Armies.Army)attacker;
            army._isWait = false;
            army._isDefend = false;

            // attack
            SetAttackDamage(attacker, defender, true);
            attacker._shotRemain -= 1;

            // attack Action
            {
                ArrayList targets = new ArrayList();
                targets.Add(defender);

                Action action = Action.CreateShootAction(attacker, attackDirection, targets, targetCell);
                _actions.Add(action);
            }

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
                        SetAttackDamage(attacker, defender, true);
                        attacker._shotRemain -= 1;

                        // attack more action
                        {
                            Action action = Action.CreateShootAction(attacker, attackDirection, targets, targetCell);
                            _actions.Add(action);
                        }
                    }
                }
            }
        }

        public void CalculateDamage(StandardCharacter attacker, StandardCharacter defender, bool isRangeAttack,
            bool hasRangePenalty, bool hasObstaclePenalty, bool hasHandToHandPenalty,
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
                Hero hero = FindHero(attacker._heroId);
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
                Hero hero = FindHero(defender._heroId);
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
            Heroes.Core.Battle.Characters.Armies.Army defendArmy = (Heroes.Core.Battle.Characters.Armies.Army)defender;
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

        public void CalculateDamage(StandardCharacter attacker, StandardCharacter defender, bool isRangeAttack,
            bool hasRangePenalty, bool hasObstaclePenalty, bool hasHandToHandPenalty)
        {
            int minDmg = 0;
            int maxDmg = 0;
            int minKill = 0;
            int maxKill = 0;
            int avgKill = 0;

            CalculateDamage(attacker, defender, isRangeAttack, hasRangePenalty, hasObstaclePenalty, hasHandToHandPenalty,
                out minDmg, out maxDmg, out minKill, out maxKill, out avgKill);

            DamageCalculatedEventArg e2 = new DamageCalculatedEventArg(minDmg, maxDmg, minKill, maxKill, avgKill);
            OnDamageCalculated(e2);

            _statusMsg = string.Format("Damage {0}-{1}, Kill {2}-{3}, Avg Kill {4}",
                e2._minDamage, e2._maxDamage, e2._minKill, e2._maxKill, e2._avgKill);
        }

        private void SetDamage(int damage, StandardCharacter target)
        {
            // defender's health
            int totalHealthRemain = target._healthRemain + (target._qtyLeft - 1) * target._health;
            totalHealthRemain -= damage;

            if (totalHealthRemain <= 0)
            {
                target._qtyLeft = 0;
                target._healthRemain = 0;
                target._isDead = true;
            }
            else
            {
                decimal a = (decimal)totalHealthRemain / (decimal)target._health;
                decimal qtyRemain = decimal.Truncate(a);
                decimal healthRemain2 = decimal.Truncate((a - qtyRemain) * (decimal)target._health);

                target._qtyLeft = (int)(qtyRemain + 1);     // extra 1 is not full health
                target._healthRemain = (int)healthRemain2;  // remaining health
                if (target._healthRemain == 0)
                {
                    target._qtyLeft -= 1;    // reduce 1 if no health
                    target._healthRemain = target._health;  // full health
                }
            }
        }
        #endregion

        #region Spell
        private void SetSpellDamage(Heroes.Core.Spell spell, StandardCharacter target)
        {
            SetDamage(spell._damage, target);
        }

        private void SetSpellEffect(Heroes.Core.Spell spell, StandardCharacter target)
        { 
        }
        #endregion

        #region Action Animation
        private void CreateAttackActions(CellPartEnum attackDirection, StandardCharacter attacker, 
            StandardCharacter defender, ArrayList path, bool hasRetaliate)
        {
            // attack
            {
                ArrayList targets = new ArrayList();
                targets.Add(defender);

                Action action = Action.CreateAttackAction(attacker, attackDirection, path, targets);
                _actions.Add(action);
            }

            // retaliate
            if (hasRetaliate)
            {
                CellPartEnum oppAttackDirection = BattleTerrain.GetOppositeDirection(attackDirection);

                ArrayList targets = new ArrayList();
                targets.Add(attacker);

                Action action = Action.CreateAttackAction(defender, oppAttackDirection, null, targets);
                _actions.Add(action);
            }

            // attack more than once
            {
                ArrayList targets = new ArrayList();
                targets.Add(defender);

                for (int i = 1; i < attacker._noOfAttack; i++)
                {
                    Action action = Action.CreateAttackAction(attacker, attackDirection, null, targets);
                    _actions.Add(action);
                }
            }
        }
        #endregion

        public static CommandTypeEnum GetCommandType(Cursor cursor)
        {
            if (cursor == Cursors.Default)
                return CommandTypeEnum.Move;
            else if (cursor == Cursors.PanEast)
                return CommandTypeEnum.AttackRight;
            else if (cursor == Cursors.PanWest)
                return CommandTypeEnum.AttackLeft;
            else if (cursor == Cursors.PanNE)
                return CommandTypeEnum.AttackUpperRight;
            else if (cursor == Cursors.PanNW)
                return CommandTypeEnum.AttackUpperLeft;
            else if (cursor == Cursors.PanSE)
                return CommandTypeEnum.AttackLowerRight;
            else if (cursor == Cursors.PanSW)
                return CommandTypeEnum.AttackLowerLeft;
            else if (cursor == Cursors.SizeAll
                || cursor == Cursors.Cross)
                return CommandTypeEnum.RangeAttack;
            else if (cursor == Cursors.Hand)
                return CommandTypeEnum.Spell;
            else
                return CommandTypeEnum.None;
        }

        private CellPartEnum GetAttackCellDirection(CommandTypeEnum cmdType)
        {
            CellPartEnum direction = CellPartEnum.Center;
            {
                if (cmdType == CommandTypeEnum.AttackLeft)
                    direction = CellPartEnum.CenterRight;
                else if (cmdType == CommandTypeEnum.AttackRight)
                    direction = CellPartEnum.CenterLeft;
                else if (cmdType == CommandTypeEnum.AttackLowerLeft)
                    direction = CellPartEnum.UpperRight;
                else if (cmdType == CommandTypeEnum.AttackLowerRight)
                    direction = CellPartEnum.UpperLeft;
                else if (cmdType == CommandTypeEnum.AttackUpperLeft)
                    direction = CellPartEnum.LowerRight;
                else if (cmdType == CommandTypeEnum.AttackUpperRight)
                    direction = CellPartEnum.LowerLeft;
            }

            return direction;
        }

        private CellPartEnum GetAttackCommandDirection(CommandTypeEnum cmdType)
        {
            CellPartEnum direction = CellPartEnum.Center;
            {
                if (cmdType == CommandTypeEnum.AttackLeft)
                    direction = CellPartEnum.CenterLeft;
                else if (cmdType == CommandTypeEnum.AttackRight)
                    direction = CellPartEnum.CenterRight;
                else if (cmdType == CommandTypeEnum.AttackLowerLeft)
                    direction = CellPartEnum.LowerLeft;
                else if (cmdType == CommandTypeEnum.AttackLowerRight)
                    direction = CellPartEnum.LowerRight;
                else if (cmdType == CommandTypeEnum.AttackUpperLeft)
                    direction = CellPartEnum.UpperLeft;
                else if (cmdType == CommandTypeEnum.AttackUpperRight)
                    direction = CellPartEnum.UpperRight;
            }

            return direction;
        }

        #region Sort Army
        public static void SortBySpeed(ArrayList stdCharacters)
        {
            IComparer myComparer = new SortBySpeedComparer();
            stdCharacters.Sort(myComparer);
        }

        public static void SortByCell(ArrayList stdCharacters)
        {
            IComparer myComparer = new SortByCellComparer();
            stdCharacters.Sort(myComparer);
        }
        #endregion

        private Heroes.Core.Battle.Characters.Hero FindHero(int heroId)
        {
            if (_attacker._id == heroId) return _attacker;
            else if (_defender != null && _defender._id == heroId) return _defender;
            else return null;
        }

        private Heroes.Core.Army FindArmy(int heroId, int slotNo)
        {
            Heroes.Core.Battle.Characters.Hero hero = FindHero(heroId);
            if (hero == null) return null;

            if (hero._armyKSlots.ContainsKey(slotNo))
                return (Heroes.Core.Army)hero._armyKSlots[slotNo];
            else
                return null;
        }

    }

    public enum CommandTypeEnum
    {
        None = 0,
        Move,
        AttackRight,
        AttackLeft,
        AttackLowerRight,
        AttackLowerLeft,
        AttackUpperRight,
        AttackUpperLeft,
        RangeAttack,
        Spell,
        Defend,
        Wait
    }

    public enum BattleSideEnum
    { 
        None = 0,
        Attacker = 1,
        Defender = 2
    }

    public class SortBySpeedComparer : IComparer
    {
        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer.Compare(Object x, Object y)
        {
            Heroes.Core.Battle.Armies.Army obj1 = (Heroes.Core.Battle.Armies.Army)x;
            Heroes.Core.Battle.Armies.Army obj2 = (Heroes.Core.Battle.Armies.Army)y;

            int i = obj2._speed - obj1._speed;
            if (i != 0) return i;

            // favour to attacker
            return (int)obj1._armySide - (int)obj2._armySide;
        }
    }

    public class SortByCellComparer : IComparer
    {
        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer.Compare(Object x, Object y)
        {
            StandardCharacter obj1 = (StandardCharacter)x;
            StandardCharacter obj2 = (StandardCharacter)y;

            return obj1._cell._row - obj2._cell._row;
        }
    }

}
