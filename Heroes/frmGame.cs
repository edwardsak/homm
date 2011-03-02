using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace Heroes
{
    public partial class frmGame : Form
    {
        #region Events
        public delegate void FormTerminatedEventHandler();

        public event FormTerminatedEventHandler FormTerminated;

        #region Event Arguments
        #endregion

        protected virtual void OnFormTerminated()
        {
            if (FormTerminated != null)
            {
                //Invokes the delegates.
                FormTerminated();
            }
        }
        #endregion

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static LockToken _lockToken = new LockToken();

        bool _isStartClicked;

        Remoting.RegisterServer _register;

        bool _isExit;

        // get/set flags
        Thread _processThread;

        // main thread timer
        System.Windows.Forms.Timer _timerGame;
        System.Windows.Forms.Timer _timerBattle;

#if DEBUG
        public static System.IO.TextWriter _logWriter = null;
#endif

        public frmGame()
        {
            InitializeComponent();

            _isStartClicked = false;

            Remoting.GameSetting._frmMap = null;
            Remoting.GameSetting._isGameStarted = false;

            _isExit = false;

            // thread
            ThreadStart entryPoint = new ThreadStart(StartProcess);
            _processThread = new Thread(entryPoint);
            _processThread.Name = "Processing Thread";

            // timer start game
            _timerGame = new System.Windows.Forms.Timer();
            _timerGame.Interval = 500;
            _timerGame.Tick += new EventHandler(_timerGame_Tick);
            _timerGame.Stop();

            _timerBattle = new System.Windows.Forms.Timer();
            _timerBattle.Interval = 500;
            _timerBattle.Tick += new EventHandler(_timerBattle_Tick);
            _timerBattle.Stop();

            this.FormClosing += new FormClosingEventHandler(frmGame_FormClosing);
            this.FormClosed += new FormClosedEventHandler(frmGame_FormClosed);

#if DEBUG
            _logWriter = new System.IO.StreamWriter("log.txt");
#endif
        }

        public DialogResult ShowDialog(bool isCreateGame)
        {
            if (isCreateGame)
            {
                Remoting.GameSetting._isServer = true;

                CreateGame();
                SetupServer();
            }
            else
            {
                this.cmdStart.Visible = false;

                Remoting.GameSetting._isServer = false;
            }

            // get changes
            _processThread.Start();
            
            return this.ShowDialog();
        }

        private void frmGame_Load(object sender, EventArgs e)
        {
            // for trigger event from .Net remoting
            Remoting.GameSetting._frmGame = this;
        }

        void frmGame_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        void frmGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isExit = true;
            _processThread.Join(30000);

#if DEBUG
            if (_logWriter != null)
                _logWriter.Close();
#endif

            // raise closing event
            OnFormTerminated();

            if (Remoting.GameSetting._frmMap != null)
            {
                Remoting.GameSetting._frmMap.Close();
                Remoting.GameSetting._frmMap.Dispose();
            }

            if (_register != null)
                _register.Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Create Game
        private void SetupServer()
        {
            _register = new Remoting.RegisterServer();

            if (!_register.GetSetting())
            {
                MessageBox.Show("Get Settings fail.");
                return;
            }

            _register.RegisterServer();
            _register.RegisterServices();
        }

        private void CreateGame()
        {
            //string ip = Setting.GetLocalIp();
            
            Player player = new Player();
            player._id = 1;
            player._ip = "127.0.0.1";

            Remoting.GameSetting._server = player;
            Remoting.GameSetting._me = player;

            Remoting.GameSetting._players.Add(player);
        }
        #endregion

        #region Start Game
        private void cmdStart_Click(object sender, EventArgs e)
        {
            lock (_lockToken)
            {
                _isStartClicked = true;
                Remoting.GameSetting._isNeedToStartGame = true;
                Remoting.GameSetting._playerCount = Remoting.GameSetting._players.Count;
                Remoting.GameSetting._currentPlayer = Remoting.GameSetting._me;
            }
        }

        public void StartGame(int playerCount)
        {
            this.Hide();

            Heroes.Core.Map.frmMap f = new Heroes.Core.Map.frmMap();
            Remoting.GameSetting._frmMap = f;
            f.IsMultiPlayer = true;
            f.ReadOnly = true;
            //f.Initialize(playerCount);
            f.Initialize(2);

            if (Remoting.GameSetting._isServer) f.Text = "Server";
            else f.Text = "Client";

            f.VisitingCastle += new Heroes.Core.Map.frmMap.VisitingCastleEventHandler(frmMap_VisitingCastle);
            f.StartingBattle += new Heroes.Core.Map.frmMap.StartingBattleEventHandler(frmMap_StartingBattle);
            f.EndTurnPressed += new Heroes.Core.Map.frmMap.EndTurnPressedEventHandler(frmMap_EndTurnPressed);
            f.FormTerminated += new Heroes.Core.Map.frmMap.FormTerminatedEventHandler(frmMap_FormTerminated);

            lock (_lockToken)
            {
                Remoting.GameSetting._isGameStarted = true;
            }
            
            f.ShowDialog();

            this.Close();
        }

        void frmMap_VisitingCastle(Heroes.Core.Map.frmMap.VisitingCastleEventArg e)
        {
            using (Heroes.Core.Castle.frmCastle f = new Heroes.Core.Castle.frmCastle())
            {
                f.ShowDialog(e._castle);
            }
        }

        void frmMap_StartingBattle(Heroes.Core.Map.frmMap.StartingBattleEventArg e)
        {
            // TODO
            //SetBattle(e._attackHero, e._defendHero, e._monster, e._defendCastle);

            lock (_lockToken)
            {
                Remoting.GameSetting._amIBattle = true;
            }

            StartBattle();

            // set battle result
            // modify data on map
            {
                if (Remoting.GameSetting._victory == Heroes.Core.Battle.BattleSideEnum.Attacker)
                {
                    // remove defender
                    if (Remoting.GameSetting._defendPlayer != null)
                    {
                        Heroes.Core.Map.Player player
                            = (Heroes.Core.Map.Player)Remoting.GameSetting.FindPlayer(Remoting.GameSetting._defendPlayer._id, Remoting.GameSetting._frmMap._players);

                        if (Remoting.GameSetting._defendHero != null)
                        {
                            Heroes.Core.Map.Hero hero 
                                = (Heroes.Core.Map.Hero)Remoting.GameSetting.FindHero(Remoting.GameSetting._defendHero._id, player._heroes);

                            hero._cell._hero = null;
                            player._heroes.Remove(hero);
                        }
                    }

                    // modified by ref 
                    // update attacker
                    //if (Remoting.GameSetting._attackPlayer != null)
                    //{
                    //    Heroes.Core.Map.Player player
                    //        = (Heroes.Core.Map.Player)Remoting.GameSetting.FindPlayer(Remoting.GameSetting._attackPlayer._id, Remoting.GameSetting._frmMap._players);

                    //    if (Remoting.GameSetting._attackHero != null)
                    //    {
                    //        Heroes.Core.Map.Hero hero 
                    //            = (Heroes.Core.Map.Hero)Remoting.GameSetting.FindHero(Remoting.GameSetting._attackHero._id, player._heroes);

                    //        hero._armyKSlots.Clear();
                    //        foreach (Heroes.Core.Army army in Remoting.GameSetting._attackArmies)
                    //        {
                    //            hero._armyKSlots.Add(army._slotNo, army);
                    //        }
                    //    }
                    //}
                }
                else
                {
                    // remove attacker
                    if (Remoting.GameSetting._attackPlayer != null)
                    {
                        Heroes.Core.Map.Player player
                            = (Heroes.Core.Map.Player)Remoting.GameSetting.FindPlayer(Remoting.GameSetting._attackPlayer._id, Remoting.GameSetting._frmMap._players);

                        if (Remoting.GameSetting._attackHero != null)
                        {
                            Heroes.Core.Map.Hero hero = (Heroes.Core.Map.Hero)Remoting.GameSetting.FindHero(Remoting.GameSetting._attackHero._id, player._heroes);

                            hero._cell._hero = null;
                            player._heroes.Remove(hero);
                        }
                    }

                    // modified by ref 

                    // update defender
                    //if (Remoting.GameSetting._defendPlayer != null)
                    //{
                    //    Heroes.Core.Map.Player player
                    //        = (Heroes.Core.Map.Player)Remoting.GameSetting.FindPlayer(Remoting.GameSetting._defendPlayer._id, Remoting.GameSetting._frmMap._players);

                    //    if (Remoting.GameSetting._defendHero != null)
                    //    {
                    //        Heroes.Core.Map.Hero hero
                    //            = (Heroes.Core.Map.Hero)Remoting.GameSetting.FindHero(Remoting.GameSetting._defendHero._id, player._heroes);

                    //        hero._armyKSlots.Clear();
                    //        foreach (Heroes.Core.Army army in Remoting.GameSetting._defendArmies)
                    //        {
                    //            hero._armyKSlots.Add(army._slotNo, army);
                    //        }
                    //    }
                    //}
                }

                Remoting.GameSetting._frmMap.Draw();
            }

            // set battle end flag
            lock (_lockToken)
            {
                Remoting.GameSetting._isBattleEnded = true;

                if (!SetBattleEnded())
                {
                    // error
                    return;
                }
            }
        }

        void frmMap_EndTurnPressed(Heroes.Core.Map.frmMap.EndTurnPressedEventArg e)
        {
            lock (_lockToken)
            {
                WriteLog("EndTurnPressed event");
                //Debug.WriteLine(string.Format("{0}: NextTurnEvent", Thread.CurrentThread.Name));

                // submit game settings to server
                if (!SetGameSettings())
                {
                    // error
                    return;
                }

                if (Remoting.GameSetting._isServer)
                {
                    WriteLog("Server: Run NextPlayer");

                    Remoting.GameSetting.NextPlayer();
                }
                else
                {
                    WriteLog("Client: Set _needToRunNextPlayer = true");

                    // client need to set NeedToRunNextPlayer flag in server
                    if (!SetNeedToRunNextPlayer())
                    {
                        // error
                        return;
                    }
                }

                // update GUI
                WriteLog("frmMap.NextPlayer");
                Remoting.GameSetting._frmMap.NextPlayer();

                WriteLog("ProcessMapEndTurn: Set frmMap.ReadOnly = true");
                Remoting.GameSetting._frmMap.ReadOnly = true;
            }
        }

        void frmMap_FormTerminated()
        {
            // if form is already hide, if close will not rise close event
            this.Show();
        }
        #endregion

        #region Network 
        public bool PplPlayersProperty
        {
            set
            {
                PplPlayers();
            }
        }

        public void PplPlayers()
        {
            DataGridView dgv = this.dgvPlayer;
            dgv.Rows.Clear();

            int rowIndex = 0;
            DataGridViewRow dgvr = null;
            foreach (Player player in Remoting.GameSetting._players)
            {
                rowIndex = dgv.Rows.Add();
                dgvr = dgv.Rows[rowIndex];
                dgvr.Cells[this.colId.Index].Value = player._id;
                dgvr.Cells[this.colPlayerIp.Index].Value = player._ip;
            }
        }

        public bool ReDrawMapProperty
        {
            set 
            {
                Remoting.GameSetting._frmMap.Draw();
            }
        }

        public bool NextPlayerProperty
        {
            set
            {
                //Debug.WriteLine(string.Format("{0}: NextPlayerProperty", Thread.CurrentThread.Name));
                lock (_lockToken)
                {
                    Remoting.GameSetting._needToRunNextPlayer = false;
                    Remoting.GameSetting.NextPlayer();

                    // update GUI
                    Remoting.GameSetting._frmMap.NextPlayer();

                    WriteLog(string.Format("NextPlayerProperty, CurrentPlayer={0}", Remoting.GameSetting._currentPlayer._id));
                }
            }
        }

        public bool StartGameProperty
        {
            set
            {
                _timerGame.Start();
            }
        }

        public bool StartBattleProperty
        {
            set
            {
                _timerBattle.Start();
            }
        }

        public BattleCommand ExecuteBattleCommandProperty
        {
            set
            {
                ExecuteBattleCommand(value);
            }
        }

        void _timerGame_Tick(object sender, EventArgs e)
        {
            _timerGame.Stop();
            StartGame(Remoting.GameSetting._playerCount);
        }

        void _timerBattle_Tick(object sender, EventArgs e)
        {
            _timerBattle.Stop();
            StartBattle();
        }

        private void StartProcess()
        {
            while (!_isExit)
            {
                Thread.Sleep(1000);
                //Debug.WriteLine(string.Format("Thread: {0:HH:mm:ss}", DateTime.Now));

                // menu
                {
                    if (Remoting.GameSetting._isServer)
                    {
                        if (!_isStartClicked)
                        {
                            // ppl players
                            SetControlPropertyThreadSafe(this, "PplPlayersProperty", true);

                            goto NEXT_ROUND;
                        }
                    }
                }

                // start game
                {
                    if (!Remoting.GameSetting._isNeedToStartGame)
                    {
                        lock (_lockToken)
                        {
                            if (!IsNeedToStartGame(out Remoting.GameSetting._isNeedToStartGame,
                                out Remoting.GameSetting._playerCount))
                            {
                                // Error
                                return;
                            }
                        }

                        goto NEXT_ROUND;
                    }

                    if (!Remoting.GameSetting._isGameStarted)
                    {
                        SetControlPropertyThreadSafe(this, "StartGameProperty", true);

                        goto NEXT_ROUND;
                    }

                    if (!Remoting.GameSetting._isInitialized)
                    {
                        if (!GetGameSettings())
                        {
                            return;
                        }

                        if (!SetInitialized(Remoting.GameSetting._me._id))
                        {
                            return;
                        }

                        // redraw map
                        SetControlPropertyThreadSafe(this, "ReDrawMapProperty", true);

                        goto NEXT_ROUND;
                    }
                }

                // map
                {
                    if (Remoting.GameSetting._needToRunNextPlayer)
                    {
                        WriteLog("_needToRunNextPlayer = true");
                        //Debug.WriteLine(string.Format("{0}: NextPlayerThread", Thread.CurrentThread.Name));
                        SetControlPropertyThreadSafe(this, "NextPlayerProperty", true);
                        goto NEXT_ROUND;
                    }

                    if (Remoting.GameSetting._frmMap.ReadOnly)
                    {
                        // if map is read only, wait until my turn
                        WriteLog("frmMap.ReadOnly = true");

                        // get current player
                        int playerId = 0;
                        if (!GetCurrentPlayer(out playerId))
                        {
                            // error
                            return;
                        }

                        // me is current player, then play
                        if (playerId == Remoting.GameSetting._me._id)
                        {
                            WriteLog("Current Player = me");
                            //Debug.WriteLine("Next Turn");

                            if (!GetGameSettings())
                            {
                                // error
                                return;
                            }

                            Heroes.Core.Map.Hero hero = (Heroes.Core.Map.Hero)((Heroes.Core.Map.Player)Remoting.GameSetting._frmMap._players[0])._heroes[1];
                            WriteLog(string.Format("Hero: {0}, {1}, {2}", hero._id, hero._cell._row, hero._cell._col));
                            //Debug.WriteLine(string.Format("Hero: {0}, {1}, {2}", hero._id, hero._cell._row, hero._cell._col));

                            // redraw map
                            SetControlPropertyThreadSafe(this, "ReDrawMapProperty", true);

                            // set current player
                            SetToCurrentPlayer(Remoting.GameSetting._me._id);

                            // start play
                            SetControlPropertyThreadSafe(Remoting.GameSetting._frmMap, "ReadOnly", false);
                        }
                    }
                }

                // battle
                {
                    if (!Remoting.GameSetting._isNeedToStartBattle)
                    {
                        bool isNeedToStartBattle = false;
                        if (!IsNeedToStartBattle(out isNeedToStartBattle))
                        {
                            // error
                            return;
                        }

                        if (isNeedToStartBattle)
                        {
                            lock (_lockToken)
                            {
                                Remoting.GameSetting._isNeedToStartBattle = isNeedToStartBattle;

                                // get battle Settings
                                if (!GetBattleSettings(out Remoting.GameSetting._attackPlayer, out Remoting.GameSetting._attackHero, out Remoting.GameSetting._attackArmies,
                                    out Remoting.GameSetting._defendPlayer, out Remoting.GameSetting._defendHero, out Remoting.GameSetting._defendCastle, out Remoting.GameSetting._defendArmies))
                                {
                                    // error
                                    return;
                                }

                                // check if me is need to battle
                                if ((Remoting.GameSetting._attackPlayer != null && Remoting.GameSetting._attackPlayer._id == Remoting.GameSetting._me._id)
                                    || (Remoting.GameSetting._defendPlayer != null && Remoting.GameSetting._defendPlayer._id == Remoting.GameSetting._me._id))
                                {
                                    Remoting.GameSetting._amIBattle = true;

                                    // StartBattle
                                    SetControlPropertyThreadSafe(this, "StartBattleProperty", true);
                                }
                                else
                                {
                                    // no need to start battle
                                    Remoting.GameSetting._amIBattle = false;
                                    Remoting.GameSetting._isBattleStarted = true;
                                }
                            }
                        }

                        goto NEXT_ROUND;
                    }

                    if (Remoting.GameSetting._amIBattle)
                    {
                        // get battle commands
                        if (!Remoting.GameSetting._isBattleEnded)
                        {
                            // get commands
                            BattleCommand cmd = null;
                            do
                            {
                                // get commands from queue
                                if (!DequeueBattleCommand(Remoting.GameSetting._me._id, out cmd))
                                {
                                    // error
                                    return;
                                }
                                if (cmd == null) break;

                                // execute commands
                                SetControlPropertyThreadSafe(this, "ExecuteBattleCommandProperty", cmd);
                            } while (cmd != null);

                            // check is battle ended
                            bool isBattleEnded = false;
                            if (!IsBattleEnded(out isBattleEnded))
                            {
                                // error
                                return;
                            }

                            lock (_lockToken)
                            {
                                Remoting.GameSetting._isBattleEnded = isBattleEnded;
                            }
                        }
                        else
                        {
                            // battle end
                            if (!ClearBattleFlags())
                            {
                                // error
                                return;
                            }
                        }
                    }
                }

            NEXT_ROUND:
                continue;
            }
        }

        private void ProcessNetworkFlag()
        {
            ProcessStartGame();

            {
                

                if (!Remoting.GameSetting._me._isInitialized)
                {
                    ProcessInit();
                    return;
                }

                ProcessMap();

                ProcessBattle();
            }
        }

        private void ProcessStartGame()
        {
            if (!Remoting.GameSetting._isNeedToStartGame)
            {
                if (Remoting.GameSetting._isServer)
                {
                    if (!_isStartClicked)
                        PplPlayers();
                    else
                    {
                        lock (_lockToken)
                        {
                            Remoting.GameSetting._isNeedToStartGame = true;
                        }
                    }
                }

                return;
            }

            if (!Remoting.GameSetting._isGameStarted)
            {
                StartGame(Remoting.GameSetting._playerCount);
                return;
            }
        }

        private void ProcessInit()
        {
            if (!GetGameSettings())
            {
                return;
            }

            if (!SetInitialized(Remoting.GameSetting._me._id))
            {
                return;
            }
        }

        private void ProcessMap()
        {
            if (Remoting.GameSetting._isServer)
            {
                // if client click next player, server need to do so
                if (Remoting.GameSetting._needToRunNextPlayer)
                {
                    Remoting.GameSetting._needToRunNextPlayer = false;
                    Remoting.GameSetting._frmMap.NextPlayer();
                }
            }

            if (Remoting.GameSetting._frmMap.ReadOnly)
            {
                // if map is read only, wait until my turn

                // get current player
                int playerId = 0;
                if (!GetCurrentPlayer(out playerId))
                {
                    // error
                    return;
                }

                // if current playe == me then play
                if (playerId == Remoting.GameSetting._me._id)
                {
                    if (!GetGameSettings())
                    {
                        // error
                        return;
                    }

                    // set current player
                    SetToCurrentPlayer(Remoting.GameSetting._me._id);

                    // start play
                    Remoting.GameSetting._frmMap.ReadOnly = false;
                }
            }
        }

        #region Battle
        private void ProcessBattle()
        {
            bool isStarted = false;
            if (!IsBattleStarted(out isStarted))
            {
                // error
                return;
            }

            if (!isStarted) return;

            if (!Remoting.GameSetting._isBattleStarted)
            {
                if (Remoting.GameSetting._attackArmies == null)
                {
                    lock (_lockToken)
                    {
                        // get once
                        // get battle Settings
                        if (!GetBattleSettings(out Remoting.GameSetting._attackPlayer, out Remoting.GameSetting._attackHero, out Remoting.GameSetting._attackArmies,
                            out Remoting.GameSetting._defendPlayer, out Remoting.GameSetting._defendHero, out Remoting.GameSetting._defendCastle, out Remoting.GameSetting._defendArmies))
                        {
                            // error
                            return;
                        }
                    }
                }

                // start battle if me is attacker or defender
                if ((Remoting.GameSetting._attackPlayer != null && Remoting.GameSetting._attackPlayer._id == Remoting.GameSetting._me._id)
                    || (Remoting.GameSetting._defendPlayer != null && Remoting.GameSetting._defendPlayer._id == Remoting.GameSetting._me._id))
                {
                    StartBattle();
                }
            }
            else
            {
                // client need to process queue command
                if (!Remoting.GameSetting._isServer)
                {
                    BattleCommand cmd = null;
                    do
                    {
                        // get commands from queue
                        if (!DequeueBattleCommand(Remoting.GameSetting._me._id, out cmd))
                        {
                            // error
                            return;
                        }
                        if (cmd == null) break;

                        // update commands
                        ExecuteBattleCommand(cmd);
                    } while (cmd != null);
                }
            }
        }

        private void StartBattle()
        {
            if (Remoting.GameSetting._frmBattle != null) return;

            {
                try
                {
                    using (frmBattle f = new frmBattle())
                    {
                        //f.ReadOnly = true;
                        f._isMultiPlayer = true;

                        f.CommandIssued += new frmBattle.CommandIssuedEventHandler(f_CommandIssued);

                        // make a copies of battle, to avoid thread issue
                        //Heroes.Core.Player attackPlayer  = null;

                        lock (_lockToken)
                        {
                            Remoting.GameSetting._frmBattle = f;
                            Remoting.GameSetting._isBattleStarted = true;

                            // make a copies of battle
                            //attackPlayer = Remoting.GameSetting._attackPlayer.Clone();
                        }
                        
                        // TODO
                        if (f.ShowDialog(Remoting.GameSetting._attackHero, Remoting.GameSetting._defendHero, null, Remoting.GameSetting._defendCastle) != DialogResult.OK)
                        {
                            //e._isCancel = true;
                            return;
                        }

                        // set battle results
                        lock (_lockToken)
                        {
                            Remoting.GameSetting._victory = f._victory;
                            //Remoting.GameSetting._attackPlayer = attackPlayer;
                        }
                    }
                }
                catch
                {
                    //e._isCancel = true;
                }
            }
        }

        void f_CommandIssued(frmBattle.CommandIssuedEventArg e)
        {
            // add battle command to queue
            if (!EnqueueBattleCommand(Remoting.GameSetting._me._id, e))
            {
                // error
                return;
            }
        }

        private bool IsNeedToStartBattle(out bool isNeedToStartBattle)
        {
            isNeedToStartBattle = false;

            Remoting.Game adp = null;
            if (Remoting.GameSetting._isServer)
            {
                adp = new Heroes.Remoting.Game();
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.IsNeedToStartBattle(out isNeedToStartBattle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool IsBattleStarted(out bool isBattleStarted)
        {
            isBattleStarted = false;

            Remoting.Game adp = null;
            if (Remoting.GameSetting._isServer)
            {
                adp = new Heroes.Remoting.Game();
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.IsBattleStarted(out isBattleStarted);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool SetBattle(Heroes.Core.Player attackPlayerMap, Heroes.Core.Hero attackHeroMap, Hashtable attackArmiesMap,
            Heroes.Core.Player defendPlayerMap, Heroes.Core.Hero defendHeroMap, Heroes.Core.Town defendCastleMap, Hashtable defendArmiesMap)
        {
            Heroes.Core.Player attackPlayer = null;
            Heroes.Core.Hero attackHero = null;
            Hashtable attackArmies = null;
            Heroes.Core.Player defendPlayer = null;
            Heroes.Core.Hero defendHero = null;
            Heroes.Core.Town defendCastle = null;
            Hashtable defendArmies = null;

            Heroes.Remoting.Game adp = null;

            if (Remoting.GameSetting._isServer)
            {
                adp = new Heroes.Remoting.Game();

                attackPlayer = attackPlayerMap;
                attackHero = attackHeroMap;
                attackArmies = attackArmiesMap;
                defendPlayer = defendPlayerMap;
                defendHero = defendHeroMap;
                defendCastle = defendCastleMap;
                defendArmies = defendArmiesMap;
            }
            else
            {
                Remoting.GameSetting.ConvertBattleToNetwork(attackPlayerMap, attackHeroMap, attackArmiesMap,
                    defendPlayerMap, defendHeroMap, defendCastleMap, defendArmiesMap,
                    out attackPlayer, out attackHero, out attackArmies,
                    out defendPlayer, out defendHero, out defendCastle, out defendArmies);

                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.SetBattle(attackPlayer, attackHero, attackArmies,
                    defendPlayer, defendHero, defendCastle, defendArmies);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool GetBattleSettings(out Heroes.Core.Player attackPlayer, out Heroes.Core.Hero attackHero, out Hashtable attackArmies,
            out Heroes.Core.Player defendPlayer, out Heroes.Core.Hero defendHero, out Heroes.Core.Town defendCastle, out Hashtable defendArmies)
        {
            attackPlayer = null;
            attackHero = null;
            attackArmies = null;
            defendPlayer = null;
            defendHero = null;
            defendCastle = null;
            defendArmies = null;

            Heroes.Remoting.Game adp = null;

            if (Remoting.GameSetting._isServer)
            {
                return true;
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.GetBattleSettings(out attackPlayer, out attackHero, out attackArmies,
                    out defendPlayer, out defendHero, out defendCastle, out defendArmies);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool ClearBattleFlags()
        {
            Heroes.Remoting.Game adp = null;

            if (Remoting.GameSetting._isServer)
            {
                adp = new Heroes.Remoting.Game();
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.ClearBattleFlags();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool EnqueueBattleCommand(int playerId, frmBattle.CommandIssuedEventArg e)
        {
            BattleCommand cmd = new BattleCommand(e._x, e._y, (int)e._button, e._doubleClick, e._cmdType);

            Remoting.Game adp = null;
            if (Remoting.GameSetting._isServer)
            {
                adp = new Heroes.Remoting.Game();
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.EnqueueBattleCommand(playerId, cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool DequeueBattleCommand(int playerId, out BattleCommand cmd)
        {
            cmd = null;

            Remoting.Game adp = null;
            if (Remoting.GameSetting._isServer)
            {
                adp = new Heroes.Remoting.Game();
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.DequeueBattleCommand(playerId, out cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool ExecuteBattleCommand(BattleCommand cmd)
        {
            lock (_lockToken)
            {
                //Remoting.GameSetting._frmBattle.ExecuteCommand(cmd);
            }

            return true;
        }

        private bool SetBattleResult()
        {
            // server no need to set Result
            if (Remoting.GameSetting._isServer)
            {
                return true;
            }
            else
            {
 
            }

            return true;
        }

        private bool SetBattleEnded()
        {
            Remoting.Game adp = null;
            if (Remoting.GameSetting._isServer)
            {
                adp = new Heroes.Remoting.Game();
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.SetBattleEnded();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool IsBattleEnded(out bool isBattleEnded)
        {
            isBattleEnded = false;

            Remoting.Game adp = null;
            if (Remoting.GameSetting._isServer)
            {
                adp = new Heroes.Remoting.Game();
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.IsBattleEnded(out isBattleEnded);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
        #endregion

        private void SetToCurrentPlayer(int playerId)
        {
            lock (_lockToken)
            {
                Remoting.GameSetting._frmMap._currentPlayer =
                    (Heroes.Core.Map.Player)Remoting.GameSetting.FindPlayer(playerId, Remoting.GameSetting._frmMap._players);
            }
        }

        #region Map
        private bool IsNeedToStartGame(out bool isNeedToStartGame, out int playerCount)
        {
            isNeedToStartGame = false;
            playerCount = 0;

            if (Remoting.GameSetting._isServer)
            {
                isNeedToStartGame = Remoting.GameSetting._isNeedToStartGame;
                playerCount = Remoting.GameSetting._players.Count;
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                Heroes.Remoting.Game adp = null;
                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }

                try
                {
                    adp.IsNeedToStartGame(out isNeedToStartGame, out playerCount);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            return true;
        }

        private bool IsGameStarted(out bool isGameStarted, out int playerCount)
        {
            isGameStarted = false;
            playerCount = 0;

            if (Remoting.GameSetting._isServer)
            {
                isGameStarted = Remoting.GameSetting._isGameStarted;
                playerCount = Remoting.GameSetting._players.Count;
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                Heroes.Remoting.Game adp = null;
                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }

                try
                {
                    adp.IsGameStarted(out isGameStarted, out playerCount);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            return true;
        }
        
        private bool GetGameSettings(out ArrayList players, out Hashtable heroCells)
        {
            players = null;
            heroCells = null;

            Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
            register._hostName = Remoting.GameSetting._serverHostName;

            Heroes.Remoting.Game adp = null;
            adp = (Heroes.Remoting.Game)register.GetObject(
                typeof(Heroes.Remoting.Game),
                Heroes.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.GetGameSettings(out players, out heroCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool GetGameSettings()
        {
            // server no need update game settings
            if (Remoting.GameSetting._isServer)
            {
                return true;
            }
            else
            {
                ArrayList players = null;
                Hashtable heroCells = null;

                if (!GetGameSettings(out players, out heroCells))
                {
                    // error
                    return false;
                }

                // update game settings
                lock (_lockToken)
                {
                    Remoting.GameSetting.ConvertNetworkToMap(players, heroCells,
                        Remoting.GameSetting._frmMap._rowCount, Remoting.GameSetting._frmMap._colCount,
                        Remoting.GameSetting._frmMap._players, Remoting.GameSetting._frmMap._mines,
                        Remoting.GameSetting._frmMap._cells);
                }
            }

            return true;
        }

        private bool SetInitialized(int playerId)
        {
            if (Remoting.GameSetting._isServer)
            {
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                Heroes.Remoting.Game adp = null;
                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }

                try
                {
                    adp.SetInitialized(playerId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            lock (_lockToken)
            {
                // flag initiallized
                Remoting.GameSetting._me._isInitialized = true;
                Remoting.GameSetting._isInitialized = true;
            }

            return true;
        }

        private bool SetGameSettings()
        {
            // server not need to submit settings
            if (Remoting.GameSetting._isServer)
            {
                WriteLog("Server: SetGameSettings");
                return true;
            }

            ArrayList players = null;
            Hashtable heroCells = null;

            Remoting.GameSetting.ConvertMapToNetwork(Remoting.GameSetting._frmMap._players, Remoting.GameSetting._frmMap._mines,
                Remoting.GameSetting._frmMap._cells,
                out players, out heroCells);

            if (!SetGameSettings(players, heroCells)) return false;

            return true;
        }

        private bool SetGameSettings(ArrayList players, Hashtable heroCells)
        {
            if (Remoting.GameSetting._isServer)
            {
                return true;
            }

            Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
            register._hostName = Remoting.GameSetting._serverHostName;

            Heroes.Remoting.Game adp = null;
            adp = (Heroes.Remoting.Game)register.GetObject(
                typeof(Heroes.Remoting.Game),
                Heroes.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.SetGameSettings(players, heroCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            WriteLog("Client:SetGameSettings");

            return true;
        }

        private bool GetCurrentPlayer(out int playerId)
        {
            playerId = 0;

            Heroes.Remoting.Game adp = null;

            if (Remoting.GameSetting._isServer)
            {
                adp = new Heroes.Remoting.Game();
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;

                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.GetCurrentPlayer(out playerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool SetNeedToRunNextPlayer()
        {
            Heroes.Remoting.Game adp = null;

            if (Remoting.GameSetting._isServer)
            {
                adp = new Heroes.Remoting.Game();
            }
            else
            {
                Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
                register._hostName = Remoting.GameSetting._serverHostName;
                
                adp = (Heroes.Remoting.Game)register.GetObject(
                    typeof(Heroes.Remoting.Game),
                    Heroes.Remoting.Game.CLASSNAME);

                if (adp == null)
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }

            try
            {
                adp.NextPlayer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
        #endregion

        #region Battle
        
        #endregion
        #endregion

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, System.Reflection.BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }

        public static void WriteLog(string msg)
        {
            WriteLog(Thread.CurrentThread.Name, msg);
        }

        public static void WriteLog(string threadName, string msg)
        {
#if DEBUG
            _logWriter.WriteLine("{0:HH:mm:ss.ms}, Thread:{1}, {2}", DateTime.Now, threadName, msg);
            _logWriter.Flush();
#endif
        }

    }

    public class LockToken
    {
        public LockToken()
        { 
        }
    }

    [Serializable]
    public class BattleCommand
    {
        public int _x;
        public int _y;
        public int _button;
        public bool _doubleClick;
        public int _cmdType;

        public BattleCommand(int x, int y, int button, bool doubleClick, int cmdType)
        {
            _x = x;
            _y = y;
            _button = button;
            _doubleClick = doubleClick;
            _cmdType = cmdType;
        }

    }

}
