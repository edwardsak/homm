using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Castle
{
    public partial class frmFort : Form
    {
        Town _town;

        //Hashtable _armyAvKIds;  // available armies to buy, Key = Army Id
        public Hashtable _armyPurKIds; // purchased armies;

        public frmBuyPikemen.DeleHasEmptySlot HasEmptySlot;
        public frmBuyArcher.DeleHasEmptySlot HasEmptySlot2;
        public frmBuyGriffin.DeleHasEmptySlot HasEmptySlot3;
        public frmBuySwordsmen.DeleHasEmptySlot HasEmptySlot4;

        public frmFort()
        {
            InitializeComponent();

            _town = new Town();
            _armyPurKIds = new Hashtable();
        }

        private void frmFort_Load(object sender, EventArgs e)
        {
            
        }

        public DialogResult ShowDialog(Heroes.Core.Town town)
        {
            _town = town;
            return this.ShowDialog();
        }

        private void cmdBuyPikemen_Click(object sender, EventArgs e)
        {
            BuyUpgArmy(sender);

            Button btn = (Button)sender;

            int buildingId = 0;
            int armyId = 0;
            frmCommonBuy f = null;

            if (btn.Equals(cmdBuyPikemen))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.Guardhouse;
                armyId = (int)Heroes.Core.ArmyIdEnum.Pikeman;
                f = new frmBuyPikemen();
            }
            else if (btn.Equals(cmdBuyArcher))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.ArchersTower;
                armyId = (int)Heroes.Core.ArmyIdEnum.Archer;
                f = new frmBuyArcher();
            }
            else if (btn.Equals(cmdBuyGriffin))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.GriffinTower;
                armyId = (int)Heroes.Core.ArmyIdEnum.Griffin;
                f = new frmBuyGriffin();
            }
            else if (btn.Equals(cmdBuySwordsmen))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.Barracks;
                armyId = (int)Heroes.Core.ArmyIdEnum.Swordman;
                f = new frmBuySwordsmen();
            }
            else if (btn.Equals(cmdBuyMonks))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.Monastery;
                armyId = (int)Heroes.Core.ArmyIdEnum.Monk;
                f = new frmBuyMonks();
            }
            else if (btn.Equals(cmdBuyCavaliers))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.TrainingGrounds;
                armyId = (int)Heroes.Core.ArmyIdEnum.Cavalier;
                f = new frmBuyCavalier();
            }
            else if (btn.Equals(cmdBuyAngels))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.PortalofGlory;
                armyId = (int)Heroes.Core.ArmyIdEnum.Angel;
                f = new frmBuyAngel();
            }
            else
                return;

            if (!_town._buildingKIds.ContainsKey(buildingId)) return;

            Building Aqty = (Building)_town._buildingKIds[buildingId];
            int a = Aqty._armyQty;

            f.HasEmptySlot = this.HasEmptySlot;
            if (f.ShowDialog(_town, Aqty, buildingId) != DialogResult.OK) return;

            //Army armyPur = null;
            //if (_armyPurKIds.ContainsKey(f._armyPur._id))
            //{
            //    armyPur = (Army)_armyPurKIds[f._armyPur._id];
            //    armyPur._qty += f._armyPur._qty;
            //}
            //else
            //{
            //    _armyPurKIds.Add(f._armyPur._id, f._armyPur);
            //}
        }

        private void BuyUpgArmy(object sender)
        {
            Button btn = (Button)sender;

            int buildingId = 0;
            int armyId = 0;
            frmCommonBuy f = null;

            if (btn.Equals(cmdBuyPikemen))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgGuardhouse;
                armyId = (int)Heroes.Core.ArmyIdEnum.Pikeman;
                f = new frmBuyPikemen();
            }
            else if (btn.Equals(cmdBuyArcher))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgArchersTower;
                armyId = (int)Heroes.Core.ArmyIdEnum.Archer;
                f = new frmBuyArcher();
            }
            else if (btn.Equals(cmdBuyGriffin))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgGriffinTower;
                armyId = (int)Heroes.Core.ArmyIdEnum.Griffin;
                f = new frmBuyGriffin();
            }
            else if (btn.Equals(cmdBuySwordsmen))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgBarracks;
                armyId = (int)Heroes.Core.ArmyIdEnum.Swordman;
                f = new frmBuySwordsmen();
            }
            else if (btn.Equals(cmdBuyMonks))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgMonastery;
                armyId = (int)Heroes.Core.ArmyIdEnum.Monk;
                f = new frmBuyMonks();
            }
            else if (btn.Equals(cmdBuyCavaliers))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgTrainingGrounds;
                armyId = (int)Heroes.Core.ArmyIdEnum.Cavalier;
                f = new frmBuyCavalier();
            }
            else if (btn.Equals(cmdBuyAngels))
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgPortalofGlory;
                armyId = (int)Heroes.Core.ArmyIdEnum.Angel;
                f = new frmBuyAngel();
            }
            else
                return;

            if (!_town._buildingKIds.ContainsKey(buildingId)) return;

            Building Aqty = (Building)_town._buildingKIds[buildingId];
            int a = Aqty._armyQty;

            f.HasEmptySlot = this.HasEmptySlot;
            if (f.ShowDialog(_town, Aqty, buildingId) != DialogResult.OK) return;
        }

        #region Exit
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
