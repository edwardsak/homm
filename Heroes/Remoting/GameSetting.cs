using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Heroes.Remoting
{
    public class GameSetting
    {
        public const int REMOTING_PORT = 4952;

        public static string _serverHostName;
        public static ArrayList _players = new ArrayList();
        public static Heroes.Core.Player _currentPlayer;
        public static int _playerCount = 0;

        public static bool _isServer = false;
        public static Player _server = null;    // who is server
        public static Player _me = null;

        public static frmGame _frmGame = null;
        public static Heroes.Core.Map.frmMap _frmMap = null;
        public static frmBattle _frmBattle = null;

        public static bool _isNeedToStartGame = false;
        public static bool _isGameStarted = false;
        public static bool _isInitialized = false;
        private static bool _isAllPlayerInitialized = false;
        public static bool _needToRunNextPlayer = false;

        #region Battle Flags
        public static bool _isNeedToStartBattle = false;
        public static bool _amIBattle = false;
        public static bool _isBattleStarted = false;
        public static Heroes.Core.Player _attackPlayer = null;
        public static Heroes.Core.Hero _attackHero = null;
        public static Hashtable _attackArmies = null;
        public static Heroes.Core.Player _defendPlayer = null;
        public static Heroes.Core.Hero _defendHero = null;
        public static Heroes.Core.Town _defendCastle = null;
        public static Hashtable _defendArmies = null;
        public static Heroes.Core.Battle.BattleSideEnum _victory = Heroes.Core.Battle.BattleSideEnum.None;
        public static bool _isBattleEnded = false;
        #endregion

        public static Queue _attackCommands = new Queue();
        public static Queue _defendCommands = new Queue();

        public static string GetLocalIp()
        {
            string hostName = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostName);
            System.Net.IPAddress[] ipAddrs = ipEntry.AddressList;

            foreach (System.Net.IPAddress ipAddr in ipAddrs)
            {
                if (ipAddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ipAddr.ToString();
                }
            }

            return "";
        }

        public static void ResetAll()
        {
            if (_frmBattle != null)
            {
                _frmBattle.Close();
                _frmBattle = null;
            }

            if (_frmMap != null)
            {
                _frmMap.Close();
                _frmMap = null;
            }

            if (_frmGame != null)
            {
                _frmGame.Close();
                _frmGame = null;
            }

            _isServer = false;
            _me = null;
            _server = null;
            _players.Clear();
            _currentPlayer = null;

            _isNeedToStartGame = false;
            _isGameStarted = false;
            _isInitialized = false;
            _isAllPlayerInitialized = false;
            _needToRunNextPlayer = false;

            _isNeedToStartBattle = false;
            _amIBattle = false;
            _isBattleStarted = false;
            _attackPlayer = null;
            _attackHero = null;
            _attackArmies = null;
            _defendPlayer = null;
            _defendHero = null;
            _defendCastle = null;
            _defendArmies = null;
            _victory = 0;
            _isBattleEnded = false;

            _attackCommands.Clear();
            _defendCommands.Clear();
        }

        public static bool IsAllPlayerInitialized()
        {
            if (_isAllPlayerInitialized) return true;

            foreach (Player player in GameSetting._players)
            {
                if (!player._isInitialized)
                {
                    _isAllPlayerInitialized = false;
                    return false;
                }
            }

            _isAllPlayerInitialized = true;
            return true;
        }

        public static void NextPlayer()
        {
            lock (frmGame._lockToken)
            {
                int index = Remoting.GameSetting._players.IndexOf(Remoting.GameSetting._currentPlayer);
                index += 1;
                if (index >= Remoting.GameSetting._players.Count) index = 0;
                Remoting.GameSetting._currentPlayer = (Player)Remoting.GameSetting._players[index];
            }
        }

        public static Heroes.Core.Player FindPlayer(int id, ArrayList players)
        {
            foreach (Heroes.Core.Player player in players)
            {
                if (id == player._id)
                {
                    return player;
                }
            }

            return null;
        }

        public static Heroes.Core.Hero FindHero(int id, ArrayList heroes)
        {
            foreach (Heroes.Core.Hero hero in heroes)
            {
                if (id == hero._id)
                {
                    return hero;
                }
            }

            return null;
        }

        public static void ConvertMapToNetwork(ArrayList mapPlayers, ArrayList mapMines, Heroes.Core.Map.Cell[,] mapCells,
            out ArrayList players, out Hashtable heroCells)
        {
            players = new ArrayList();
            heroCells = new Hashtable();

            // copy players and heroes
            foreach (Heroes.Core.Map.Player player in mapPlayers)
            {
                Heroes.Core.Player player2 = new Heroes.Core.Player();
                player2._id = player._id;
                player2._gold = player._gold;
                players.Add(player2);

                foreach (Heroes.Core.Map.Hero hero in player._heroes)
                {
                    Heroes.Core.Hero hero2 = new Heroes.Core.Hero();
                    hero2._id = hero._id;
                    hero2._player = player2;
                    player2._heroes.Add(hero2);

                    // cell in map
                    if (hero._cell != null)
                    {
                        GameCell cell = new GameCell(hero._cell._row, hero._cell._col);
                        heroCells.Add(hero2._id, cell);
                    }

                    foreach (Heroes.Core.Army army in hero._armyKSlots.Values)
                    {
                        Heroes.Core.Army army2 = new Heroes.Core.Army();
                        army2._id = army._id;
                        army2._slotNo = army._slotNo;
                        army2._qty = army._qty;

                        hero2._armyKSlots.Add(army2._slotNo, army2);
                    }
                }
            }

            // copy Towns
            int index = 0;
            foreach (Heroes.Core.Map.Player player in mapPlayers)
            {
                Heroes.Core.Player player2 = (Heroes.Core.Player)players[index];

                foreach (Heroes.Core.Map.Town town in player._castles)
                {
                    Heroes.Core.Town town2 = new Heroes.Core.Town();
                    town2._id = town._id;
                    town2._player = player2;
                    player2._castles.Add(town2);

                    foreach (Heroes.Core.Army army in town._armyAvKIds.Values)
                    {
                        Heroes.Core.Army army2 = new Heroes.Core.Army();
                        army2._id = army._id;
                        army2._qty = army._qty;

                        town2._armyAvKIds.Add(army2._id, army2);
                    }

                    foreach (Heroes.Core.Army army in town._armyInCastleKSlots.Values)
                    {
                        Heroes.Core.Army army2 = new Heroes.Core.Army();
                        army2._id = army._id;
                        army2._slotNo = army._slotNo;
                        army2._qty = army._qty;

                        town2._armyInCastleKSlots.Add(army2._id, army2);
                    }

                    if (town._heroVisit != null)
                    {
                        int indexPlayer = mapPlayers.IndexOf(town._heroVisit._player);
                        Heroes.Core.Map.Player playerVisit = (Heroes.Core.Map.Player)mapPlayers[indexPlayer];
                        int indexHero = playerVisit._heroes.IndexOf(town._heroVisit);

                        Heroes.Core.Player playerVisit2 = (Heroes.Core.Player)players[indexPlayer];
                        Heroes.Core.Hero heroVisit2 = (Heroes.Core.Hero)playerVisit2._heroes[indexHero];
                        town2._heroVisit = heroVisit2;
                    }
                }

                index += 1;
            }

            // copy mines
            foreach (Heroes.Core.Map.Mine mine in mapMines)
            {
                Heroes.Core.Mine mine2 = new Heroes.Core.Mine(mine._id);

                if (mine._player != null)
                {
                    // get player
                    Heroes.Core.Player player2 = GameSetting.FindPlayer(mine._player._id, players);
                    mine2._player = player2;
                    player2._mineKTypes[(int)Heroes.Core.MineTypeEnum.Gold].Add(mine2);
                }
                else
                {
                    mine2._player = null;
                }
            }
        }

        public static void ConvertNetworkToMap(ArrayList players, Hashtable heroCells,
            int rowCount, int colCount, ArrayList mapPlayers, ArrayList mapMines, Heroes.Core.Map.Cell[,] mapCells)
        {
            // clear hero on cells
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    mapCells[row, col]._hero = null;
                }
            }

            // update player, heroes
            foreach (Heroes.Core.Player player in players)
            {
                Heroes.Core.Map.Player player2 = (Heroes.Core.Map.Player)Remoting.GameSetting.FindPlayer(player._id, mapPlayers);
                player2._heroes.Clear();

                foreach (Heroes.Core.Hero hero in player._heroes)
                {
                    Heroes.Core.Map.Hero hero2 = new Heroes.Core.Map.Hero(hero._id, player2);
                    hero2._image = player2._heroImage;
                    player2._heroes.Add(hero2);

                    foreach (Heroes.Core.Army army in hero._armyKSlots.Values)
                    {
                        hero2._armyKSlots.Add(army._slotNo, army);
                    }

                    if (heroCells.Contains(hero._id))
                    {
                        Remoting.GameCell cell = (Remoting.GameCell)heroCells[hero._id];
                        hero2._cell = mapCells[cell._row, cell._col];
                        hero2._cell._hero = hero2;
                    }

                }
            }

            // get a list of heroes
            Hashtable heroes2 = new Hashtable();
            foreach (Heroes.Core.Map.Player player2 in mapPlayers)
            {
                foreach (Heroes.Core.Map.Hero hero2 in player2._heroes)
                {
                    heroes2.Add(hero2._id, hero2);
                }
            }

            // get a list of castle
            Hashtable towns2 = new Hashtable();
            foreach (Heroes.Core.Map.Player player2 in mapPlayers)
            {
                foreach (Heroes.Core.Map.Town town2 in player2._castles)
                {
                    towns2.Add(town2._id, town2);

                    // clear owner
                    town2._player = null;
                }
            }

            // update castle
            foreach (Heroes.Core.Player player in players)
            {
                Heroes.Core.Map.Player player2 = (Heroes.Core.Map.Player)Remoting.GameSetting.FindPlayer(player._id, mapPlayers);
                player2._castles.Clear();

                foreach (Heroes.Core.Town town in player._castles)
                {
                    Heroes.Core.Map.Town town2 = (Heroes.Core.Map.Town)towns2[town._id];
                    player2._castles.Add(town2);
                    town2._player = player2;

                    // armies available
                    town2._armyAvKIds.Clear();
                    foreach (Heroes.Core.Army army in town._armyAvKIds.Values)
                    {
                        town2._armyAvKIds.Add(army._id, army);
                    }

                    // armies in castle
                    town2._armyInCastleKSlots.Clear();
                    foreach (Heroes.Core.Army army in town._armyInCastleKSlots.Values)
                    {
                        town2._armyInCastleKSlots.Add(army._slotNo, army);
                    }

                    if (town._heroVisit != null)
                    {
                        town2._heroVisit = (Heroes.Core.Map.Hero)heroes2[town._heroVisit._id];
                    }
                }
            }

            // get a list of mines
            Hashtable mines2 = new Hashtable();
            foreach (Heroes.Core.Map.Mine mine2 in mapMines)
            {
                mines2.Add(mine2._id, mine2);

                // clear owner
                mine2._player = null;
            }

            // update mines
            int goldMineType = (int)Heroes.Core.MineTypeEnum.Gold;
            foreach (Heroes.Core.Player player in players)
            {
                Heroes.Core.Map.Player player2 = (Heroes.Core.Map.Player)Remoting.GameSetting.FindPlayer(player._id, mapPlayers);
                player2._mineKTypes[goldMineType].Clear();

                foreach (Heroes.Core.Mine mine in player._mineKTypes[goldMineType])
                {
                    Heroes.Core.Map.Mine mine2 = (Heroes.Core.Map.Mine)mines2[mine._id];
                    player2._mineKTypes[goldMineType].Add(mine2);
                    mine2._player = player2;
                }
            }
        }

        public static void ConvertBattleToNetwork(Heroes.Core.Player attackPlayerMap, Heroes.Core.Hero attackHeroMap, Hashtable attackArmiesMap,
            Heroes.Core.Player defendPlayerMap, Heroes.Core.Hero defendHeroMap, Heroes.Core.Town defendCastleMap, Hashtable defendArmiesMap,
            out Heroes.Core.Player attackPlayer, out Heroes.Core.Hero attackHero, out Hashtable attackArmies,
            out Heroes.Core.Player defendPlayer, out Heroes.Core.Hero defendHero, out Heroes.Core.Town defendCastle, out Hashtable defendArmies)
        {
            attackPlayer = null;
            attackHero = null;
            attackArmies = new Hashtable();
            defendPlayer = null;
            defendHero = null;
            defendCastle = null;
            defendArmies = new Hashtable();

            if (attackArmiesMap != null)
            {
                foreach (Heroes.Core.Army armyMap in attackArmiesMap.Values)
                {
                    Heroes.Core.Army army = new Heroes.Core.Army();
                    army._id = armyMap._id;
                    army._qty = armyMap._qty;
                    army._slotNo = armyMap._slotNo;
                    attackArmies.Add(army._slotNo, army);
                }
            }

            if (attackPlayerMap != null)
            {
                attackPlayer = new Heroes.Core.Player();
                attackPlayer._id = attackPlayerMap._id;
            }

            if (attackHeroMap != null)
            {
                attackHero = new Heroes.Core.Hero();
                attackHero._id = attackHeroMap._id;
                attackHero._player = attackPlayer;
                attackHero._armyKSlots = attackArmies;
            }

            if (defendArmiesMap != null)
            {
                foreach (Heroes.Core.Army armyMap in defendArmiesMap.Values)
                {
                    Heroes.Core.Army army = new Heroes.Core.Army();
                    army._id = armyMap._id;
                    army._qty = armyMap._qty;
                    army._slotNo = armyMap._slotNo;
                    defendArmies.Add(army._slotNo, army);
                }
            }

            if (defendPlayerMap != null)
            {
                defendPlayer = new Heroes.Core.Player();
                defendPlayer._id = defendPlayerMap._id;
            }

            if (defendHeroMap != null)
            {
                defendHero = new Heroes.Core.Hero();
                defendHero._id = defendHeroMap._id;
                defendHero._player = defendPlayer;
                defendHero._armyKSlots = defendArmies;
            }
            
            // castle

        }

    }
}
