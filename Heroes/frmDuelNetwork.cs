using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes
{
    public partial class frmDuelNetwork : Form
    {
        #region Events
        public delegate void GameStartedEventHandler();
        public delegate void NextTurnClickEventHandler();
        public delegate void StartingBattleEventHandler(StartingBattleEventArg e);
        public delegate void GettingArtifactEventHandler(GettingArtifactEventArg e);

        public event GameStartedEventHandler GameStarted;
        public event NextTurnClickEventHandler NextTurnClick;
        public event StartingBattleEventHandler StartingBattle;
        public event GettingArtifactEventHandler GettingArtifact;

        #region Event Arguments
        public class StartingBattleEventArg
        {
            public Heroes.Core.Hero _attackHero;
            public Heroes.Core.Hero _defendHero;
            public Heroes.Core.Monster _monster;
            public bool _success;

            public StartingBattleEventArg(Heroes.Core.Hero attackHero, Heroes.Core.Hero defendHero, Heroes.Core.Monster monster)
            {
                _attackHero = attackHero;
                _defendHero = defendHero;
                _monster = monster;
                _success = false;
            }
        }

        public class GettingArtifactEventArg
        {
            public Heroes.Core.Heros.ArtifactLevelEnum _level;
            public Heroes.Core.Heros.Artifact _artifact;

            public GettingArtifactEventArg(Heroes.Core.Heros.ArtifactLevelEnum level)
            {
                _level = level;
                _artifact = null;
            }
        }
        #endregion

        protected virtual void OnGameStarted()
        {
            if (GameStarted != null)
            {
                //Invokes the delegates.
                GameStarted();
            }
        }

        protected virtual void OnNextTurnClick()
        {
            if (NextTurnClick != null)
            {
                //Invokes the delegates.
                NextTurnClick();
            }
        }

        protected virtual void OnStartingBattle(StartingBattleEventArg e)
        {
            if (StartingBattle != null)
            {
                //Invokes the delegates.
                StartingBattle(e);
            }
        }

        protected virtual void OnGettingArtifact(GettingArtifactEventArg e)
        {
            if (GettingArtifact != null)
            {
                //Invokes the delegates.
                GettingArtifact(e);
            }
        }
        #endregion

        Hashtable _playerKIds;
        Heroes.Core.Player _currentPlayer;
        public Heroes.Core.Hero _currentHero;

        bool _readOnly;

        bool _isFirstRun;
        bool _hasVisitKnowStone;

        int _day;
        int _week;
        int _month;

        int[] _minQtys = new int[] { 1, 5, 10, 20, 50, 100, 250, 500, 1000, 5000, 10000, 50000 };
        int[] _maxQtys = new int[] { 4, 9, 19, 49, 99, 249, 499, 999, 4999, 9999, 49999, 99999 };

        int[] _artifactVisitCounts;

        public frmDuelNetwork()
        {
            InitializeComponent();

            _playerKIds = new Hashtable();
            _currentPlayer = null;
            _currentHero = null;

            this.ReadOnly = true;

            _isFirstRun = true;
            _hasVisitKnowStone = false;

            _day = 1;
            _week = 1;
            _month = 1;

            this._artifactVisitCounts = new int [4];
        }

        private void frmDuelNetwork_Load(object sender, EventArgs e)
        {
            
        }

        public DialogResult ShowDialog(Heroes.Core.Player player, int startingHeroId, ArrayList playerIds)
        {
            _currentPlayer = player;
            InitPlayer(_currentPlayer, startingHeroId);
            SetCurrentHero();

            this.lblPlayerId.Text = _currentPlayer._id.ToString();

            cboOtherPlayerId.Items.Clear();
            foreach (int playerId in playerIds)
            {
                if (playerId == _currentPlayer._id) continue;

                cboOtherPlayerId.Items.Add(playerId);
            }
            if (cboOtherPlayerId.Items.Count > 0) cboOtherPlayerId.SelectedIndex = 0;

            // raise event
            OnGameStarted();

            return this.ShowDialog();
        }

        private void InitPlayer(Heroes.Core.Player player, int startingHeroId)
        {
            player._wood = 20;
            player._mercury = 10;
            player._ore = 20;
            player._sulfur = 10;
            player._crystal = 10;
            player._gem = 10;
            player._gold = 20000;

            //player._wood = 10;
            //player._mercury = 0;
            //player._ore = 10;
            //player._sulfur = 0;
            //player._crystal = 0;
            //player._gem = 0;
            //player._gold = 10000;

            Random rnd = new Random();

            // hero
            Heroes.Core.Hero hero = (Heroes.Core.Hero)Heroes.Core.Setting._heros[startingHeroId];
            hero._playerId = player._id;
            hero._player = player;
            hero._level = 1;
            hero._experience = rnd.Next(45, 99);
            //hero._movementPoint = 1;
            hero._movementPoint = 2;
            hero._movementPointLeft = hero._movementPoint;
            player._heroes.Add(hero);

            // army
            {
                Heroes.Core.Army army = new Heroes.Core.Army();
                army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Pikeman]);
                army._heroId = hero._id;
                army._playerId = player._id;
                army._qty = rnd.Next(20, 49);
                //army._qty = rnd.Next(500, 999);
                army._slotNo = 1;
                hero._armyKSlots.Add(army._slotNo, army);

                army = new Heroes.Core.Army();
                army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Archer]);
                army._heroId = hero._id;
                army._playerId = player._id;
                army._qty = rnd.Next(10, 19);
                //army._qty = rnd.Next(500, 999);
                army._slotNo = 0;
                hero._armyKSlots.Add(army._slotNo, army);

                //army = new Heroes.Core.Army();
                //army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.RoyalGiffin]);
                //army.AddAttribute(hero);
                //army._heroId = hero._id;
                //army._playerId = player._id;
                //army._qty = rnd.Next(10, 19);
                //army._slotNo = 4;
                //hero._armyKSlots.Add(army._slotNo, army);

                //army = new Heroes.Core.Army();
                //army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Crusader]);
                //army.AddAttribute(hero);
                //army._heroId = hero._id;
                //army._playerId = player._id;
                //army._qty = rnd.Next(10, 19);
                //army._slotNo = 5;
                //hero._armyKSlots.Add(army._slotNo, army);

                //army = new Heroes.Core.Army();
                //army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Champion]);
                //army.AddAttribute(hero);
                //army._heroId = hero._id;
                //army._playerId = player._id;
                //army._qty = rnd.Next(5, 9);
                //army._slotNo = 2;
                //hero._armyKSlots.Add(army._slotNo, army);

                //army = new Heroes.Core.Army();
                //army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Zealot]);
                //army.AddAttribute(hero);
                //army._heroId = hero._id;
                //army._playerId = player._id;
                //army._qty = rnd.Next(10, 19);
                //army._slotNo = 6;
                //hero._armyKSlots.Add(army._slotNo, army);

                //army = new Heroes.Core.Army();
                //army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Archangel]);
                //army.AddAttribute(hero);
                //army._heroId = hero._id;
                //army._playerId = player._id;
                //army._qty = rnd.Next(1, 4);
                //army._slotNo = 3;
                //hero._armyKSlots.Add(army._slotNo, army);
            }

            // spells
            {
                Heroes.Core.Spell spell = new Heroes.Core.Spell();
                spell.CopyFrom((Heroes.Core.Spell)Heroes.Core.Setting._spells[(int)Heroes.Core.SpellIdEnum.MagicArrow]);
                hero._spells.Add(spell._id, spell);

                spell = new Heroes.Core.Spell();
                spell.CopyFrom((Heroes.Core.Spell)Heroes.Core.Setting._spells[(int)Heroes.Core.SpellIdEnum.IceBolt]);
                hero._spells.Add(spell._id, spell);

                spell = new Heroes.Core.Spell();
                spell.CopyFrom((Heroes.Core.Spell)Heroes.Core.Setting._spells[(int)Heroes.Core.SpellIdEnum.MeteorShower]);
                hero._spells.Add(spell._id, spell);

                spell = new Heroes.Core.Spell();
                spell.CopyFrom((Heroes.Core.Spell)Heroes.Core.Setting._spells[(int)Heroes.Core.SpellIdEnum.LightningBolt]);
                hero._spells.Add(spell._id, spell);

                spell = new Heroes.Core.Spell();
                spell.CopyFrom((Heroes.Core.Spell)Heroes.Core.Setting._spells[(int)Heroes.Core.SpellIdEnum.Implosion]);
                hero._spells.Add(spell._id, spell);

                spell = new Heroes.Core.Spell();
                spell.CopyFrom((Heroes.Core.Spell)Heroes.Core.Setting._spells[(int)Heroes.Core.SpellIdEnum.FireBall]);
                hero._spells.Add(spell._id, spell);

                spell = new Heroes.Core.Spell();
                spell.CopyFrom((Heroes.Core.Spell)Heroes.Core.Setting._spells[(int)Heroes.Core.SpellIdEnum.Inferno]);
                hero._spells.Add(spell._id, spell);
            }

            // artifacts
            //for (int i = 0; i < 20; i++)
            {
                GettingArtifactEventArg e2 = new GettingArtifactEventArg(Heroes.Core.Heros.ArtifactLevelEnum.Treasure);
                OnGettingArtifact(e2);

                if (e2._artifact != null)
                {
                    hero.AddArtifacts(e2._artifact);
                }
            }

            hero.CalculateAll();
            hero._spellPointLeft = hero._maxSpellPoint;

            // castle
            {
                Heroes.Core.Town town = new Heroes.Core.Town();
                town._id = player._id;
                town._playerId = player._id;
                town._player = player;
                town._heroVisit = hero;
                player._castles.Add(town);

                // buildings
                {
                    Heroes.Core.Building building = new Heroes.Core.Building();
                    building.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.VillageHall]);
                    town._buildingKIds.Add(building._id, building);

                    building = new Heroes.Core.Building();
                    building.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Tavern]);
                    town._buildingKIds.Add(building._id, building);

                    building = new Heroes.Core.Building();
                    building.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Fort]);
                    town._buildingKIds.Add(building._id, building);
                }
            }
        }

        public void SetPlayer(Heroes.Core.Player player, Hashtable playerKIds)
        {
            lock (frmGame2._lockMe)
            {
                this._currentPlayer = player;
                this._playerKIds = playerKIds;

                SetCurrentHero();

                this._currentPlayer.ResetBuilt();
                this._currentPlayer.ResetMovementPoints();

                if (!_isFirstRun)
                {
                    this._currentPlayer.AddResources();
                }

                this._currentHero._spellPointLeft = this._currentHero._maxSpellPoint;

                // calculate days
                if (!_isFirstRun)
                {
                    _day += 1;
                    while (_day > 7)
                    {
                        _week += 1;
                        _day -= 7;

                        this._currentPlayer.AddGrowth();

                        while (_week > 4)
                        {
                            _month += 1;
                            _week -= 4;
                        }
                    }
                }

                this.tsslblStatus.Text = string.Format("Month {0}, Week {1}, Day {2}", _month, _week, _day);
            }
        }

        private void SetCurrentHero()
        {
            if (_currentPlayer._heroes.Count > 0)
                _currentHero = (Heroes.Core.Hero)_currentPlayer._heroes[0];
            else
                _currentHero = null;
        }

        public bool ReadOnly
        {
            set
            {
                lock (frmGame2._lockMe)
                {
                    _readOnly = value;

                    cmdNextTurn.Enabled = !_readOnly;

                    cmdWood.Enabled = !_readOnly;
                    cmdMercury.Enabled = cmdWood.Enabled;
                    cmdOre.Enabled = cmdWood.Enabled;
                    cmdSulfur.Enabled = cmdWood.Enabled;
                    cmdCrystal.Enabled = cmdWood.Enabled;
                    cmdGem.Enabled = cmdWood.Enabled;
                    cmdGold.Enabled = cmdWood.Enabled;

                    cmdWarriorTomb.Enabled = !_readOnly;
                    cmdDwarvenTreasury.Enabled = cmdWarriorTomb.Enabled;
                    this.cmdGriffinConserv.Enabled = cmdWarriorTomb.Enabled;
                    this.cmdDragonUtopia.Enabled = cmdWarriorTomb.Enabled;

                    if (_isFirstRun)
                    {
                        cmdSteal.Enabled = false;
                        cmdConquer.Enabled = false;
                    }
                    else
                    {
                        cmdSteal.Enabled = !_readOnly;
                        cmdConquer.Enabled = !_readOnly;
                    }

                    if (_hasVisitKnowStone)
                        cmdKnowledgeStone.Enabled = false;
                    else
                        cmdKnowledgeStone.Enabled = !_readOnly;
                }
            }
        }

        private void cmdNextTurn_Click(object sender, EventArgs e)
        {
            this.ReadOnly = true;

            _isFirstRun = false;

            // raise event
            OnNextTurnClick();
        }

        private void cmdCastle_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            Heroes.Core.Castle.frmCastle f = new Heroes.Core.Castle.frmCastle();
            f.ReadOnly = this._readOnly;
            f.ShowDialog((Heroes.Core.Town)this._currentPlayer._castles[0]);

            if (_currentHero != null)
            {
                _currentHero.AddAttributeToArmies();
            }
        }

        private void cmdHeroInfo_Click(object sender, EventArgs e)
        {
            if (_currentHero == null) return;

            Heroes.Core.Map.frmHeroInfo f = new Heroes.Core.Map.frmHeroInfo();
            f.ShowDialog(this._currentHero);
        }

        private void cmdWood_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (this._currentHero._movementPointLeft <= 0)
            {
                MessageBox.Show("No more movement points.");
                return;
            }

            int mineType = 0;
            int[] armyLevels = null;
            if (cmdWood.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Wood;
                armyLevels = new int[] { 1, 2 };
            }
            else if (cmdOre.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Ore;
                armyLevels = new int[] { 1, 2 };
            }
            else if (cmdMercury.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Mercury;
                armyLevels = new int[] { 3, 4 };
            }
            else if (cmdSulfur.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Sulfur;
                armyLevels = new int[] { 3, 4 };
            }
            else if (cmdCrystal.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Crystal;
                armyLevels = new int[] { 3, 4 };
            }
            else if (cmdGem.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Gem;
                armyLevels = new int[] { 3, 4 };
            }
            else if (cmdGold.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Gold;
                armyLevels = new int[] { 5, 6 };
            }
            else
                return;

            this._currentHero._movementPointLeft -= 1;

            ArrayList mines = _currentPlayer._mineKTypes[mineType];
            int count = mines.Count;
            count += 1;

            Heroes.Core.Monster monster = CreateMonster(armyLevels, 2, count);

            if (chkQuickCombat.Checked)
            {
                Heroes.Core.Battle.Quick.BattleCommander quickBattle = new Heroes.Core.Battle.Quick.BattleCommander(this._currentPlayer, this._currentHero,
                    null, null, monster, true);
                Heroes.Core.Battle.BattleSideEnum victory = quickBattle.Start();

                if (!ShowBattleResult(victory, this._currentPlayer._id,
                    quickBattle._attackHero, quickBattle._defendHero, quickBattle._monster)) return;
            }
            else
            {
                StartingBattleEventArg e2 = new StartingBattleEventArg(this._currentHero, null, monster);
                OnStartingBattle(e2);

                if (!e2._success) return;
            }

            Heroes.Core.Mine mine = new Heroes.Core.Mine();
            mine.CopyFrom((Heroes.Core.Mine)Heroes.Core.Setting._mineTypes[mineType]);
            mines.Add(mine);
        }

        private bool ShowBattleResult(Heroes.Core.Battle.BattleSideEnum victory,
            int currentPlayerId,
            Heroes.Core.Hero attackHero, Heroes.Core.Hero defendHero, Heroes.Core.Monster monster)
        {
            using (Heroes.Core.Battle.frmBattleResult f2 = new Heroes.Core.Battle.frmBattleResult())
            {
                if (victory == Heroes.Core.Battle.BattleSideEnum.Attacker)
                {
                    if (attackHero._playerId == currentPlayerId)
                    {
                        // victory
                        f2.ShowDialog(1, attackHero, currentPlayerId,
                            attackHero, defendHero, monster);

                        this._currentHero._experience += f2._experience;
                        this.LevelUp(this._currentHero);

                        return true;
                    }
                    else
                    {
                        // defeat
                        f2.ShowDialog(2, attackHero, currentPlayerId,
                            attackHero, defendHero, monster);

                        // do not remove hero
                        this.ResurrectHero(this._currentHero);
                        //_currentPlayer._heroes.Remove(this._currentHero);
                        //_currentHero = null;

                        return false;
                    }
                }
                else
                {
                    if (defendHero != null)
                    {
                        if (defendHero._playerId == currentPlayerId)
                        {
                            // victory
                            f2.ShowDialog(1, defendHero, currentPlayerId,
                                attackHero, defendHero, monster);

                            this._currentHero._experience += f2._experience;
                            this.LevelUp(this._currentHero);

                            return true;
                        }
                        else
                        {
                            // defeat
                            f2.ShowDialog(2, defendHero, currentPlayerId,
                                attackHero, defendHero, monster);

                            // do not remove hero
                            this.ResurrectHero(this._currentHero);
                            //_currentPlayer._heroes.Remove(this._currentHero);
                            //_currentHero = null;

                            return false;
                        }
                    }
                    else if (monster != null)
                    {
                        // defeat
                        f2.ShowDialog(2, monster, currentPlayerId,
                            attackHero, defendHero, monster);

                        // do not remove hero
                        this.ResurrectHero(this._currentHero);
                        //_currentPlayer._heroes.Remove(this._currentHero);
                        //_currentHero = null;

                        return false;
                    }
                }
            }

            return false;
        }

        private bool CaptureMine(int count, int mineLevel)
        {
            Heroes.Core.Monster monster = new Heroes.Core.Monster();
            monster._id = 1;

            Random rnd = new Random();

            // random monster level
            int armyId = 0;
            int armyLevel = 0;
            {
                int[] armyIds = null;
                int[] armyLevels = null;

                if (mineLevel == 1)
                {
                    armyIds = new int[] { (int)Heroes.Core.ArmyIdEnum.Pikeman, (int)Heroes.Core.ArmyIdEnum.Halberdier,
                        (int)Heroes.Core.ArmyIdEnum.Archer, (int)Heroes.Core.ArmyIdEnum.Marksman };
                    armyLevels = new int[] { 1, 1, 2, 2 };
                }
                else if (mineLevel == 2)
                {
                    armyIds = new int[] { 
                        (int)Heroes.Core.ArmyIdEnum.Griffin, (int)Heroes.Core.ArmyIdEnum.RoyalGiffin,
                        (int)Heroes.Core.ArmyIdEnum.Swordman, (int)Heroes.Core.ArmyIdEnum.Crusader };
                    armyLevels = new int[] { 3, 3, 4, 4 };
                }
                else if (mineLevel == 3)
                {
                    armyIds = new int[] { (int)Heroes.Core.ArmyIdEnum.Monk, (int)Heroes.Core.ArmyIdEnum.Zealot,
                        (int)Heroes.Core.ArmyIdEnum.Cavalier, (int)Heroes.Core.ArmyIdEnum.Champion,
                        (int)Heroes.Core.ArmyIdEnum.Angel, (int)Heroes.Core.ArmyIdEnum.Archangel };
                    armyLevels = new int[] { 5, 5, 6, 6, 7, 7 };
                }
                else
                {
                    armyIds = new int[] { (int)Heroes.Core.ArmyIdEnum.Pikeman };
                    armyLevels = new int[] { 1 };
                }

                int index = rnd.Next(0, armyIds.Length);
                armyId = armyIds[index];
                armyLevel = armyLevels[index];
            }

            // random qty
            int totalQty = 0;
            {
                int qtyIndex = 0;

                if (mineLevel == 2)
                    qtyIndex = count + 2 - armyLevel + 3;
                else if (mineLevel == 3)
                    qtyIndex = count + 2 - armyLevel + 5;
                else
                    qtyIndex = count + 2 - armyLevel + 1;

                if (qtyIndex < 0) qtyIndex = 0;

                int minQty = 0;
                if (qtyIndex < _minQtys.Length) minQty = _minQtys[qtyIndex];
                else minQty = 99999;

                int maxQty = 0;
                if (qtyIndex < _minQtys.Length) maxQty = _maxQtys[qtyIndex];
                else maxQty = 99999;

                totalQty = rnd.Next(minQty, maxQty);
            }

            // random slot
            {
                int slots = 0;
                int qtyPerSlot = 0;

                // do until qty per slot is more than zero
                do
                {
                    slots = rnd.Next(1, 6);
                    qtyPerSlot = totalQty / slots;
                } while (qtyPerSlot <= 0);

                int[] slotNos = null;
                Heroes.Core.Army army = null;
                if (slots == 1)
                {
                    slotNos = new int[] { 3 };
                }
                else if (slots == 2)
                {
                    slotNos = new int[] { 2, 4 };
                }
                else if (slots == 3)
                {
                    slotNos = new int[] { 0, 3, 6 };
                }
                else if (slots == 4)
                {
                    slotNos = new int[] { 0, 2, 4, 6 };
                }
                else if (slots == 5)
                {
                    slotNos = new int[] { 0, 2, 3, 4, 6 };
                }

                foreach (int slotNo in slotNos)
                {
                    army = new Heroes.Core.Army();
                    army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[armyId]);
                    army._qty = qtyPerSlot;
                    army._slotNo = slotNo;
                    monster._armyKSlots.Add(army._slotNo, army);
                }
            }

            StartingBattleEventArg e2 = new StartingBattleEventArg(this._currentHero, null, monster);
            OnStartingBattle(e2);

            return e2._success;
        }

        private bool Battle(Heroes.Core.Hero attackHero, Heroes.Core.Hero defendHero, Heroes.Core.Monster monster)
        {
            lock (frmGame2._lockMe)
            {
                using (frmBattle f = new frmBattle())
                {
                    f.ShowDialog(attackHero, defendHero, monster, null);

                    using (Heroes.Core.Battle.frmBattleResult f2 = new Heroes.Core.Battle.frmBattleResult())
                    {
                        if (f._victory == Heroes.Core.Battle.BattleSideEnum.Attacker)
                        {
                            if (f._engine._attacker._playerId == this._currentPlayer._id)
                            {
                                // victory
                                f2.ShowDialog(1, f._engine._attacker, this._currentPlayer._id,
                                    f._engine._attacker, f._engine._defender, f._engine._monster);

                                this._currentHero._experience += f2._experience;
                                LevelUp(this._currentHero);

                                return true;
                            }
                            else
                            {
                                // defeat
                                f2.ShowDialog(2, f._engine._attacker, this._currentPlayer._id,
                                    f._engine._attacker, f._engine._defender, f._engine._monster);

                                // do not remove hero
                                ResurrectHero(this._currentHero);
                                //_currentPlayer._heroes.Remove(this._currentHero);
                                //_currentHero = null;

                                return false;
                            }
                        }
                        else
                        {
                            if (f._engine._defender != null)
                            {
                                if (f._engine._defender._playerId == this._currentPlayer._id)
                                {
                                    // victory
                                    f2.ShowDialog(1, f._engine._defender, this._currentPlayer._id,
                                        f._engine._attacker, f._engine._defender, f._engine._monster);

                                    this._currentHero._experience += f2._experience;
                                    LevelUp(this._currentHero);

                                    return true;
                                }
                                else
                                {
                                    // defeat
                                    f2.ShowDialog(2, f._engine._defender, this._currentPlayer._id,
                                        f._engine._attacker, f._engine._defender, f._engine._monster);

                                    // do not remove hero
                                    ResurrectHero(this._currentHero);
                                    //_currentPlayer._heroes.Remove(this._currentHero);
                                    //_currentHero = null;

                                    return false;
                                }
                            }
                            else if (f._engine._monster != null)
                            {
                                // defeat
                                f2.ShowDialog(2, f._engine._monster, this._currentPlayer._id,
                                    f._engine._attacker, f._engine._defender, f._engine._monster);

                                // do not remove hero
                                ResurrectHero(this._currentHero);
                                //_currentPlayer._heroes.Remove(this._currentHero);
                                //_currentHero = null;

                                return false;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void ResurrectHero(Heroes.Core.Hero hero)
        {
            hero._armyKSlots.Clear();

            {
                Heroes.Core.Army army = new Heroes.Core.Army();
                army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Pikeman]);
                army.AddAttribute(hero);
                army._heroId = hero._id;
                army._playerId = hero._playerId;
                army._qty = 1;
                army._slotNo = 0;
                hero._armyKSlots.Add(army._slotNo, army);
            }
        }

        public void LevelUp(Heroes.Core.Hero hero)
        {
            while (hero.IsLevelUp())
            {
                Heroes.Core.Map.frmLevelUp f = new Heroes.Core.Map.frmLevelUp();
                f.ShowDialog(hero);
            }
        }

        private void cmdOtherHeroInfo_Click(object sender, EventArgs e)
        {
            Heroes.Core.Hero hero = GetHero();
            if (hero == null) return;

            frmOtherHeroInfo f = new frmOtherHeroInfo();
            f.ShowDialog(hero);
        }

        private Heroes.Core.Hero GetHero()
        {
            if (cboOtherPlayerId.SelectedIndex < 0) return null;

            int playerId = System.Convert.ToInt32(cboOtherPlayerId.Text);
            if (!this._playerKIds.ContainsKey(playerId)) return null;

            Heroes.Core.Player player = (Heroes.Core.Player)this._playerKIds[playerId];
            if (player._heroes.Count < 1) return null;

            Heroes.Core.Hero hero = (Heroes.Core.Hero)player._heroes[0];

            return hero;
        }

        private void cmdConquer_Click(object sender, EventArgs e)
        {

        }

        private void cmdSteal_Click(object sender, EventArgs e)
        {
            Heroes.Core.Hero hero = GetHero();
            if (hero == null) return;

            StartingBattleEventArg e2 = new StartingBattleEventArg(this._currentHero, hero, null);
            OnStartingBattle(e2);
        }

        private void cmdKnowledgeStone_Click(object sender, EventArgs e)
        {
            _hasVisitKnowStone = true;
            cmdKnowledgeStone.Enabled = false;

            this._currentHero._experience += 1000;
            LevelUp(this._currentHero);
        }

        private void cmdWarriorTomb_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (this._currentHero._movementPointLeft <= 0)
            {
                MessageBox.Show("No more movement points.");
                return;
            }

            int qtyIndex = 0;
            int tombIndex = 0;
            Heroes.Core.Heros.ArtifactLevelEnum artifactLevel = Heroes.Core.Heros.ArtifactLevelEnum.Treasure;
            int[] armyLevels = null;
            if (btn.Equals(cmdWarriorTomb))
            {
                qtyIndex = 2;
                tombIndex = 0;
                artifactLevel = Heroes.Core.Heros.ArtifactLevelEnum.Treasure;
                armyLevels = new int[] { 1, 2 };
            }
            else if (btn.Equals(cmdDwarvenTreasury))
            {
                qtyIndex = 2;
                tombIndex = 1;
                artifactLevel = Heroes.Core.Heros.ArtifactLevelEnum.Minor;
                armyLevels = new int[] { 3, 4 };
            }
            else if (btn.Equals(cmdGriffinConserv))
            {
                qtyIndex = 2;
                tombIndex = 2;
                artifactLevel = Heroes.Core.Heros.ArtifactLevelEnum.Major;
                armyLevels = new int[] { 5, 6 };
            }
            else if (btn.Equals(cmdDragonUtopia))
            {
                qtyIndex = 2;
                tombIndex = 3;
                artifactLevel = Heroes.Core.Heros.ArtifactLevelEnum.Relic;
                armyLevels = new int[] { 7 };
            }
            else
                return;

            int count = _artifactVisitCounts[tombIndex];
            count += 1;

            this._currentHero._movementPointLeft -= 1;

            Heroes.Core.Monster monster = CreateMonster(armyLevels, qtyIndex, count);

            if (chkQuickCombat.Checked)
            {
                Heroes.Core.Battle.Quick.BattleCommander quickBattle = new Heroes.Core.Battle.Quick.BattleCommander(this._currentPlayer, this._currentHero,
                    null, null, monster, true);
                Heroes.Core.Battle.BattleSideEnum victory = quickBattle.Start();

                if (!ShowBattleResult(victory, this._currentPlayer._id,
                    quickBattle._attackHero, quickBattle._defendHero, quickBattle._monster)) return;
            }
            else
            {
                StartingBattleEventArg e2 = new StartingBattleEventArg(this._currentHero, null, monster);
                OnStartingBattle(e2);

                if (!e2._success) return;
            }

            _artifactVisitCounts[tombIndex] = count;

            GettingArtifactEventArg e3 = new GettingArtifactEventArg(artifactLevel);
            OnGettingArtifact(e3);

            if (e3._artifact != null)
            {
                MessageBox.Show(string.Format("You get {0}.", e3._artifact._name));

                this._currentHero.AddArtifacts(e3._artifact);
                this._currentHero.CalculateAll();
            }
        }

        private Heroes.Core.Monster CreateMonster(int[] armyLevels, int startingQtyIndex, int multiplier)
        {
            Heroes.Core.Monster monster = new Heroes.Core.Monster();
            monster._id = 1;

            Random rnd = new Random();

            // random monster level
            int levelIndex = rnd.Next(0, armyLevels.Length);
            int level = armyLevels[levelIndex];

            // create armyIds
            ArrayList ids = new ArrayList();
            Hashtable armies = Heroes.Core.Setting._armyKLevelKIds[level];
            foreach (int id in armies.Keys)
            {
                ids.Add(id);
            }

            int index = rnd.Next(0, ids.Count);
            int armyId = (int)ids[index];

            // random qty
            int totalQty = 0;
            {
                int qtyIndex = startingQtyIndex - levelIndex;   // less qty if higher level
                if (qtyIndex < 0) qtyIndex = 0;
                
                int minQty = 0;
                if (qtyIndex < _minQtys.Length) minQty = _minQtys[qtyIndex];
                else minQty = 99999;

                int maxQty = 0;
                if (qtyIndex < _minQtys.Length) maxQty = _maxQtys[qtyIndex];
                else maxQty = 99999;

                totalQty = rnd.Next(minQty, maxQty + 1);
                totalQty *= multiplier;
            }

            // random slot
            {
                int slots = 0;
                int qtyPerSlot = 0;

                // do until qty per slot is more than zero
                do
                {
                    slots = rnd.Next(1, 6);
                    qtyPerSlot = totalQty / slots;
                } while (qtyPerSlot <= 0);

                int[] slotNos = null;
                Heroes.Core.Army army = null;
                if (slots == 1)
                {
                    slotNos = new int[] { 3 };
                }
                else if (slots == 2)
                {
                    slotNos = new int[] { 2, 4 };
                }
                else if (slots == 3)
                {
                    slotNos = new int[] { 0, 3, 6 };
                }
                else if (slots == 4)
                {
                    slotNos = new int[] { 0, 2, 4, 6 };
                }
                else if (slots == 5)
                {
                    slotNos = new int[] { 0, 2, 3, 4, 6 };
                }

                foreach (int slotNo in slotNos)
                {
                    army = new Heroes.Core.Army();
                    army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[armyId]);
                    army._qty = qtyPerSlot;
                    army._slotNo = slotNo;
                    monster._armyKSlots.Add(army._slotNo, army);
                }
            }

            return monster;
        }

    }
}
