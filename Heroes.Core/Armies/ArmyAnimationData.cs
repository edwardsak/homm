using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Armies
{
    public class ArmyAnimationData
    {
        public const string PURPOSE_STANDING = "S";
        public const string PURPOSE_MOVING = "M";
        public const string PURPOSE_START_MOVING = "MB";
        public const string PURPOSE_STOP_MOVING = "ME";
        public const string PURPOSE_ATTACK_STRAIGHT_BEGIN = "ASB";
        public const string PURPOSE_ATTACK_STRAIGHT_END = "ASE";
        public const string PURPOSE_SHOOT_STRAIGHT_BEGIN = "SSB";
        public const string PURPOSE_SHOOT_STRAIGHT_END = "SSE";
        public const string PURPOSE_DEFEND = "DF";
        public const string PURPOSE_GETTING_HIT = "GH";
        public const string PURPOSE_DEATH = "D";
    }
}
