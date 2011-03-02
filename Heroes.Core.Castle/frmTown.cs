using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Castle
{
    public partial class frmTown : Form 
    {
        Town _town;

        public frmTown()
        {
            InitializeComponent();
        }

        #region Load Town Info
        private void frmTown_Load(object sender, EventArgs e)
        {
            ChangeImage();
            ChangeImage2();
        }

        private void ChangeImage()
        {
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Guardhouse))
            {
                cmdGuard.BackgroundImage = Heroes.Core.Castle.Properties.Resources.UpgGuardhouse;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgGuardhouse))
            {
                cmdGuard.BackgroundImage = Heroes.Core.Castle.Properties.Resources.GuardhouseDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.ArchersTower))
            {
                cmdArcher.BackgroundImage = Heroes.Core.Castle.Properties.Resources.UpgArcherTower;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgArchersTower))
            {
                cmdArcher.BackgroundImage = Heroes.Core.Castle.Properties.Resources.ArcherTowerDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.GriffinTower))
            {
                cmdGriffin.BackgroundImage = Heroes.Core.Castle.Properties.Resources.UpgGriffinTower;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgGriffinTower))
            {
                cmdGriffin.BackgroundImage = Heroes.Core.Castle.Properties.Resources.GriffinTowerDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Barracks))
            {
                cmdSwordsmen.BackgroundImage = Heroes.Core.Castle.Properties.Resources.UpgBarracks;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgBarracks))
            {
                cmdSwordsmen.BackgroundImage = Heroes.Core.Castle.Properties.Resources.BarracksDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Monastery))
            {
                cmdMonastery.BackgroundImage = Heroes.Core.Castle.Properties.Resources.UpgMonasteryTown;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgMonastery))
            {
                cmdMonastery.BackgroundImage = Heroes.Core.Castle.Properties.Resources.MonasteryDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.TrainingGrounds))
            {
                cmdTrainingGround.BackgroundImage = Heroes.Core.Castle.Properties.Resources.UpgTrainingGrounds;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgTrainingGrounds))
            {
                cmdTrainingGround.BackgroundImage = Heroes.Core.Castle.Properties.Resources.TraininggroundDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.PortalofGlory))
            {
                cmdAngel.BackgroundImage = Heroes.Core.Castle.Properties.Resources.UpgPortalofGlory;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgPortalofGlory))
            {
                cmdAngel.BackgroundImage = Heroes.Core.Castle.Properties.Resources.PortalofGloryDone;
            }
        }

        private void ChangeImage2()
        {
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.TownHall))
            {
                cmdTownHall.BackgroundImage = Heroes.Core.Castle.Properties.Resources.CityHall;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.CityHall))
            {
                cmdTownHall.BackgroundImage = Heroes.Core.Castle.Properties.Resources.Capitall;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Capitol))
            {
                cmdTownHall.BackgroundImage = Heroes.Core.Castle.Properties.Resources.CapitalDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Citadel))
            {
                cmdCitadel.BackgroundImage = Heroes.Core.Castle.Properties.Resources.Castlel;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Castle))
            {
                cmdCitadel.BackgroundImage = Heroes.Core.Castle.Properties.Resources.CastleDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.BrotherhoodOfTheSword))
            {
                cmdTavern.BackgroundImage = Heroes.Core.Castle.Properties.Resources.BrotherhoodDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Blacksmith))
            {
                cmdBlackSmith.BackgroundImage = Heroes.Core.Castle.Properties.Resources.BlacksmithDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Marketplace))
            {
                cmdMarketplace.BackgroundImage = Heroes.Core.Castle.Properties.Resources.ResourceSilo;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.ResourceSilo))
            {
                cmdMarketplace.BackgroundImage = Heroes.Core.Castle.Properties.Resources.ResourceSiloDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel1))
            {
                cmdMageGuildLevel1.BackgroundImage = Heroes.Core.Castle.Properties.Resources.MageGuildLevel12;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel2))
            {
                cmdMageGuildLevel1.BackgroundImage = Heroes.Core.Castle.Properties.Resources.MageGuildLevel13;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel3))
            {
                cmdMageGuildLevel1.BackgroundImage = Heroes.Core.Castle.Properties.Resources.MageGuildLevel14;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel4))
            {
                cmdMageGuildLevel1.BackgroundImage = Heroes.Core.Castle.Properties.Resources.MageGuildLevel4Done;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Shipyard))
            {
                cmdShipyard.BackgroundImage = Heroes.Core.Castle.Properties.Resources.Lighthouse;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Lighthouse))
            {
                cmdShipyard.BackgroundImage = Heroes.Core.Castle.Properties.Resources.LighthouseDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Stables))
            {
                cmdStable.BackgroundImage = Heroes.Core.Castle.Properties.Resources.StableDone;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.GriffinBastion))
            {
                cmdGriffinBastion.BackgroundImage = Heroes.Core.Castle.Properties.Resources.GriffinBastionDone;
            }
        }
        #endregion

        public DialogResult ShowDialog(Town town)
        {
            _town = town;
            return this.ShowDialog();
        }

        #region Army Building
        private void cmdGuard_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgGuardhouse))
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Guardhouse))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Guardhouse]);
                
                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b2.FirstGrowth();
                
                _town._buildingKIds.Add(b2._id, b2);
                
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgGuardhouse))
            {
                // get basic building
                Heroes.Core.Building b1 = (Heroes.Core.Building)_town._buildingKIds[(int)Heroes.Core.BuildingIdEnum.Guardhouse];
                
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.UpgGuardhouse]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b1._armyQty;
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.Guardhouse);

                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdArcher_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgArchersTower))
            {
                return;
            }
            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.ArchersTower))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.ArchersTower]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b2.FirstGrowth();
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgArchersTower))
            {
                // get basic building
                Heroes.Core.Building b1 = (Heroes.Core.Building)_town._buildingKIds[(int)Heroes.Core.BuildingIdEnum.ArchersTower];

                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.UpgArchersTower]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b1._armyQty;
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.ArchersTower);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdGriffin_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgGriffinTower))
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.GriffinTower))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.GriffinTower]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b2.FirstGrowth();
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgGriffinTower))
            {
                if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.GriffinBastion))
                {
                    MessageBox.Show("Requires : GriffinBastion");
                    return;
                }
                // get basic building
                Heroes.Core.Building b1 = (Heroes.Core.Building)_town._buildingKIds[(int)Heroes.Core.BuildingIdEnum.GriffinTower];

                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.UpgGriffinTower]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b1._armyQty;
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.GriffinTower);

                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
          
        }

        private void cmdSwordsmen_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgBarracks))
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Barracks))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Barracks]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b2.FirstGrowth();
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgBarracks))
            {
                // get basic building
                Heroes.Core.Building b1 = (Heroes.Core.Building)_town._buildingKIds[(int)Heroes.Core.BuildingIdEnum.Barracks];

                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.UpgBarracks]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b1._armyQty;
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.Barracks);

                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdMonastery_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgMonastery))
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Monastery))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Monastery]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b2.FirstGrowth();
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgMonastery))
            {
                // get basic building
                Heroes.Core.Building b1 = (Heroes.Core.Building)_town._buildingKIds[(int)Heroes.Core.BuildingIdEnum.Monastery];

                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.UpgMonastery]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b1._armyQty;
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.Monastery);

                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdTrainingGround_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgTrainingGrounds))
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Stables))
            {
                MessageBox.Show("Requires : Stable");
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.TrainingGrounds))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.TrainingGrounds]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b2.FirstGrowth();
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgTrainingGrounds))
            {
                // get basic building
                Heroes.Core.Building b1 = (Heroes.Core.Building)_town._buildingKIds[(int)Heroes.Core.BuildingIdEnum.TrainingGrounds];

                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.UpgTrainingGrounds]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b1._armyQty;
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.TrainingGrounds);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdAngel_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }
            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgPortalofGlory))
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.PortalofGlory))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.PortalofGlory]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b2.FirstGrowth();
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.UpgPortalofGlory))
            {
                // get basic building
                Heroes.Core.Building b1 = (Heroes.Core.Building)_town._buildingKIds[(int)Heroes.Core.BuildingIdEnum.PortalofGlory];

                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.UpgPortalofGlory]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._armyQty = b1._armyQty;
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.PortalofGlory); 

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }
        #endregion

        #region Support Building
        private void cmdTownHall_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Capitol))
            {
                return;
            }

            if (_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.CityHall))
            {
                // get basic building
                Heroes.Core.Building b1 = (Heroes.Core.Building)_town._buildingKIds[(int)Heroes.Core.BuildingIdEnum.CityHall];

                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Capitol]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._gold = b1._gold;
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.CityHall);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.TownHall))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.TownHall]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.VillageHall);
                _town._isBuilt = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.CityHall))
            {
                // get basic building
                Heroes.Core.Building b1 = (Heroes.Core.Building)_town._buildingKIds[(int)Heroes.Core.BuildingIdEnum.TownHall];

                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.CityHall]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._gold = b1._gold;
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.TownHall);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Capitol))
            {
                // get basic building
                Heroes.Core.Building b1 = (Heroes.Core.Building)_town._buildingKIds[(int)Heroes.Core.BuildingIdEnum.CityHall];

                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Capitol]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                b2._gold = b1._gold;
                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.CityHall);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdCitadel_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Citadel))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Citadel]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Castle))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Castle]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.Citadel);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdTavern_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.BrotherhoodOfTheSword))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.BrotherhoodOfTheSword]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.Tavern);
                _town._isBuilt = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdBlackSmith_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Blacksmith))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Blacksmith]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdMarketplace_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Marketplace))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Marketplace]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.ResourceSilo))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.ResourceSilo]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                //_town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.Marketplace);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdMageGuildLevel1_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel1))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.MageGuildLevel1]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel2))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.MageGuildLevel2]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.MageGuildLevel1);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel3))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.MageGuildLevel3]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.MageGuildLevel2);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.MageGuildLevel4))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.MageGuildLevel4]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                _town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.MageGuildLevel3);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdShipyard_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }
            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Shipyard))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Shipyard]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Lighthouse))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Heroes.Core.Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Lighthouse]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;
                frmCastle.build.Add(b2._id, b2);

                // remove basic building
                //_town._buildingKIds.Remove((int)Heroes.Core.BuildingIdEnum.Shipyard);

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdStable_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }
            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.Stables))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.Stables]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }

        private void cmdGriffinBastion_Click(object sender, EventArgs e)
        {
            if (_town._isBuilt == true)
            {
                return;
            }
            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.GriffinTower))
            {
                MessageBox.Show("Requires : GriffinTower");
                return;
            }

            if (!_town._buildingKIds.ContainsKey((int)Heroes.Core.BuildingIdEnum.GriffinBastion))
            {
                Heroes.Core.Building b2 = new Building();
                b2.CopyFrom((Building)Heroes.Core.Setting._buildings[(int)Heroes.Core.BuildingIdEnum.GriffinBastion]);

                if (!b2.CanBuild(_town._player))
                {
                    MessageBox.Show("Cannot build this building.");
                    return;
                }

                b2.DeductResources(_town._player);

                _town._buildingKIds.Add(b2._id, b2);
                _town._isBuilt = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            this.Close();
        }
        #endregion

        #region Cancel
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
