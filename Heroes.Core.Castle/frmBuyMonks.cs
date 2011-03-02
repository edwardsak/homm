using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Castle
{
    public partial class frmBuyMonks : frmCommonBuy
    {
        public frmBuyMonks()
        {
            InitializeComponent();
        }

        protected override Army NewArmy()
        {
            Heroes.Core.Army army = new Army();
            army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Monk]);
            return army;
        }
    }
}
