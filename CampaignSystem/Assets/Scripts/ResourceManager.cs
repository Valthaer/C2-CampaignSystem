using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace LonelyCastle.A2
{
    public static class ResourceManager 
    {
        //Pasar a un fichero externo de recursos
        
        //USER INTERFACE
        public static float ScrollSpeed { get { return 0.1f; } }
        public static float ScrollMaxSpeed { get { return 5f; } }
        public static float ScrollHeightSpeed { get { return 25f; } }
        public static float RotateSpeed { get { return 100; } }
        public static int ScrollWidth { get { return 15; } }
        public static float MinCameraHeight { get { return 2; } }
        public static float MaxCameraHeight { get { return 500; } }

        public static CultureInfo UserCultureInfo { get; set; }

        //UNITS ADN BUILDINGS

        public static List<UnitDTO> UnitsTypesList { get; set; }
        public static List<BuildingDTO> BuildingsTypesList { get; set; }

        public static int WorkerBuildRate { get { return 10; } }
        public static int WorkerRepairRate { get { return 10; } }

        //TERRAIN

        // Heights, use Biomas reference, but as summary:
        //Max camera height: 500
        //Max terrain height: 300
        //Min terrain height:0       
        public static Dictionary<string, TextureDataDTO> TerrainTexturesList { get; set; }
        public static List<TreeDataDTO> TreeTypesList { get; set; }
        public static float MaxTreesUsableSteepness { get { return 50.0f; } }
        public static float MaxInfantryUsableSteepness{get { return 50.0f; }}
        public static float MaxCavalryUsableSteepness { get { return 35.0f; } }
        public static int ForestsToGenerate { get { return 200; } }
        public static int WorldForestsDensity { get { return (int)WorldForestDensityEnum.Average; } }
        public static int MaxNumberOfTrees { get { return 30000; } } // TODO hacer que se use la de arriba WorldForestsDensity para cambiar el valor de esta

        public static float MapSizeMultiplier
        {
            get
            {
                float mapSizeMultiplier = (float)SceneManagerInstance.Map.Terrain.Size/2048f;
                //Designer maps are 3 regions per side and resolution 1025 for Genesis,with 2048x2048 real result

                return mapSizeMultiplier;
            }
        }

        //MISC
        public static Vector3 InvalidPosition { get { return new Vector3(-99999, -99999, -99999); } }
        
        private static SceneManager sceneManagerInstance;

        public static SceneManager SceneManagerInstance
        {
            get
            {
                if (sceneManagerInstance == null)
                {
                    sceneManagerInstance = GameObject.Find("SceneManagerPrefab").GetComponent<SceneManager>();
                }
                return sceneManagerInstance;    
            }            
        }

        public enum WorldObjectTypes
        {
            Unit = 1,
            Building = 2,
            GameResource = 3
        }
        
        public enum ButtonMenuLocationsEnum
        {
            BuildingBasicMenu,
            BuildingIndustryMenu,
            BuildingFoodMenu,
            BuildingMilitaryMenu,
            BuildingDefensesMenu,
            EscMenu,
            HomeWindow,
            BuildingHeaderMenu
        }

        //BASIC RESOURCES :
        //Resources typed by name that must exist always
        
        public enum WorldForestDensityEnum
        {
            //In %
            Low = 15,
            Relative = 25,
            Average = 40,
            High = 50,
            VeryHigh = 65
        }

        public enum UnitBasicTypes
        {
            Avatar = 1,
            Citizen = 2,
            Worker = 3,
            ArmedPeasantN0 = 4
        }

        public enum BuildingBasicTypes
        {
            PopulationCenterN0 = 1, //Hoguera
            HouseN1 = 2, //Casa
            WoodcuttersHutN0 = 3, //Cabaña de leñador,
            StonecuttersHutN0 = 4, //Cabaña del Picapedrero
            AppleOrchardN0 = 5 //Huerto de Manzanas
        }

        public enum BuildingStatusLevels
        {
            ConstructionSite = 0,
            Builded = 1,
            NeedsRepairings = 2,
            Ruinous = 3,
            Destroyed = 4,
            Demolished = 5
        }

        public enum MapCellGroundStatusType
        {
            IsPassableNoBorderNoBuildingNoGameResource = 1,
            IsPassableNoBorderIsBuildingNoGameResource = 2,
            NoPassableIsBorderIsBuildingNoGameResource = 3,
            NoPassableNoBorderIsBuildingNoGameResource = 4,
            IsPassableNoBorderNoBuildingIsGameResource = 5,
            NoPassableIsBorderNoBuildingIsGameResource = 6,
            NoPassableNoBorderNoBuildingIsGameResource = 7,
            IsPassableIsBorderIsBuildingNoGameResource = 8
        }

        public enum ButtonClickFunctionsByIdCall
        {
            BuildingBasicPopulationCenterN0 = 1,
            HeaderBuildingBasicMenu = 2,
            HeaderBuildingIndustryMenu = 3,
            HeaderBuildingFoodMenu = 4,
            HeaderBuildingMilitaryMenu = 5,
            HeaderBuildingDefensesMenu = 6,
            HeaderBuildingOtherMenu = 7,
            BuildingBasicHouseN1 = 8,
            BuildingIndustryWoodcuttersHutN0 = 9,
            
        }

        static ResourceManager()
        {
            UserCultureInfo = new CultureInfo("es-ES");
            //UserCultureInfo = new CultureInfo("en-US");

            /*TODO It must be developed a proccess to load entity types with all their data from a text file. For example, a tree with their type, altitude grow range, 
            resources name, list to be added, etc*/

            TreeTypesList = new List<TreeDataDTO>();
            TerrainTexturesList = new Dictionary<string, TextureDataDTO>();

            BuildingsTypesList = new List<BuildingDTO>();
            UnitsTypesList = new List<UnitDTO>();
            
            //TREES

            //Pino
            TreeDataDTO TreeSmallPine = new TreeDataDTO();
            TreeSmallPine.IdTree = 0;
            TreeSmallPine.Name = "TreeSmallPine";
            TreeSmallPine.Family = "Pine";
            TreeSmallPine.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MinHeight;
            TreeSmallPine.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["MountainFootHills"].MaxHeight;
            TreeSmallPine.DensityInForest = 0.7f;
            TreeSmallPine.DensityInForestForTypeInFamily = 0.3f;
            TreeSmallPine.AreaCells = 9;
            TreeSmallPine.Prefabs = new List<Object>();            
            TreeSmallPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_10"));
            TreeSmallPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_11"));
            TreeSmallPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_12"));
            TreeSmallPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_16"));
            TreeSmallPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_05"));
            TreeSmallPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_02"));
            TreeTypesList.Add(TreeSmallPine);

            TreeDataDTO TreeMediumPine = new TreeDataDTO();
            TreeMediumPine.IdTree = 1;
            TreeMediumPine.Name = "TreeMediumPine";
            TreeMediumPine.Family = "Pine";            
            TreeMediumPine.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MinHeight;
            TreeMediumPine.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["MountainFootHills"].MaxHeight;
            TreeMediumPine.DensityInForest = 0.7f;
            TreeMediumPine.DensityInForestForTypeInFamily = 0.4f;
            TreeMediumPine.AreaCells = 9;
            TreeMediumPine.Prefabs = new List<Object>();
            TreeMediumPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_13"));
            TreeMediumPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_21"));
            TreeMediumPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_22"));
            TreeMediumPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_23"));
            TreeMediumPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_24"));
            TreeMediumPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_25"));
            TreeMediumPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_26"));
            TreeTypesList.Add(TreeMediumPine);

            TreeDataDTO TreeBigPine = new TreeDataDTO();
            TreeBigPine.IdTree = 2;
            TreeBigPine.Name = "TreeBigPine";
            TreeBigPine.Family = "Pine";            
            TreeBigPine.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MinHeight;
            TreeBigPine.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["MountainFootHills"].MaxHeight;
            TreeBigPine.DensityInForest = 0.7f;
            TreeBigPine.DensityInForestForTypeInFamily = 0.3f;
            TreeBigPine.AreaCells = 9;
            TreeBigPine.Prefabs = new List<Object>();
            TreeBigPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_28"));
            TreeBigPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_09"));
            TreeBigPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_01"));
            TreeBigPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_07"));
            TreeBigPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_06"));
            TreeBigPine.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_03"));
            TreeTypesList.Add(TreeBigPine);

            //Abeto
            TreeDataDTO TreeSmallFir = new TreeDataDTO();
            TreeSmallFir.IdTree = 3;
            TreeSmallFir.Name = "TreeSmallFir";
            TreeSmallFir.Family = "Fir";            
            TreeSmallFir.MinGrowAlttitude = GenesisResourcesManager.BiomasList["MountainFootHills"].MinHeight; 
            TreeSmallFir.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Mountain"].MaxHeight;
            TreeSmallFir.DensityInForest = 0.6f;
            TreeSmallFir.DensityInForestForTypeInFamily = 0.3f;
            TreeSmallFir.AreaCells = 9;
            TreeSmallFir.Prefabs = new List<Object>();
            TreeSmallFir.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_15"));
            TreeSmallFir.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_17"));
            TreeTypesList.Add(TreeSmallFir);

            TreeDataDTO TreeMediumFir = new TreeDataDTO();
            TreeMediumFir.IdTree = 4;
            TreeMediumFir.Name = "TreeMediumFir";
            TreeMediumFir.Family = "Fir";            
            TreeMediumFir.MinGrowAlttitude = GenesisResourcesManager.BiomasList["MountainFootHills"].MinHeight;
            TreeMediumFir.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Mountain"].MaxHeight;
            TreeMediumFir.DensityInForest = 0.6f;
            TreeMediumFir.DensityInForestForTypeInFamily = 0.4f;
            TreeMediumFir.AreaCells = 9;
            TreeMediumFir.Prefabs = new List<Object>();
            TreeMediumFir.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_18"));
            TreeMediumFir.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_19"));
            TreeMediumFir.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_20"));
            TreeTypesList.Add(TreeMediumFir);

            TreeDataDTO TreeBigFir = new TreeDataDTO();
            TreeBigFir.IdTree = 5;
            TreeBigFir.Name = "TreeBigFir";
            TreeBigFir.Family = "Fir";            
            TreeBigFir.MinGrowAlttitude = GenesisResourcesManager.BiomasList["MountainFootHills"].MinHeight;
            TreeBigFir.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Mountain"].MaxHeight;
            TreeBigFir.DensityInForest = 0.6f;
            TreeBigFir.DensityInForestForTypeInFamily = 0.3f;
            TreeBigFir.AreaCells = 9;
            TreeBigFir.Prefabs = new List<Object>();
            TreeBigFir.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_08"));
            TreeBigFir.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Conifer/Conifer_Trees/Conifer_27"));
            TreeTypesList.Add(TreeBigFir);

            //Haya
            TreeDataDTO TreeSmallBeech = new TreeDataDTO();
            TreeSmallBeech.IdTree = 6;
            TreeSmallBeech.Name = "TreeSmallBeech";
            TreeSmallBeech.Family = "Beech";
            TreeSmallBeech.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MinHeight;
            TreeSmallBeech.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MaxHeight;
            TreeSmallBeech.DensityInForest = 0.4f;
            TreeSmallBeech.DensityInForestForTypeInFamily = 0.2f;
            TreeSmallBeech.AreaCells = 9;
            TreeSmallBeech.Prefabs = new List<Object>();
            TreeSmallBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_20"));
            TreeSmallBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_21"));
            TreeSmallBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_22"));
            TreeSmallBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_23"));            
            TreeTypesList.Add(TreeSmallBeech);

            TreeDataDTO TreeMediumBeech = new TreeDataDTO();
            TreeMediumBeech.IdTree = 7;
            TreeMediumBeech.Name = "TreeMediumBeech";
            TreeMediumBeech.Family = "Beech";
            TreeMediumBeech.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MinHeight;
            TreeMediumBeech.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MaxHeight;
            TreeMediumBeech.DensityInForest = 0.4f;
            TreeMediumBeech.DensityInForestForTypeInFamily = 0.4f;
            TreeMediumBeech.AreaCells = 25;
            TreeMediumBeech.Prefabs = new List<Object>();
            TreeMediumBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_15"));
            TreeMediumBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_16"));
            TreeMediumBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_17"));
            TreeMediumBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_18"));
            TreeMediumBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_19"));
            TreeMediumBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_24"));
            TreeMediumBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_25"));
            TreeMediumBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_26"));
            TreeMediumBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_27"));
            TreeMediumBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_28"));
            TreeTypesList.Add(TreeMediumBeech);

            TreeDataDTO TreeBigBeech = new TreeDataDTO();
            TreeBigBeech.IdTree = 8;
            TreeBigBeech.Name = "TreeBigBeech";
            TreeBigBeech.Family = "Beech";
            TreeBigBeech.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MinHeight;
            TreeBigBeech.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MaxHeight;
            TreeBigBeech.DensityInForest = 0.4f;
            TreeBigBeech.DensityInForestForTypeInFamily = 0.4f;
            TreeBigBeech.AreaCells = 25;
            TreeBigBeech.Prefabs = new List<Object>();
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_01"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_02"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_03"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_04"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_05"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_06"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_07"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_08"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_09"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_10"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_11"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_12"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_13"));
            TreeBigBeech.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_14"));
            TreeTypesList.Add(TreeBigBeech);

            //Roble
            TreeDataDTO TreeSmallOak = new TreeDataDTO();
            TreeSmallOak.IdTree = 10;
            TreeSmallOak.Name = "TreeSmallOak";
            TreeSmallOak.Family = "Oak";
            TreeSmallOak.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Hills"].MinHeight;
            TreeSmallOak.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MaxHeight;
            TreeSmallOak.DensityInForest = 0.4f;
            TreeSmallOak.DensityInForestForTypeInFamily = 0.4f;
            TreeSmallOak.AreaCells = 25;
            TreeSmallOak.Prefabs = new List<Object>();
            TreeSmallOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_08"));
            TreeSmallOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_09"));
            TreeSmallOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_10"));
            TreeSmallOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_11"));
            TreeSmallOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_12"));
            TreeTypesList.Add(TreeSmallOak);

            TreeDataDTO TreeMediumOak = new TreeDataDTO();
            TreeMediumOak.IdTree = 11;
            TreeMediumOak.Name = "TreeMediumOak";
            TreeMediumOak.Family = "Oak";            
            TreeMediumOak.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Hills"].MinHeight;
            TreeMediumOak.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MaxHeight;
            TreeMediumOak.DensityInForest = 0.4f;
            TreeMediumOak.DensityInForestForTypeInFamily = 0.4f;
            TreeMediumOak.AreaCells = 25;
            TreeMediumOak.Prefabs = new List<Object>();
            TreeMediumOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_01"));
            TreeMediumOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_02"));
            TreeMediumOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_03"));
            TreeMediumOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_04"));
            TreeMediumOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_13"));
            TreeTypesList.Add(TreeMediumOak);

            TreeDataDTO TreeBigOak = new TreeDataDTO();
            TreeBigOak.IdTree = 12;
            TreeBigOak.Name = "TreeBigOak";
            TreeBigOak.Family = "Oak";            
            TreeBigOak.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Hills"].MinHeight;
            TreeBigOak.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Grassland"].MaxHeight;
            TreeBigOak.DensityInForest = 0.4f;
            TreeBigOak.DensityInForestForTypeInFamily = 0.2f;
            TreeBigOak.AreaCells = 25;
            TreeBigOak.Prefabs = new List<Object>();
            TreeBigOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_05"));
            TreeBigOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_06"));
            TreeBigOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_07"));
            TreeBigOak.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Oak/Oak_Trees/Oak_14"));
            TreeTypesList.Add(TreeBigOak);

            //Encina
            TreeDataDTO TreeSmallIlex = new TreeDataDTO();
            TreeSmallIlex.IdTree = 13;
            TreeSmallIlex.Name = "TreeSmallIlex";
            TreeSmallIlex.Family = "Ilex";            
            TreeSmallIlex.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MinHeight;
            TreeSmallIlex.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Hills"].MaxHeight;
            TreeSmallIlex.DensityInForest = 0.2f;
            TreeSmallIlex.DensityInForestForTypeInFamily = 0.3f;
            TreeSmallIlex.AreaCells = 25;
            TreeSmallIlex.Prefabs = new List<Object>();
            TreeSmallIlex.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_15"));
            TreeTypesList.Add(TreeSmallIlex);

            TreeDataDTO TreeMediumIlex = new TreeDataDTO();
            TreeMediumIlex.IdTree = 14;
            TreeMediumIlex.Name = "TreeMediumIlex";
            TreeMediumIlex.Family = "Ilex";            
            TreeMediumIlex.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MinHeight;
            TreeMediumIlex.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Hills"].MaxHeight;
            TreeMediumIlex.DensityInForest = 0.2f;
            TreeMediumIlex.DensityInForestForTypeInFamily = 0.5f;
            TreeMediumIlex.AreaCells = 25;
            TreeMediumIlex.Prefabs = new List<Object>();
            TreeMediumIlex.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_15"));
            TreeTypesList.Add(TreeMediumIlex);

            TreeDataDTO TreeBigIlex = new TreeDataDTO();
            TreeBigIlex.IdTree = 15;
            TreeBigIlex.Name = "TreeBigIlex";
            TreeBigIlex.Family = "Ilex";            
            TreeBigIlex.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MinHeight;
            TreeBigIlex.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Hills"].MaxHeight;
            TreeBigIlex.DensityInForest = 0.2f;
            TreeBigIlex.DensityInForestForTypeInFamily = 0.2f;
            TreeBigIlex.AreaCells = 25;
            TreeBigIlex.Prefabs = new List<Object>();
            TreeBigIlex.Prefabs.Add(Resources.Load(@"Vegetation/Trees/Beech/Beech_Trees/Beech_15"));
            TreeTypesList.Add(TreeBigIlex);

            //Olmo
            TreeDataDTO TreeSmallElm = new TreeDataDTO();
            TreeSmallElm.IdTree = 16;
            TreeSmallElm.Name = "TreeSmallElm";
            TreeSmallElm.Family = "Elm";            
            TreeSmallElm.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MinHeight;
            TreeSmallElm.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MaxHeight;
            TreeSmallElm.DensityInForest = 0.5f;
            TreeSmallElm.DensityInForestForTypeInFamily = 0.3f;
            TreeSmallElm.AreaCells = 9;
            TreeSmallElm.Prefabs = new List<Object>();
            TreeSmallElm.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_small_1"));
            TreeSmallElm.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_small_3"));
            TreeTypesList.Add(TreeSmallElm);

            TreeDataDTO TreeMediumElm = new TreeDataDTO();
            TreeMediumElm.IdTree = 17;
            TreeMediumElm.Name = "TreeMediumElm";
            TreeMediumElm.Family = "Elm";            
            TreeMediumElm.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MinHeight;
            TreeMediumElm.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MaxHeight;
            TreeMediumElm.DensityInForest = 0.5f;
            TreeMediumElm.DensityInForestForTypeInFamily = 0.5f;
            TreeMediumElm.AreaCells = 25;
            TreeMediumElm.Prefabs = new List<Object>();
            TreeMediumElm.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_medium_1"));
            TreeMediumElm.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_medium_2"));
            TreeMediumElm.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_medium_3"));
            TreeTypesList.Add(TreeMediumElm);

            TreeDataDTO TreeBigElm = new TreeDataDTO();
            TreeBigElm.IdTree = 18;
            TreeBigElm.Name = "TreeBigElm";
            TreeBigElm.Family = "Elm";            
            TreeBigElm.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MinHeight;
            TreeBigElm.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MaxHeight;
            TreeBigElm.DensityInForest = 0.5f;
            TreeBigElm.DensityInForestForTypeInFamily = 0.2f;
            TreeBigElm.AreaCells = 25;
            TreeBigElm.Prefabs = new List<Object>();
            TreeBigElm.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_big_1"));
            TreeBigElm.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_big_2"));
            TreeBigElm.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_big_3"));
            TreeTypesList.Add(TreeBigElm);

            //Chopo - Abedul
            TreeDataDTO TreeSmallBirch = new TreeDataDTO();
            TreeSmallBirch.IdTree = 19;
            TreeSmallBirch.Name = "TreeSmallBirch";
            TreeSmallBirch.Family = "Birch";
            TreeSmallBirch.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Riverbank"].MinHeight;
            TreeSmallBirch.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MaxHeight;
            TreeSmallBirch.DensityInForest = 0.5f;
            TreeSmallBirch.DensityInForestForTypeInFamily = 0.3f;
            TreeSmallBirch.AreaCells = 9;
            TreeSmallBirch.Prefabs = new List<Object>();
            TreeSmallBirch.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_small_1"));
            TreeSmallBirch.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_small_3"));
            TreeTypesList.Add(TreeSmallBirch);

            TreeDataDTO TreeMediumBirch = new TreeDataDTO();
            TreeMediumBirch.IdTree = 20;
            TreeMediumBirch.Name = "TreeMediumBirch";
            TreeMediumBirch.Family = "Birch";
            TreeMediumBirch.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Riverbank"].MinHeight;
            TreeMediumBirch.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MaxHeight;
            TreeMediumBirch.DensityInForest = 0.5f;
            TreeMediumBirch.DensityInForestForTypeInFamily = 0.4f;
            TreeMediumBirch.AreaCells = 25;
            TreeMediumBirch.Prefabs = new List<Object>();
            TreeMediumBirch.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_medium_1"));
            TreeMediumBirch.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_medium_2"));
            TreeMediumBirch.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_medium_3"));
            TreeTypesList.Add(TreeMediumBirch);

            TreeDataDTO TreeBigBirch = new TreeDataDTO();
            TreeBigBirch.IdTree = 21;
            TreeBigBirch.Name = "TreeBigBirch";
            TreeBigBirch.Family = "Birch";
            TreeBigBirch.MinGrowAlttitude = GenesisResourcesManager.BiomasList["Riverbank"].MinHeight;
            TreeBigBirch.MaxGrowAlttitude = GenesisResourcesManager.BiomasList["Plains"].MaxHeight;
            TreeBigBirch.DensityInForest = 0.5f;
            TreeBigBirch.DensityInForestForTypeInFamily = 0.4f;
            TreeBigBirch.AreaCells = 25;
            TreeBigBirch.Prefabs = new List<Object>();
            TreeBigBirch.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_big_1"));
            TreeBigBirch.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_big_2"));
            TreeBigBirch.Prefabs.Add(Resources.Load(@"Vegetation/Trees/BirchTrees/birch_big_3"));
            TreeTypesList.Add(TreeBigBirch);

            //TEXTURES
            //LIMITE 32 TEXTURAS PARA EL HEIGHTMAP!!!!!!!!!!!!!!

            //Snow

            TextureDataDTO smoothUniformSnowTexture = new TextureDataDTO();
            smoothUniformSnowTexture.Name = "SmoothUniformSnow";
            smoothUniformSnowTexture.TextureNameToLoad = @"Terrain/Snow/SmoothUniformSnow/Snow01";
            TerrainTexturesList.Add(smoothUniformSnowTexture.Name, smoothUniformSnowTexture);
            
            //TextureDataDTO grassSnowTexture = new TextureDataDTO();
            //grassSnowTexture.Name = "GrassSnow";
            //grassSnowTexture.TextureNameToLoad = @"Terrain/Snow/GrassSnow/fe_vil_snow_02_DIF";
            //TerrainTexturesList.Add(grassSnowTexture.Name, grassSnowTexture);

            TextureDataDTO browStonesLooseSnowTexture = new TextureDataDTO();
            browStonesLooseSnowTexture.Name = "BrowStonesLooseSnow";
            browStonesLooseSnowTexture.TextureNameToLoad = @"Terrain/Snow/BrowStonesLooseSnow/Snow02";
            TerrainTexturesList.Add(browStonesLooseSnowTexture.Name, browStonesLooseSnowTexture);

            TextureDataDTO heavyUniformSnowTexture = new TextureDataDTO();
            heavyUniformSnowTexture.Name = "HeavyUniformSnow";
            heavyUniformSnowTexture.TextureNameToLoad = @"Terrain/Snow/HeavyUniformSnow/fe_vil_snow_01_DIF";
            TerrainTexturesList.Add(heavyUniformSnowTexture.Name, heavyUniformSnowTexture);

            //TextureDataDTO snowCoverTexture = new TextureDataDTO();
            //snowCoverTexture.Name = "SnowCover";
            //snowCoverTexture.TextureNameToLoad = @"Terrain/Snow/SnowCover/Snow_Cover_01";
            //TerrainTexturesList.Add(snowCoverTexture.Name, snowCoverTexture);

            //Forest
            //TextureDataDTO grassLeavesTexture = new TextureDataDTO();
            //grassLeavesTexture.Name = "GrassLeaves";
            //grassLeavesTexture.TextureNameToLoad = @"Terrain/Forest/GrassLeaves/diffuse";
            //TerrainTexturesList.Add(grassLeavesTexture.Name, grassLeavesTexture);

            //TextureDataDTO forestFloorTexture = new TextureDataDTO();
            //forestFloorTexture.Name = "ForestFloor";
            //forestFloorTexture.TextureNameToLoad = @"Terrain/Forest/ForestFloor/Forest_Floor";
            //TerrainTexturesList.Add(forestFloorTexture.Name, forestFloorTexture);

            //Road
            TextureDataDTO irregularCobbleStoneTexture = new TextureDataDTO();
            irregularCobbleStoneTexture.Name = "IrregularCobbleStone";
            irregularCobbleStoneTexture.TextureNameToLoad = @"Terrain/Road/IrregularCobbleStone/fe_vil_cobblestone_02_DIF";
            TerrainTexturesList.Add(irregularCobbleStoneTexture.Name, irregularCobbleStoneTexture);

            //Grass
            TextureDataDTO smoothTailsGrassTexture = new TextureDataDTO();
            smoothTailsGrassTexture.Name = "SmoothTailsGrass";
            smoothTailsGrassTexture.TextureNameToLoad = @"Terrain/Grass/SmoothTailsGrass/Grass_02";
            TerrainTexturesList.Add(smoothTailsGrassTexture.Name, smoothTailsGrassTexture);

            TextureDataDTO smoothTailsGrassPlusTexture = new TextureDataDTO();
            smoothTailsGrassPlusTexture.Name = "SmoothTailsGrassPlus";
            smoothTailsGrassPlusTexture.TextureNameToLoad = @"Terrain/Grass/SmoothTailsGrassPlus/Grass_01";
            TerrainTexturesList.Add(smoothTailsGrassPlusTexture.Name, smoothTailsGrassPlusTexture);

            TextureDataDTO smoothTailsGrassPlusPlusTexture = new TextureDataDTO();
            smoothTailsGrassPlusPlusTexture.Name = "SmoothTailsGrassPlusPlus";
            smoothTailsGrassPlusPlusTexture.TextureNameToLoad = @"Terrain/Grass/SmoothTailsGrassPlusPlus/Grass_03";
            TerrainTexturesList.Add(smoothTailsGrassPlusPlusTexture.Name, smoothTailsGrassPlusPlusTexture);

            TextureDataDTO drySandyGrassTexture = new TextureDataDTO();
            drySandyGrassTexture.Name = "DrySandyGrass";
            drySandyGrassTexture.TextureNameToLoad = @"Terrain/Grass/DrySandyGrass/diffuse";
            TerrainTexturesList.Add(drySandyGrassTexture.Name, drySandyGrassTexture);

            TextureDataDTO greenLeavesGrassTexture = new TextureDataDTO();
            greenLeavesGrassTexture.Name = "GreenLeavesGrass";
            greenLeavesGrassTexture.TextureNameToLoad = @"Terrain/Grass/GreenLeavesGrass/fe_vil_grass_01_DIF";
            TerrainTexturesList.Add(greenLeavesGrassTexture.Name, greenLeavesGrassTexture);

            TextureDataDTO groundDarkGreenMossGrassTexture = new TextureDataDTO();
            groundDarkGreenMossGrassTexture.Name = "GroundDarkGreenMossGrass";
            groundDarkGreenMossGrassTexture.TextureNameToLoad = @"Terrain/Grass/GroundDarkGreenMossGrass/diffuse";
            TerrainTexturesList.Add(groundDarkGreenMossGrassTexture.Name, groundDarkGreenMossGrassTexture);

            TextureDataDTO sandyDarkGreenGrassTexture = new TextureDataDTO();
            sandyDarkGreenGrassTexture.Name = "SandyDarkGreenGrass";
            sandyDarkGreenGrassTexture.TextureNameToLoad = @"Terrain/Grass/SandyDarkGreenGrass/diffuse";
            TerrainTexturesList.Add(sandyDarkGreenGrassTexture.Name, sandyDarkGreenGrassTexture);
            
            TextureDataDTO semiDryYellowGreenGrassTexture = new TextureDataDTO();
            semiDryYellowGreenGrassTexture.Name = "SemiDryYellowGreenGrass";
            semiDryYellowGreenGrassTexture.TextureNameToLoad = @"Terrain/Grass/SemiDryYellowGreenGrass/diffuse";
            TerrainTexturesList.Add(semiDryYellowGreenGrassTexture.Name, semiDryYellowGreenGrassTexture);
            
            TextureDataDTO smallStonesGrassMossGroundTexture = new TextureDataDTO();
            smallStonesGrassMossGroundTexture.Name = "SmallStonesGrassMossGround";
            smallStonesGrassMossGroundTexture.TextureNameToLoad = @"Terrain/Grass/SmallStonesGrassMossGround/diffuse";
            TerrainTexturesList.Add(smallStonesGrassMossGroundTexture.Name, smallStonesGrassMossGroundTexture);

            TextureDataDTO smallStonesGrassMossground2Texture = new TextureDataDTO();
            smallStonesGrassMossground2Texture.Name = "SmallStonesGrassMossground2";
            smallStonesGrassMossground2Texture.TextureNameToLoad = @"Terrain/Grass/SmallStonesGrassMossground2/fe_vil_grass_03_DIF";
            TerrainTexturesList.Add(smallStonesGrassMossground2Texture.Name, smallStonesGrassMossground2Texture);

            TextureDataDTO uniformMossTexture = new TextureDataDTO();
            uniformMossTexture.Name = "UniformMoss";
            uniformMossTexture.TextureNameToLoad = @"Terrain/Grass/UniformMoss/MossGrass01";
            TerrainTexturesList.Add(uniformMossTexture.Name, uniformMossTexture);

            TextureDataDTO uniformSemiDrySandyGreenGrassTexture = new TextureDataDTO();
            uniformSemiDrySandyGreenGrassTexture.Name = "UniformSemiDrySandyGreenGrass";
            uniformSemiDrySandyGreenGrassTexture.TextureNameToLoad = @"Terrain/Grass/UniformSemiDrySandyGreenGrass/Grass01";
            TerrainTexturesList.Add(uniformSemiDrySandyGreenGrassTexture.Name, uniformSemiDrySandyGreenGrassTexture);

            TextureDataDTO uniformGreenGrassTexture = new TextureDataDTO();
            uniformGreenGrassTexture.Name = "UniformGreenGrass";
            uniformGreenGrassTexture.TextureNameToLoad = @"Terrain/Grass/UniformGreenGrass/UniformGreenGrass";
            TerrainTexturesList.Add(uniformGreenGrassTexture.Name, uniformGreenGrassTexture);            

            //TextureDataDTO uniformGreenMossedGrassTexture = new TextureDataDTO();
            //uniformGreenMossedGrassTexture.Name = "UniformGreenMossedGrass";
            //uniformGreenMossedGrassTexture.TextureNameToLoad = @"Terrain/Grass/UniformGreenMossedGrass/Grass_1";
            //TerrainTexturesList.Add(uniformGreenMossedGrassTexture.Name, uniformGreenMossedGrassTexture);

            TextureDataDTO uniformGreyGrassTexture = new TextureDataDTO();
            uniformGreyGrassTexture.Name = "UniformGreyGrass";
            uniformGreyGrassTexture.TextureNameToLoad = @"Terrain/Grass/UniformGreyGrass/Grass_2";
            TerrainTexturesList.Add(uniformGreyGrassTexture.Name, uniformGreyGrassTexture);

            //Underwater
            TextureDataDTO blueDeepWaterTexture = new TextureDataDTO();
            blueDeepWaterTexture.Name = "BlueDeepWater";
            blueDeepWaterTexture.TextureNameToLoad = @"Terrain/Underwater/BlueDeepWater/BlueDeepWater";
            TerrainTexturesList.Add(blueDeepWaterTexture.Name, blueDeepWaterTexture);

            //TextureDataDTO coastSandySandUnderwaterTexture = new TextureDataDTO();
            //coastSandySandUnderwaterTexture.Name = "CoastSandySandUnderwater";
            //coastSandySandUnderwaterTexture.TextureNameToLoad = @"Terrain/Underwater/CoastSandySandUnderwater/diffuse";
            //TerrainTexturesList.Add(coastSandySandUnderwaterTexture.Name, coastSandySandUnderwaterTexture);

            //TextureDataDTO greySandUnderwaterTexture = new TextureDataDTO();
            //greySandUnderwaterTexture.Name = "GreySandUnderwater";
            //greySandUnderwaterTexture.TextureNameToLoad = @"Terrain/Underwater/GreySandUnderwater/diffuse";
            //TerrainTexturesList.Add(greySandUnderwaterTexture.Name, greySandUnderwaterTexture);

            //TextureDataDTO lowCoastSandUnderwaterTexture = new TextureDataDTO();
            //lowCoastSandUnderwaterTexture.Name = "LowCoastSandUnderwater";
            //lowCoastSandUnderwaterTexture.TextureNameToLoad = @"Terrain/Underwater/LowCoastSandUnderwater/diffuse";
            //TerrainTexturesList.Add(lowCoastSandUnderwaterTexture.Name, lowCoastSandUnderwaterTexture);

            //Clay

            //TextureDataDTO rockyClayPlusTexture = new TextureDataDTO();
            //rockyClayPlusTexture.Name = "RockyClayPlus";
            //rockyClayPlusTexture.TextureNameToLoad = @"Terrain/Clay/RockyClayPlus/diffuse";
            //TerrainTexturesList.Add(rockyClayPlusTexture.Name, rockyClayPlusTexture);
            
            TextureDataDTO smallStonesClayTexture = new TextureDataDTO();
            smallStonesClayTexture.Name = "SmallStonesClay";
            smallStonesClayTexture.TextureNameToLoad = @"Terrain/Clay/SmallStonesClay/diffuse";
            TerrainTexturesList.Add(smallStonesClayTexture.Name, smallStonesClayTexture);
            
            TextureDataDTO toughClayTexture = new TextureDataDTO();
            toughClayTexture.Name = "ToughClay";
            toughClayTexture.TextureNameToLoad = @"Terrain/Clay/ToughClay/diffuse";
            TerrainTexturesList.Add(toughClayTexture.Name, toughClayTexture);
            
            TextureDataDTO smoothGroundClayTexture = new TextureDataDTO();
            smoothGroundClayTexture.Name = "SmoothGroundClay";
            smoothGroundClayTexture.TextureNameToLoad = @"Terrain/Clay/SmoothGroundClay/EarthyRock01";
            TerrainTexturesList.Add(smoothGroundClayTexture.Name, smoothGroundClayTexture);

            //Granite

            TextureDataDTO grassRocksGraniteTexture = new TextureDataDTO();
            grassRocksGraniteTexture.Name = "GrassRocksGranite";
            grassRocksGraniteTexture.TextureNameToLoad = @"Terrain/Granite/GrassRocksGranite/GrassRocks01";
            TerrainTexturesList.Add(grassRocksGraniteTexture.Name, grassRocksGraniteTexture);

            TextureDataDTO mossGraniteTexture = new TextureDataDTO();
            mossGraniteTexture.Name = "MossGranite";
            mossGraniteTexture.TextureNameToLoad = @"Terrain/Granite/MossGranite/diffuse";
            TerrainTexturesList.Add(mossGraniteTexture.Name, mossGraniteTexture);

            TextureDataDTO darkGranitePlusTexture = new TextureDataDTO();
            darkGranitePlusTexture.Name = "DarkGranitePlus";
            darkGranitePlusTexture.TextureNameToLoad = @"Terrain/Granite/DarkGranitePlus/diffuse";
            TerrainTexturesList.Add(darkGranitePlusTexture.Name, darkGranitePlusTexture);

            //Limestone

            TextureDataDTO darkMossLimestoneTexture = new TextureDataDTO();
            darkMossLimestoneTexture.Name = "DarkMossLimestone";
            darkMossLimestoneTexture.TextureNameToLoad = @"Terrain/Limestone/DarkMossLimestone/diffuse";
            TerrainTexturesList.Add(darkMossLimestoneTexture.Name, darkMossLimestoneTexture);

            TextureDataDTO smoothLimestoneTexture = new TextureDataDTO();
            smoothLimestoneTexture.Name = "SmoothLimestone";
            smoothLimestoneTexture.TextureNameToLoad = @"Terrain/Limestone/SmoothLimestone/diffuse";
            TerrainTexturesList.Add(smoothLimestoneTexture.Name, smoothLimestoneTexture);

            TextureDataDTO darkLimestoneTexture = new TextureDataDTO();
            darkLimestoneTexture.Name = "DarkLimestone";
            darkLimestoneTexture.TextureNameToLoad = @"Terrain/Limestone/DarkLimestone/diffuse";
            TerrainTexturesList.Add(darkLimestoneTexture.Name, darkLimestoneTexture);

            TextureDataDTO rockyLimestoneTexture = new TextureDataDTO();
            rockyLimestoneTexture.Name = "RockyLimestone";
            rockyLimestoneTexture.TextureNameToLoad = @"Terrain/Limestone/RockyLimestone/diffuse";
            TerrainTexturesList.Add(rockyLimestoneTexture.Name, rockyLimestoneTexture);

            //Ground
            TextureDataDTO verySmallRocksBrownGroundTexture = new TextureDataDTO();
            verySmallRocksBrownGroundTexture.Name = "VerySmallRocksBrownGround";
            verySmallRocksBrownGroundTexture.TextureNameToLoad = @"Terrain/Ground/VerySmallRocksBrownGround/diffuse";
            TerrainTexturesList.Add(verySmallRocksBrownGroundTexture.Name, verySmallRocksBrownGroundTexture);

            TextureDataDTO mudMossTexture = new TextureDataDTO();
            mudMossTexture.Name = "MudMoss";
            mudMossTexture.TextureNameToLoad = @"Terrain/Ground/MudMoss/diffuse";
            TerrainTexturesList.Add(mudMossTexture.Name, mudMossTexture);

            //TextureDataDTO rockyMossGroundTexture = new TextureDataDTO();
            //rockyMossGroundTexture.Name = "RockyMossGround";
            //rockyMossGroundTexture.TextureNameToLoad = @"Terrain/Ground/RockyMossGround/RockyGround01";
            //TerrainTexturesList.Add(rockyMossGroundTexture.Name, rockyMossGroundTexture);

            TextureDataDTO smallRocksBrownGroundTexture = new TextureDataDTO();
            smallRocksBrownGroundTexture.Name = "SmallRocksBrownGround";
            smallRocksBrownGroundTexture.TextureNameToLoad = @"Terrain/Ground/SmallRocksBrownGround/diffuse";
            TerrainTexturesList.Add(smallRocksBrownGroundTexture.Name, smallRocksBrownGroundTexture);

            TextureDataDTO smallStonesGrassMossGround2Texture = new TextureDataDTO();
            smallStonesGrassMossGround2Texture.Name = "SmallStonesGrassMossGround2";
            smallStonesGrassMossGround2Texture.TextureNameToLoad = @"Terrain/Ground/SmallStonesGrassMossGround2/fe_vil_grass_03_DIF";
            TerrainTexturesList.Add(smallStonesGrassMossGround2Texture.Name, smallStonesGrassMossGround2Texture);

            //TextureDataDTO smallStonesGroundTexture = new TextureDataDTO();
            //smallStonesGroundTexture.Name = "SmallStonesGround";
            //smallStonesGroundTexture.TextureNameToLoad = @"Terrain/Ground/SmallStonesGround/Raw_Dirt_Disp";
            //TerrainTexturesList.Add(smallStonesGroundTexture.Name, smallStonesGroundTexture);
            
            //LIMITE 32 TEXTURAS PARA EL HEIGHTMAP!!!!!!!!!!!!!!


            //BUILDINGS

            BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.PopulationCenterN0, BuildingStatusLevels.ConstructionSite, "Hoguera en construcción", 1000,
                new Vector3(2, 0, 2), true, 4,10,50, new int[10, 10]{
                { 3, 3,3,3, 3, 3 ,3,3,3,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 3,3,3, 3, 3 ,3,3,3,3 },
            }));

            BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.PopulationCenterN0, BuildingStatusLevels.Builded, "Hoguera", 1000,
                new Vector3(2, 0, 2), true, 5, 10,50, new int[10, 10]{
                { 3, 3,3,3, 3, 3 ,3,3,3,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 4,4,4, 4, 4 ,4,4,4,3 },
                { 3, 3,3,3, 3, 3 ,3,3,3,3 },
            }));
            
            BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.HouseN1, BuildingStatusLevels.ConstructionSite, "Cabaña en construcción", 1000,
                new Vector3(2, 0, 2), true, 4,0, new int[6, 6]{
                { 3, 3, 3, 3,3,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 3, 3, 3,3,3 },
            }));

            BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.HouseN1, BuildingStatusLevels.Builded, "Cabaña", 1000,
               new Vector3(2, 0, 2), true, 4, 0, new int[6, 6]{
                { 3, 3, 3, 3,3,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 3, 3, 3,3,3 },
           }));

            BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.WoodcuttersHutN0, BuildingStatusLevels.ConstructionSite, "Cabaña del Leñador en construcción", 1000,
                new Vector3(2, 0, 2), true, 0,0, new int[6, 6]{
                { 3, 3, 3, 3,3,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 3, 3, 3,3,3 },
            }));

            BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.WoodcuttersHutN0, BuildingStatusLevels.Builded, "Cabaña del Leñador", 1000,
                new Vector3(2, 0, 2), true, 0, 0, new int[6, 6]{
                { 3, 3, 3, 3,3,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 3, 3, 3,3,3 },
            }));

            BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.StonecuttersHutN0, BuildingStatusLevels.ConstructionSite, "Cabaña del Picapedrero en construcción", 1000,
                new Vector3(2, 0, 2), true, 0, 0, new int[6, 6]{
                { 3, 3, 3, 3,3,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 3, 3, 3,3,3 },
            }));

            BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.StonecuttersHutN0, BuildingStatusLevels.Builded, "Cabaña del Picapedrero", 1000,
                new Vector3(2, 0, 2), true, 0, 0, new int[6, 6]{
                { 3, 3, 3, 3,3,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 4, 4, 4,4,3 },
                { 3, 3, 3, 3,3,3 },
            }));

            BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.AppleOrchardN0, BuildingStatusLevels.ConstructionSite, "Huerto de Manzanas en Construcción", 1000,
               new Vector3(2, 0, 2), true, 0, 0, new int[8, 8]{
                { 3, 3, 3, 3 ,3,3,3,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 3, 3, 3 ,3,3,3,3 },
           }));

            //BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.AppleOrchardN0, BuildingStatusLevels.Builded, ResourceManager.SceneManagerInstance.LanguageManagerInstance.GetString("AppleOrchard"), 1000,
            BuildingsTypesList.Add(new BuildingDTO(BuildingBasicTypes.AppleOrchardN0, BuildingStatusLevels.Builded, "Huerto de manzanas", 1000,
               new Vector3(2, 0, 2), true, 0, 0, new int[8, 8]{
                { 3, 3, 3, 3 ,3,3,3,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 4, 4, 4 ,4,4,4,3 },
                { 3, 3, 3, 3 ,3,3,3,3 },
           }));

            //UNITS
            UnitDTO Worker = new UnitDTO();
            Worker.IdUnit = (int)UnitBasicTypes.Worker;
            Worker.UnitType = UnitBasicTypes.Worker;
            Worker.Name = "Trabajador";
            Worker.IsSelectable = false;
            //Worker.PrefabToLoadName = UnitBasicTypes.Worker.ToString() + "Prefab";
            Worker.PrefabToLoadName = "Units/Villagers/Samples/male_01";
            Worker.Prefab = Resources.Load(Worker.PrefabToLoadName);
            UnitsTypesList.Add(Worker);

            UnitDTO ArmedPeasant = new UnitDTO();
            ArmedPeasant.IdUnit = (int)UnitBasicTypes.ArmedPeasantN0;
            ArmedPeasant.UnitType = UnitBasicTypes.ArmedPeasantN0;
            ArmedPeasant.Name = "Campesino armado";
            ArmedPeasant.IsSelectable = true;
            //Worker.PrefabToLoadName = UnitBasicTypes.Worker.ToString() + "Prefab";
            ArmedPeasant.PrefabToLoadName = "Units/Villagers/Samples/male_01";
            ArmedPeasant.Prefab = Resources.Load(ArmedPeasant.PrefabToLoadName);
            UnitsTypesList.Add(ArmedPeasant);

            //LOADS

            if (TerrainTexturesList != null)
            {
                int count = 0;

                foreach (TextureDataDTO texture in TerrainTexturesList.Values)
                {
                    texture.Id = count;
                    if (texture.Texture == null)
                        texture.Texture = (Texture2D)Resources.Load(texture.TextureNameToLoad);
                    count++;
                }
            }
        }
    }
} 