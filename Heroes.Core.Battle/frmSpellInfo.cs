using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Battle
{
    public partial class frmSpellInfo : Form
    {
        public frmSpellInfo()
        {
            InitializeComponent();
        }

        private void frmSpellInfo_Load(object sender, EventArgs e)
        {

        }

        public void Show(IWin32Window owner, Spell spell)
        {
            this.lblName.Text = spell._name;
            this.lblNameSmall.Text = spell._name;

            this.lblDesc.Text = string.Format("Does {0} points of damage.", spell._damage);

            if (System.IO.File.Exists(spell._bookImgFileName))
                this.picSpell.Image = Image.FromFile(spell._bookImgFileName);
            else
                this.picSpell.Image = null;

            this.Show(owner);
        }

    }
}
