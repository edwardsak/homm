using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;

namespace Heroes.Core
{
    [Serializable]
    public class Building
    {
        public int _id;
        public int _raceId;
        public string _name;
        public string _description;

        public int _wood;
        public int _ore;
        public int _gem;
        public int _sulfur;
        public int _crystal;
        public int _mercury;
        public int _gold;

        public bool _isDwelling;    // flag to tell this building can generate creature
        public int _armyQty;    // army qty that can be recruited
        public int _growth;     // growth per week
        public int _hordeId;    // building that add growth

        public int _woodGen;
        public int _oreGen;
        public int _gemGen;
        public int _sulfurGen;
        public int _crystalGen;
        public int _mercuryGen;
        public int _goldGen;

        public string _imgFileName;
        public Point _point;
        public int _orderSeq;   // start from 0, bigger the number, more in front to draw

        public Building()
        {
            Init(0);
        }

        public Building(int id)
        {
            Init(id);
        }

        private void Init(int id)
        {
            _id = id;
            _raceId = 0;
            _name = "";
            _description = "";

            _wood = 0;
            _mercury = 0;
            _ore = 0;
            _sulfur = 0;
            _crystal = 0;
            _gem = 0;
            _gold = 0;

            _isDwelling = false;
            _armyQty = 0;
            _growth = 0;
            _hordeId = 0;

            _woodGen = 0;
            _mercuryGen = 0;
            _oreGen = 0;
            _sulfurGen = 0;
            _crystalGen = 0;
            _gemGen = 0;
            _goldGen = 0;

            _imgFileName = "";
            _point = new Point(0, 0);
            _orderSeq = 0;
        }

        public void CopyFrom(Building building)
        {
            this._id = building._id;
            this._raceId = building._raceId;
            this._name = building._name;
            this._description = building._description;

            this._wood = building._wood;
            this._mercury = building._mercury;
            this._ore = building._ore;
            this._sulfur = building._sulfur;
            this._crystal = building._crystal;
            this._gem = building._gem;
            this._gold = building._gold;

            this._isDwelling = building._isDwelling;
            this._armyQty = building._armyQty;
            this._growth = building._growth;
            this._hordeId = building._hordeId;

            this._woodGen = building._woodGen;
            this._mercuryGen = building._mercuryGen;
            this._oreGen = building._oreGen;
            this._sulfurGen = building._sulfurGen;
            this._crystalGen = building._crystalGen;
            this._gemGen = building._gemGen;
            this._goldGen = building._goldGen;

            this._imgFileName = building._imgFileName;
            this._point = new Point(building._point.X, building._point.Y);
            this._orderSeq = building._orderSeq;
        }

        public int FirstGrowth()
        {
            return (int)decimal.Ceiling((decimal)this._growth / 2m);
        }

        public bool CanBuild(Player player)
        {
            if (player._wood < this._wood) return false;
            if (player._ore < this._ore) return false;
            if (player._gem < this._gem) return false;
            if (player._sulfur < this._sulfur) return false;
            if (player._crystal < this._crystal) return false;
            if (player._mercury < this._mercury) return false;
            if (player._gold < this._gold) return false;

            return true;
        }

        public void DeductResources(Player player)
        {
            player._wood -= this._wood;
            player._ore -= this._ore;
            player._gem -= this._gem;
            player._sulfur -= this._sulfur;
            player._crystal -= this._crystal;
            player._mercury -= this._mercury;
            player._gold -= this._gold;
        }

    }

    public class CompareOrderSeq : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            Building b1 = (Building)x;
            Building b2 = (Building)y;
            return b1._orderSeq - b2._orderSeq;
        }

        #endregion
    }
}
