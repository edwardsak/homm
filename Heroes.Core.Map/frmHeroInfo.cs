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
    public partial class frmHeroInfo : Form
    {
        Heroes.Core.Hero _hero;
        PictureBox[] _picSkills;

        Dictionary<Heroes.Core.Heros.EquipPartEnum, PictureBox[]> _picEquipedArtifacts;
        PictureBox[] _picUnequipedArtifacts;
        PictureBox _picSelArtifact;
        int _pageNo;
        int _pageSize;

        public frmHeroInfo()
        {
            InitializeComponent();

            _hero = null;

            _picSkills = new PictureBox[] 
            { 
                this.picSkill1, this.picSkill2, this.picSkill3, this.picSkill4,
                this.picSkill2, this.picSkill3, this.picSkill4, this.picSkill5
            };

            _picEquipedArtifacts = new Dictionary<Heroes.Core.Heros.EquipPartEnum, PictureBox[]>();
            _picEquipedArtifacts.Add(Heroes.Core.Heros.EquipPartEnum.RightHand, new PictureBox[] { picRightHand });
            _picEquipedArtifacts.Add(Heroes.Core.Heros.EquipPartEnum.LeftHand, new PictureBox[] { picLeftHand });
            _picEquipedArtifacts.Add(Heroes.Core.Heros.EquipPartEnum.Head, new PictureBox[] { picHead });
            _picEquipedArtifacts.Add(Heroes.Core.Heros.EquipPartEnum.Torso, new PictureBox[] { picTorso });
            _picEquipedArtifacts.Add(Heroes.Core.Heros.EquipPartEnum.Feet, new PictureBox[] { picFeet });
            _picEquipedArtifacts.Add(Heroes.Core.Heros.EquipPartEnum.Neck, new PictureBox[] { picNeck });
            _picEquipedArtifacts.Add(Heroes.Core.Heros.EquipPartEnum.Shoulder, new PictureBox[] { picShoulder });
            _picEquipedArtifacts.Add(Heroes.Core.Heros.EquipPartEnum.Ring, new PictureBox[] { picRingRight, picRingLeft });
            _picEquipedArtifacts.Add(Heroes.Core.Heros.EquipPartEnum.Misc, new PictureBox[] { picMisc1, picMisc2, picMisc3, picMisc4, picMisc5 });
            
            _picUnequipedArtifacts = new PictureBox[] { picBackPack1, picBackPack2, picBackPack3, picBackPack4, picBackPack5 };
            _pageSize = _picUnequipedArtifacts.Length;

            foreach (Heroes.Core.Heros.EquipPartEnum key in this._picEquipedArtifacts.Keys)
            {
                PictureBox[] picBoxs = this._picEquipedArtifacts[key];

                foreach (PictureBox picBox in picBoxs)
                {
                    picBox.Tag = key;
                    picBox.MouseClick += new MouseEventHandler(picBox_MouseClick);
                }
            }

            foreach (PictureBox picBox in _picUnequipedArtifacts)
            {
                picBox.Tag = Heroes.Core.Heros.EquipPartEnum.BackPack;
                picBox.MouseClick += new MouseEventHandler(picBox_MouseClick);
            }

            _picSelArtifact = null;
            _pageNo = 0;
        }

        private void frmHeroInfo_Load(object sender, EventArgs e)
        {

        }

        public DialogResult ShowDialog(Heroes.Core.Hero hero)
        {
            _hero = hero;

            if (System.IO.File.Exists(_hero._infoImgFileName))
                picHero.Image = Image.FromFile(_hero._infoImgFileName);
            else
                picHero.Image = null;

            this.lblHeroName.Text = _hero._name;
            this.lblHeroLevel.Text = string.Format("Level {0} {1}", _hero._level, _hero.GetHeroTypeName());
            this.lblExp.Text = _hero._experience.ToString();

            PplAtts();

            PplSkills();

            PplArtifacts();

            return this.ShowDialog();
        }

        private void PplAtts()
        {
            this.lblSpellPoint.Text = string.Format("{0}/{1}", _hero._spellPointLeft, _hero._maxSpellPoint);

            this.lblAttack.Text = _hero._attack.ToString();
            this.lblDefense.Text = _hero._defense.ToString();
            this.lblPower.Text = _hero._power.ToString();
            this.lblKnowledge.Text = _hero._knowledge.ToString();
        }

        private void PplSkills()
        {
            int index = 0;
            foreach (Heroes.Core.Skill skill in _hero._skills.Values)
            {
                if (System.IO.File.Exists(skill._infoImgFileNames[skill._level - 1]))
                    _picSkills[index].Image = Image.FromFile(skill._infoImgFileNames[skill._level - 1]);
                else
                    _picSkills[index].Image = null;

                index += 1;
            }
        }

        private void PplArtifacts()
        {
            ClearArtifacts();

            foreach (Heroes.Core.Heros.EquipPartEnum key in _hero._artifactKEquips.Keys)
            {
                Heroes.Core.Heros.Artifact[] artifacts = (Heroes.Core.Heros.Artifact[])_hero._artifactKEquips[key];

                int index = 0;
                foreach (Heroes.Core.Heros.Artifact a in artifacts)
                {
                    if (a == null)
                    {
                        index += 1;
                        continue;
                    }

                    PictureBox[] pics = _picEquipedArtifacts[key];

                    if (index > pics.Length - 1) continue;

                    PictureBox pic = pics[index];

                    if (pic.Image == null)
                    {
                        if (System.IO.File.Exists(a._infoImgFileName))
                            pic.Image = Image.FromFile(a._infoImgFileName);
                    }

                    index += 1;
                }
            }

            for (int i = 0; i < this._picUnequipedArtifacts.Length; i++)
            {
                int index = i + _pageNo * _pageSize;
                if (index < _hero._artifactUnequips.Count)
                {
                    Heroes.Core.Heros.Artifact a = (Heroes.Core.Heros.Artifact)_hero._artifactUnequips[index];
                    PictureBox pic = this._picUnequipedArtifacts[i];

                    if (System.IO.File.Exists(a._infoImgFileName))
                        pic.Image = Image.FromFile(a._infoImgFileName);
                }
            }
        }

        private void ClearArtifacts()
        {
            foreach (PictureBox[] pics in _picEquipedArtifacts.Values)
            {
                foreach (PictureBox pic in pics)
                {
                    pic.Image = null;
                }
            }

            foreach (PictureBox pic in _picUnequipedArtifacts)
            {
                pic.Image = null;
            }
        }

        #region Equip, Unequip
        void picBox_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            if (e.Button == MouseButtons.Left)
            {
                PictureBox prevPic = this._picSelArtifact;

                // must click artifact first
                if (prevPic == null && pic.Image == null) return;

                this._picSelArtifact = pic;

                pic.BorderStyle = BorderStyle.Fixed3D;

                if (prevPic == null) return;
                if (prevPic.Image == null) return;  // cannot move from empty
                if (prevPic.Equals(pic)) return;    // click on self

                try
                {
                    // get prevIndex
                    Heroes.Core.Heros.EquipPartEnum prevEquipPart = (Heroes.Core.Heros.EquipPartEnum)prevPic.Tag;

                    int prevIndex = FindIndex(prevEquipPart, prevPic);
                    if (prevIndex < 0) return;  // error, picbox is not in equip nor backpack

                    Heroes.Core.Heros.Artifact prevArtifact = GetArtifact(prevEquipPart, prevIndex);
                    if (prevArtifact == null) return;   // error;

                    // get index
                    Heroes.Core.Heros.EquipPartEnum equipPart = (Heroes.Core.Heros.EquipPartEnum)pic.Tag;

                    int index = FindIndex(equipPart, pic);
                    if (index < 0) return;  // error, picbox is not in equip nor backpack

                    Heroes.Core.Heros.Artifact artifact = GetArtifact(equipPart, index);

                    if (artifact == null)
                    {
                        // move
                        if (equipPart == Heroes.Core.Heros.EquipPartEnum.BackPack)
                        {
                            // move to backpack

                            if (prevEquipPart == Heroes.Core.Heros.EquipPartEnum.BackPack)
                                return;     // no effect move between backPack
                            else
                            {
                                ((Heroes.Core.Heros.Artifact[])_hero._artifactKEquips[prevEquipPart])[prevIndex] = null;

                                _hero._artifactUnequips.Add(prevArtifact);
                            }
                        }
                        else
                        {
                            // move to equip
                            if (prevArtifact._equipPart != equipPart)
                            {
                                MessageBox.Show("Wrong part.");
                                return;
                            }

                            if (prevEquipPart == Heroes.Core.Heros.EquipPartEnum.BackPack)
                                _hero._artifactUnequips.Remove(prevArtifact);
                            else
                                ((Heroes.Core.Heros.Artifact[])_hero._artifactKEquips[prevEquipPart])[prevIndex] = null;

                            ((Heroes.Core.Heros.Artifact[])_hero._artifactKEquips[equipPart])[index] = prevArtifact;
                        }
                    }
                    else
                    {
                        // exchange
                        if (equipPart == Heroes.Core.Heros.EquipPartEnum.BackPack)
                        {
                            if (prevEquipPart == Heroes.Core.Heros.EquipPartEnum.BackPack)
                            {
                                // no effect exchange between backpack
                                return;
                            }
                            else
                            {
                                // exchange from equip part to backpack
                                if (artifact._equipPart != prevEquipPart)
                                {
                                    MessageBox.Show("Wrong part.");
                                    return;
                                }

                                ((Heroes.Core.Heros.Artifact[])_hero._artifactKEquips[prevEquipPart])[prevIndex] = artifact;

                                _hero._artifactUnequips.Remove(artifact);
                                _hero._artifactUnequips.Add(prevArtifact);
                            }
                        }
                        else
                        {
                            if (prevEquipPart == Heroes.Core.Heros.EquipPartEnum.BackPack)
                            {
                                // exchange from backpack to equip
                                if (prevArtifact._equipPart != equipPart)
                                {
                                    MessageBox.Show("Wrong part.");
                                    return;
                                }
                                
                                ((Heroes.Core.Heros.Artifact[])_hero._artifactKEquips[equipPart])[index] = prevArtifact;
                                    
                                _hero._artifactUnequips.Remove(prevArtifact);
                                _hero._artifactUnequips.Add(artifact);
                            }
                            else
                            {
                                // exchange between equip
                                if (prevArtifact._equipPart != equipPart)
                                {
                                    MessageBox.Show("Wrong part.");
                                    return;
                                }
                                
                                if (artifact._equipPart != prevEquipPart)
                                {
                                    MessageBox.Show("Wrong part.");
                                    return;
                                }

                                ((Heroes.Core.Heros.Artifact[])_hero._artifactKEquips[equipPart])[index] = prevArtifact;
                                ((Heroes.Core.Heros.Artifact[])_hero._artifactKEquips[prevEquipPart])[prevIndex] = artifact;
                            }
                        }
                    }
                }
                finally
                {
                    _hero.CalculateAll();

                    PplAtts();
                    PplArtifacts();

                    prevPic.BorderStyle = BorderStyle.None;
                    pic.BorderStyle = BorderStyle.None;
                    this._picSelArtifact = null;
                }
            }
        }

        private int FindIndex(PictureBox[] picBoxs, PictureBox pic)
        {
            for (int i = 0; i < picBoxs.Length; i++)
            {
                PictureBox pic2 = picBoxs[i];
                if (pic2.Equals(pic)) return i;
            }

            return -1;
        }

        private int FindIndex(Heroes.Core.Heros.EquipPartEnum equipPart, PictureBox pic)
        {
            int prevIndex = -1;

            if (equipPart == Heroes.Core.Heros.EquipPartEnum.BackPack)
            {
                prevIndex = FindIndex(_picUnequipedArtifacts, pic);
                prevIndex += _pageNo * _pageSize;
            }
            else
            {
                PictureBox[] prevPicBoxs = _picEquipedArtifacts[equipPart];
                prevIndex = FindIndex(prevPicBoxs, pic);
            }

            return prevIndex;
        }

        private Heroes.Core.Heros.Artifact GetArtifact(Heroes.Core.Heros.EquipPartEnum equipPart, int index)
        {
            if (equipPart == Heroes.Core.Heros.EquipPartEnum.BackPack)
            {
                if (index > _hero._artifactUnequips.Count - 1) return null;      // error, out of index boundary
                return (Heroes.Core.Heros.Artifact)_hero._artifactUnequips[index];
            }
            else
            {
                Heroes.Core.Heros.Artifact[] artifacts = (Heroes.Core.Heros.Artifact[])_hero._artifactKEquips[equipPart];
                if (index > artifacts.Length - 1) return null;   // error, out of index boundary
                return artifacts[index];
            }
        }
        #endregion

        private void cmdPrev_Click(object sender, EventArgs e)
        {
            if (_pageNo < 1) return;
            _pageNo -= 1;

            PplArtifacts();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            decimal maxPage = (decimal)_hero._artifactUnequips.Count / (decimal)_pageSize;
            maxPage += 1;   // extra empty page

            if (_pageNo > maxPage - 1) return;
            _pageNo += 1;

            PplArtifacts();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
