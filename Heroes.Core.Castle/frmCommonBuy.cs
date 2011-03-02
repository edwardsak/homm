using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Castle
{
    public partial class frmCommonBuy : Form
    {
        public delegate bool DeleHasEmptySlot();
        public DeleHasEmptySlot HasEmptySlot;

        Heroes.Core.Town _town;
        //public Army _armyPur;
        Building _building;

        int BuildingId;

        public frmCommonBuy()
        {
            InitializeComponent();

            //_armyPur = new Army();
            BuildingId = 0;

            this.lblAv.Text = "";
            this.txtQty.TextChanged += new EventHandler(txtQty_TextChanged);
        }

        private void frmCommonBuy_Load(object sender, EventArgs e)
        {

        }

        public DialogResult ShowDialog(Heroes.Core.Town town, Building buildingPurArmy , int Id)
        {
            _town = town;
            _building = buildingPurArmy;
            BuildingId = Id;

            this.lblCost.Text = _building._gold.ToString();
            this.txtQty.Text = _building._armyQty.ToString();
            this.lblAv.Text = this.txtQty.Text;

            int amt = Heroes.Core.Army.CalculateAmt(_building._armyQty, _building._gold);
            lblAmt.Text = amt.ToString();

            return this.ShowDialog();
        }

        protected virtual Heroes.Core.Army NewArmy()
        {
            return new Army();
        }

        void txtQty_TextChanged(object sender, EventArgs e)
        {
            int qty = 0;

            if (txtQty.Text.Length > 0)
                qty = System.Convert.ToInt32(txtQty.Text);

            int amt = Heroes.Core.Army.CalculateAmt(qty, _building._gold);
            lblAmt.Text = amt.ToString();
        }

        #region Validate
        private bool ValidateQty()
        {
            if (txtQty.Text == "")
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Buy
        private void cmdBuy_Click(object sender, EventArgs e)
        {
            if (!ValidateQty()) return;

            int slotNo = _town.FindFirstEmptySlotNo();

            if (slotNo < 0)
            {
                MessageBox.Show("There are no more room in garrison for this army.");
                return;
            }

            int qtyPur = System.Convert.ToInt32(txtQty.Text);

            // create purchased army and minus available army and check costing
            Heroes.Core.Army armyPur = NewArmy();
            armyPur._qty = qtyPur;

            armyPur.CalculateCost();

            if (armyPur._qty == 0)
            {
                return;
            }

            if (!armyPur.CanBuy(_town._player))
            {
                MessageBox.Show("Not enought resources.");
                return;
            }

            armyPur.DeductResources(_town._player);

            {
                armyPur._playerId = _town._playerId;
                armyPur._slotNo = slotNo;
                _town._armyInCastleKSlots.Add(armyPur._slotNo, armyPur);

                _building._armyQty -= qtyPur;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Exit
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        private void picArmy_Click(object sender, EventArgs e)
        {

        }

        private void picArmyLv2_Click(object sender, EventArgs e)
        {
            int buildingId = 0;
            int armyId = 0;
            frmCommonBuyUpg f = null;

            if (BuildingId == (int)Heroes.Core.BuildingIdEnum.UpgGuardhouse)
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgGuardhouse;
                armyId = (int)Heroes.Core.ArmyIdEnum.Halberdier;
                f = new frmBuyHalberdier();
                this.Close();
            }

            else if (BuildingId == (int)Heroes.Core.BuildingIdEnum.UpgArchersTower)
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgArchersTower;
                armyId = (int)Heroes.Core.ArmyIdEnum.Marksman;
                f = new frmBuyMarksmen();
                this.Close();
            }

            else if (BuildingId == (int)Heroes.Core.BuildingIdEnum.UpgGriffinTower)
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgGriffinTower;
                armyId = (int)Heroes.Core.ArmyIdEnum.RoyalGiffin;
                f = new frmBuyRoyalGriffin();
                this.Close();
            }

            else if (BuildingId == (int)Heroes.Core.BuildingIdEnum.UpgBarracks)
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgBarracks;
                armyId = (int)Heroes.Core.ArmyIdEnum.Crusader;
                f = new frmBuyCrusader();
                this.Close();
            }

            else if (BuildingId == (int)Heroes.Core.BuildingIdEnum.UpgMonastery)
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgMonastery;
                armyId = (int)Heroes.Core.ArmyIdEnum.Zealot;
                f = new frmBuyZealot();
                this.Close();
            }

            else if (BuildingId == (int)Heroes.Core.BuildingIdEnum.UpgTrainingGrounds)
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgTrainingGrounds;
                armyId = (int)Heroes.Core.ArmyIdEnum.Champion;
                f = new frmBuyChampion();
                this.Close();
            }

            else if (BuildingId == (int)Heroes.Core.BuildingIdEnum.UpgPortalofGlory)
            {
                buildingId = (int)Heroes.Core.BuildingIdEnum.UpgPortalofGlory;
                armyId = (int)Heroes.Core.ArmyIdEnum.Archangel;
                f = new frmBuyArchangel();
                this.Close();
            }

            else
                return;

            if (!_town._buildingKIds.ContainsKey(buildingId)) return;

            Building Aqty = (Building)_town._buildingKIds[buildingId];
            int a = Aqty._armyQty;

            if (f.ShowDialog(_town, Aqty) != DialogResult.OK) return;
        }

    }
}
