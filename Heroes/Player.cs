using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes
{
    [Serializable]
    public class Player : Heroes.Core.Player
    {
        public string _ip;
        public bool _isInitialized;

        public Player()
        {
            _ip = "";
            _isInitialized = false;
        }

    }
}
