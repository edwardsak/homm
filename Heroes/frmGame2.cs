using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Heroes
{
    public partial class frmGame2 : Form
    {
        int _playerIdMe;
        Heroes.Core.Player _playerMe;
        Hashtable _playerKIds;
        ArrayList _playerIds;   // all players id in this game
        int _startingHeroId;
        bool _isDoingTurn;
        bool _isBattleStarted;

        Heroes.Core.Hero _attackHero;
        Heroes.Core.Hero _defendHero;

        // get/set flags
        Thread _processThread;

        // timer to start game
        System.Windows.Forms.Timer _timerGame;

        // timer to start battle
        System.Windows.Forms.Timer _timerBattle;

        public static LockMe _lockMe = new LockMe();

        bool _isExit;

        frmDuelNetwork _frmDuel;
        frmBattle _frmBattle;


        public frmGame2()
        {
            InitializeComponent();

            _playerIdMe = 0;
            _playerMe = null;
            _playerKIds = new Hashtable();
            _startingHeroId = 0;
            _isDoingTurn = false;
            _isBattleStarted = false;

            _attackHero = null;
            _defendHero = null;

            _isExit = false;

            _frmDuel = null;
            _frmBattle = null;

            // thread
            ThreadStart entryPoint = new ThreadStart(StartProcess);
            _processThread = new Thread(entryPoint);
            _processThread.Name = "Processing Thread";

            // timer to start game
            _timerGame = new System.Windows.Forms.Timer();
            _timerGame.Interval = 500;
            _timerGame.Tick += new EventHandler(_timerGame_Tick);
            _timerGame.Stop();

            // timer to start battle
            _timerBattle = new System.Windows.Forms.Timer();
            _timerBattle.Interval = 500;
            _timerBattle.Tick += new EventHandler(_timerBattle_Tick);
            _timerBattle.Stop();

            this.FormClosing += new FormClosingEventHandler(frmGame_FormClosing);
            this.FormClosed += new FormClosedEventHandler(frmGame_FormClosed);
        }

        private void frmGame2_Load(object sender, EventArgs e)
        {

        }

        void frmGame_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        void frmGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isExit = true;
            _processThread.Join(30000);
        }

        public DialogResult ShowDialog(Heroes.Core.Player player, bool isMaster)
        {
            _playerMe = player;
            this._playerIdMe = player._id;

            if (isMaster)
            {
                this.Text = "Server";
                cmdStart.Visible = true;
            }
            else
            {
                this.Text = "Client";
                cmdStart.Visible = false;
            }

            // start thread
            _processThread.Start();

            return this.ShowDialog();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (!RemoteStartGame()) return;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartProcess()
        {
            while (!_isExit)
            {
                Thread.Sleep(1000);
                //Debug.WriteLine(string.Format("Thread: {0:HH:mm:ss}", DateTime.Now));

                // wait to join game
                bool isWaitToJoingGame = false;
                if (!RemoteIsWaitToJoinGame(out isWaitToJoingGame))
                {
                    Debug.WriteLine("RemoteIsWaitToJoinGame failed.");
                    return;
                }

                if (isWaitToJoingGame)
                {
                    // get players
                    //GetPlayers();
                    //ppl players
                    //SetControlPropertyThreadSafe(this, "PplPlayersProperty", true);
                    goto NEXT_ROUND;
                }

                // starting game
                bool isNeedToStartGame = false;
                if (!RemoteIsNeedToStartGame(this._playerMe._id, out isNeedToStartGame))
                {
                    Debug.WriteLine("RemoteIsNeedToStartGame failed.");
                    return;
                }

                if (isNeedToStartGame)
                {
                    if (!RemoteSetIsNeedToStartGame(this._playerMe._id))
                    {
                        Debug.WriteLine("RemoteSetIsNeedToStartGame failed.");
                        return;
                    }

                    if (!RemoteGetPlayerIds(out this._playerIds))
                    {
                        Debug.WriteLine("RemoteGetPlayerIds failed.");
                        return;
                    }

                    if (!RemoteGetPlayer())
                    {
                        Debug.WriteLine("RemoteGetPlayer failed.");
                        return;
                    }

                    if (!RemoteGetStartingHeroId(this._playerMe._id, out this._startingHeroId))
                    {
                        Debug.WriteLine("RemoteGetStartingHeroId failed.");
                        return;
                    }

                    frmGame.SetControlPropertyThreadSafe(this, "StartGameProperty", true);
                    goto NEXT_ROUND;
                }

                // game started
                bool isGameStarted = false;
                if (!RemoteIsGameStarted(out isGameStarted))
                {
                    Debug.WriteLine("RemoteIsGameStarted failed.");
                    return;
                }

                if (isGameStarted)
                {
                    int playerId = 0;
                    if (!RemoteGetCurrentPlayerId(out playerId))
                    {
                        Debug.WriteLine("RemoteGetCurrentPlayerId failed.");
                        return;
                    }

                    if (this._playerMe._id == playerId)
                    {
                        if (!this._isDoingTurn)
                        {
                            lock (_lockMe)
                            {
                                this._isDoingTurn = true;
                            }

                            Hashtable playerKIds = null;
                            if (!RemoteGetAllPlayers(out playerKIds))
                            {
                                Debug.WriteLine("RemoteGetAllPlayers failed.");
                                return;
                            }
                            if (playerKIds == null)
                            {
                                Debug.WriteLine("playes is null.");
                                return;
                            }

                            lock (_lockMe)
                            {
                                this._playerKIds = playerKIds;
                                if (!this._playerKIds.ContainsKey(this._playerIdMe))
                                {
                                    Debug.WriteLine("Me is not found.");
                                    return;
                                }

                                this._playerMe = (Heroes.Core.Player)this._playerKIds[this._playerIdMe];
                            }

                            _frmDuel.SetPlayer(this._playerMe, this._playerKIds);

                            frmGame.SetControlPropertyThreadSafe(this, "MapReadOnlyProperty", false);
                            goto NEXT_ROUND;
                        }
                    }

                    bool _isNeedToStartBattle = false;
                    if (!RemoteIsNeedToStartBattle(this._playerIdMe, out _isNeedToStartBattle))
                    {
                        Debug.WriteLine("RemoteGetStartingBattle failed.");
                        return;
                    }

                    if (_isNeedToStartBattle)
                    {
                        if (!RemoteSetIsNeedToStartBattle(this._playerIdMe))
                        {
                            Debug.WriteLine("RemoteSetBattleStarted failed.");
                            return;
                        }

                        if (!RemoteGetBattle(out this._attackHero, out this._defendHero))
                        {
                            Debug.WriteLine("RemoteGetBattle failed.");
                            return;
                        }

                        frmGame.SetControlPropertyThreadSafe(this, "StartBattleProperty", false);
                        goto NEXT_ROUND;
                    }

                    if (this._isBattleStarted)
                    {
                        // get commands
                        Heroes.Core.Remoting.BattleCommand cmd = null;
                        do
                        {
                            // get commands from queue
                            if (!DequeueBattleCommand(this._playerIdMe, out cmd))
                            {
                                // error
                                return;
                            }
                            if (cmd == null) break;

                            // execute commands
                            frmGame.SetControlPropertyThreadSafe(this, "ExecuteBattleCommandProperty", cmd);
                        } while (cmd != null);
                    }
                }

            NEXT_ROUND:
                continue;
            }
        }

        #region Remoting
        private bool RemoteStartGame()
        {
            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.StartGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteGetPlayerIds(out ArrayList playerIds)
        {
            playerIds = null;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                playerIds = adp.GetPlayerIds();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteGetPlayer()
        {
            Heroes.Core.Player player = null;
            if (!RemoteGetPlayer(_playerMe._id, out player)) return false;
            if (player == null) return false;
            _playerMe = player;

            return true;
        }

        private bool RemoteGetPlayer(int playerId, out Heroes.Core.Player player)
        {
            player = null;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                player = adp.GetPlayer(playerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteGetAllPlayers(out Hashtable players)
        {
            players = null;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                players = adp.GetAllPlayers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteSetPlayer(Heroes.Core.Player player)
        {
            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.SetPlayer(player);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteGetStartingHeroId(int playerId, out int heroId)
        {
            heroId = 0;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                heroId = adp.GetStartingHeroId(playerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteIsWaitToJoinGame(out bool b)
        {
            b = false;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                b = adp.IsWaitToJoinGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteIsNeedToStartGame(int playerId, out bool b)
        {
            b = false;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                b = adp.IsNeedToStartGame(playerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteSetIsNeedToStartGame(int playerId)
        {
            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.SetIsNeedToStartGame(playerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteSetGameStarted(int playerId)
        {
            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.SetGameStarted(playerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteIsGameStarted(out bool b)
        {
            b = false;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                b = adp.IsGameStarted();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteGetCurrentPlayerId(out int playerId)
        {
            playerId = 0;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                playerId = adp.GetCurrentPlayerId();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteNextTurn(int playerId)
        {
            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.NextPlayer(playerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteGetArtifact(Heroes.Core.Heros.ArtifactLevelEnum level, out int id)
        {
            id = 0;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                id = adp.GetRndArtifactId((int)level);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
        #endregion

        #region Remoting Battle
        private bool RemoteSetStartingBattle(Heroes.Core.Hero attackHero, Heroes.Core.Hero defendHero)
        {
            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.SetStartingBattle(attackHero, defendHero);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteIsNeedToStartBattle(int playerId, out bool b)
        {
            b = false;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                b = adp.IsNeedToStartBattle(playerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteSetIsNeedToStartBattle(int playerId)
        {
            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.SetIsNeedToStartBattle(playerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool RemoteGetBattle(out Heroes.Core.Hero attackHero, out Heroes.Core.Hero defendHero)
        {
            attackHero = null;
            defendHero = null;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.GetBattle(out attackHero, out defendHero);
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
            Heroes.Core.Remoting.BattleCommand cmd 
                = new Heroes.Core.Remoting.BattleCommand(e._x, e._y, (int)e._button, e._doubleClick, e._cmdType, e._spell);

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
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

        private bool DequeueBattleCommand(int playerId, out Heroes.Core.Remoting.BattleCommand cmd)
        {
            cmd = null;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = Setting._remoteHostName;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
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

        private bool ExecuteBattleCommand(Heroes.Core.Remoting.BattleCommand cmd)
        {
            lock (_lockMe)
            {
                _frmBattle.ExecuteCommand(cmd);
            }

            return true;
        }
        #endregion

        #region Invoke
        public bool StartGameProperty
        {
            set
            {
                _timerGame.Start();
            }
        }

        public bool MapReadOnlyProperty
        {
            set
            {
                _frmDuel.ReadOnly = value;
            }
        }

        public bool StartBattleProperty
        {
            set
            {
                _timerBattle.Start();
            }
        }

        public Heroes.Core.Remoting.BattleCommand ExecuteBattleCommandProperty
        {
            set
            {
                ExecuteBattleCommand(value);
            }
        }

        void _timerGame_Tick(object sender, EventArgs e)
        {
            _timerGame.Stop();

            StartGame();
        }

        private void StartGame()
        {
            try
            {
                this.Hide();

                Debug.WriteLine("Start frmDuelNetwork");
                using (frmDuelNetwork f = new frmDuelNetwork())
                {
                    _frmDuel = f;
                    f.GameStarted += new frmDuelNetwork.GameStartedEventHandler(f_GameStarted);
                    f.NextTurnClick += new frmDuelNetwork.NextTurnClickEventHandler(f_NextTurnClick);
                    f.StartingBattle += new frmDuelNetwork.StartingBattleEventHandler(f_StartingBattle);
                    f.GettingArtifact += new frmDuelNetwork.GettingArtifactEventHandler(f_GettingArtifact);
                    f.ShowDialog(this._playerMe, this._startingHeroId, this._playerIds);
                }
            }
            finally
            {
                this.Close();
            }
        }

        void f_GameStarted()
        {
            if (!RemoteSetPlayer(this._playerMe))
            {
                Debug.WriteLine("RemoteSetPlayer failed.");
                return;
            }

            if (!RemoteSetGameStarted(this._playerMe._id))
            {
                Debug.WriteLine("RemoteSetGameStarted failed.");
                return;
            }
        }

        void f_NextTurnClick()
        {
            if (!RemoteSetPlayer(this._playerMe))
            {
                Debug.WriteLine("RemoteSetPlayer failed.");
                return;
            }

            if (!RemoteNextTurn(this._playerMe._id))
            {
                Debug.WriteLine("RemoteNextTurn failed.");
                return;
            }

            lock (_lockMe)
            {
                this._isDoingTurn = false;
            }
        }

        void f_StartingBattle(frmDuelNetwork.StartingBattleEventArg e)
        {
            if (e._monster != null)
            {
                e._success = Battle(e._attackHero, null, e._monster);
            }
            else
            {
                if (!RemoteSetStartingBattle(e._attackHero, e._defendHero))
                {
                    Debug.WriteLine("RemoteSetStartingBattle failed.");
                    return;
                }
            }
        }

        void f_GettingArtifact(frmDuelNetwork.GettingArtifactEventArg e)
        {
            int id = 0;
            if (!RemoteGetArtifact(e._level, out id))
            {
                Debug.WriteLine("RemoteGetArtifact failed.");
                return;
            }

            if (Heroes.Core.Setting._artifacts.ContainsKey(id))
                e._artifact = (Heroes.Core.Heros.Artifact)Heroes.Core.Setting._artifacts[id];
            else
                e._artifact = null;
        }

        private void StartBattle()
        {
            Debug.WriteLine("Start Battle");

            lock (_lockMe)
            {
                this._isBattleStarted = true;
            }

            Battle(this._attackHero, this._defendHero, null);

            if (this._playerIdMe == this._attackHero._playerId)
            {
                if (!RemoteSetPlayer(this._playerMe))
                {
                    Debug.WriteLine("RemoteSetPlayer attack failed.");
                    return;
                }
            }
            else if (this._playerIdMe == this._defendHero._playerId)
            {
                if (!RemoteSetPlayer(this._playerMe))
                {
                    Debug.WriteLine("RemoteSetPlayer defend failed.");
                    return;
                }
            }

            lock (_lockMe)
            {
                this._isBattleStarted = false;
            }
        }

        public bool Battle(Heroes.Core.Hero attackHero, Heroes.Core.Hero defendHero, Heroes.Core.Monster monster)
        {
            lock (frmGame2._lockMe)
            {
                using (frmBattle f = new frmBattle())
                {
                    _frmBattle = f;

                    f._playerIdMe = this._playerIdMe;

                    // set multiplayer flag
                    if (defendHero != null) f._isMultiPlayer = true;
                    else f._isMultiPlayer = false;

                    f.CommandIssued += new frmBattle.CommandIssuedEventHandler(f_CommandIssued);

                    f.ShowDialog(attackHero, defendHero, monster, null);

                    using (Heroes.Core.Battle.frmBattleResult f2 = new Heroes.Core.Battle.frmBattleResult())
                    {
                        if (f._victory == Heroes.Core.Battle.BattleSideEnum.Attacker)
                        {
                            if (f._engine._attacker._playerId == this._playerIdMe)
                            {
                                // victory
                                f2.ShowDialog(1, f._engine._attacker, this._playerIdMe,
                                    f._engine._attacker, f._engine._defender, f._engine._monster);

                                this._frmDuel._currentHero._experience += f2._experience;
                                this._frmDuel.LevelUp(this._frmDuel._currentHero);

                                return true;
                            }
                            else
                            {
                                // defeat
                                f2.ShowDialog(2, f._engine._attacker, this._playerIdMe,
                                    f._engine._attacker, f._engine._defender, f._engine._monster);

                                // do not remove hero
                                this._frmDuel.ResurrectHero(this._frmDuel._currentHero);
                                //_currentPlayer._heroes.Remove(this._currentHero);
                                //_currentHero = null;

                                return false;
                            }
                        }
                        else
                        {
                            if (f._engine._defender != null)
                            {
                                if (f._engine._defender._playerId == this._playerIdMe)
                                {
                                    // victory
                                    f2.ShowDialog(1, f._engine._defender, this._playerIdMe,
                                        f._engine._attacker, f._engine._defender, f._engine._monster);

                                    this._frmDuel._currentHero._experience += f2._experience;
                                    this._frmDuel.LevelUp(this._frmDuel._currentHero);

                                    return true;
                                }
                                else
                                {
                                    // defeat
                                    f2.ShowDialog(2, f._engine._defender, this._playerIdMe,
                                        f._engine._attacker, f._engine._defender, f._engine._monster);

                                    // do not remove hero
                                    this._frmDuel.ResurrectHero(this._frmDuel._currentHero);
                                    //_currentPlayer._heroes.Remove(this._currentHero);
                                    //_currentHero = null;

                                    return false;
                                }
                            }
                            else if (f._engine._monster != null)
                            {
                                // defeat
                                f2.ShowDialog(2, f._engine._monster, this._playerIdMe,
                                    f._engine._attacker, f._engine._defender, f._engine._monster);

                                // do not remove hero
                                this._frmDuel.ResurrectHero(this._frmDuel._currentHero);
                                //_currentPlayer._heroes.Remove(this._currentHero);
                                //_currentHero = null;

                                return false;
                            }
                        }
                    }
                }
            }

            return false;
        }

        void f_CommandIssued(frmBattle.CommandIssuedEventArg e)
        {
            // add battle command to queue
            if (!EnqueueBattleCommand(this._playerIdMe, e))
            {
                // error
                return;
            }
        }

        void _timerBattle_Tick(object sender, EventArgs e)
        {
            _timerBattle.Stop();

            StartBattle();
        }
        #endregion

    }

    public class LockMe
    { 
    }

}
