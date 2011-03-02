using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Heroes.Core.Map
{
    public class Town : Heroes.Core.Town
    {
        public Image _image;

        public Town(int id, Image img)
        {
            _id = id;
            _image = img;

            Heroes.Core.Army army = new Heroes.Core.Army();
            army._id = (int)Heroes.Core.ArmyIdEnum.Pikeman;
            army._qty = 14;
            army._name = "Pikemen";
            base._armyAvKIds.Add(army._id, army);

            Heroes.Core.Army army1 = new Heroes.Core.Army();
            army1._id = (int)Heroes.Core.ArmyIdEnum.Archer;
            army1._qty = 9;
            army1._name = "Archer";
            base._armyAvKIds.Add(army1._id, army1);

            Heroes.Core.Army army2 = new Heroes.Core.Army();
            army2._id = (int)Heroes.Core.ArmyIdEnum.Griffin;
            army2._qty = 7;
            army2._name = "Griffin";
            base._armyAvKIds.Add(army2._id, army2);

            Heroes.Core.Army army3 = new Heroes.Core.Army();
            army3._id = (int)Heroes.Core.ArmyIdEnum.Swordman;
            army3._qty = 4;
            army3._name = "Swordman";
            base._armyAvKIds.Add(army3._id, army3);

            Heroes.Core.Army army4 = new Heroes.Core.Army();
            army4._id = (int)Heroes.Core.ArmyIdEnum.Monk;
            army4._qty = 3;
            army4._name = "Monk";
            base._armyAvKIds.Add(army4._id, army4);

            Heroes.Core.Army army5 = new Heroes.Core.Army();
            army5._id = (int)Heroes.Core.ArmyIdEnum.Cavalier;
            army5._qty = 2;
            army5._name = "Cavalier";
            base._armyAvKIds.Add(army5._id, army5);

            Heroes.Core.Army army6 = new Heroes.Core.Army();
            army6._id = (int)Heroes.Core.ArmyIdEnum.Angel;
            army6._qty = 1;
            army6._name = "Angel";
            base._armyAvKIds.Add(army6._id, army6);
        }

    }
}
