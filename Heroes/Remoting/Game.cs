using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Heroes.Remoting
{
    [Serializable]
    public class Game : System.MarshalByRefObject
    {
        public const string CLASSNAME = "Heroes.Remoting.Game";

        #region Map
        public void Join(string yourIp, out int id)
        {
            id = 0;

            Player player = new Player();
            player._id = GameSetting._players.Count + 1;
            player._ip = yourIp;

            Remoting.GameSetting._players.Add(player);
            Remoting.GameSetting._playerCount = GameSetting._players.Count;

            id = player._id;
        }

        public void IsNeedToStartGame(out bool isGameStarted, out int playerCount)
        {
            playerCount = GameSetting._players.Count;
            isGameStarted = Remoting.GameSetting._isGameStarted;
        }

        public void IsGameStarted(out bool isGameStarted, out int playerCount)
        {
            playerCount = GameSetting._players.Count;
            isGameStarted = Remoting.GameSetting._isGameStarted;
        }

        public void GetGameSettings(out ArrayList players, out Hashtable heroCells)
        {
            GameSetting.ConvertMapToNetwork(GameSetting._frmMap._players, GameSetting._frmMap._mines, GameSetting._frmMap._cells,
                out players, out heroCells);
        }

        public void SetGameSettings(ArrayList players, Hashtable heroCells)
        {
            lock (frmGame._lockToken)
            {
                GameSetting.ConvertNetworkToMap(players, heroCells,
                    GameSetting._frmMap._rowCount, GameSetting._frmMap._colCount,
                    GameSetting._frmMap._players, GameSetting._frmMap._mines,
                    GameSetting._frmMap._cells);

                frmGame.WriteLog("Network", "SetGameSettings");
            }
        }

        public void SetInitialized(int playerId)
        {
            foreach (Player player in GameSetting._players)
            {
                if (player._id == playerId)
                {
                    player._isInitialized = true;
                }
            }
        }

        public void IsAllPlayerInitialized(out bool b)
        {
            b = GameSetting.IsAllPlayerInitialized();
        }

        public void GetCurrentPlayer(out int playerId)
        {
            playerId = GameSetting._currentPlayer._id;
        }

        public void NextPlayer()
        {
            lock (frmGame._lockToken)
            {
                GameSetting._needToRunNextPlayer = true;
            }
        }
        #endregion

        #region Battle
        public void IsNeedToStartBattle(out bool isNeedToStartBattle)
        {
            isNeedToStartBattle = GameSetting._isNeedToStartBattle;
        }

        public void IsBattleStarted(out bool isBattleStarted)
        {
            isBattleStarted = GameSetting._isBattleStarted;
        }

        public void SetBattle(Heroes.Core.Player attackPlayer, Heroes.Core.Hero attackHero, Hashtable attackArmies,
            Heroes.Core.Player defendPlayer, Heroes.Core.Hero defendHero, Heroes.Core.Town defendCastle, Hashtable defendArmies)
        {
            lock (frmGame._lockToken)
            {
                GameSetting._isNeedToStartBattle = true;
                GameSetting._attackPlayer = attackPlayer;
                GameSetting._attackHero = attackHero;
                GameSetting._attackArmies = attackArmies;
                GameSetting._defendPlayer = defendPlayer;
                GameSetting._defendHero = defendHero;
                GameSetting._defendCastle = defendCastle;
                GameSetting._defendArmies = defendArmies;
            }
        }

        public void GetBattleSettings(out Heroes.Core.Player attackPlayer, out Heroes.Core.Hero attackHero, out Hashtable attackArmies,
            out Heroes.Core.Player defendPlayer, out Heroes.Core.Hero defendHero, out Heroes.Core.Town defendCastle, out Hashtable defendArmies)
        {
            GameSetting.ConvertBattleToNetwork(GameSetting._attackPlayer, GameSetting._attackHero, GameSetting._attackArmies,
                GameSetting._defendPlayer, GameSetting._defendHero, GameSetting._defendCastle, GameSetting._defendArmies,
                out attackPlayer, out attackHero, out attackArmies,
                out defendPlayer, out defendHero, out defendCastle, out defendArmies);
        }

        public void ClearBattleFlags()
        {
            // clear Battle Flag
            GameSetting._isBattleStarted = false;
            GameSetting._attackPlayer = null;
            GameSetting._attackHero = null;
            GameSetting._attackArmies = null;
            GameSetting._defendPlayer = null;
            GameSetting._defendHero = null;
            GameSetting._defendCastle = null;
            GameSetting._defendArmies = null;
        }

        public void EnqueueBattleCommand(int playerId, BattleCommand cmd)
        {
            if (GameSetting._attackPlayer != null && playerId == GameSetting._attackPlayer._id)
            {
                lock (frmGame._lockToken)
                {
                    GameSetting._attackCommands.Enqueue(cmd);
                }
            }
            else if (GameSetting._defendPlayer != null && playerId == GameSetting._defendPlayer._id)
            {
                lock (frmGame._lockToken)
                {
                    GameSetting._defendCommands.Enqueue(cmd);
                }
            }
        }

        public void DequeueBattleCommand(int playerId, out BattleCommand cmd)
        {
            cmd = null;

            if (GameSetting._attackPlayer != null && playerId == GameSetting._attackPlayer._id)
            {
                // attacker get defender commands
                if (GameSetting._defendCommands.Count > 0)
                {
                    lock (frmGame._lockToken)
                    {
                        cmd = (BattleCommand)GameSetting._defendCommands.Dequeue();
                    }
                }
            }
            else if (GameSetting._defendPlayer != null && playerId == GameSetting._defendPlayer._id)
            {
                // defender get attacker commands
                if (GameSetting._attackCommands.Count > 0)
                {
                    lock (frmGame._lockToken)
                    {
                        cmd = (BattleCommand)GameSetting._attackCommands.Dequeue();
                    }
                }
            }
        }

        public void SetBattleResult(int victory,
            Heroes.Core.Player attackPlayer, Heroes.Core.Hero attackHero, Hashtable attackArmies,
            Heroes.Core.Player defendPlayer, Heroes.Core.Hero defendHero, Heroes.Core.Town defendCastle, Hashtable defendArmies)
        {
            lock (frmGame._lockToken)
            {
                GameSetting._isBattleEnded = true;
                GameSetting._victory = (Heroes.Core.Battle.BattleSideEnum)victory;
                GameSetting._attackPlayer = attackPlayer;
                GameSetting._attackHero = attackHero;
            }
        }

        public void SetBattleEnded()
        {
            lock (frmGame._lockToken)
            {
                GameSetting._isBattleEnded = true;
            }
        }

        public void IsBattleEnded(out bool isBattleEnded)
        {
            isBattleEnded = GameSetting._isBattleEnded;
        }
        #endregion

    }
}
