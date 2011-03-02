using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Battle.Characters
{
    public class Hero : Heroes.Core.Hero
    {
        public Heroes.Core.Hero _originalHero;

        public Hero(int id)
            : base(id)
        { 
        }

    }
}
