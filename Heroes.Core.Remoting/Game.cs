using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace Heroes.Core.Remoting
{
    [Serializable]
    public class Game : System.MarshalByRefObject
    {
        public const string CLASSNAME = "Heroes.Core.Remoting.Game";

        #region Saves
        internal bool ReadPlayerIds()
        {
            GameSetting._playerNameKIds.Clear();

            string fileName = string.Format(@"{0}\Saves\PlayerId.txt", Heroes.Core.Setting._appStartupPath);
            if (!System.IO.File.Exists(fileName)) return false;

            using (StreamReader sr = new StreamReader(fileName))
            {
                string strLine = "";

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    string[] datas = strLine.Split(new char[] { ',' });

                    for (int i = 0; i < datas.Length; i += 2)
                    {
                        int playerId = System.Convert.ToInt32(datas[i]);
                        string playerName = datas[i + 1];

                        GameSetting._playerNameKIds.Add(playerId, playerName);
                    }

                    break;
                }
            }

            return true;
        }

        public bool IsPlayerExist(string playerName)
        {
            string fileName = GetSaveFileName(playerName);
            return System.IO.File.Exists(fileName);
        }

        public bool ReadPlayer(string playerName, out Player player)
        {
            player = null;

            string fileName = GetSaveFileName(playerName);
            if (!System.IO.File.Exists(fileName)) return false;

            using (StreamReader sr = new StreamReader(fileName))
            {
                string strLine = "";

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // uid,name,level,exp,a,d,p,k
                    string[] datas = null;
                    Hero hero = null;
                    {
                        datas = strLine.Split(new char[] { ',' });

                        player = new Player();
                        player._id = System.Convert.ToInt32(datas[0]);
                        player._name = datas[1];

                        hero = new Hero();
                        hero._id = player._id;
                        hero._name = player._name;
                        hero._level = System.Convert.ToInt32(datas[2]);
                        hero._experience = System.Convert.ToInt32(datas[3]);
                        hero._attack = System.Convert.ToInt32(datas[4]);
                        hero._defense = System.Convert.ToInt32(datas[5]);
                        hero._power = System.Convert.ToInt32(datas[6]);
                        hero._knowledge = System.Convert.ToInt32(datas[7]);
                    }

                    // skillId,skillLevel,...
                    {
                        datas = strLine.Split(new char[] { ',' });

                        for (int i = 0; i < datas.Length; i += 2)
                        {
                            int skillId = System.Convert.ToInt32(datas[i]);
                            int skillLevel = System.Convert.ToInt32(datas[i + 1]);

                            Skill skill = new Skill();
                            skill.CopyFrom((Skill)Heroes.Core.Setting._skills[skillId]);
                            skill._level = skillLevel;

                            hero._skills.Add(skill._id, skill);
                        }
                    }

                    // spells
                    {
                        datas = strLine.Split(new char[] { ',' });

                        for (int i = 0; i < datas.Length; i++)
                        {
                            int spellId = System.Convert.ToInt32(datas[i]);

                            Spell spell = new Spell();
                            spell.CopyFrom((Spell)Heroes.Core.Setting._spells[spellId]);

                            hero._spells.Add(spell._id, spell);
                        }
                    }

                    // artifacts
                }
            }

            return true;
        }

        public bool WritePlayer(Player player)
        {
            // skip if no hero
            if (player._heroes.Count < 1) return true;
            Heroes.Core.Hero hero = (Hero)player._heroes[0];

            string fileName = GetSaveFileName(player._name);

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                // uid,name,level,exp,a,d,p,k
                {
                    sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", player._id, player._name,
                        hero._level, hero._experience, hero._attack, hero._defense, hero._power, hero._knowledge));
                }

                // skillId,skillLevel,...
                {
                    System.Text.StringBuilder sb = new StringBuilder();
                    foreach (Skill skill in hero._skills)
                    {
                        if (sb.Length > 0) sb.Append(",");
                        sb.AppendFormat("{0},{1}", skill._id, skill._level);
                    }
                    sw.WriteLine(sb.ToString());
                }

                // spells
                {
                    System.Text.StringBuilder sb = new StringBuilder();
                    foreach (Spell spell in hero._spells)
                    {
                        if (sb.Length > 0) sb.Append(",");
                        sb.AppendFormat("{0}", spell._id);
                    }
                    sw.WriteLine(sb.ToString());
                }

                // artifacts
            }

            return true;
        }

        private string GetSaveFileName(string playerName)
        {
            return string.Format(@"{0}\Saves\{1}.txt", Heroes.Core.Setting._appStartupPath, playerName);
        }

        public void JoinGame(string playerName, out Heroes.Core.Player player)
        {
            player = null;

            if (IsPlayerExist(playerName))
            {

            }
            else
            {
 
            }
        }
        #endregion

        #region Create, join game
        public void CreateGame(out Heroes.Core.Player player)
        {
            player = null;

            lock (GameSetting._lockMe)
            {
                GameSetting._players.Clear();
                GameSetting._currentPlayerId = 0;

                GameSetting._isWaitToJoinGame = true;
                GameSetting._playerStartingGames.Clear();

                GameSetting._playerGameStarteds.Clear();
                GameSetting._isGameStarted = false;

                int playerId = 1;
                CreatePlayer(playerId);
                player = (Heroes.Core.Player)GameSetting._playerKIds[playerId];
            }
        }

        public bool IsWaitToJoinGame()
        {
            return GameSetting._isWaitToJoinGame;
        }

        public bool IsPlayerJoined(string playerName)
        {
            foreach (Heroes.Core.Player player in GameSetting._players)
            {
                if (string.Compare(player._name, playerName, true) == 0) 
                    return true;
            }

            return false;
        }

        public void JoinGame(out Heroes.Core.Player player)
        {
            player = null;

            if (!GameSetting._isWaitToJoinGame) return;

            lock (GameSetting._lockMe)
            {
                int playerId = GameSetting._players.Count + 1;
                CreatePlayer(playerId);
                player = (Heroes.Core.Player)GameSetting._playerKIds[playerId];
            }
        }

        private void CreatePlayer(int playerId)
        {
            Heroes.Core.Player player = new Player();
            player._id = playerId;
            GameSetting._players.Add(player);
            GameSetting._playerKIds.Add(player._id, player);
        }

        // get players in this game
        public ArrayList GetPlayers()
        {
            return GameSetting._players;
        }
        #endregion

        #region Start Game
        // Game master click start game
        public void StartGame()
        {
            lock (GameSetting._lockMe)
            {
                GameSetting._isWaitToJoinGame = false;

                foreach (Heroes.Core.Player player in GameSetting._players)
                {
                    GameSetting._playerStartingGames.Add(player._id, true);
                }

                GameSetting._currentPlayerId = 1;   // first player is game master

                foreach (Heroes.Core.Player player in GameSetting._players)
                {
                    InitPlayer(player);
                }
            }
        }

        private void InitPlayer(Heroes.Core.Player player)
        {
            Random rnd = new Random();
            int heroId = 0;
            do
            {
                heroId = rnd.Next(1, 16);
            }
            while (GameSetting._heroKIds.ContainsKey(heroId));

            GameSetting._heroKIds.Add(heroId, heroId);
            GameSetting._startingHeroKPlayerId.Add(player._id, heroId);
        }

        public bool IsNeedToStartGame(int playerId)
        {
            return (bool)GameSetting._playerStartingGames[playerId];
        }

        public void SetIsNeedToStartGame(int playerId)
        {
            if (!GameSetting._playerStartingGames.ContainsKey(playerId)) return;
            GameSetting._playerStartingGames[playerId] = false;
        }

        public ArrayList GetPlayerIds()
        {
            ArrayList playerIds = new ArrayList();
            foreach (Heroes.Core.Player player in GameSetting._players)
            {
                playerIds.Add(player._id);
            }
            return playerIds;
        }

        public int GetStartingHeroId(int playerId)
        {
            if (!GameSetting._startingHeroKPlayerId.ContainsKey(playerId)) return 0;
            return (int)GameSetting._startingHeroKPlayerId[playerId];
        }

        // each player need to set GameStarted flag
        public void SetGameStarted(int playerId)
        {
            if (GameSetting._playerGameStarteds.ContainsKey(playerId)) return;
            GameSetting._playerGameStarteds.Add(playerId, true);

            if (GameSetting._playerGameStarteds.Count == GameSetting._players.Count)
                GameSetting._isGameStarted = true;
        }

        public bool IsGameStarted()
        {
            return GameSetting._isGameStarted;
        }
        #endregion

        #region In Game
        public Heroes.Core.Player GetPlayer(int playerId)
        {
            if (GameSetting._playerKIds.ContainsKey(playerId))
                return (Heroes.Core.Player)GameSetting._playerKIds[playerId];
            else
                return null;
        }

        public Hashtable GetAllPlayers()
        {
            return GameSetting._playerKIds;
        }

        public void SetPlayer(Heroes.Core.Player player)
        {
            if (!GameSetting._playerKIds.ContainsKey(player._id)) return;
            GameSetting._playerKIds[player._id] = player;

            int index = Heroes.Core.Player.FindPlayerIndex(GameSetting._players, player._id);
            if (index < 0) return;
            GameSetting._players[index] = player;
        }

        public int GetCurrentPlayerId()
        {
            return GameSetting._currentPlayerId;
        }

        public void NextPlayer(int currentPlayerId)
        {
            lock (GameSetting._lockMe)
            {
                int playerId = Heroes.Core.Player.NextPlayerId(GameSetting._players, currentPlayerId);
                if (playerId < 0) return;
                GameSetting._currentPlayerId = playerId;
            }
        }

        public int GetRndArtifactId(int level)
        {
            lock (GameSetting._lockMe)
            {
                Hashtable artifacts = Heroes.Core.Setting._artifactKLevelKIds[(Heroes.Core.Heros.ArtifactLevelEnum)level];

                ArrayList ids = new ArrayList();
                foreach (Heroes.Core.Heros.Artifact a in artifacts.Values)
                {
                    if (GameSetting._artifactKIds.ContainsKey(a._id)) continue;

                    ids.Add(a);
                }

                // skip if no more artifact
                if (ids.Count < 1) return 0;

                Random rnd = new Random();
                int index = rnd.Next(0, ids.Count);
                Heroes.Core.Heros.Artifact a2 = (Heroes.Core.Heros.Artifact)ids[index];

                GameSetting._artifactKIds.Add(a2._id, a2._id);

                return a2._id;
            }
        }
        #endregion

        #region Battle
        public void SetStartingBattle(Heroes.Core.Hero attackHero, Heroes.Core.Hero defendHero)
        {
            lock (GameSetting._lockMe)
            {
                GameSetting._attackHero = attackHero;
                GameSetting._defendHero = defendHero;

                GameSetting._playerNeedToStartBattles.Clear();
                GameSetting._playerNeedToStartBattles.Add(attackHero._playerId, true);
                GameSetting._playerNeedToStartBattles.Add(defendHero._playerId, true);
            }
        }

        public bool IsNeedToStartBattle(int playerId)
        {
            if (GameSetting._playerNeedToStartBattles.ContainsKey(playerId))
                return (bool)GameSetting._playerNeedToStartBattles[playerId];
            else
                return false;
        }

        public void SetIsNeedToStartBattle(int playerId)
        {
            if (!GameSetting._playerNeedToStartBattles.ContainsKey(playerId)) return;
            GameSetting._playerNeedToStartBattles[playerId] = false;
        }

        public void GetBattle(out Heroes.Core.Hero attackHero, out Heroes.Core.Hero defendHero)
        {
            attackHero = GameSetting._attackHero;
            defendHero = GameSetting._defendHero;
        }

        public void EnqueueBattleCommand(int playerId, BattleCommand cmd)
        {
            if (GameSetting._attackHero != null && playerId == GameSetting._attackHero._playerId)
            {
                lock (GameSetting._lockMe)
                {
                    GameSetting._attackCommands.Enqueue(cmd);
                }
            }
            else if (GameSetting._defendHero != null && playerId == GameSetting._defendHero._playerId)
            {
                lock (GameSetting._lockMe)
                {
                    GameSetting._defendCommands.Enqueue(cmd);
                }
            }
        }

        public void DequeueBattleCommand(int playerId, out BattleCommand cmd)
        {
            cmd = null;

            if (GameSetting._attackHero != null && playerId == GameSetting._attackHero._playerId)
            {
                // attacker get defender commands
                if (GameSetting._defendCommands.Count > 0)
                {
                    lock (GameSetting._lockMe)
                    {
                        cmd = (BattleCommand)GameSetting._defendCommands.Dequeue();
                    }
                }
            }
            else if (GameSetting._defendHero != null && playerId == GameSetting._defendHero._playerId)
            {
                // defender get attacker commands
                if (GameSetting._attackCommands.Count > 0)
                {
                    lock (GameSetting._lockMe)
                    {
                        cmd = (BattleCommand)GameSetting._attackCommands.Dequeue();
                    }
                }
            }
        }
        #endregion

    }
}
