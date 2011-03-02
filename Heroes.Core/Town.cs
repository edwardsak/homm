using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Heroes.Core
{
    [Serializable]
    public class Town
    {
        public int _id;
        public int _playerId;
        public string _armyName;
        public Player _player;
        public Hashtable _buildingKIds; // buildings in castle
        public Hashtable _armyAvKIds;     // army available to purchase, key = ArmyId
        public Hashtable _armyInCastleKSlots;     // army in castle
        public Hashtable _armyInCastleLabels;
        public Hero _heroInCastle;      // hero defending in castle
        public Hero _heroVisit;  // hero visiting castle
        public bool _isBuilt;

        Town _town;

        public Town()
        {
            _id = 0;
            _playerId = 0;
            _armyName = "";
            _player = null;
            _buildingKIds = new Hashtable();
            _armyAvKIds = new Hashtable();
            _armyInCastleKSlots = new Hashtable();
            _armyInCastleLabels = new Hashtable();
            _heroInCastle = null;
            _heroVisit = null;
            _isBuilt = false;
        }

        public static Army FindArmy(Hashtable armyKSlots, int armyId)
        {
            foreach (Army army in armyKSlots.Values)
            {
                if (army._id == armyId) return army;
            }

            return null;
        }

        #region Auto Increment for Army Quantity
        public void AddGrowth()
        {
            decimal multiplier = 0m;
            if (_buildingKIds.ContainsKey((int)BuildingIdEnum.Castle))
                multiplier = 1m;
            else if (_buildingKIds.ContainsKey((int)BuildingIdEnum.Citadel))
                multiplier = 0.5m;

            foreach (Building building in _buildingKIds.Values)
            {
                if (!building._isDwelling) continue;

                int growth = building._growth;
                if (this._buildingKIds.ContainsKey(building._hordeId))
                    growth += ((Heroes.Core.Building)this._buildingKIds[building._hordeId])._growth;

                building._armyQty += (int)decimal.Truncate(growth * (1 + multiplier));
            }
        }
        #endregion

        #region Find the Army Slot
        public int FindFirstEmptySlotNo()
        {
            for (int slot = 0; slot < 7; slot++)
            {
                if(!_armyInCastleKSlots.ContainsKey(slot)) return slot;

                foreach (Army army in _armyInCastleKSlots.Values)
                {
                    if (_armyInCastleLabels.ContainsKey(army._id)) return slot;
                }
                
            }
            return -1;
        }

        public int FindMatchSlotNo(Hashtable armyKSlots, int armyId)
        {
            for (int slot = 0; slot < 7; slot++)
            {
                foreach (Army army in armyKSlots.Values)
                {
                    if (army._id == armyId)
                    return slot;
                }
                
            }
            return -1;
        }


        #endregion
    }
}
