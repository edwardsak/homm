using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core
{
    [Serializable]
    public class Mine
    {
        public int _id;
        public int _playerId;
        public Player _player;
        public string _name;
        public MineTypeEnum _mineType;
        public int _wood;
        public int _mercury;
        public int _ore;
        public int _sulfur;
        public int _crystal;
        public int _gem;
        public int _gold;
        public string _description;

        public Mine()
        {
            _id = 0;
            _playerId = 0;
            _player = null;
            _name = "";
        }

        public Mine(int id)
        {
            _id = id;
            _playerId = 0;
            _player = null;
            _name = "";
        }

        public void CopyFrom(Mine mine)
        {
            this._id = mine._id;
            this._playerId = mine._playerId;
            this._name = mine._name;
            this._mineType = mine._mineType;
            this._wood = mine._wood;
            this._mercury = mine._mercury;
            this._ore = mine._ore;
            this._sulfur = mine._sulfur;
            this._crystal = mine._crystal;
            this._gem = mine._gem;
            this._gold = mine._gold;
            this._description = mine._description;
        }

    }
}
