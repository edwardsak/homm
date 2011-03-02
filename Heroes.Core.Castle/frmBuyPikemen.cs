using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Castle
{
    public partial class frmBuyPikemen : frmCommonBuy
    {
        public frmBuyPikemen()
        {
            InitializeComponent();
        }

        private void frmBuyPikemen_Load(object sender, EventArgs e)
        {

        }

        protected override Army NewArmy()
        {
            Heroes.Core.Army army = new Army();
            army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Pikeman]);
            return army;
        }

    }
}
