using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Heroes.Core.Remoting
{
    public class GameSetting
    {
        public static LockMe _lockMe = new LockMe();

        public static Hashtable _playerNameKIds = new Hashtable();

        public static ArrayList _players = new ArrayList();
        public static Hashtable _playerKIds = new Hashtable();
        public static int _currentPlayerId = 0;

        public static Hashtable _heroKIds = new Hashtable();    // key = heroId, hero is unique
        public static Hashtable _startingHeroKPlayerId = new Hashtable();   // key = playerId, value = heroId

        public static Hashtable _artifactKIds = new Hashtable();    // key = artifactId, artifact is unique

        public static bool _isWaitToJoinGame = false;
        public static Hashtable _playerStartingGames = new Hashtable(); // key = playerId, value = bool

        public static Hashtable _playerGameStarteds = new Hashtable();  // key = playerId, value = bool
        public static bool _isGameStarted = false;

        public static Heroes.Core.Hero _attackHero = null;
        public static Heroes.Core.Hero _defendHero = null;
        public static Hashtable _playerNeedToStartBattles = new Hashtable();    // key = playerId, value = bool

        public static Queue _attackCommands = new Queue();
        public static Queue _defendCommands = new Queue();

        public static int _day = 0;
        public static int _week = 0;    // 7 days = 1 week
        public static int _month = 0;   // 4 weeks = 1 month
    }

    public class LockMe
    { 
    }

}
