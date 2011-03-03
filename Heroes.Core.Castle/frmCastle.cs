using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Heroes.Core.Castle
{
    public partial class frmCastle : Form
    {
        private bool _readOnly;

        string _curDir;
        string cur;

        public static Building _buildingName = null;
        public static Dictionary<int, Building> build = new Dictionary<int, Building>();

        Town _town;

        Label[] lblTownGold;
        Label[] lblResources;
        Label[] lblTime;

        Label[] _lblArmyInCastles;
        Label[] _lblArmyVisits;
        Label _lblSelArmy;  // selected army

        Rectangle _villageHallRect;
        MyButton _buttonHall;
        MyButton _buttonFort;
        MyButton _buttonShipyard;

        MyButton _buttonMage1;
        MyButton _buttonMage2;
        MyButton _buttonMage3;
        MyButton _buttonMage4;

        public frmCastle()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw 
                | ControlStyles.UserPaint, true);
            
            #region Castle images click
            _villageHallRect = new Rectangle(0, 220, 216, 129);

            _buttonHall = new MyButton(0, 220, 216, 129);
            _buttonHall.Clicked += new MyButton.ClickedEventHandler(_buttonHall_Clicked);

            _buttonFort = new MyButton(596, 64, 205, 124);
            _buttonFort.Clicked += new MyButton.ClickedEventHandler(_buttonFort_Clicked);

            _buttonShipyard = new MyButton(517, 199, 70, 30);
            _buttonShipyard.Clicked += new MyButton.ClickedEventHandler(_buttonShipyard_Clicked);

            _buttonMage1 = new MyButton(705, 160, 93, 103);
            _buttonMage1.Clicked += new MyButton.ClickedEventHandler(_buttonMage1_Clicked);

            _buttonMage2 = new MyButton(705, 129, 93, 31);
            _buttonMage2.Clicked += new MyButton.ClickedEventHandler(_buttonMage2_Clicked);

            _buttonMage3 = new MyButton(702, 101, 96, 28);
            _buttonMage3.Clicked += new MyButton.ClickedEventHandler(_buttonMage3_Clicked);

            _buttonMage4 = new MyButton(702, 70, 96, 31);
            _buttonMage4.Clicked += new MyButton.ClickedEventHandler(_buttonMage4_Clicked);

            this.castleView1.MouseClick += new MouseEventHandler(castleView1_MouseClick);
            #endregion

            _curDir = Application.StartupPath;

            _lblSelArmy = null;

            #region Label
            //this.panel1.Hide();
            
            this.lblTownGold = new Label[] { this.lblGold };
            this.lblResources = new Label[] { this.lblWood, this.lblMercury, this.lblOre, this.lblSulfur, this.lblCrystal, this.lblGem };
            this.lblTime = new Label[] { this.lblMonth, this.lblWeek, this.lblDay };

            this._lblArmyInCastles = new Label[] { this.lblArmyInCastle1, this.lblArmyInCastle2, this.lblArmyInCastle3, this.lblArmyInCastle4, 
                this.lblArmyInCastle5, this.lblArmyInCastle6, this.lblArmyInCastle7 };
            this._lblArmyVisits = new Label[] { this.lblArmyVisit1, this.lblArmyVisit2, this.lblArmyVisit3, this.lblArmyVisit4, 
                this.lblArmyVisit5, this.lblArmyVisit6, this.lblArmyVisit7 };
            #endregion

            foreach (Label lbl in this._lblArmyInCastles)
            {
                lbl.MouseClick += new MouseEventHandler(lbl_MouseClick);
            }

            foreach (Label lbl in this._lblArmyVisits)
            {
                lbl.MouseClick += new MouseEventHandler(lbl_MouseClick);
            }
        }

        void _buttonMage1_Clicked(object sender)
        {
            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel1)) return;

            frmBuySpellBook f = new frmBuySpellBook();
            f.ShowDialog();
        }
        void _buttonMage2_Clicked(object sender)
        {
            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel2)) return;
            frmBuySpellBook f = new frmBuySpellBook();
            f.ShowDialog();
        }
        void _buttonMage3_Clicked(object sender)
        {
            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel3)) return;
            frmBuySpellBook f = new frmBuySpellBook();
            f.ShowDialog();
        }
        void _buttonMage4_Clicked(object sender)
        {
            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel4)) return;
            frmBuySpellBook f = new frmBuySpellBook();
            f.ShowDialog();
        }

        private void frmCastle_Load(object sender, EventArgs e)
        {
            
        }

        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
            }
        }

        public DialogResult ShowDialog(Town town)
        {
            _town = town;

            PplTown();

            this.castleView1._town = town;
            this.castleView1.Draw();

            return this.ShowDialog();
        }

        void castleView1_MouseClick(object sender, MouseEventArgs e)
        {
            _buttonHall.Click(e.X, e.Y);
            _buttonFort.Click(e.X, e.Y);
            _buttonShipyard.Click(e.X, e.Y);
            _buttonMage1.Click(e.X, e.Y);
            _buttonMage2.Click(e.X, e.Y);
            _buttonMage3.Click(e.X, e.Y);
            _buttonMage4.Click(e.X, e.Y);
        }

        void _buttonFort_Clicked(object sender)
        {
            //if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Citadel)) return;

            frmFort f = new frmFort();
            //f.HasEmptySlot = HasEmptySlot;
            //f.HasEmptySlot2 = HasEmptySlot;
            //f.HasEmptySlot3 = HasEmptySlot;
            //f.HasEmptySlot4 = HasEmptySlot;
            //f.HasEmptySlot5 = HasEmptySlot;
            //f.HasEmptySlot6 = HasEmptySlot;
            //f.HasEmptySlot7 = HasEmptySlot;
            f.ShowDialog(_town);

            PplArmies(this._lblArmyInCastles, _town._armyInCastleKSlots);
        }

        void _buttonHall_Clicked(object sender)
        {
            frmTown f = new frmTown();
            if (f.ShowDialog(_town) != DialogResult.OK) return;
            
            this.castleView1.Draw();
        }

        void _buttonShipyard_Clicked(object sender)
        {
            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Shipyard)) return;

            frmShipyard f = new frmShipyard();
            f.ShowDialog(_town);
        }

        void txtArmyInCastle1_MouseClick(object sender, MouseEventArgs e)
        {
            //TextBox txtBox = (TextBox)sender;

            //if (_txtCurrentArmy != null)
            //{
            //    try
            //    {
            //        if (_town._heroVisit == null) return;

            //        int castleSlotNo = FindSlotNo(this._lblArmyInCastles, _txtCurrentArmy);
            //        if (castleSlotNo < 0) return;

            //        // move army from castle to vising hero
            //        if (HasTextBox(this.txtArmyInCastles, _txtCurrentArmy) && HasTextBox(this.txtHeroArmies, txtBox))
            //        {
            //            //int castleSlotNo = FindSlotNo(this.txtArmyInCastles, _txtCurrentArmy);
            //            if (castleSlotNo < 0) return;

            //            int visitSlotNo = FindSlotNo(this.txtHeroArmies, txtBox);
            //            if (visitSlotNo < 0) return;

            //            MoveArmyFromCastleToVisit(castleSlotNo, visitSlotNo);

            //            PplArmies(this._lblArmyVisits, _town._heroVisit._armyKSlots);
            //            PplArmies(this._lblArmyInCastles, _town._armyInCastleKSlots);
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}

            //_txtCurrentArmy = txtBox;
        }

        #region Slot to slot
        void lbl_MouseClick(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;

            Label prevlbl = this._lblSelArmy;
            this._lblSelArmy = lbl;

            // show selected
            if (prevlbl != null) prevlbl.BorderStyle = BorderStyle.None;
            lbl.BorderStyle = BorderStyle.Fixed3D;

            // skip if not previous selected any slot
            if (prevlbl == null) return;

            int slotNoInCastle = FindSlotNo(this._lblArmyInCastles, lbl);
            int slotNoVisit = FindSlotNo(this._lblArmyVisits, lbl);

            // skip if not click InCastle or Visit slot
            if (slotNoInCastle < 0 && slotNoVisit < 0)
            {
                return;
            }

            int prevSlotNoInCastle = FindSlotNo(this._lblArmyInCastles, prevlbl);
            int prevSlotNoVisit = FindSlotNo(this._lblArmyVisits, prevlbl);

            // set selected if not yet click on any army
            if (prevSlotNoInCastle < 0 && prevSlotNoVisit < 0)
            {
                // error
                return;
            }

            // move or merge or exchange slot between army InCastles
            if (prevSlotNoInCastle > -1 && slotNoInCastle > -1)
            {
                // skip if click on self
                if (prevSlotNoInCastle == slotNoInCastle) return;

                if (!SlotToSlot(prevSlotNoInCastle, _town._armyInCastleKSlots, slotNoInCastle, _town._armyInCastleKSlots)) return;

                PplArmies(this._lblArmyInCastles, _town._armyInCastleKSlots);
            }
            // move / merge / exchange army from InCastle to visit
            else if (prevSlotNoInCastle > -1 && slotNoVisit > -1)
            {
                // skip if no visiting hero
                if (_town._heroVisit == null) return;

                if (!SlotToSlot(prevSlotNoInCastle, _town._armyInCastleKSlots, slotNoVisit, _town._heroVisit._armyKSlots)) return;

                PplArmies(this._lblArmyInCastles, _town._armyInCastleKSlots);
                PplArmies(this._lblArmyVisits, _town._heroVisit._armyKSlots);
            }
            // move / merge / exchange army from visit to InCastle
            else if (prevSlotNoVisit > -1 && slotNoInCastle > -1)
            {
                // skip if no visiting hero
                if (_town._heroVisit == null) return;

                if (!SlotToSlot(prevSlotNoVisit, _town._heroVisit._armyKSlots, slotNoInCastle, _town._armyInCastleKSlots)) return;

                PplArmies(this._lblArmyInCastles, _town._armyInCastleKSlots);
                PplArmies(this._lblArmyVisits, _town._heroVisit._armyKSlots);
            }
            // move or merge or exchange slot between army visits
            else if (slotNoVisit > -1 && prevSlotNoVisit > -1)
            {
                // skip if click on self
                if (prevSlotNoVisit == slotNoVisit) return;

                // skip if no visiting hero
                if (_town._heroVisit == null) return;

                if (!SlotToSlot(prevSlotNoVisit, _town._heroVisit._armyKSlots, slotNoVisit, _town._heroVisit._armyKSlots)) return;

                PplArmies(this._lblArmyVisits, _town._heroVisit._armyKSlots);
            }

            // done
            // clear selection
            prevlbl.BorderStyle = BorderStyle.None;
            lbl.BorderStyle = BorderStyle.None;

            this._lblSelArmy = null;
        }

        private bool SlotToSlot(int prevSlotNo, Hashtable prevArmies, int slotNo, Hashtable armies)
        {
            // get previous army InCastle
            if (!prevArmies.ContainsKey(prevSlotNo))
            {
                // error
                return false;
            }
            Army prevArmy = (Army)prevArmies[prevSlotNo];

            if (armies.ContainsKey(slotNo))
            {
                // merge / exchange slot
                Army army = (Army)armies[slotNo];

                if (army._id == prevArmy._id)
                {
                    // merge

                    if (prevArmies.Count <= 1)
                    {
                        // hero must has at least one slot
                        if (IsHeroEmptyAfterRemove(prevArmies))
                        {
                            MessageBox.Show("Hero must has a least 1 army.");
                            return false;
                        }
                    }

                    army._qty += prevArmy._qty;
                    prevArmies.Remove(prevSlotNo);

                    // set hero Id
                    army._heroId = GetHeroId(armies);
                }
                else
                {
                    // exchange slot
                    prevArmy._slotNo = slotNo;
                    army._slotNo = prevSlotNo;

                    armies[slotNo] = prevArmy;
                    prevArmies[prevSlotNo] = army;

                    // set hero Id
                    prevArmy._heroId = GetHeroId(armies);
                    army._heroId = GetHeroId(prevArmies);
                }
            }
            else
            {
                // move

                // if not move in same armies, check empy armies
                if (!prevArmies.Equals(armies) && prevArmies.Count <= 1)
                {
                    // hero must has at least one slot
                    if (IsHeroEmptyAfterRemove(prevArmies))
                    {
                        MessageBox.Show("Hero must has a least 1 army.");
                        return false;
                    }
                }

                prevArmy._slotNo = slotNo;

                if (armies.ContainsKey(slotNo))
                    armies[slotNo] = prevArmy;
                else
                    armies.Add(slotNo, prevArmy);

                prevArmies.Remove(prevSlotNo);

                // set hero Id
                prevArmy._heroId = GetHeroId(armies);
            }

            return true;
        }

        private bool IsHeroEmptyAfterRemove(Hashtable prevArmies)
        {
            // hero must has at least one slot
            if ((this._town._heroVisit != null && prevArmies.Equals(this._town._heroVisit._armyKSlots))
                || (this._town._heroInCastle != null && prevArmies.Equals(this._town._heroInCastle._armyKSlots)))
                return true;

            return false;
        }

        private int GetHeroId(Hashtable armies)
        {
            // set hero Id
            if (_town._heroVisit != null && _town._heroVisit._armyKSlots.Equals(armies))
                return _town._heroVisit._id;
            else if (_town._heroInCastle != null && _town._heroInCastle._armyKSlots.Equals(armies))
                return _town._heroVisit._id;
            else
                return 0;
        }
        #endregion

        private void MoveArmyFromCastleToVisit(int castleSlotNo, int visitSlotNo)
        {
            if (!_town._armyInCastleKSlots.ContainsKey(castleSlotNo)) return;
            Heroes.Core.Army armyInCastle = (Heroes.Core.Army)_town._armyInCastleKSlots[castleSlotNo];

            // add to visiting hero army
            try
            {
                if (_town._heroVisit._armyKSlots.ContainsKey(visitSlotNo))
                {
                    Heroes.Core.Army army = (Heroes.Core.Army)_town._heroVisit._armyKSlots[visitSlotNo];
                    army._qty += armyInCastle._qty;
                }

                else
                {
                    Heroes.Core.Army army = new Army();
                    army._qty = armyInCastle._qty;
                    _town._heroVisit._armyKSlots.Add(visitSlotNo, armyInCastle);
                }


                // remove army in castle
                _town._armyInCastleKSlots.Remove(castleSlotNo);
            }
            catch
            {
            }
        }

        #region List out all the information
        private void PplTown()
        {
            PplGold1(lblTownGold, _town._player._gold);
            PplResources1(lblResources, _town._player._wood, _town._player._mercury, _town._player._ore, _town._player._sulfur, _town._player._crystal, _town._player._gem);
            //PplTIMES(lblTime, );
            PplArmies(this._lblArmyInCastles, _town._armyInCastleKSlots);

            PplVisitingHero();
        }

        private void PplGold1(Label[] txtBoxs, int money)
        {
            lblGold.Text = money.ToString();
        }

        private void PplResources1(Label[] txtBoxs, int wood, int mercury, int ore, int sulfur, int crystal, int gem)
        {
            lblWood.Text = wood.ToString();
            lblMercury.Text = mercury.ToString();
            lblOre.Text = ore.ToString();
            lblSulfur.Text = sulfur.ToString();
            lblCrystal.Text = crystal.ToString();
            lblGem.Text = gem.ToString();
        }

        private void PplTIMES(Label[] txtBoxs, int mth, int wk, int dy )
        {
            lblMonth.Text = mth.ToString();
            lblWeek.Text = wk.ToString();
            lblDay.Text = dy.ToString();
        }
        #endregion

        #region Get and Show Hero Army Team
        private void PplVisitingHero()
        {
            if (_town._heroVisit == null) return;

            if (System.IO.File.Exists(_town._heroVisit._infoImgFileName))
                this.lblHeroVisit.Image = Image.FromFile(_town._heroVisit._infoImgFileName);
            else
                this.lblHeroVisit.Image = null;

            PplArmies(this._lblArmyVisits, _town._heroVisit._armyKSlots);
        }

        private void PplArmies(Label[] txtBoxs, Hashtable armyKSlots)
        {
            foreach (Label txtBox in txtBoxs)
            {
                txtBox.Text = "";
                txtBox.Image = null;
            }

            foreach (Army army in armyKSlots.Values)
            {
                if (System.IO.File.Exists(army._slotImgFileName))
                    txtBoxs[army._slotNo].Image = Image.FromFile(army._slotImgFileName);

                txtBoxs[army._slotNo].Text = army._qty.ToString();
            }
        }
        #endregion

        private void AddToArmyInCastle(Hashtable armyPurKIds)
        {
            // add army to armyInCastle
            foreach (Army armyPur in armyPurKIds.Values)
            {
                Army army = Heroes.Core.Town.FindArmy(_town._armyInCastleKSlots, armyPur._id);

                if (army != null)
                {
                    army._qty += armyPur._qty;
                }
                else
                {
                    // find empty slot
                    int slotNo = FindEmptySlot();
                    if (slotNo < 0)
                    {
                        MessageBox.Show("error");
                        return;
                    }
                    armyPur._slotNo = slotNo;

                    _town._armyInCastleKSlots.Add(slotNo, armyPur);
                }
            }
        }

        private int FindSlotNo(Label[] txtBoxs, Label txtBox)
        {
            for (int i = 0; i < txtBoxs.Length; i++)
            {
                if (txtBox.Equals(txtBoxs[i])) return i;
            }

            return -1;
        }

        private bool HasTextBox(TextBox[] txtBoxs, TextBox txtBox)
        {
            foreach (TextBox txtBox1 in txtBoxs)
            {
                if (txtBox1.Equals(txtBox))
                    return true;
            }

            return false;
        }

        private bool HasEmptySlot()
        {
            if (_town._armyInCastleKSlots.Count < 7) return true;
            return false;
        }

        private int FindEmptySlot()
        {
            for (int slotNo = 0; slotNo < 7; slotNo++)
            {
                if (!_town._armyInCastleKSlots.ContainsKey(slotNo)) return slotNo;
            }

            return -1;
        }

        #region Exit
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Split
        private void cmdSplit_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Draw
        protected override void OnPaint(PaintEventArgs e)
        {
            //Draw();

            base.OnPaint(e);
        }

        private void Draw2()
        { 
            Bitmap bmp = new Bitmap(this.castleView1.ClientSize.Width, this.castleView1.ClientSize.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // clear
                g.Clear(Color.White);

                Image bg = Heroes.Core.Castle.Properties.Resources.TBcsback;
                g.DrawImage(bg, 0, 0, bg.Width, bg.Height);

                // sort by orderSeq
                ArrayList buildings = new ArrayList();
                foreach (Building building in _town._buildingKIds.Values)
                {
                    buildings.Add(building);
                }
                buildings.Sort(new Heroes.Core.CompareOrderSeq());

                // draw buildings
                foreach (Building building in buildings)
                {
                    if (!System.IO.File.Exists(building._imgFileName)) continue;

                    Image img = Image.FromFile(building._imgFileName);
                    g.DrawImage(img, building._point.X, building._point.Y, img.Width, img.Height);
                }
                




                //#region Town Building
                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.TownHall))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.TownHall;
                //    //Image imgBg = Image.FromFile(string.Format(@"{0}\TownHall.png", cur));
                //    g.DrawImage(imgBg, 12, 176, imgBg.Width, imgBg.Height);

                //    Image imgT = Heroes.Core.Castle.Properties.Resources.BasicTavern;
                //    g.DrawImage(imgT, 5, 232, imgT.Width, imgT.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Citadel))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Citadel;
                //    g.DrawImage(imgBg, 477, 66, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.BrotherhoodOfTheSword))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Tavern;
                //    g.DrawImage(imgBg, 0, 205, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Blacksmith))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Blacksmith;
                //    g.DrawImage(imgBg, 215, 250, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Marketplace))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Marketplace;
                //    g.DrawImage(imgBg, 413, 263, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel1))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.MageGuildLevel1;
                //    g.DrawImage(imgBg, 705, 160, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Shipyard))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Shipyard;
                //    g.DrawImage(imgBg, 477, 133, imgBg.Width, imgBg.Height);

                //    if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Citadel))
                //    {
                //        Image imgBg1 = Heroes.Core.Castle.Properties.Resources.ShipyardNoRiver;
                //        g.DrawImage(imgBg1, 477, 133, imgBg1.Width, imgBg1.Height);
                //    }
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Stables))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Stable;
                //    g.DrawImage(imgBg, 385, 193, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.GriffinBastion))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.GriffinBastion;
                //    g.DrawImage(imgBg, 74, 55, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Ship))
                //{
                //    Image imgShip = Heroes.Core.Castle.Properties.Resources.Ship;
                //    g.DrawImage(imgShip, 477, 132, imgShip.Width, imgShip.Height);

                //    if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Citadel))
                //    {
                //        Image imgBg1 = Heroes.Core.Castle.Properties.Resources.ShipNoRiver;
                //        g.DrawImage(imgBg1, 477, 133, imgBg1.Width, imgBg1.Height);
                //    }
                //}
                //#endregion

                //#region Army Camp
                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Guardhouse))
                //{
                //    //Image imgBg = Image.FromFile(string.Format(@"{0}\GuardHouse.png", cur));
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.GuardHouse;
                //    g.DrawImage(imgBg, 305, 90, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.ArchersTower))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.ArcherTower;
                //    g.DrawImage(imgBg, 360, 130, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.GriffinTower))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.GriffinTower;
                //    g.DrawImage(imgBg, 74, 55, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Barracks))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Barracks;
                //    g.DrawImage(imgBg, 176, 100, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Monastery))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Monastery;
                //    g.DrawImage(imgBg, 564, 210, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.TrainingGrounds))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.TrainingGround;
                //    g.DrawImage(imgBg, 175, 190, imgBg.Width, imgBg.Height);

                //    Image imgBg1 = Heroes.Core.Castle.Properties.Resources.TownHall;
                //    g.DrawImage(imgBg1, 12, 176, imgBg1.Width, imgBg1.Height);

                //    Image imgT = Heroes.Core.Castle.Properties.Resources.BasicTavern;
                //    g.DrawImage(imgT, 5, 232, imgT.Width, imgT.Height);

                //    if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Tavern))
                //    {
                //        Image img = Heroes.Core.Castle.Properties.Resources.Tavern;
                //        g.DrawImage(img, 0, 205, img.Width, img.Height);
                //    }
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.PortalofGlory))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.PortalOfGlory;
                //    g.DrawImage(imgBg, 300, 0, imgBg.Width, imgBg.Height);
                //}
                //#endregion

                //#region Army Upgrade Building
                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgGuardhouse))
                //{
                //    //Image imgBg = Image.FromFile(string.Format(@"{0}\GuardHouse.png", cur));
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.GuardhouseUpg2;
                //    g.DrawImage(imgBg, 305, 65, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgArchersTower))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.ArcherTowerUpg2;
                //    g.DrawImage(imgBg, 360, 115, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgGriffinTower))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.GriffinTowerUpg2;
                //    g.DrawImage(imgBg, 74, 55, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgBarracks))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.BarracksUpg2;
                //    g.DrawImage(imgBg, 176, 100, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgMonastery))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Monastery2;
                //    g.DrawImage(imgBg, 564, 172, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgTrainingGrounds))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.TrainingGroundUpg2;
                //    g.DrawImage(imgBg, 175, 190, imgBg.Width, imgBg.Height);

                //    Image imgBg1 = Heroes.Core.Castle.Properties.Resources.TownHall;
                //    g.DrawImage(imgBg1, 12, 176, imgBg1.Width, imgBg1.Height);

                //    Image imgT = Heroes.Core.Castle.Properties.Resources.BasicTavern;
                //    g.DrawImage(imgT, 5, 232, imgT.Width, imgT.Height);

                //    if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Tavern))
                //    {
                //        Image img = Heroes.Core.Castle.Properties.Resources.Tavern;
                //        g.DrawImage(img, 0, 205, img.Width, img.Height);
                //    }
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgPortalofGlory))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.PortalofGloryUpg2;
                //    g.DrawImage(imgBg, 300, 0, imgBg.Width, imgBg.Height);
                //}
                //#endregion

                //#region Town Upgrade Building
                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.CityHall))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.CityHallOut;
                //    g.DrawImage(imgBg, 12, 164, imgBg.Width, imgBg.Height);

                //    Image imgT = Heroes.Core.Castle.Properties.Resources.BasicTavern;
                //    g.DrawImage(imgT, 5, 232, imgT.Width, imgT.Height);

                //    if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.BrotherhoodOfTheSword))
                //    {
                //        Image imgBg1 = Heroes.Core.Castle.Properties.Resources.Tavern;
                //        g.DrawImage(imgBg1, 0, 205, imgBg1.Width, imgBg1.Height);
                //    }
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Capitol))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.CapitalOut;
                //    g.DrawImage(imgBg, 12, 154, imgBg.Width, imgBg.Height);

                //    Image imgT = Heroes.Core.Castle.Properties.Resources.BasicTavern;
                //    g.DrawImage(imgT, 5, 232, imgT.Width, imgT.Height);

                //    if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.BrotherhoodOfTheSword))
                //    {
                //        Image imgBg1 = Heroes.Core.Castle.Properties.Resources.Tavern;
                //        g.DrawImage(imgBg1, 0, 205, imgBg1.Width, imgBg1.Height);
                //    }
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Castle))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.CastleOut;
                //    g.DrawImage(imgBg, 477, 37, imgBg.Width, imgBg.Height);

                //    if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel1))
                //    {
                //        Image imgBg1 = Heroes.Core.Castle.Properties.Resources.MageGuildLevel1;
                //        g.DrawImage(imgBg1, 705, 160, imgBg1.Width, imgBg1.Height);
                //    }

                //    if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Shipyard))
                //    {
                //        Image imgBg2 = Heroes.Core.Castle.Properties.Resources.Shipyard;
                //        g.DrawImage(imgBg2, 477, 133, imgBg2.Width, imgBg2.Height);
                //    }

                //    if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Monastery))
                //    {
                //        Image imgBg3 = Heroes.Core.Castle.Properties.Resources.Monastery;
                //        g.DrawImage(imgBg3, 564, 210, imgBg3.Width, imgBg3.Height);
                //    }
                //    if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgMonastery))
                //    {
                //        Image imgBg4 = Heroes.Core.Castle.Properties.Resources.Monastery2;
                //        g.DrawImage(imgBg4, 564, 172, imgBg4.Width, imgBg4.Height);
                //    }
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.ResourceSilo))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Silo;
                //    g.DrawImage(imgBg, 487, 226, imgBg.Width, imgBg.Height);

                //    Image imgBg1 = Heroes.Core.Castle.Properties.Resources.Marketplace;
                //    g.DrawImage(imgBg1, 413, 263, imgBg1.Width, imgBg1.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel2))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Mage2;
                //    g.DrawImage(imgBg, 705, 129, imgBg.Width, imgBg.Height);
                //}
                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel3))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Mage3;
                //    g.DrawImage(imgBg, 702, 101, imgBg.Width, imgBg.Height);
                //}
                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel4))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.Mage4;
                //    g.DrawImage(imgBg, 702, 70, imgBg.Width, imgBg.Height);
                //}

                //if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Lighthouse))
                //{
                //    Image imgBg = Heroes.Core.Castle.Properties.Resources.lighthouse22;
                //    g.DrawImage(imgBg, 532, 72, imgBg.Width, imgBg.Height);

                //    Image imgBg1 = Heroes.Core.Castle.Properties.Resources.Shipyard;
                //    g.DrawImage(imgBg1, 477, 133, imgBg1.Width, imgBg1.Height);

                //    if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Monastery))
                //    {
                //        Image imgBg3 = Heroes.Core.Castle.Properties.Resources.Monastery;
                //        g.DrawImage(imgBg3, 564, 210, imgBg3.Width, imgBg3.Height);
                //    }

                //    if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Ship))
                //    {
                //        Image imgShip = Heroes.Core.Castle.Properties.Resources.Ship;
                //        g.DrawImage(imgShip, 477, 132, imgShip.Width, imgShip.Height);
                //    }
                //}
                //#endregion

            }
            using (Graphics g = this.castleView1.CreateGraphics())
            {
                g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
            }
        }
        #endregion

    }
}
