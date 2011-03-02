using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Castle
{
    public partial class frmBuySpellBook : Form
    {
        public frmBuySpellBook()
        {
            InitializeComponent();
        }

        #region Buy
        private void cmdBuy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Spell book enable hero to cast Spell during battle");
            this.Close();
        }
        #endregion

        #region Close
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
