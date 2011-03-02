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
    public partial class frmDuel : Form
    {
        ArrayList _players;
        Heroes.Core.Player _playerMe;
        Heroes.Core.Player _currentPlayer;
        Heroes.Core.Hero _currentHero;

        public frmDuel()
        {
            InitializeComponent();

            _players = new ArrayList();

            Random rnd = new Random();
            int heroId = rnd.Next(1, 16);
            CreatePlayer(heroId);
            //CreatePlayer(2);

            _playerMe = (Heroes.Core.Player)_players[0];
            _currentPlayer = (Heroes.Core.Player)_players[0];
            _currentHero = (Heroes.Core.Hero)_currentPlayer._heroes[0];
        }

        private void frmDuel_Load(object sender, EventArgs e)
        {

        }

        private void CreatePlayer(int id)
        {
            Heroes.Core.Player player = new Heroes.Core.Player();
            player._id = id;
            player._wood = 20;
            player._mercury = 10;
            player._ore = 20;
            player._sulfur = 10;
            player._crystal = 10;
            player._gem = 10;
            player._gold = 20000;
            _players.Add(player);

            Random rnd = new Random();

            // hero
            Heroes.Core.Hero hero = (Heroes.Core.Hero)Heroes.Core.Setting._heros[id];
            hero._playerId = player._id;
            hero._player = player;
            hero._level = 1;
            hero._experience = rnd.Next(45, 99);
            hero.CalculateMaxSpellPoint();
            hero._spellPointLeft = hero._maxSpellPoint;
            hero._movementPoint = 1;
            hero._movementPointLeft = hero._movementPoint;
            player._heroes.Add(hero);

            // army
            {
                Heroes.Core.Army army = new Heroes.Core.Army();
                army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Pikeman]);
                army.AddAttribute(hero);
                army._heroId = hero._id;
                army._playerId = player._id;
                //army._qty = rnd.Next(20, 49);
                army._qty = rnd.Next(500, 999);
                army._slotNo = 1;
                hero._armyKSlots.Add(army._slotNo, army);

                army = new Heroes.Core.Army();
                army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Archer]);
                army.AddAttribute(hero);
                army._heroId = hero._id;
                army._playerId = player._id;
                //army._qty = rnd.Next(10, 19);
                army._qty = rnd.Next(500, 999);
                army._slotNo = 0;
                hero._armyKSlots.Add(army._slotNo, army);

                //army = new Heroes.Core.Army();
                //army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Griffin]);
                //army.AddAttribute(hero);
                //army._heroId = hero._id;
                //army._playerId = player._id;
                //army._qty = rnd.Next(10, 19);
                ////army._qty = rnd.Next(500, 999);
                //army._slotNo = 4;
                //hero._armyKSlots.Add(army._slotNo, army);

                //army = new Heroes.Core.Army();
                //army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Swordman]);
                //army.AddAttribute(hero);
                //army._heroId = hero._id;
                //army._playerId = player._id;
                //army._qty = rnd.Next(10, 19);
                ////army._qty = rnd.Next(500, 999);
                //army._slotNo = 5;
                //hero._armyKSlots.Add(army._slotNo, army);

                //army = new Heroes.Core.Army();
                //army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Cavalier]);
                //army.AddAttribute(hero);
                //army._heroId = hero._id;
                //army._playerId = player._id;
                //army._qty = rnd.Next(5, 9);
                ////army._qty = rnd.Next(500, 999);
                //army._slotNo = 2;
                //hero._armyKSlots.Add(army._slotNo, army);

                //army = new Heroes.Core.Army();
                //army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Monk]);
                //army.AddAttribute(hero);
                //army._heroId = hero._id;
                //army._playerId = player._id;
                //army._qty = rnd.Next(10, 19);
                ////army._qty = rnd.Next(500, 999);
                //army._slotNo = 6;
                //hero._armyKSlots.Add(army._slotNo, army);

                //army = new Heroes.Core.Army();
                //army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Angel]);
                //army.AddAttribute(hero);
                //army._heroId = hero._id;
                //army._playerId = player._id;
                //army._qty = rnd.Next(1, 4);
                ////army._qty = rnd.Next(500, 999);
                //army._slotNo = 3;
                //hero._armyKSlots.Add(army._slotNo, army);
            }

            // spells
            {
                Heroes.Core.Spell spell = new Heroes.Core.Spell();
                spell.CopyFrom((Heroes.Core.Spell)Heroes.Core.Setting._spells[(int)Heroes.Core.SpellIdEnum.MagicArrow]);
                spell.CalculateDamage(hero);
                hero._spells.Add(spell._id, spell);

                spell = new Heroes.Core.Spell();
                spell.CopyFrom((Heroes.Core.Spell)Heroes.Core.Setting._spells[(int)Heroes.Core.SpellIdEnum.Haste]);
                hero._spells.Add(spell._id, spell);
            }

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

        private void cmdCastle1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            Heroes.Core.Player player = null;
            if (btn.Equals(cmdCastle1))
                player = (Heroes.Core.Player)_players[0];
            else
                player = (Heroes.Core.Player)_players[1];

            Heroes.Core.Castle.frmCastle f = new Heroes.Core.Castle.frmCastle();
            f.ShowDialog((Heroes.Core.Town)player._castles[0]);
        }

        private void cmdLevelUp1_Click(object sender, EventArgs e)
        {
            this._currentHero._experience += 1000;
            LevelUp(this._currentHero);
        }

        private void LevelUp(Heroes.Core.Hero hero)
        {
            while (hero.IsLevelUp())
            {
                Heroes.Core.Map.frmLevelUp f = new Heroes.Core.Map.frmLevelUp();
                f.ShowDialog(hero);
            }
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            this.Hide();

            Heroes.Core.Player player1 = (Heroes.Core.Player)_players[0];
            Heroes.Core.Hero hero1 = (Heroes.Core.Hero)player1._heroes[0];

            Heroes.Core.Player player2 = (Heroes.Core.Player)_players[1];
            Heroes.Core.Hero hero2 = (Heroes.Core.Hero)player2._heroes[0];

            frmBattle f = new frmBattle();
            f.ShowDialog(hero1, hero2, null, null);

            this.Show();
        }

        private bool Battle(Heroes.Core.Hero hero, Heroes.Core.Monster monster)
        {
            this.Hide();

            try
            {
                using (frmBattle f = new frmBattle())
                {
                    f.ShowDialog(hero, null, monster, null);

                    using (Heroes.Core.Battle.frmBattleResult f2 = new Heroes.Core.Battle.frmBattleResult())
                    {
                        if (f._victory == Heroes.Core.Battle.BattleSideEnum.Attacker)
                        {
                            if (f._engine._attacker._playerId == this._playerMe._id)
                            {
                                // victory
                                f2.ShowDialog(1, f._engine._attacker, this._playerMe._id,
                                    f._engine._attacker, f._engine._defender, f._engine._monster);

                                this._currentHero._experience += f2._experience;
                                LevelUp(this._currentHero);

                                return true;
                            }
                            else
                            {
                                // defeat
                                f2.ShowDialog(2, f._engine._attacker, this._playerMe._id,
                                    f._engine._attacker, f._engine._defender, f._engine._monster);

                                _currentPlayer._heroes.Remove(this._currentHero);
                                _currentHero = null;

                                return false;
                            }
                        }
                        else
                        {
                            if (f._engine._defender != null)
                            {
                                if (f._engine._defender._playerId == this._playerMe._id)
                                {
                                    // victory
                                    f2.ShowDialog(1, f._engine._defender, this._playerMe._id,
                                        f._engine._attacker, f._engine._defender, f._engine._monster);

                                    this._currentHero._experience += f2._experience;
                                    LevelUp(this._currentHero);

                                    return true;
                                }
                                else
                                {
                                    // defeat
                                    f2.ShowDialog(2, f._engine._defender, this._playerMe._id,
                                        f._engine._attacker, f._engine._defender, f._engine._monster);

                                    _currentPlayer._heroes.Remove(this._currentHero);
                                    _currentHero = null;

                                    return false;
                                }
                            }
                            else if (f._engine._monster != null)
                            {
                                // defeat
                                f2.ShowDialog(2, f._engine._monster, this._playerMe._id,
                                    f._engine._attacker, f._engine._defender, f._engine._monster);

                                _currentPlayer._heroes.Remove(this._currentHero);
                                _currentHero = null;

                                return false;
                            }
                        }
                    }
                }
            }
            finally
            {
                this.Show();
            }

            return false;
        }

        private void cmdMercury_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (this._currentHero._movementPointLeft <= 0)
            {
                MessageBox.Show("No more movement points.");
                return;
            }

            int mineType = 0;
            int level = 0;
            if (cmdWood.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Wood;
                level = 1;
            }
            else if (cmdOre.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Ore;
                level = 1;
            }
            else if (cmdMercury.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Mercury;
                level = 2;
            }
            else if (cmdSulfur.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Sulfur;
                level = 2;
            }
            else if (cmdCrystal.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Crystal;
                level = 2;
            }
            else if (cmdGem.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Gem;
                level = 2;
            }
            else if (cmdGold.Equals(btn))
            {
                mineType = (int)Heroes.Core.MineTypeEnum.Gold;
                level = 3;
            }
            else
                return;

            this._currentHero._movementPointLeft -= 1;

            ArrayList mines = _currentPlayer._mineKTypes[mineType];

            if (!CaptureMine(mines.Count, level)) return;

            Heroes.Core.Mine mine = new Heroes.Core.Mine();
            mine.CopyFrom((Heroes.Core.Mine)Heroes.Core.Setting._mineTypes[mineType]);
            mines.Add(mine);
        }

        private bool CaptureMine(int count, int level)
        {
            // Few 1-4
            // Several 5-9
            // Pack 10-19
            // Lots 20-49
            // Horde 50-99
            // Throng 100-249
            // Swarm 250-499
            // Zounds 500-999
            // Legion 1000+ 

            Heroes.Core.Monster monster = new Heroes.Core.Monster();
            monster._id = 1;

            Random rnd = new Random();

            int totalQty = 0;

            if (level == 2)
                totalQty = rnd.Next(20, 49);
            if (level == 3)
                totalQty = rnd.Next(50, 99);
            else
                totalQty = rnd.Next(10, 19);
            totalQty *= (count + 1);

            int slots = rnd.Next(1, 5);
            int qtyPerSlot = totalQty / slots;

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
                army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Pikeman]);
                army._qty = qtyPerSlot;
                army._slotNo = slotNo;
                monster._armyKSlots.Add(army._slotNo, army);
            }

            return Battle(_currentHero, monster);
        }

        private void cmdNextTurn_Click(object sender, EventArgs e)
        {
            // next player
            int index = _players.IndexOf(this._currentPlayer);
            index += 1;
            if (index >= this._players.Count) index = 0;
            this._currentPlayer = (Heroes.Core.Player)this._players[index];

            if (this._currentPlayer._heroes.Count > 0)
            {
                this._currentHero = (Heroes.Core.Hero)this._currentPlayer._heroes[0];

                // reset to full spell points
                this._currentHero._spellPointLeft = this._currentHero._maxSpellPoint;
            }

            _currentPlayer.AddResources();
            _currentPlayer.AddGrowth();
            _currentPlayer.ResetBuilt();
            _currentPlayer.ResetMovementPoints();
        }

        private void cmdHeroInfo_Click(object sender, EventArgs e)
        {
            if (_currentHero == null) return;

            Heroes.Core.Map.frmHeroInfo f = new Heroes.Core.Map.frmHeroInfo();
            f.ShowDialog(this._currentHero);
        }   

    }
}
