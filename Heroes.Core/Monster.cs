using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Heroes.Core
{
    public class Monster
    {
        public int _id;
        public string _name;
        public Hashtable _armyKSlots;

        public Monster()
        {
            _id = 0;
            _name = "";
            _armyKSlots = new Hashtable();
        }

        public void CopyFrom(Monster monster)
        {
            this._id = monster._id;
            this._name = monster._name;
        }

    }

    
}
