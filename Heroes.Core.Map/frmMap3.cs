using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Heroes.Core.Map
{
    public partial class frmMap3 : Form
    {        
        #region Map Functions Related Global Variable
        TextReader tr;
        TextWriter tw;

        Dictionary<string, string> MapSettings;

        int bottomCenterY;
        int rightCenterX;

        int Xmove;
        int Ymove;

        //int cellXmove;
        //int cellYmove;

        string currentActionk;

        string k;
        string _dirCur = Application.StartupPath;
        string _dirImages = Application.StartupPath + @"\Images\Map\";

        // Stored data of Passiblity Cell(Row,Col), loaded from text file by function button_LoadPassibilityCellRecord_Click(), button located in Tab Control
        string[] sa;

        //int panelTop;
        //int panelLeft;
        //int panelBottom;
        //int panelRight;

        Image minimap;
        Image bigMap;

        //Cell2 cell;

        //public static Point _miniMapPt;

        Timer timer1;
        Timer timer2;
        Timer timer3;

        Graphics g1;
        Graphics g2;

        public int _centerPanelX;
        public int _centerPanelY;
        public int _rightCenterPanelX;
        public int _rightCenterPanelY;

        Terrain.MapTerrain _mapTerrain;

        public delegate void FormTerminatedEventHandler();

        public event FormTerminatedEventHandler FormTerminated;

        DataGridView dg;
        DataGridView dg2;
        DataGridView dg3;
        DataGridView dg4;
        #endregion

        #region Player Related Global Variable

        Timer timerMoveItem;
        int timerMoveItemCounter;

        int mapItemX;
        int mapItemY;

        public int heroId;
        PictureBox picContainer;

        Controller controller;

        // Variable copy from frmMap.cs
        public ArrayList _players;
        public Player _currentPlayer;
        Hero _currentHero;
        public Hashtable _monsters;
        public ArrayList _mines;

        string _curDir;
        // End of Variable copy from frmMap.cs

        #endregion       

        public frmMap3()
        {

            // Variable copy from frmMap.cs
            _curDir = Application.StartupPath;

            _players = new ArrayList();
            _currentPlayer = null;
            _monsters = new Hashtable();
            _mines = new ArrayList();
            // End of Variable copy from frmMap.cs

            InitializeComponent();

            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);


            this.StartPosition = FormStartPosition.CenterScreen;

            _mapTerrain = new Heroes.Core.Map.Terrain.MapTerrain();

            //System.Reflection.Assembly asm = System.Reflection.Assembly.Load("Heroes.Core.Map");
            //bigMap = Image.FromStream(asm.GetManifestResourceStream("Heroes.Core.Map.Images.2_s.bmp"));
            bigMap = Image.FromFile(_dirImages + "bigmap.bmp");
            minimap = Image.FromFile(_dirImages + "minimap.bmp");

            this.bigMapController1._bigMap = bigMap;
            this.bigMapController1._mapTerrain = _mapTerrain;
                        
            timer1 = new Timer();
            timer1.Interval = 10;
            timer1.Stop();
            timer1.Tick += new EventHandler(timer1_Tick);

            timer2 = new Timer();
            timer2.Interval = 500;
            timer2.Stop();
            timer2.Tick += new EventHandler(timer2_Tick);

            timer3 = new Timer();
            timer3.Interval = 1;
            timer3.Stop();
            timer3.Tick += new EventHandler(timer3_Tick);

            timerMoveItem = new Timer();
            timerMoveItem.Interval = 10;
            timerMoveItem.Stop();
            timerMoveItem.Tick += new EventHandler(timerMoveItem_Tick);

            timerMoveItemCounter = 0;

            this.panelMiniMap.MouseClick += new MouseEventHandler(panel_MiniMap_MouseClick);
            this.bigMapController1.MouseClick += new MouseEventHandler(panelBigMap_MouseClick);
            this.FormClosed += new FormClosedEventHandler(frmMap3_FormClosed);
            this.dataGridView1.SelectionChanged += new EventHandler(dg_SelectionChanged);
            this.dataGridView2.SelectionChanged += new EventHandler(dg_SelectionChanged);
            this.dataGridView3.SelectionChanged += new EventHandler(dg_SelectionChanged);
            this.checkBox1.CheckStateChanged += new EventHandler(checkBox1_CheckStateChanged);
            this.checkBox2.CheckStateChanged += new EventHandler(checkBox2_CheckStateChanged);
            this.MouseWheel += new MouseEventHandler(frmMap3_MouseWheel);

            this.panel_top.MouseMove += new MouseEventHandler(panel_top_MouseMove);
            this.panel_rightTop.MouseMove += new MouseEventHandler(panel_rightTop_MouseMove);
            this.panel_rightBottom.MouseMove += new MouseEventHandler(panel_rightBottom_MouseMove);
            this.panel_right.MouseMove += new MouseEventHandler(panel_right_MouseMove);
            this.panel_leftTop.MouseMove += new MouseEventHandler(panel_leftTop_MouseMove);
            this.panel_leftBottom.MouseMove += new MouseEventHandler(panel_leftBottom_MouseMove);
            this.panel_left.MouseMove += new MouseEventHandler(panel_left_MouseMove);
            this.panel_bottom.MouseMove += new MouseEventHandler(panel_bottom_MouseMove); 

            this.panel_center.MouseMove += new MouseEventHandler(panel_center_MouseMove);
            this.bigMapController1.MouseMove += new MouseEventHandler(panel_center_MouseMove);

            this.tabControl1.MouseMove += new MouseEventHandler(tabControl1_MouseMove);

            this.comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
        }

        private void frmMap3_Load(object sender, EventArgs e)
        {
            mapItemX = 0;
            mapItemY = 0;

            MapSettings = new Dictionary<string, string>();

            heroId = -1;

            this.comboBox1.SelectedIndex = 0;

            FindBottomAndRightCenterRowCol();

            k = "Mouse Move";

            this.label3.Visible = false;

            _centerPanelX = this.bigMapController1.Width / 2;
            _centerPanelY = this.bigMapController1.Height / 2;
            _rightCenterPanelX = bigMap.Width - this.bigMapController1.ClientSize.Width;
            _rightCenterPanelY = bigMap.Height - this.bigMapController1.ClientSize.Height;

            dg = this.dataGridView1;
            dg2 = this.dataGridView2;
            dg3 = this.dataGridView3;
            dg4 = this.dataGridView4;

            int da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "gv._bigMapPtX";           // 0
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "gv._bigMapPtY";           // 1
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "Big Map Click e.Y";       // 2
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "Big Map Click e.X";       // 3
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "gv._bigMapPtX_eX";        // 4
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "gv._bigMapPtY_eY";        // 5
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "Cell Row";                // 6
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "Cell Col";                // 7
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "gv._cellDrawPointX";      // 8
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "gv._cellDrawPointY";      // 9
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "Current Action";          // 10
            dg.Rows[da].Cells[1].Style.WrapMode = DataGridViewTriState.True;
            dg.Rows[da].Height = 32;
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "Cell [0, 0] xy";            // 11
            da = dg.Rows.Add();
            dg.Rows[da].Cells[0].Value = "Cell [35, 35] xy";          // 12

            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Current Player";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Total Move Left";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Total Heroes";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Total Mines";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Total Castle";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Gold";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Wood";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Mercury";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Sulfur";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Gem";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Crystal";
            da = dg2.Rows.Add();
            dg2.Rows[da].Cells[0].Value = "Ore";

            da = dg3.Rows.Add();
            dg3.Rows[da].Cells[0].Value = "Player";
            da = dg3.Rows.Add();
            dg3.Rows[da].Cells[0].Value = "Hero Name";
            da = dg3.Rows.Add();
            dg3.Rows[da].Cells[0].Value = "Hero ID";
            da = dg3.Rows.Add();
            dg3.Rows[da].Cells[0].Value = "Total Move Left";
            da = dg3.Rows.Add();
            dg3.Rows[da].Cells[0].Value = "Army";

            this.checkBox1.CheckState = CheckState.Checked;
            timer1.Start();
            LoadMapSettings();

            int p = bigMap.Height - this.bigMapController1.ClientSize.Height;
            int q = bigMap.Width - this.bigMapController1.ClientSize.Width;

            for (int i = 0; i < _mapTerrain._totalCellRow; i++)
            {
                if (p <= _mapTerrain.cellXYss[i][1][1] + _mapTerrain.cellHeight)
                {
                    bottomCenterY = i;
                    break;
                }
            }

            for (int i = 0; i < _mapTerrain._totalCellCol; i++)
            {
                if (q <= _mapTerrain.cellXYss[1][i][0] + _mapTerrain.cellWidth)
                {
                    rightCenterX = i;
                    break;
                }
            }

            //for (int i = 0; i < 4; i++)
            //{
            //    Initialize(i);
            //}
            Initialize(1);
        }

        #region Create Map Functions & Components

        void LoadMapSettings()
        {
            try
            {
                tr = new StreamReader(Application.StartupPath + @"\MapSettings.txt");
                MapSettings.Clear();
                string s = tr.ReadLine();
                while (s != null)
                {
                    string[] ss = s.Split(',');
                    MapSettings.Add(ss[0], ss[1]);
                    s = tr.ReadLine();
                }
                tr.Close();
            }
            catch
            {
                if (tr != null)
                    tr.Close();
                CreateAndSaveMapSettings();
            }
            finally
            {
                if (tr != null)
                    tr.Close();
            }
            try
            {
                if (MapSettings["ViewGrid"] == "Checked")
                    this.checkBox1.CheckState = CheckState.Checked;
                else
                    this.checkBox1.CheckState = CheckState.Unchecked;
                if (MapSettings["ViewPassibility"] == "Checked")
                    this.checkBox2.CheckState = CheckState.Checked;
                else
                    this.checkBox2.CheckState = CheckState.Unchecked;
                if (MapSettings["MouseScrollSpeed"] == "0")
                    this.comboBox1.SelectedIndex = 0;
                else if (MapSettings["MouseScrollSpeed"] == "2")
                    this.comboBox1.SelectedIndex = 2;
                else
                    this.comboBox1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void CreateAndSaveMapSettings()
        {
            MapSettings.Clear();
            MapSettings.Add("ViewGrid", this.checkBox1.CheckState.ToString());
            MapSettings.Add("ViewPassibility", this.checkBox2.CheckState.ToString());
            MapSettings.Add("MouseScrollSpeed", this.comboBox1.SelectedIndex.ToString());
            tw = new StreamWriter(Application.StartupPath + @"\MapSettings.txt");
            foreach (KeyValuePair<string, string> pair in MapSettings)
            {
                tw.WriteLine(string.Format("{0},{1}", pair.Key, pair.Value));
            }
            tw.Close();
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == 0)
            {
                timer3.Interval = 70;
            }
            else if (this.comboBox1.SelectedIndex == 1)
            {
                timer3.Interval = 120;
            }
            else if (this.comboBox1.SelectedIndex == 2)
            {
                timer3.Interval = 1;
            }
        }

        void tabControl1_MouseMove(object sender, MouseEventArgs e)
        {
            timer3.Stop();
        }

        void timer2_Tick(object sender, EventArgs e)
        {
            if (gv.StartRecordCellPassibility == 1)
            {
                this.label3.Text = "Start Recording Passibility Cell...";
                if (this.label3.Visible == false)
                    this.label3.Visible = true;
                else
                    this.label3.Visible = false;
            }
        }
        
        void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Draw();
        }

        void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.CheckState == CheckState.Checked)
                gv._viewPassibilityCell = 1;
            else
                gv._viewPassibilityCell = 0;
            Draw();
        }

        void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
                gv._viewGridLine = 1;
            else
                gv._viewGridLine = 0;
            Draw();
        }

        void dg_SelectionChanged(object sender, EventArgs e)
        {
            dg.ClearSelection();
            dg2.ClearSelection();
            dg3.ClearSelection();
        }

        void frmMap3_FormClosed(object sender, FormClosedEventArgs e)
        {
            CreateAndSaveMapSettings();
            OnFormTerminated();
        }
        
        protected virtual void OnFormTerminated()
        {
            if (FormTerminated != null)
            {
                //Invokes the delegates.
                FormTerminated();
            }
        }
        
        void panelBigMap_MouseClick(object sender, MouseEventArgs e)
        {
            _mapTerrain.FindCell4(e.X, e.Y);
           
            if (gv.StartRecordCellPassibility == 1)
            {
                int ContainSameCell = 0;
                for (int i = 0; i < dg4.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dg4.Rows[i].Cells[0].Value) == gv.cellRecordRow && Convert.ToInt32(dg4.Rows[i].Cells[1].Value) == gv.cellRecordCol)
                    {
                        ContainSameCell = 1;
                        _mapTerrain.cellXYss[gv.cellRecordRow][gv.cellRecordCol][2] = 0;
                        dg4.Rows.RemoveAt(i);
                        break;
                    }
                }

                if (ContainSameCell == 0)
                {
                    int a = dg4.Rows.Add();
                    dg4.Rows[a].Cells[0].Value = gv.cellRecordRow;
                    dg4.Rows[a].Cells[1].Value = gv.cellRecordCol;
                    _mapTerrain.cellXYss[gv.cellRecordRow][gv.cellRecordCol][2] = 1;
                }
                Draw();
            }

            dg.Rows[0].Cells[1].Value = gv._bigMapPtX.ToString();
            dg.Rows[1].Cells[1].Value = gv._bigMapPtY.ToString();
            dg.Rows[2].Cells[1].Value = e.X.ToString();
            dg.Rows[3].Cells[1].Value = e.Y.ToString();
            dg.Rows[4].Cells[1].Value = gv._bigMapPtX_eX.ToString();
            dg.Rows[5].Cells[1].Value = gv._bigMapPtY_eY.ToString();
            if (gv.StartRecordCellPassibility == 1)
            {
                dg.Rows[6].Cells[1].Value = gv.cellRecordRow.ToString();
                dg.Rows[7].Cells[1].Value = gv.cellRecordCol.ToString();
            }
            else
            {
                dg.Rows[6].Cells[1].Value = gv._row.ToString();
                dg.Rows[7].Cells[1].Value = gv._col.ToString();
            }
            dg.Rows[8].Cells[1].Value = gv._cellDrawPointX.ToString();
            dg.Rows[9].Cells[1].Value = gv._cellDrawPointY.ToString();
            dg.Rows[10].Cells[1].Value = "BigMap Click";
            dg.Rows[11].Cells[1].Value = _mapTerrain._cells[0, 0]._rect.X + ", " + _mapTerrain._cells[0, 0]._rect.Y;
            dg.Rows[12].Cells[1].Value = _mapTerrain._cells[35, 35]._rect.X + ", " + _mapTerrain._cells[35, 35]._rect.Y;
            dg.Rows[6].Cells[1].Style.BackColor = Color.Yellow;
            dg.Rows[7].Cells[1].Style.BackColor = Color.Yellow;
            dg.Rows[6].Cells[0].Style.BackColor = Color.Yellow;
            dg.Rows[7].Cells[0].Style.BackColor = Color.Yellow;
        }

        void panel_MiniMap_MouseClick(object sender, MouseEventArgs e)
        {
            int bigX = e.X * this.bigMap.Width / minimap.Width;
            int bigY = e.Y * this.bigMap.Height / minimap.Height;

            bigX = bigX - _centerPanelX;
            bigY = bigY - _centerPanelY;

            if (bigX <= 71) bigX = 0;
            if (bigY <= 71) bigY = 0;
            if (bigX >= _rightCenterPanelX) bigX = _rightCenterPanelX;
            if (bigY >= _rightCenterPanelY) bigY = _rightCenterPanelY;

            gv._bigMapPtX = bigX;
            gv._bigMapPtY = bigY;

            if (gv._bigMapPtX == 0)
                gv._cellDrawPointX = 71;
            else
                gv._cellDrawPointX = 0;

            if (gv._bigMapPtY == 0)
                gv._cellDrawPointY = 71;
            else
                gv._cellDrawPointY = 0;

            _mapTerrain.findCellFromSmallMap3();

            dg.Rows[0].Cells[1].Value = gv._bigMapPtX.ToString();
            dg.Rows[1].Cells[1].Value = gv._bigMapPtY.ToString();
            dg.Rows[2].Cells[1].Value = "";
            dg.Rows[3].Cells[1].Value = "";
            dg.Rows[4].Cells[1].Value = gv._bigMapPtX_eX.ToString();
            dg.Rows[5].Cells[1].Value = gv._bigMapPtY_eY.ToString();
            dg.Rows[6].Cells[1].Value = gv._row.ToString();
            dg.Rows[7].Cells[1].Value = gv._col.ToString();
            dg.Rows[8].Cells[1].Value = gv._cellDrawPointX.ToString();
            dg.Rows[9].Cells[1].Value = gv._cellDrawPointY.ToString();
            dg.Rows[10].Cells[1].Value = "MiniMap Click";
            dg.Rows[11].Cells[1].Value = _mapTerrain._cells[0, 0]._rect.X + ", " + _mapTerrain._cells[0, 0]._rect.Y;
            dg.Rows[12].Cells[1].Value = _mapTerrain._cells[35, 35]._rect.X + ", " + _mapTerrain._cells[35, 35]._rect.Y;
            dg.Rows[6].Cells[1].Style.BackColor = Color.Yellow;
            dg.Rows[7].Cells[1].Style.BackColor = Color.Yellow;
            dg.Rows[6].Cells[0].Style.BackColor = Color.Yellow;
            dg.Rows[7].Cells[0].Style.BackColor = Color.Yellow;

            Draw();
        }

        private void MyDraw(int miniX, int miniY)
        {
            int x = miniX * this.bigMap.Width / minimap.Width;
            int y = miniY * this.bigMap.Height / minimap.Height;

            Cell3 cellBigMap = _mapTerrain.FindCell(x, y);

            gv._bigMapPtX = cellBigMap._rect.X;
            gv._bigMapPtY = cellBigMap._rect.Y;
            
            Draw();
        }

        public void Draw()
        {
            try
            {
                this.bigMapController1.Draw();
                //Bitmap bmp = new Bitmap(this.bigMapController1.ClientSize.Width, this.bigMapController1.ClientSize.Height);
                //using (Graphics g = Graphics.FromImage(bmp))
                //{
                //    g.Clear(Color.White);
                //    g.DrawImage(bigMap, -gv._bigMapPtX, -gv._bigMapPtY, bigMap.Width, bigMap.Height);

                //    _mapTerrain.Draw2(g);

                    
                //}


                //using (Graphics g = this.panelBigMap.CreateGraphics())
                //{
                //    g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
                //}

                using (Graphics g2 = this.panelMiniMap.CreateGraphics())
                {
                    g2.DrawImage(minimap, 0, 0, 200, 200);
                }

               
            }
            catch
            {
                
            }
        }
               
        protected override void OnPaint(PaintEventArgs e)
        {
            //Draw();
            base.OnPaint(e);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateAndSaveMapSettings();
            this.Close();
        }

        void ClearDGRVRow(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells[1].Value = "";
            }
        }

        //public static Point ViewToTerrain(Point pt)
        //{
        //    return new Point(pt.X - _offsetPt.X, pt.Y - _offsetPt.Y);
        //}

        //public static Point TerrainToView(Point pt)
        //{
        //    return new Point(pt.X + _offsetPt.X, pt.Y + _offsetPt.Y);
        //}

        private void button_startRecordCellPassibility_Click(object sender, EventArgs e)
        {
            gv.StartRecordCellPassibility = 1;
            LoadPassibilityCellRecordFromTxt();
            this.tabPage_cellPassibility.Focus();
            this.tabPage_cellPassibility.Show();
            this.tabPage_cellPassibility.BringToFront();
            this.timer2.Start();
        }

        private void button_stopRecordCellPassiblity_Click(object sender, EventArgs e)
        {
            gv.StartRecordCellPassibility = 0;
            this.timer2.Stop();
            this.label3.Visible = true;
            this.label3.Text = "Cell Passibility Recording stoped...";
            DialogResult dr = MessageBox.Show("Do you want to save the Recorded Passibility Cells?", "Save", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                SavePassibilityCellRecordToTxt();
            this.tabPage_debug.BringToFront();
        }

        private void button_SaveRecordedPassibilityCell_Click(object sender, EventArgs e)
        {
            SavePassibilityCellRecordToTxt();
            LoadPassibilityCellRecordFromTxt();
        }

        private void button_LoadPassibilityCellRecord_Click(object sender, EventArgs e)
        {
            LoadPassibilityCellRecordFromTxt();
        }

        void LoadPassibilityCellRecordFromTxt()
        {
            try
            {
                TextReader tr = new StreamReader(Application.StartupPath + @"\cellpassibility.txt");
                string s = tr.ReadLine();
                dg4.Rows.Clear();
                int a = 0;
                //char[] delimeters = new char[] { ',' };
                //string se = ",";
                while (s != null)
                {
                    //MessageBox.Show(s);

                    //string[] ss = s.Split(new string[] { "," }, StringSplitOptions.None);
                    string[] ss = s.Split(',');
                    a = dg4.Rows.Add();
                    dg4.Rows[a].Cells[0].Value = ss[0];
                    dg4.Rows[a].Cells[1].Value = ss[1];
                    s = tr.ReadLine();
                }
                tr.Close();
            }
            catch
            { }
            finally
            {
                if (tr != null)
                    tr.Close();
            }
        }

        void SavePassibilityCellRecordToTxt()
        {
            TextWriter tw = new StreamWriter(Application.StartupPath + @"\cellpassibility.txt");
            for (int i = 0; i < dg4.Rows.Count; i++)
            {
                tw.WriteLine(string.Format("{0},{1}", dg4.Rows[i].Cells[0].Value.ToString(), dg4.Rows[i].Cells[1].Value.ToString()));
            }
            tw.Close();
        }

        void MapMoveInPanel()
        {
            gv._bigMapPtX += (_mapTerrain.cellWidth * Xmove);
            gv._bigMapPtY += (_mapTerrain.cellHeight * Ymove);

            // Xmove
            if (gv._bigMapPtX <= 0)
            {
                gv._bigMapPtX = 0;
                gv._col = 0;
                gv._cellDrawPointX = _mapTerrain.startX;
            }
            else if (gv._bigMapPtX < _mapTerrain.startX)
            {
                gv._col = 0;
                gv._cellDrawPointX = _mapTerrain.startX - gv._bigMapPtX;
            }
            else if (gv._bigMapPtX >= _mapTerrain.startX && gv._bigMapPtX <= _centerPanelX)
            {
                gv._col += (1 * Xmove);
                gv._bigMapPtX = _mapTerrain.cellXYss[1][gv._col][0];
                gv._cellDrawPointX = 0;
            }
            else if (gv._bigMapPtX > _centerPanelX)
            {
                gv._cellDrawPointX = 0;
                gv._col = rightCenterX;
                gv._bigMapPtX = _mapTerrain.cellXYss[1][gv._col][0];
            }
            // End of Xmove

            // YMove
            if (gv._bigMapPtY <= 0)
            {
                gv._bigMapPtY = 0;
                gv._row = 0;
                gv._cellDrawPointY = _mapTerrain.startY;
            }
            else if (gv._bigMapPtY < _mapTerrain.startY)
            {
                gv._row = 0;
                gv._cellDrawPointY = _mapTerrain.startY - gv._bigMapPtY;
            }
            else if (gv._bigMapPtY >= _mapTerrain.startY && gv._bigMapPtY <= _rightCenterPanelY)
            {
                gv._row += (1 * Ymove);
                gv._bigMapPtY = _mapTerrain.cellXYss[gv._row][1][1];
                gv._cellDrawPointY = 0;
            }
            else if (gv._bigMapPtY > _rightCenterPanelY)
            {
                gv._cellDrawPointY = 0;
                gv._row = bottomCenterY;
                gv._bigMapPtY = _mapTerrain.cellXYss[gv._row][1][1];
                //MessageBox.Show("_rightCenterPanelY=" + _rightCenterPanelY.ToString());
            }
            // End of Ymove

            ClearDGRVRow(dg);
            try
            {
                dg.Rows[0].Cells[1].Value = gv._bigMapPtX.ToString();
                dg.Rows[1].Cells[1].Value = gv._bigMapPtY.ToString();
                dg.Rows[8].Cells[1].Value = gv._cellDrawPointX.ToString();
                dg.Rows[9].Cells[1].Value = gv._cellDrawPointY.ToString();
                dg.Rows[10].Cells[1].Value = currentActionk;
                dg.Rows[6].Cells[1].Value = gv._row.ToString();
                dg.Rows[7].Cells[1].Value = gv._col.ToString();
            }
            catch
            { }
            Draw();
        }


        void frmMap3_MouseWheel(object sender, MouseEventArgs e)
        {
            Xmove = 0;
            if (e.Delta > 0)
            {
                Ymove = -2;
                currentActionk = "Mouse Scroll Up";
                MapMoveInPanel();
            }
            else
            {
                Ymove = 2;
                currentActionk = "Mouse Scroll Down";
                MapMoveInPanel();
            }
        }

        void timer3_Tick(object sender, EventArgs e)
        {
            MapMoveInPanel();
        }

        void panel_bottom_MouseMove(object sender, MouseEventArgs e)
        {
            Ymove = 1;
            Xmove = 0;
            currentActionk = "Panel Bottom MouseHover";
            timer3.Start();
        }

        void panel_left_MouseMove(object sender, MouseEventArgs e)
        {
            Ymove = 0;
            Xmove = -1;
            currentActionk = "Panel Left MouseHover";
            timer3.Start();
        }

        void panel_leftBottom_MouseMove(object sender, MouseEventArgs e)
        {
            Ymove = 1;
            Xmove = -1;
            currentActionk = "Panel LeftBottom MouseHover";
            timer3.Start();
        }

        void panel_leftTop_MouseMove(object sender, MouseEventArgs e)
        {
            Ymove = -1;
            Xmove = -1;
            currentActionk = "Panel LeftTop MouseHover";
            timer3.Start();
        }

        void panel_right_MouseMove(object sender, MouseEventArgs e)
        {
            Ymove = 0;
            Xmove = 1;
            currentActionk = "Panel Right MouseHover";
            timer3.Start();
        }

        void panel_rightBottom_MouseMove(object sender, MouseEventArgs e)
        {
            Ymove = 1;
            Xmove = 1;
            currentActionk = "Panel RightBottom MouseHover";
            timer3.Start();
        }

        void panel_rightTop_MouseMove(object sender, MouseEventArgs e)
        {
            Ymove = -1;
            Xmove = 1;
            currentActionk = "Panel RightTop MouseHover";
            timer3.Start();
        }

        void panel_top_MouseMove(object sender, MouseEventArgs e)
        {
            Ymove = -1;
            Xmove = 0;
            currentActionk = "Panel Top MouseHover";
            timer3.Start();
        }

        void panel_center_MouseMove(object sender, MouseEventArgs e)
        {
            timer3.Stop();
            dg.Rows[10].Cells[1].Value = "Panel Center Move";
        }

        void FindBottomAndRightCenterRowCol()
        {
            // Find Row
            for (int i = 0; i < _mapTerrain._totalCellRow; i++)
            {
                if (_rightCenterPanelY < _mapTerrain.cellXYss[i][1][1]+_mapTerrain.cellHeight)
                {
                    gv.bottomCenterRow = i;
                    break;
                }
            }
            // Find Col
            for (int i = 0; i < _mapTerrain._totalCellCol; i++)
            {
                if (_rightCenterPanelX < _mapTerrain.cellXYss[1][i][0] + _mapTerrain.cellWidth)
                {
                    gv.RightCenterCol = i;
                    break;
                }
            }
        }
        #endregion

        #region Create Player Related Component

        public void Initialize(int playerCount)
        {
            if (playerCount >= 1)
            {
                Player player1 = new Player(1);
                _players.Add(player1);

                _currentPlayer = player1;

                // player images
                player1._heroImage = Image.FromFile(string.Format(@"{0}\Images\Map\hero.png", _curDir));
                player1._heroHighlight = Image.FromFile(string.Format(@"{0}\Images\Map\hero_r_tr.gif", _curDir));
                player1._heroSelect = Image.FromFile(string.Format(@"{0}\Images\Map\hero_r_s.gif", _curDir));
                player1._goldMine = Image.FromFile(string.Format(@"{0}\Images\Map\GoldMine_r.png", _curDir));

                // castle
                //Town castle = new Town(1, Image.FromFile(string.Format(@"{0}\Images\Map\castle_r.png", _curDir)));
                //castle._player = player1;
                //player1._castles.Add(castle);
                //_cells[1, 1]._castle = castle;

                // create hero
                CreateHero(player1, 5, 5);
                //CreateHero(2, player1, 1, 7);
            }

            if (playerCount >= 2)
            {
                Player player2 = new Player(2);
                _players.Add(player2);

                // player image
                player2._heroImage = Image.FromFile(string.Format(@"{0}\Images\Map\hero_g.png", _curDir));
                player2._heroHighlight = Image.FromFile(string.Format(@"{0}\Images\Map\hero_g_tr.gif", _curDir));
                player2._heroSelect = Image.FromFile(string.Format(@"{0}\Images\Map\hero_g_s.gif", _curDir));
                player2._goldMine = Image.FromFile(string.Format(@"{0}\Images\Map\GoldMine_g.png", _curDir));
                player2._mineKTypes = new Dictionary<int, ArrayList>();
                player2._mineKTypes.Add((int)Heroes.Core.MineTypeEnum.Gold, new ArrayList());

                // castle
                //Town castle = new Town(2, Image.FromFile(string.Format(@"{0}\Images\Map\castle_g.png", _curDir)));
                //castle._player = player2;
                //player2._castles.Add(castle);
                //_cells[1, 17]._castle = castle;

                // create hero
                CreateHero(player2, 14, 16);
                //CreateHero(player2, 2, 17);
            }

            if (playerCount >= 3)
            {
                Player player3 = new Player(3);
                _players.Add(player3);

                // player image
                player3._heroImage = Image.FromFile(string.Format(@"{0}\Images\Map\hero_b.png", _curDir));
                player3._heroHighlight = Image.FromFile(string.Format(@"{0}\Images\Map\hero_b_tr.gif", _curDir));
                player3._heroSelect = Image.FromFile(string.Format(@"{0}\Images\Map\hero_b_s.gif", _curDir));
                player3._goldMine = Image.FromFile(string.Format(@"{0}\Images\Map\GoldMine_b.png", _curDir));
                player3._mineKTypes = new Dictionary<int, ArrayList>();
                player3._mineKTypes.Add((int)Heroes.Core.MineTypeEnum.Gold, new ArrayList());

                // Castle
                //Town castle = new Town(3, Image.FromFile(string.Format(@"{0}\Images\Map\castle_b.png", _curDir)));
                //castle._player = player3;
                //player3._castles.Add(castle);
                //_cells[9, 1]._castle = castle;

                // create hero
                CreateHero(player3, 20, 27);
                //CreateHero(6, player3, 9, 2);
            }

            if (playerCount >= 4)
            {
                Player player4 = new Player(4);
                _players.Add(player4);

                // player image
                player4._heroImage = Image.FromFile(string.Format(@"{0}\Images\Map\hero_y.png", _curDir));
                player4._heroHighlight = Image.FromFile(string.Format(@"{0}\Images\Map\hero_y_tr.gif", _curDir));
                player4._heroSelect = Image.FromFile(string.Format(@"{0}\Images\Map\hero_y_s.gif", _curDir));
                player4._goldMine = Image.FromFile(string.Format(@"{0}\Images\Map\GoldMine_y.png", _curDir));
                player4._mineKTypes = new Dictionary<int, ArrayList>();
                player4._mineKTypes.Add((int)Heroes.Core.MineTypeEnum.Gold, new ArrayList());

                // Castle
                //Town castle = new Town(4, Image.FromFile(string.Format(@"{0}\Images\Map\castle_y.png", _curDir)));
                //castle._player = player4;
                //player4._castles.Add(castle);
                //_cells[9, 17]._castle = castle;

                // create hero
                CreateHero(player4, 32, 31);
                //CreateHero(8, player4, 9, 16);
            }

        }

        private void CreateHero(Player player, int cellsRow, int cellsCol)
        {
            heroId += 1;

            Hero hero = new Heroes.Core.Map.Heros.Knight();
            hero._id = heroId;
            hero._playerId = player._id;
            hero._player = player;
            hero._image = player._heroImage;
            player._heroes.Add(hero);

            // spells
            Heroes.Core.Spell spell = new Heroes.Core.Spell();
            hero._spells.Add(spell._id, spell);

            picContainer = new PictureBox();
            picContainer.Size = new Size(_mapTerrain.cellWidth, _mapTerrain.cellHeight);
            picContainer.Location = new Point(_mapTerrain.cellXYss[cellsRow][cellsCol][0], _mapTerrain.cellXYss[cellsRow][cellsCol][1]);
            picContainer.Image = player._heroHighlight;
            picContainer.BorderStyle = BorderStyle.None;
            picContainer.BackColor = Color.Transparent;
            this.picContainer.MouseClick += new MouseEventHandler(picContainer_MouseClick);
            this.bigMapController1.Controls.Add(picContainer);
        }

        void picContainer_MouseClick(object sender, MouseEventArgs e)
        {
            MoveMapItem();
        }

        private void MoveMapItem()
        {
            timerMoveItem.Start();
        }

        void timerMoveItem_Tick(object sender, EventArgs e)
        {
            timerMoveItemCounter += 1;
            if (timerMoveItemCounter == 8)
            {
                timerMoveItem.Stop();
                timerMoveItemCounter = 0;
            }
            else
            {
                mapItemX = picContainer.Location.X + 15;
                mapItemY = picContainer.Location.Y + 15;
                picContainer.Location = new Point(mapItemX, mapItemY);
                Draw();
            }
        }

        #endregion
    }
}
