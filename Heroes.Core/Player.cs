using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Heroes.Core
{
    [Serializable]
    public class Player
    {
        public int _id;
        public string _name;
        public bool _isComputer;

        public int _wood;
        public int _mercury;
        public int _ore;
        public int _sulfur;
        public int _crystal;
        public int _gem;
        public int _gold;

        public ArrayList _castles;
        public ArrayList _heroes;
        public Dictionary<int, ArrayList> _mineKTypes;

        public Player()
        {
            Init(0);
        }

        public Player(int id)
        {
            Init(id);
        }

        private void Init(int id)
        {
            _id = id;
            _name = "";
            _isComputer = false;
            _wood = 0;
            _mercury = 0;
            _ore = 0;
            _sulfur = 0;
            _crystal = 0;
            _gem = 0;
            _gold = 0;

            _castles = new ArrayList();
            _heroes = new ArrayList();

            _mineKTypes = new Dictionary<int, ArrayList>();
            _mineKTypes.Add((int)Heroes.Core.MineTypeEnum.Wood, new ArrayList());
            _mineKTypes.Add((int)Heroes.Core.MineTypeEnum.Mercury, new ArrayList());
            _mineKTypes.Add((int)Heroes.Core.MineTypeEnum.Ore, new ArrayList());
            _mineKTypes.Add((int)Heroes.Core.MineTypeEnum.Sulfur, new ArrayList());
            _mineKTypes.Add((int)Heroes.Core.MineTypeEnum.Crystal, new ArrayList());
            _mineKTypes.Add((int)Heroes.Core.MineTypeEnum.Gem, new ArrayList());
            _mineKTypes.Add((int)Heroes.Core.MineTypeEnum.Gold, new ArrayList());
        }

        public void CopyFrom(Player player)
        {
            this._id = player._id;
            this._name = player._name;
            this._isComputer = player._isComputer;

            this._wood = player._wood;
            this._mercury = player._mercury;
            this._ore = player._ore;
            this._sulfur = player._sulfur;
            this._crystal = player._crystal;
            this._gem = player._gem;
            this._gold = player._gold;
        }

        public void AddResources()
        {
            foreach (ArrayList mines in this._mineKTypes.Values)
            {
                foreach (Heroes.Core.Mine mine in mines)
                {
                    this._wood += mine._wood;
                    this._mercury += mine._mercury;
                    this._ore += mine._ore;
                    this._sulfur += mine._sulfur;
                    this._crystal += mine._crystal;
                    this._gem += mine._gem;
                    this._gold += mine._gold;
                }
            }

            foreach (Heroes.Core.Town town in this._castles)
            {
                foreach (Heroes.Core.Building building in town._buildingKIds.Values)
                {
                    this._wood += building._woodGen;
                    this._mercury += building._mercuryGen;
                    this._ore += building._oreGen;
                    this._sulfur += building._sulfurGen;
                    this._crystal += building._crystalGen;
                    this._gem += building._gemGen;
                    this._gold += building._goldGen;
                }
            }
        }

        public void AddGrowth()
        {
            foreach (Town town in _castles)
            {
                town.AddGrowth();
            }
        }

        public void ResetBuilt()
        {
            foreach (Heroes.Core.Town town in this._castles)
            {
                town._isBuilt = false;
            }
        }

        public void ResetMovementPoints()
        {
            foreach (Heroes.Core.Hero hero in this._heroes)
            {
                hero._movementPointLeft = hero._movementPoint;
            }
        }

        public static int FindPlayerIndex(ArrayList players, int playerId)
        {
            int index = 0;
            foreach (Player player in players)
            {
                if (player._id == playerId) return index;

                index += 1;
            }

            return -1;
        }

        public static Player FindPlayer(ArrayList players, int playerId)
        {
            int index = FindPlayerIndex(players, playerId);
            if (index < 0) return null;
            return (Player)players[index];
        }

        public static int NextPlayerId(ArrayList players, int currentPlayerId)
        {
            if (players.Count < 1) return -1;

            int index = FindPlayerIndex(players, currentPlayerId);
            index = NextPlayerIndex(players, index);
            if (index < 0) return -1;

            Player player2 = (Player)players[index];
            return player2._id;
        }

        public static int NextPlayerIndex(ArrayList players, int index)
        {
            if (index < 0) return -1;
            else if (index >= players.Count - 1) index = 0;
            else index += 1;

            return index;
        }

    }
}
