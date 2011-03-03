using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Castle
{
    public partial class frmCommonBuyUpg : Form
    {
        public frmCommonBuyUpg()
        {
            InitializeComponent();

            this.lblAv.Text = "";
            this.txtQty.TextChanged += new EventHandler(txtQty_TextChanged);
        }

        Heroes.Core.Town _town;
        Building _building;

        public DialogResult ShowDialog(Heroes.Core.Town town, Building buildingPurArmy)
        {
            _town = town;
            _building = buildingPurArmy;

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
    }
}
