using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes
{
    public partial class frmOtherHeroInfo : Form
    {
        Label[] _lblArmyNames;
        Label[] _lblArmyQtys;

        public frmOtherHeroInfo()
        {
            InitializeComponent();

            _lblArmyNames = new Label[] 
            {
                this.lblArmyName1, this.lblArmyName2, this.lblArmyName3, this.lblArmyName4, 
                this.lblArmyName5, this.lblArmyName6, this.lblArmyName7
            };

            _lblArmyQtys = new Label[] 
            {
                this.lblArmy1, this.lblArmy2, this.lblArmy3, this.lblArmy4, 
                this.lblArmy5, this.lblArmy6, this.lblArmy7
            };
        }

        private void frmOtherHeroInfo_Load(object sender, EventArgs e)
        {

        }

        public DialogResult ShowDialog(Heroes.Core.Hero hero)
        {
            this.lblHeroName.Text = hero._name;
            this.lblHeroLevel.Text = string.Format("Level {0} {1}", hero._level, hero.GetHeroTypeName());

            Clear();
            PplArmies(hero);

            return this.ShowDialog();
        }

        private void Clear()
        {
            foreach (Label lbl in this._lblArmyNames)
            {
                lbl.Text = "";
            }

            foreach (Label lbl in this._lblArmyQtys)
            {
                lbl.Text = "";
            }
        }

        private void PplArmies(Heroes.Core.Hero hero)
        {
            foreach (Heroes.Core.Army army in hero._armyKSlots.Values)
            {
                this._lblArmyNames[army._slotNo].Text = army._name;
                this._lblArmyQtys[army._slotNo].Text = army.GetArmySize();
            }
        }

    }
}
