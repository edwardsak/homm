using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Heroes.Core.Castle
{
    public partial class frmBuyGriffin : frmCommonBuy
    {
        public frmBuyGriffin()
        {
            InitializeComponent();
        }

        protected override Army NewArmy()
        {
            Heroes.Core.Army army = new Army();
            army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Griffin]);
            return army;
        }
    }
}
