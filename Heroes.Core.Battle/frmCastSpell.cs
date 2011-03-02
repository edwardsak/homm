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
    public partial class frmCastSpell : Form
    {
        Heroes.Core.Battle.Characters.Hero _hero;
        ArrayList _spells;
        int _currentPage;
        Panel[] _panelSpells;
        Label[] _lblSpells;
        Label[] _lblNames;

        frmSpellInfo _frmSpellInfo;

        public frmCastSpell()
        {
            InitializeComponent();

            SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw 
                | ControlStyles.UserPaint, true);

            _hero = null;
            _spells = new ArrayList();
            _currentPage = 0;

            _frmSpellInfo = null;
            
            _panelSpells = new Panel[] 
            { 
                this.panelSpell1, this.panelSpell2, this.panelSpell3, this.panelSpell4, this.panelSpell5, this.panelSpell6, 
                this.panelSpell7, this.panelSpell8, this.panelSpell9, this.panelSpell10, this.panelSpell11, this.panelSpell12
            };

            _lblSpells = new Label[]
            {
                this.lblSpell1, this.lblSpell2, this.lblSpell3, this.lblSpell4, this.lblSpell5, this.lblSpell6,
                this.lblSpell7, this.lblSpell8, this.lblSpell9, this.lblSpell10, this.lblSpell11, this.lblSpell12
            };

            _lblNames = new Label[]
            {
                this.lblSpellName1, this.lblSpellName2, this.lblSpellName3, this.lblSpellName4, this.lblSpellName5, this.lblSpellName6,
                this.lblSpellName7, this.lblSpellName8, this.lblSpellName9, this.lblSpellName10, this.lblSpellName11, this.lblSpellName12
            };

            this.panelCancel.Click += new EventHandler(panelCancel_Click);

            foreach (Panel p in _panelSpells)
            {
                p.MouseClick += new MouseEventHandler(p_MouseClick);
                p.MouseDown += new MouseEventHandler(p_MouseDown);
                p.MouseUp += new MouseEventHandler(p_MouseUp);
            }
        }

        private void frmCastSpell_Load(object sender, EventArgs e)
        {

        }

        public DialogResult ShowDialog(Heroes.Core.Battle.Characters.Hero hero)
        {
            _hero = hero;

            foreach (Heroes.Core.Spell spell in hero._spells.Values)
            {
                _spells.Add(spell);
            }

            this.lblSpellPoint.Text = _hero._spellPointLeft.ToString();

            Clear();
            PplSpells();

            return this.ShowDialog();
        }

        private void PplSpells()
        {
            int index = _currentPage * 12;
            int spellIndex = 0;
            while (index < _spells.Count)
            {
                Heroes.Core.Spell spell = (Heroes.Core.Spell)_spells[index];

                if (System.IO.File.Exists(spell._bookImgFileName))
                    this._panelSpells[spellIndex].BackgroundImage = Image.FromFile(spell._bookImgFileName);
                else
                    this._panelSpells[spellIndex].BackgroundImage = null;

                this._lblNames[spellIndex].Text = spell._name;
                this._lblSpells[spellIndex].Text = string.Format("Level {0}\nSpell Points: {1}", spell._level, spell._cost);

                spellIndex += 1;
                index += 1;
            }
        }

        private void Clear() 
        {
            foreach (Panel p in _panelSpells)
            {
                p.BackgroundImage = null;
            }

            foreach (Label lbl in _lblSpells)
            {
                lbl.Text = "";
            }

            foreach (Label lbl in _lblNames)
            {
                lbl.Text = "";
            }
        }

        void panelCancel_Click(object sender, EventArgs e)
        {
            _hero._currentSpell = null;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        void p_MouseClick(object sender, MouseEventArgs e)
        {
            Panel p = (Panel)sender;

            if (e.Button == MouseButtons.Left)
            {
                int index = GetSpellIndex(p);
                if (index < 0) return;

                _hero._currentSpell = (Heroes.Core.Spell)_spells[index];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private int GetSpellIndex(Panel p)
        {
            int index = 0;
            foreach (Panel p2 in this._panelSpells)
            {
                if (p.Equals(p2))
                {
                    break;
                }

                index += 1;
            }

            index += _currentPage * 12;

            if (index >= _spells.Count) return -1;
            else return index;
        }

        void p_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Panel p = (Panel)sender;

                int index = GetSpellIndex(p);
                if (index < 0) return;

                Spell spell = (Heroes.Core.Spell)_spells[index];

                _frmSpellInfo = new frmSpellInfo();
                _frmSpellInfo.StartPosition = FormStartPosition.Manual;

                Point pt = this.PointToScreen(new Point(e.X, e.Y));
                _frmSpellInfo.Location = pt;

                _frmSpellInfo.Show(this, spell);
            }
        }

        void p_MouseUp(object sender, MouseEventArgs e)
        {
            if (_frmSpellInfo != null)
            {
                _frmSpellInfo.Close();
                _frmSpellInfo.Dispose();
                _frmSpellInfo = null;
            }
        }

    }
}
