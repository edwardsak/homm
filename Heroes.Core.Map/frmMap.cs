using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Heroes.Core.Map
{
    public partial class frmMap : Form
    {
        #region Events
        public delegate void StartingBattleEventHandler(StartingBattleEventArg e);
        public delegate void VisitingCastleEventHandler(VisitingCastleEventArg e);
        public delegate void EndTurnPressedEventHandler(EndTurnPressedEventArg e);
        public delegate void FormTerminatedEventHandler();

        public event StartingBattleEventHandler StartingBattle;
        public event VisitingCastleEventHandler VisitingCastle;
        public event EndTurnPressedEventHandler EndTurnPressed;
        public event FormTerminatedEventHandler FormTerminated;

        #region Event Arguments
        public class StartingBattleEventArg
        {
            public Heroes.Core.Hero _attackHero;
            public Heroes.Core.Hero _defendHero;
            public Heroes.Core.Monster _monster;
            public Heroes.Core.Town _defendCastle;
            public int _victory;

            public StartingBattleEventArg(Heroes.Core.Hero attackHero, Heroes.Core.Hero defendHero, Heroes.Core.Monster monster, Heroes.Core.Town defendCastle)
            {
                _attackHero = attackHero;
                _defendHero = defendHero;
                _monster = monster;
                _defendCastle = defendCastle;
                _victory = 0;
            }
        }

        public class VisitingCastleEventArg
        {
            public Heroes.Core.Town _castle;

            public VisitingCastleEventArg(Heroes.Core.Town castle)
            {
                _castle = castle;
            }
        }

        public class EndTurnPressedEventArg
        {
        }
        #endregion

        protected virtual void OnStartingBattle(StartingBattleEventArg e)
        {
            if (StartingBattle != null)
            {
                //Invokes the delegates.
                StartingBattle(e);
            }
        }

        protected virtual void OnVisitingCastle(VisitingCastleEventArg e)
        {
            if (VisitingCastle != null)
            {
                //Invokes the delegates.
                VisitingCastle(e);
            }
        }

        protected virtual void OnEndTurnPressed(EndTurnPressedEventArg e)
        {
            if (EndTurnPressed != null)
            {
                //Invokes the delegates.
                EndTurnPressed(e);
            }
        }

        protected virtual void OnFormTerminated()
        {
            if (FormTerminated != null)
            {
                //Invokes the delegates.
                FormTerminated();
            }
        }
        #endregion

        #region Variables
        public int _rowCount = 11;   // total rows of cells
        public int _colCount = 19;   // total cells per row

        public Cell[,] _cells;

        public ArrayList _players;
        public Player _currentPlayer;
        Hero _currentHero;
        public Hashtable _monsters;
        public ArrayList _mines;

        string _curDir;
        #endregion

        #region Flags
        bool _isMultiPlayer;
        bool _isReadOnly;
        #endregion

        public frmMap()
        {
            InitializeComponent();

            _curDir = Application.StartupPath;

            #region Flags
            _isMultiPlayer = false;
            _isReadOnly = false;
            #endregion

            _players = new ArrayList();
            _currentPlayer = null;
            _monsters = new Hashtable();
            _mines = new ArrayList();

            _rowCount = 11;
            _colCount = 19;

            CreateCells();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.FormClosing += new FormClosingEventHandler(frmMap_FormClosing);
            this.FormClosed += new FormClosedEventHandler(frmMap_FormClosed);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Draw();
            this.dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
        }

        void frmMap_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        void frmMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            // raise closing event
            OnFormTerminated();
        }

        private void CreateCells()
        {
            int rowCount = _rowCount;
            int colCount = _colCount;
            _cells = new Cell[rowCount, colCount];

            int w = 50;
            int h = 50;
            int starty = 45;
            int startx = 39;
            int x = startx;
            int y = starty;

            for (int row = 0; row < rowCount; row++)
            {
                x = startx;

                for (int col = 0; col < colCount; col++)
                {
                    Cell cell = new Cell();
                    cell._row = row;
                    cell._col = col;

                    cell.BorderStyle = BorderStyle.None;
                    cell.BackColor = Color.Transparent;
                    cell.Left = x;
                    cell.Top = y;
                    cell.Width = w;
                    cell.Height = h;
                    this.Controls.Add(cell);

                    cell.MouseClick += new MouseEventHandler(cell_MouseClick);
                    cell.MouseHover += new EventHandler(cell_MouseHover);
                    cell.MouseLeave += new EventHandler(cell_MouseLeave);
                    cell.MouseDoubleClick += new MouseEventHandler(cell_MouseDoubleClick);

                    _cells[row, col] = cell;

                    x += w;
                }

                y += h;
            }
        }

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
                Town castle = new Town(1, Image.FromFile(string.Format(@"{0}\Images\Map\castle_r.png", _curDir)));
                castle._player = player1;
                player1._castles.Add(castle);
                _cells[1, 1]._castle = castle;

                // create hero
                CreateHero(1, player1, 2, 1);
                CreateHero(2, player1, 1, 7);
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
                Town castle = new Town(2, Image.FromFile(string.Format(@"{0}\Images\Map\castle_g.png", _curDir)));
                castle._player = player2;
                player2._castles.Add(castle);
                _cells[1, 17]._castle = castle;

                // create hero
                CreateHero(3, player2, 1, 9);
                CreateHero(4, player2, 2, 17);
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
                Town castle = new Town(3, Image.FromFile(string.Format(@"{0}\Images\Map\castle_b.png", _curDir)));
                castle._player = player3;
                player3._castles.Add(castle);
                _cells[9, 1]._castle = castle;

                // create hero
                CreateHero(5, player3, 8, 1);
                CreateHero(6, player3, 9, 2);
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
                Town castle = new Town(4, Image.FromFile(string.Format(@"{0}\Images\Map\castle_y.png", _curDir)));
                castle._player = player4;
                player4._castles.Add(castle);
                _cells[9, 17]._castle = castle;

                // create hero
                CreateHero(7, player4, 8, 17);
                CreateHero(8, player4, 9, 16);
            }

            CreateMonster(1, ArmyIdEnum.Pikeman, 3, 7);

            CreateMines();

            Draw();

            PplInitInfo();
        }

        private void CreateHero(int heroId, Player player, int cellsRow, int cellsCol)
        {
            Hero hero = new Heroes.Core.Map.Heros.Knight();
            hero._id = heroId;
            hero._playerId = player._id;
            hero._player = player;
            hero._image = player._heroImage;
            player._heroes.Add(hero);

            // spells
            Heroes.Core.Spell spell = new Heroes.Core.Spell();
            hero._spells.Add(spell._id, spell);

            hero._cell = _cells[cellsRow, cellsCol];
            hero._cell._hero = hero;

            {
                Armies.Pikeman c = new Heroes.Core.Map.Armies.Pikeman();
                c._heroId = hero._id;
                c._playerId = hero._playerId;
                c.AddAttribute(hero);
                c._qty = 20;
                c._slotNo = 0;
                hero._armyKSlots.Add(c._slotNo, c);
            }

            //{
            //    Armies.Pikeman c = new Heroes.Core.Map.Armies.Pikeman();
            //    c._heroId = hero._id;
            //    c._playerId = hero._playerId;
            //    c.AddAttribute(hero);
            //    c._qty = 10;
            //    c._slotNo = 1;
            //    hero._armyKSlots.Add(c._slotNo, c);
            //}

            //{
            //    Armies.Archer c = new Heroes.Core.Map.Armies.Archer();
            //    c._heroId = hero._id;
            //    c._playerId = hero._playerId;
            //    c.AddAttribute(hero);
            //    c._qty = 10;
            //    c._slotNo = 2;
            //    hero._armyKSlots.Add(c._slotNo, c);
            //}

            //{
            //    Armies.Griffin c = new Heroes.Core.Map.Armies.Griffin();
            //    c._heroId = hero._id;
            //    c._playerId = hero._playerId;
            //    c.AddAttribute(hero);
            //    c._qty = 10;
            //    c._slotNo = 3;
            //    hero._armyKSlots.Add(c._slotNo, c);
            //}
        }

        private void CreateMonster(int id, Heroes.Core.ArmyIdEnum armyId, int row, int col)
        {
            Heroes.Core.Map.Monsters.Monster monster = null;

            switch (armyId)
            {
                case ArmyIdEnum.Pikeman:
                    monster = new Heroes.Core.Map.Monsters.Pikeman();
                    break;
            }

            monster._id = id;
            monster._cell = _cells[row, col];
            monster._cell._monster = monster;

            {
                Armies.Pikeman c = new Heroes.Core.Map.Armies.Pikeman();
                c._qty = 10;
                c._slotNo = 3;
                monster._armyKSlots.Add(c._slotNo, c);
            }

            _monsters.Add(id, monster);
        }

        private void CreateMines()
        {
            Image img = Image.FromFile(string.Format(@"{0}\Images\Map\GoldMine.png", _curDir));

            int mineId = 1;
            Mine mine = new Mine(img, mineId);
            _mines.Add(mine);
            Cell cell = _cells[0, 2];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[10, 2];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[0, 16];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[10, 16];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[2, 5];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[8, 5];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[8, 13];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[2, 13];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[3, 10];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[5, 1];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[5, 17];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[4, 8];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[6, 8];
            cell._mine = mine;

            mineId += 1;
            mine = new Mine(img, mineId);
            _mines.Add(mine);
            cell = _cells[6, 10];
            cell._mine = mine;
        }

        private void PplInitInfo()
        {
            this.label_currentPlayer.Text = "Player " + _currentPlayer._id;
            this.label_gold.Text = _currentPlayer._gold.ToString();

            int count = 0;
            if (_currentPlayer._mineKTypes.ContainsKey((int)Heroes.Core.MineTypeEnum.Gold))
            {
                count = _currentPlayer._mineKTypes[(int)Heroes.Core.MineTypeEnum.Gold].Count;
            }
            this.label_mines.Text = count.ToString();
            this.label_heroes.Text = _currentPlayer._heroes.Count.ToString();

            // hightlight image
            foreach (Hero hero in _currentPlayer._heroes)
            {
                hero._movementPointLeft = hero._movementPoint;
                hero._image = _currentPlayer._heroHighlight;
            }
        }

        #region Properties
        public bool IsMultiPlayer
        {
            get { return _isMultiPlayer; }
            set
            {
                _isMultiPlayer = value;
            }
        }

        public bool ReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                this.msNextTurn.Enabled = !_isReadOnly;
            }
        }
        #endregion

        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
        }

        #region Draw
        public void Draw()
        {
            for (int row = 0; row < _rowCount; row++)
            {
                for (int col = 0; col < _colCount; col++)
                {
                    _cells[row, col].Draw();
                }
            }
        }
        #endregion

        #region Mouse Click/Double Click/Leave/Hover On Map
        void cell_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (cell._castle != null)
            {
                cell._castle._heroVisit = null;

                VisitingCastleEventArg eventArg = new VisitingCastleEventArg(cell._castle);
                OnVisitingCastle(eventArg);
            }
        }

        void cell_MouseLeave(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender;
            if ((cell._col == 1 || cell._col == 17) && (cell._row == 1 || cell._row == 9))
            {
                _cells[1, 1].Image = Image.FromFile(string.Format(@"{0}\Images\Map\castle_r.png", _curDir));
                _cells[1, 17].Image = Image.FromFile(string.Format(@"{0}\Images\Map\castle_g.png", _curDir));
                _cells[9, 1].Image = Image.FromFile(string.Format(@"{0}\Images\Map\castle_b.png", _curDir));
                _cells[9, 17].Image = Image.FromFile(string.Format(@"{0}\Images\Map\castle_y.png", _curDir));
            }
        }

        void cell_MouseHover(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender;
            if ((cell._col == 1 || cell._col == 17) && (cell._row == 1 || cell._row == 9))
            {
                _cells[cell._row, cell._col].Image = Image.FromFile(string.Format(@"{0}\Images\Map\castle.gif", _curDir));
            }
        }

        void cell_MouseClick(object sender, MouseEventArgs e)
        {
            Cell cell = (Cell)sender;
            Debug.WriteLine(string.Format("{0},{1}", cell._row, cell._col));

            if (e.Button == MouseButtons.Left)
            {
                if (cell._hero != null)
                {
                    if (cell._hero._player.Equals(_currentPlayer))
                    {
                        _currentHero = (Hero)cell._hero;
                        _currentHero._image = _currentPlayer._heroSelect;
                        Draw();

                        //PplHeroInfo();
                    }

                    // cannot move to self
                    //if (cell._hero != null && cell._hero.Equals(hero)) return;
                }
                else
                {
                    this.dataGridView1.Rows.Clear();
                }
            }
            else
            {
                if (_currentHero == null) return;

                _currentHero._experience += 1000;
                if (_currentHero.IsLevelUp())
                {
                    frmLevelUp f = new frmLevelUp();
                    f.ShowDialog(_currentHero);
                }
            }
        }

        private void PplHeroInfo()
        {
            string k = "";
            DataGridView dg = this.dataGridView1;

            dg.Rows.Clear();
            int rowIndex = dg.Rows.Add();
            dg.Rows[rowIndex].Cells[0].Value = "Hero ID:";
            dg.Rows[rowIndex].Cells[1].Value = _currentHero._id.ToString();
            rowIndex = dg.Rows.Add();
            dg.Rows[rowIndex].Cells[0].Value = "Move Left:";
            dg.Rows[rowIndex].Cells[1].Value = _currentHero._movementPointLeft.ToString();
            rowIndex = dg.Rows.Add();
            rowIndex = dg.Rows.Add();
            dg.Rows[rowIndex].Cells[0].Value = "Soldier Type:";
            dg.Rows[rowIndex].Cells[1].Value = "Qty";
            for (int jjj = 0; jjj < 7; jjj++)
            {
                if (_currentHero._armyKSlots[jjj] != null)
                {
                    k = _currentHero._armyKSlots[jjj].ToString();
                    string[] kk = k.Split('.');
                    rowIndex = dg.Rows.Add();
                    dg.Rows[rowIndex].Cells[0].Value = kk[4];
                    dg.Rows[rowIndex].Cells[1].Value = ((Heroes.Core.Army)_currentHero._armyKSlots[jjj])._qty.ToString();
                }
            }
        }
        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DirectionEnum GetDirection(Cell cellSrc, Cell cellDest)
        {


            return DirectionEnum.None;
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // cannot move when it is read only
            if (_isReadOnly) return;
            if (_currentHero == null) return;

            if (_currentHero._movementPointLeft == 0)
            {
                _currentHero._image = _currentPlayer._heroImage;
                Draw();
            }
            else
            {
                DirectionEnum direction = DirectionEnum.None;

                if (e.KeyCode == Keys.Up)
                {
                    e.Handled = true;
                    direction = DirectionEnum.Up;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    e.Handled = true;
                    direction = DirectionEnum.Down;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    e.Handled = true;
                    direction = DirectionEnum.Left;
                }

                else if (e.KeyCode == Keys.Right)
                {
                    e.Handled = true;
                    direction = DirectionEnum.Right;
                }
                else
                    return;

                if (_currentPlayer._heroes.Count < 1) return;
                if (_currentHero == null) return;

                Cell cell = GetNextCell(_currentHero._cell, direction);
                if (cell == null) return;

                if (cell._hero != null)
                {
                    if (cell._hero._player.Equals(_currentPlayer))
                    {
                        // exchange armies

                    }
                    else
                    {
                        // attack hero
                        Attack(cell);
                        if (_currentHero._movementPointLeft == 0)
                        {
                            _currentHero._image = _currentPlayer._heroImage;
                        }
                    }
                }
                else if (cell._monster != null)
                {
                    // attack monster
                    Attack(cell);
                    if (_currentHero._movementPointLeft == 0)
                    {
                        _currentHero._image = _currentPlayer._heroImage;
                    }
                }
                else if (cell._castle != null)
                {
                    if (cell._castle._player.Equals(_currentPlayer))
                    {
                        // visit castle
                        VisitCastle(cell);
                        if (_currentHero._movementPointLeft == 0)
                        {
                            _currentHero._image = _currentPlayer._heroImage;
                            Draw();
                        }
                    }
                    else
                    {
                        // attack castle
                    }
                }
                else
                    MoveHero(_currentHero, cell);
            }

        }

        private void MoveHero(Hero hero, Cell cell)
        {
            // no more movement points
            if (hero._movementPointLeft <= 0) return;

            // move hero
            {
                hero._cell._hero = null;
                hero._cell = cell;
                cell._hero = hero;

                hero._movementPointLeft -= 1;
                if (hero._movementPointLeft == 0)
                {
                    hero._image = _currentPlayer._heroImage;
                }
            }

            int goldMineType = (int)Heroes.Core.MineTypeEnum.Gold;

            if (cell._mine != null)
            {
                if (cell._mine._player == null)
                {
                    cell._mine._player = _currentPlayer;
                    cell._mine._image = _currentPlayer._goldMine;

                    _currentPlayer._mineKTypes[goldMineType].Add(cell._mine);

                    this.label_mines.Text = _currentPlayer._mineKTypes[goldMineType].Count.ToString();
                }
                else
                {
                    if (cell._mine._player == _currentPlayer)
                    { }
                    else
                    {
                        Player p = (Player)cell._mine._player;
                        p._mineKTypes[goldMineType].Remove(cell._mine);
                        cell._mine._player = _currentPlayer;
                        cell._mine._image = _currentPlayer._goldMine;
                        _currentPlayer._mineKTypes[goldMineType].Add(cell._mine);
                        this.label_mines.Text = _currentPlayer._mineKTypes[goldMineType].Count.ToString();
                    }
                }
                // replace image

                // change income
            }

            Draw();
        }

        private void Attack(Cell cell)
        {
            // confirmation message
            if (MessageBox.Show("Do you want to attack?", "Attack", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            // minus movement point
            _currentHero._movementPointLeft -= 1;
            if (_currentHero._movementPointLeft == 0)
            {
                _currentHero._image = _currentPlayer._heroImage;
                Draw();
            }

            // start battle
            {
                // clone
                Heroes.Core.Hero attackHero = _currentHero;
                //Heroes.Core.Hero attackHero = new Heroes.Core.Hero();
                //attackHero.CopyFrom(_currentHero);

                Heroes.Core.Hero defendHero = cell._hero;
                //Heroes.Core.Hero defendHero = new Heroes.Core.Hero();
                //defendHero.CopyFrom(cell._hero);

                Heroes.Core.Monster monster = cell._monster;

                Heroes.Core.Town castle = cell._castle;

                StartingBattleEventArg eventArg = new StartingBattleEventArg(attackHero, defendHero, monster, castle);
                OnStartingBattle(eventArg);

                // set battle result
                if (!_isMultiPlayer)
                {
                    if (eventArg._victory == 1)
                    {
                        // attacker win

                        // remove defender
                        Heroes.Core.Player player = cell._hero._player;
                        player._heroes.Remove(cell._hero);
                        cell._hero = null;
                    }
                    else
                    {
                        // defender win

                        // remove attacker
                        Heroes.Core.Player player = _currentHero._player;
                        player._heroes.Remove(_currentHero);
                        cell._hero = null;
                    }

                    Draw();
                }
            }
        }

        private void VisitCastle(Cell cell)
        {
            cell._castle._heroVisit = _currentHero;

            VisitingCastleEventArg eventArg = new VisitingCastleEventArg(cell._castle);
            OnVisitingCastle(eventArg);

            // minus movement point
            _currentHero._movementPointLeft -= 1;
            if (_currentHero._movementPointLeft == 0)
            {
                _currentHero._image = _currentPlayer._heroImage;
                Draw();
            }
        }

        private Cell GetNextCell(Cell currentCell, DirectionEnum direction)
        {
            switch (direction)
            {
                case DirectionEnum.Up:
                    {
                        if (currentCell._row - 1 < 0) return null;

                        Cell cell = _cells[currentCell._row - 1, currentCell._col];
                        return cell;
                    }
                case DirectionEnum.Down:
                    {
                        if (currentCell._row + 1 >= _rowCount) return null;
                        //if (_cells[currentCell._row + 1, currentCell._col]._hero != null) return null;

                        Cell cell = _cells[currentCell._row + 1, currentCell._col];
                        return cell;
                    }
                case DirectionEnum.Left:
                    {
                        if (currentCell._col - 1 < 0) return null;
                        //if (_cells[currentCell._row, currentCell._col -1]._hero != null) return null;

                        Cell cell = _cells[currentCell._row, currentCell._col - 1];
                        return cell;
                    }
                case DirectionEnum.Right:
                    {
                        if (currentCell._col + 1 >= _colCount) return null;
                        //if (_cells[currentCell._row, currentCell._col + 1]._hero != null) return null;

                        Cell cell = _cells[currentCell._row, currentCell._col + 1];
                        return cell;
                    }
            }

            return null;
        }

        private List<Cell> GetNearbyCell(Cell currentCell, DirectionEnum direction)
        {
            Cell ccell;
            List<Cell> listcell = new List<Cell>();
            for (int zz = 1; zz < _currentHero._movementPointLeft; zz++)
            {
                ccell = new Cell();
                ccell._col = currentCell._col - zz;
                listcell.Add(ccell);
                ccell = new Cell();
                ccell._col = currentCell._col + zz;
                listcell.Add(ccell);
                ccell = new Cell();
                ccell._row = currentCell._row - zz;
                listcell.Add(ccell);
                ccell = new Cell();
                ccell._row = currentCell._row + zz;
                listcell.Add(ccell);
            }
            return listcell;
        }

        private void FindPath(Cell cell)
        {

        }

        public void HeroLevelUp(Heroes.Core.Hero hero, int experience)
        {
            hero._experience += experience;
            int nextLevelExp = (int)Heroes.Core.Setting._heroExpKLvs[hero._level + 1];

            if (hero._experience >= nextLevelExp)
            {
                hero._level += 1;

                // show level up screen
                MessageBox.Show(string.Format("Hero Level Upgrade/r/n/r/nUpgrade from Level:{0} to Level:{1}/r/n/r/nCurrent Experience:{2}", (hero._level - 1).ToString(), hero._level.ToString(), hero._experience.ToString()));
            }
        }

        #region End Turn
        private void msNextTurn_Click(object sender, EventArgs e)
        {
            // cannot end turn when it is read only
            if (_isReadOnly) return;

            if (_isMultiPlayer)
            {
                EndTurnPressedEventArg e2 = new EndTurnPressedEventArg();
                OnEndTurnPressed(e2);
            }
            else
            {
                NextPlayer();
            }
        }

        public void NextPlayer()
        {
            // reset hero image
            foreach (Hero hero in _currentPlayer._heroes)
            {
                hero._image = _currentPlayer._heroImage;
            }

            // add resources
            int goldMineType = (int)Heroes.Core.MineTypeEnum.Gold;
            _currentPlayer._gold += (_currentPlayer._mineKTypes[goldMineType].Count) * 2000;
            if (gv._turnCount == 7)
            {
                gv._turnCount = 0;
            }
            else
            {
                gv._turnCount += 1;
            }
            if (gv._turnCount == 0)
            {
                gv._turnCount += 1;
                foreach (Heroes.Core.Town town in _currentPlayer._castles)
                {
                    foreach (Heroes.Core.Army army in town._armyAvKIds.Values)
                    {
                        // hardcode growth 5 units
                        army._qty += 20;
                    }
                }
            }


            // get next player
            {
                int index = _players.IndexOf(_currentPlayer);
                index += 1;
                if (index >= _players.Count) index = 0;
                _currentPlayer = (Player)_players[index];
            }

            // reset movement points
            foreach (Hero hero in _currentPlayer._heroes)
            {
                hero._movementPointLeft = hero._movementPoint;
                hero._image = _currentPlayer._heroHighlight;
            }
            Draw();

            this.label_currentPlayer.Text = "Player " + _currentPlayer._id;
            this.label_gold.Text = _currentPlayer._gold.ToString();
            this.label_heroes.Text = _currentPlayer._heroes.Count.ToString();
            this.label_mines.Text = _currentPlayer._mineKTypes[goldMineType].Count.ToString();

            // Do not pop message
            //MessageBox.Show(string.Format("Now is Player {0}", _currentPlayer._id));
        }
        #endregion

    }

    public enum DirectionEnum
    {
        None,
        Right,
        Down,
        Left,
        Up
    }

}
