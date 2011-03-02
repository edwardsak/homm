using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Battle
{
    public partial class frmArmyInfo : Form
    {
        public frmArmyInfo()
        {
            InitializeComponent();

            this.lblName.Text = "";
            this.lblAttack.Text = "";
            this.lblDefense.Text = "";
            this.lblDamage.Text = "";
            this.lblShot.Text = "";
            this.lblHealth.Text = "";
            this.lblHealthRemain.Text = "";
            this.lblSpeed.Text = "";
            this.picPortrait.Image = null;
        }

        private void frmArmyInfo_Load(object sender, EventArgs e)
        {

        }

        public void Show(IWin32Window owner, Heroes.Core.Battle.Characters.Armies.Army army)
        {
            this.picPortrait.Image = army._portrait;

            this.lblName.Text = army._name;

            // attack
            {
                System.Text.StringBuilder sbAttack = new StringBuilder();
                sbAttack.AppendFormat("{0}", army._basicAttack);
                if (army._basicAttack != army._attack)
                    sbAttack.AppendFormat("({0})", army._attack);
                this.lblAttack.Text = sbAttack.ToString();
            }

            // defense
            {
                System.Text.StringBuilder sbDefense = new StringBuilder();
                sbDefense.AppendFormat("{0}", army._basicDefense);
                if (army._basicDefense != army._defense)
                    sbDefense.AppendFormat("({0})", army._defense);
                this.lblDefense.Text = sbDefense.ToString();
            }

            // speed
            {
                System.Text.StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}", army._basicSpeed);
                if (army._basicSpeed != army._speed)
                    sb.AppendFormat("({0})", army._speed);
                this.lblSpeed.Text = sb.ToString();
            }

            this.lblDamage.Text = string.Format("{0}-{1}", army._minDamage, army._maxDamage);

            if (army._hasRangeAttack)
                this.lblShot.Text = army._shotRemain.ToString();
            else
                this.lblShot.Text = "";

            // health
            {
                System.Text.StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}", army._basicHealth);
                if (army._basicHealth != army._health)
                    sb.AppendFormat("({0})", army._health);
                this.lblHealth.Text = sb.ToString();
            }
            
            if (army._isDead)
                this.lblHealthRemain.Text = "Dead";
            else
                this.lblHealthRemain.Text = army._healthRemain.ToString();

            this.Show(owner);
        }

    }
}
