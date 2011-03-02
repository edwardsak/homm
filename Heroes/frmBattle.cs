using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;

using Heroes.Core;
using Heroes.Core.Battle;
using Heroes.Core.Battle.Rendering;
using Heroes.Core.Battle.OtherIO;
using Heroes.Core.Battle.Characters;
using Heroes.Core.Battle.Characters.Armies;
using Heroes.Core.Battle.Characters.Commands;
using Heroes.Core.Battle.Terrains;

namespace Heroes
{
    public partial class frmBattle : Form
    {
        #region Events
        public delegate void CommandIssuedEventHandler(CommandIssuedEventArg e);

        public event CommandIssuedEventHandler CommandIssued;

        #region Event Arguments
        public class CommandIssuedEventArg
        {
            public int _x;
            public int _y;
            public MouseButtons _button;
            public bool _doubleClick;
            public int _cmdType;
            public Spell _spell;

            public CommandIssuedEventArg(int x, int y, MouseButtons button, bool doubleClick, int cmdType, Spell spell)
            {
                _x = x;
                _y = y;
                _button = button;
                _doubleClick = doubleClick;
                _cmdType = cmdType;
                _spell = spell;
            }
        }
        #endregion

        protected virtual void OnCommandIssued(CommandIssuedEventArg e)
        {
            if (CommandIssued != null)
            {
                //Invokes the delegates.
                CommandIssued(e);
            }
        }
        #endregion

        SoundPlayer sp;

        TimeSpan ts;

        public Controller _controller;
        public Renderer _renderer;
        public BattleEngine _engine;

        public Poller _poller;
        public InputCommand _inputMethod;

        public bool _isActivated;
        public bool _clicked;

        Heroes.Core.Hero _attackHero;
        Heroes.Core.Hero _defendHero;
        Heroes.Core.Monster _monster;
        Heroes.Core.Town _defendCastle;

        public Heroes.Core.Battle.BattleSideEnum _victory;

        Timer _timer1;
        bool _isExit;

        // flags
        public bool _isMultiPlayer;
        public int _playerIdMe;

        // hero info
        Heroes.Core.Battle.frmHeroInfo _frmHeroInfo;

        // army info
        Heroes.Core.Battle.frmArmyInfo _frmArmyInfo;

        // button rectangle
        Rectangle _rectRetreat;
        Rectangle _rectCastSpell;
        Rectangle _rectWait;
        Rectangle _rectDefend;
        Rectangle _rectStatus;

        public frmBattle()
        {
            InitializeComponent();

            ts = BasicEngine.TurnTimeSpan;

            // Make sure all the painting occurs in the PaintEvent
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);

            _isActivated = true;
            _clicked = false;

            // flags
            _isMultiPlayer = false;
            _playerIdMe = 0;

            _timer1 = new Timer();
            _timer1.Interval = 100;
            _timer1.Stop();
            _timer1.Tick += new EventHandler(_timer1_Tick);

            _isExit = false;

            _frmHeroInfo = null;
            _frmArmyInfo = null;

            // bar
            _rectRetreat = new Rectangle(104, 556 + 4, 50, 38);
            _rectCastSpell = new Rectangle(644, 556 + 4, 50, 38);
            _rectWait = new Rectangle(695, 556 + 4, 50, 38);
            _rectDefend = new Rectangle(746, 556 + 4, 50, 38);
            _rectStatus = new Rectangle(214, 556 + 7, 400, 32);

            this.MouseMove += new MouseEventHandler(frmMain_MouseMove);
            this.MouseClick += new MouseEventHandler(frmMain_MouseClick);
            this.MouseDoubleClick += new MouseEventHandler(frmMain_MouseDoubleClick);
            this.MouseDown += new MouseEventHandler(frmBattle_MouseDown);
            this.MouseUp += new MouseEventHandler(frmBattle_MouseUp);

            this.FormClosing += new FormClosingEventHandler(frmMain_FormClosing);
        }

