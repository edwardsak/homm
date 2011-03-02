using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Battle
{
    public partial class frmVictory : Form
    {
        public frmVictory()
        {
            InitializeComponent();
        }

        private void frmVictory_Load(object sender, EventArgs e)
        {

        }

        public DialogResult ShowDialog(Heroes.Core.Hero attackHero, Heroes.Core.Hero defendHero, 
            Heroes.Core.Monster monster, Heroes.Core.Town defendTown)
        {


            return this.ShowDialog();
        }

    }
}
