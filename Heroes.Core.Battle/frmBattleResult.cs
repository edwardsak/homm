using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Battle
{
    public partial class frmBattleResult : Form
    {
        public int _experience;

        public frmBattleResult()
        {
            InitializeComponent();

            _experience = 0;

            // A glorious victory!
            // For valor in combat, [heroname] receives [exp] experience

            // Your forces suffer a bitter defeat, and [heroname] abandons your cause

            // The cowardly [heroname] flees from battle.
        }

        private void frmBattleResult_Load(object sender, EventArgs e)
        {

        }

        public DialogResult ShowDialog(int resultType, object victory, int invokerPlayerId,
            Heroes.Core.Hero attackHero, Heroes.Core.Hero defendHero, Heroes.Core.Monster monster)
        {
            lblName1.Text = attackHero._name;

            if (defendHero != null)
            {
                lblName2.Text = defendHero._name;
            }
            else if (monster != null)
            {
                lblName2.Text = monster._name;
            }

            if (victory.Equals(attackHero))
            {
                lblStatus1.Text = "Victorious";
                lblStatus2.Text = "Defeated";
            }
            else
            {
                lblStatus1.Text = "Defeated";
                lblStatus2.Text = "Victorious";
            }

            string name = "";
            if (attackHero._playerId == invokerPlayerId)
            {
                name = attackHero._name;
            }
            else if (defendHero != null && defendHero._playerId == invokerPlayerId)
            {
                name = defendHero._name;
            }

            PplCasualties(1, attackHero._armyKSlots);
            if (defendHero != null) PplCasualties(2, defendHero._armyKSlots);
            if (monster != null) PplCasualties(2, monster._armyKSlots);

            if (resultType == 1)
            {
                if (victory.Equals(attackHero))
                {
                    if (defendHero != null)
                        _experience = CalculateExp(defendHero._armyKSlots);
                    else if (monster != null)
                        _experience = CalculateExp(monster._armyKSlots);
                }
                else if (defendHero != null && victory.Equals(defendHero))
                    _experience = CalculateExp(attackHero._armyKSlots);

                System.Text.StringBuilder sb = new StringBuilder();
                sb.Append("A glorious victory!\n\n");
                sb.AppendFormat("For valor in combat, {0} receives {1} experience", name, _experience);
                lblStatusMsg.Text = sb.ToString();
            }
            else if (resultType == 2)
            {
                lblStatusMsg.Text = string.Format("Your forces suffer a bitter defeat, and {0} abandons your cause", name);
            }
            else if (resultType == 3)
            {
                lblStatusMsg.Text = string.Format("The cowardly {0} flees from battle.", name);
            }

            return this.ShowDialog();
        }

        private void PplCasualties(int side, Hashtable armies)
        { 
        }

        private int CalculateExp(Hashtable armies)
        {
            int exp = 0;
            foreach (Heroes.Core.Army army in armies.Values)
            {
                exp += army._experience * (army._qty - army._qtyLeft);
            }

            return exp;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
