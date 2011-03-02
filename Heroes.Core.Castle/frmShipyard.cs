using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Castle
{
    public partial class frmShipyard : Form
    {
        Town _town;

        public frmShipyard()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(Town town)
        {
            _town = town;
            return this.ShowDialog();
        }

        private void cmdBuy_Click(object sender, EventArgs e)
        {
            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Ship))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Ship]);
                _town._buildingKIds.Add(b2._id, b2);
                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        #region Close
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
