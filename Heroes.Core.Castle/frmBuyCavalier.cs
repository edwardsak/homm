﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Castle
{
    public partial class frmBuyCavalier : frmCommonBuy
    {
        public frmBuyCavalier()
        {
            InitializeComponent();
        }

        protected override Army NewArmy()
        {
            Heroes.Core.Army army = new Army();
            army.CopyFrom((Heroes.Core.Army)Heroes.Core.Setting._armies[(int)Heroes.Core.ArmyIdEnum.Cavalier]);
            return army;
        }
    }
}
