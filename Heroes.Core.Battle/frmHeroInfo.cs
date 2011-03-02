using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Battle
{
    public partial class frmHeroInfo : Form
    {
        public frmHeroInfo()
        {
            InitializeComponent();

            this.lblLevel.Text = "";
            this.lblAttack.Text = "";
            this.lblDefense.Text = "";
            this.lblPower.Text = "";
            this.lblKnowledge.Text = "";
            this.lblSpellPoint.Text = "";
            this.pictureBox1.Image = null;
        }

        private void frmHeroInfo_Load(object sender, EventArgs e)
        {

        }

        public void Show(IWin32Window owner, Heroes.Core.Battle.Characters.Hero hero)
        {
            this.lblLevel.Text = string.Format("Level {0} {1}", hero._level, hero.GetHeroTypeName());
            this.lblAttack.Text = hero._attack.ToString();
            this.lblDefense.Text = hero._defense.ToString();
            this.lblPower.Text = hero._power.ToString();
            this.lblKnowledge.Text = hero._knowledge.ToString();
            this.lblSpellPoint.Text = string.Format("{0}/{1}", hero._spellPointLeft, hero._maxSpellPoint);

            Show(owner);
        }

    }
}
