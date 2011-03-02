using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Map
{
    public partial class frmLevelUp : Form
    {
        Heroes.Core.Hero _hero;

        PictureBox _selectedAtt;
        PictureBox _selectedSkill;

        ArrayList _skills;
        PictureBox[] _cmdAtts;
        PictureBox[] _cmdSkills;
        Label[] _lblSkillNames;

        public frmLevelUp()
        {
            InitializeComponent();

            this.picAttack.Click += new EventHandler(cmdAttack_Click);
            this.picDefense.Click += new EventHandler(cmdAttack_Click);
            this.picPower.Click += new EventHandler(cmdAttack_Click);
            this.picKnowledge.Click += new EventHandler(cmdAttack_Click);

            this.picSkill1.Click += new EventHandler(cmdSkill1_Click);
            this.picSkill2.Click += new EventHandler(cmdSkill1_Click);
            this.picSkill3.Click += new EventHandler(cmdSkill1_Click);
            this.picSkill4.Click += new EventHandler(cmdSkill1_Click);

            _skills = new ArrayList();
            _cmdAtts = new PictureBox[] { this.picAttack, this.picDefense, this.picPower, this.picKnowledge };
            _cmdSkills = new PictureBox[] { this.picSkill1, this.picSkill2, this.picSkill3, this.picSkill4 };
            _lblSkillNames = new Label[] { this.lblSkillName1, this.lblSkillName2, this.lblSkillName3, this.lblSkillName4 };
        }

        void cmdAttack_Click(object sender, EventArgs e)
        {
            ClearBorder(this._cmdAtts);

            this._selectedAtt = (PictureBox)sender;
            this._selectedAtt.BorderStyle = BorderStyle.Fixed3D;
        }

        void cmdSkill1_Click(object sender, EventArgs e)
        {
            ClearBorder(this._cmdSkills);

            this._selectedSkill = (PictureBox)sender;
            this._selectedSkill.BorderStyle = BorderStyle.Fixed3D;
        }

        private void ClearBorder(PictureBox[] pics)
        {
            foreach (PictureBox pic in pics)
            {
                pic.BorderStyle = BorderStyle.None;
            }
        }

        private void frmLevelUp_Load(object sender, EventArgs e)
        {

        }

        public DialogResult ShowDialog(Heroes.Core.Hero hero)
        {
            _hero = hero;

            lblTitle.Text = string.Format("{0} has gain a level.", hero._name);
            lblLevel.Text = string.Format("{0} is now a level {1} {2}.", hero._name, hero._level + 1, hero.GetHeroTypeName());

            if (System.IO.File.Exists(hero._infoImgFileName))
                this.picHero.Image = Image.FromFile(hero._infoImgFileName);
            else
                this.picHero.Image = null;

            GetSkillAv();
            PplSkills();

            return this.ShowDialog();
        }

        private void GetSkillAv()
        {
            _skills.Clear();

            int existingSlot = 2;
            int newSlot = 2;

            // get existing skills
            {
                Hashtable idLevels = new Hashtable();

                foreach (Skill skill in _hero._skills.Values)
                {
                    if (skill._level >= 3) continue;

                    idLevels.Add(skill._id, skill._level + 1);
                }

                if (idLevels.Count < 2)
                {
                    existingSlot = idLevels.Count;
                    newSlot = 4 - existingSlot;
                }

                ArrayList lst = new ArrayList();
                foreach (int id in idLevels.Keys)
                {
                    lst.Add(id);
                }

                // random
                GetRandomSkills(idLevels, lst, existingSlot);
            }

            // new skills
            {
                Hashtable idLevels = new Hashtable();
                for (int id = (int)Heroes.Core.SkillIdEnum.Archery; id <= (int)Heroes.Core.SkillIdEnum.WaterMagic; id++)
                {
                    idLevels.Add(id, 1);
                }

                // remove existing skills
                foreach (Skill skill in _hero._skills.Values)
                {
                    if (idLevels.ContainsKey(skill._id)) idLevels.Remove(skill._id);
                }

                ArrayList lst = new ArrayList();
                foreach (int id in idLevels.Keys)
                {
                    lst.Add(id);
                }

                if (lst.Count < newSlot) newSlot = lst.Count;

                // random
                GetRandomSkills(idLevels, lst, newSlot);
            }
        }

        private void GetRandomSkills(Hashtable idLevels, ArrayList lst, int slots)
        {
            // random
            Random rnd = new Random();
            Hashtable skills = new Hashtable();
            while (skills.Count < slots)
            {
                int index = rnd.Next(0, lst.Count);
                int id2 = (int)lst[index];
                if (skills.ContainsKey(id2)) continue;
                skills.Add(id2, id2);

                Skill skill = new Skill();
                skill.CopyFrom((Skill)Heroes.Core.Setting._skills[id2]);
                skill._level = (int)idLevels[id2];
                _skills.Add(skill);
            }
        }

        private void PplSkills()
        {
            int index = 0;
            foreach (Heroes.Core.Skill skill in _skills)
            {
                this._cmdSkills[index].Image = Image.FromFile(skill._infoImgFileNames[skill._level - 1]);
                this._cmdSkills[index].Visible = true;

                this._lblSkillNames[index].Text = string.Format("{0}\n{1}", skill.GetLevelName(), skill._name);
                this._lblSkillNames[index].Visible = true;

                index += 1;
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (_selectedAtt == null) return;
            if (_selectedSkill == null) return;

            _hero._level += 1;

            // add attribute
            {
                if (_selectedAtt.Equals(picAttack))
                    _hero._basicAttack += 1;
                else if (_selectedAtt.Equals(picDefense))
                    _hero._basicDefense += 1;
                else if (_selectedAtt.Equals(picPower))
                    _hero._basicPower += 1;
                else if (_selectedAtt.Equals(picKnowledge))
                    _hero._basicKnowledge += 1;
            }

            // add skill
            {
                int index = 0;
                foreach (PictureBox pic in this._cmdSkills)
                {
                    if (_selectedSkill.Equals(pic)) break;

                    index += 1;
                }

                Heroes.Core.Skill skill = (Heroes.Core.Skill)_skills[index];

                if (_hero._skills.ContainsKey(skill._id))
                {
                    ((Heroes.Core.Skill)_hero._skills[skill._id])._level = skill._level;
                }
                else
                {
                    _hero._skills.Add(skill._id, skill);
                }
            }

            // calculate spell damage, cost
            _hero.CalculateAll();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