        void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sp != null) sp.Stop();

            _isExit = true;
        }

        ~frmBattle()
        {
            this._controller.DisposeGraphics();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string s = string.Format("{0:ss}",DateTime.Now);
            string musicPath = "";
            int m = Convert.ToInt32(s);
            while (m > 1)
            {
                if (m > 10)
                {
                    m -= 10;
                }
                else
                    m -= 2;
            }
            
            if (m < 0)
                m += 1;
            
            if (m == 0)
                musicPath = Application.StartupPath + @"\music\war01.wav";
            else
                musicPath = Application.StartupPath + @"\music\war02.wav";

            //sp = new SoundPlayer(musicPath);
            //sp.PlayLooping();

            _timer1.Start();
        }

        public DialogResult ShowDialog(Heroes.Core.Hero attackHero, Heroes.Core.Hero defendHero, Heroes.Core.Monster monster, Heroes.Core.Town defendCastle)
        {
            _attackHero = attackHero;
            _defendHero = defendHero;
            _monster = monster;
            _defendCastle = defendCastle;

            _controller = new Controller(this, null, new Size(800, 600), null);
            _poller = new Poller(this);
            _inputMethod = new InputPollerCommand(_poller);

            SetupEngine();
            SetupRenderer();

            return this.ShowDialog();
        }

        void _timer1_Tick(object sender, EventArgs e)
        {
            _timer1.Stop();

            //Microsoft.DirectX.AudioVideoPlayback.Audio audioPlayer
            //    = new Microsoft.DirectX.AudioVideoPlayback.Audio(@"C:\Program Files\3DO\Heroes3\MP3\COMBAT01.MP3", true);
            //audioPlayer.Volume = -2000;

            Start();
        }

        private void Start()
        {
            //try
            while (!_isExit)
            {
                this._controller.Render();

                // display frame rate
                this.Text = string.Format("FrameRate: {0}", this._controller.FrameRate);

                long ettk = this._controller.ElapsedTimeSinceLastRender.Ticks;

                while (ettk >= ts.Ticks)
                {
                    ettk = ettk - ts.Ticks;

                    // get input
                    this._poller.Poll();

                    this._engine.Run(BasicEngine.TurnTimeSpan);
                    //mw.Text += string.Format(", Turn: {0}", mw._engine.TurnCount);

                    ts = BasicEngine.TurnTimeSpan;
                }
                ts = ts - TimeSpan.FromTicks(ettk);

                Application.DoEvents();

                if (this._isActivated == false)
                {
                    System.Threading.Thread.Sleep(20);
                }
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show("There was an error (" + ex.GetType().Name + "): " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace, "Texas Quest Error");
            //}
        }

        private void SetupRenderer()
        {
            _renderer = new BattleRenderer(_engine);

            _controller.Renderer = _renderer;
        }

        protected void SetupEngine()
        {
            _engine = new BattleEngine(_controller, _attackHero, _defendHero, _monster, _defendCastle);
            _engine._rectStatusMsg = _rectStatus;

            _engine.BattleEnded += new BattleEngine.BattleEndedEventHandler(_engine_BattleEnded);
            _engine.DamageCalculated += new BattleEngine.DamageCalculatedEventHandler(_engine_DamageCalculated);
        }

        void _engine_DamageCalculated(BattleEngine.DamageCalculatedEventArg e)
        {
            //this.lblStatus.Text = string.Format("Damage {0}-{1}, Kill {2}-{3}, Avg Kill {4}",
            //    e._minDamage, e._maxDamage, e._minKill, e._maxKill, e._avgKill);
        }

        void _engine_BattleEnded(Heroes.Core.Battle.BattleEngine.BattleEndedEventArg e)
        {
            _victory = e._victory;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #region Mouse Input
        void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Cursor == Cursors.Hand) return;

            _engine.ProcessMouseMove(e.X, e.Y, this);
        }

        void frmMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (!CanIMove()) return;

            // cannot click if not my turn
            if (this._engine._turn._currentCharacter._playerId != this._playerIdMe) return;

            if (e.Button == MouseButtons.Left)
            {
                if (_rectRetreat.Contains(e.X, e.Y))
                {
                    this.Close();
                    return;
                }
                else if (_rectCastSpell.Contains(e.X, e.Y))
                {
                    Heroes.Core.Battle.Characters.Hero hero = GetCurrentHero();
                    ShowCastSpell(hero);
                    return;
                }
                else if (_rectWait.Contains(e.X, e.Y))
                {
                    _engine._turn.Wait();

                    CommandIssuedEventArg e2 = new CommandIssuedEventArg(e.X, e.Y, e.Button, true, (int)CommandTypeEnum.Wait, null);
                    OnCommandIssued(e2);
                    return;
                }
                else if (_rectDefend.Contains(e.X, e.Y))
                {
                    _engine._turn.Defend();

                    CommandIssuedEventArg e2 = new CommandIssuedEventArg(e.X, e.Y, e.Button, true, (int)CommandTypeEnum.Defend, null);
                    OnCommandIssued(e2);
                    return;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // cancel cast spell
                if (this.Cursor == Cursors.Hand)
                {
                    Heroes.Core.Battle.Characters.Hero hero = GetCurrentHero();
                    ShowCastSpell(hero);
                    return;
                }
            }

            {
                Heroes.Core.Battle.CommandTypeEnum cmdType = Heroes.Core.Battle.BattleEngine.GetCommandType(this.Cursor);
                _engine.ProcessMouseClick(e.X, e.Y, e.Button, false, cmdType, null);
            }
        }

        void frmMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!CanIMove()) return;

            // cannot click if not my turn
            if (this._engine._turn._currentCharacter._playerId != this._playerIdMe) return;

            Heroes.Core.Battle.CommandTypeEnum cmdType = Heroes.Core.Battle.BattleEngine.GetCommandType(this.Cursor);

            Heroes.Core.Spell spell = null;
            if (cmdType == CommandTypeEnum.Spell)
            {
                Heroes.Core.Battle.Characters.Hero hero = GetCurrentHero();
                spell = hero._currentSpell;
            }

            _engine.ProcessMouseClick(e.X, e.Y, e.Button, true, cmdType, spell);

            // reset cursor
            this.Cursor = Cursors.Default;

            CommandIssuedEventArg e2 = new CommandIssuedEventArg(e.X, e.Y, e.Button, true, (int)cmdType, spell);
            OnCommandIssued(e2);
        }

        void frmBattle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // get hero info
                // attack hero
                if (_engine._attacker != null)
                {
                    Heroes.Core.Battle.Characters.Hero hero = _engine._attacker;
                    if (hero._rect.Contains(e.X, e.Y))
                    {
                        _frmHeroInfo = new frmHeroInfo();
                        _frmHeroInfo.StartPosition = FormStartPosition.Manual;

                        Point pt = this.PointToScreen(new Point(e.X, e.Y));
                        _frmHeroInfo.Location = pt;

                        _frmHeroInfo.Show(this, hero);

                        return;
                    }
                }

                // defend hero
                if (_engine._defender != null)
                {
                    Heroes.Core.Battle.Characters.Hero hero = (Heroes.Core.Battle.Characters.Hero)_engine._defender;
                    if (hero._rect.Contains(e.X, e.Y))
                    {
                        _frmHeroInfo = new frmHeroInfo();
                        _frmHeroInfo.StartPosition = FormStartPosition.Manual;

                        Point pt = this.PointToScreen(new Point(e.X, e.Y));
                        _frmHeroInfo.Location = pt;

                        _frmHeroInfo.Show(this, hero);

                        return;
                    }
                }

                // get army info
                {
                    Heroes.Core.Battle.Characters.Armies.Army army = _engine.GetArmy(e.X, e.Y);
                    if (army == null) return;

                    _frmArmyInfo = new frmArmyInfo();
                    _frmArmyInfo.StartPosition = FormStartPosition.Manual;

                    Point pt = this.PointToScreen(new Point(e.X, e.Y));
                    _frmArmyInfo.Location = pt;

                    _frmArmyInfo.Show(this, army);
                    
                    return;
                }
            }
        }

        void frmBattle_MouseUp(object sender, MouseEventArgs e)
        {
            if (_frmHeroInfo != null)
            {
                _frmHeroInfo.Close();
                _frmHeroInfo.Dispose();
                _frmHeroInfo = null;
            }

            if (_frmArmyInfo != null)
            {
                _frmArmyInfo.Close();
                _frmArmyInfo.Dispose();
                _frmArmyInfo = null;
            }
        }

        private bool CanIMove()
        {
            if (_isMultiPlayer)
            {
                if (_engine._turn._currentCharacter._playerId != this._playerIdMe)
                {
                    return false;
                }
            }

            return true;
        }

        public void ExecuteCommand(Heroes.Core.Remoting.BattleCommand cmd)
        {
            if (cmd._cmdType == (int)Heroes.Core.Battle.CommandTypeEnum.Defend)
            {
                _engine._turn.Defend();
            }
            else if (cmd._cmdType == (int)Heroes.Core.Battle.CommandTypeEnum.Wait)
            {
                _engine._turn.Wait();
            }
            else
            {
                _engine.ProcessMouseClick(cmd._x, cmd._y, (MouseButtons)cmd._button, cmd._doubleClick,
                    (Heroes.Core.Battle.CommandTypeEnum)cmd._cmdType, cmd._spell);
            }
        }
        #endregion

        private void ShowCastSpell(Heroes.Core.Battle.Characters.Hero hero)
        {
            Heroes.Core.Battle.frmCastSpell f = new frmCastSpell();
            if (f.ShowDialog(hero) == DialogResult.Cancel)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            this.Cursor = Cursors.Hand;
        }

        private Heroes.Core.Battle.Characters.Hero GetCurrentHero()
        {
            int heroId = _engine._turn._currentCharacter._heroId;
            Heroes.Core.Battle.Characters.Hero hero = null;
            if (_engine._attacker._id == heroId)
                hero = _engine._attacker;
            else if (_engine._defender != null && _engine._defender._id == heroId)
                hero = _engine._defender;
            else
                return null;

            return hero;
        }

    }
}
