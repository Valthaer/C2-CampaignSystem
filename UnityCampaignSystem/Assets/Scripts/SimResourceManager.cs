using System;
using System.Collections.Generic;

namespace LonelyCastle.A4.Common.WorldEngine
{
    public static class SRM //SimResourceManager
    {
        //DEBUG DATA
        public enum LogLevel
        {
            Verbose,
            Debug,
            Info,
            Warning,
            Error
        }

        public enum GenderEnum
        {
            Male = 0,
            Female = 1,
            Generic = 2
        }

        public enum NavalBattleInitPositionEnum
        {
            North,
            East,
            South,
            West,
            Center,
            Harbour,
            ParallelToSouthSameRoute,
            ParallelToSouthOppositeRoute,
            PersecutingCenter
        }

        public enum NavalBattleFormationEnum
        {
            Line,
            Column,
            Echelon,
            Convoy,
            Diamond,
            Scattered
        }

        public enum ShipSide
        {
            ProaPorBabor,
            ProaPorEstribor,
            AmuraDeBabor, //Port bow
            TravesDeBabor, //Port beam
            AletaDeBabor, //Port quarter
            Popa,
            AletaDeEstribor,
            TravesDeEstribor,
            AmuraDeEstribor,
            AlcazarDePopa, //Quarterdeck 
            Cubierta, //Main deck
            CastilloDeProa, //Forecastle
            BajoLineaFlotacionPorProaABabor,
            BajoLineaFlotacionPorProaAEstribor,
            BajoLineaFlotacionPorBabor,
            BajoLineaFlotacionPorEstribor,
            BajoLineaFlotacionPorPopaABabor,
            BajoLineaFlotacionPorPopaAEstribor
        }

        public enum Side
        {
            Port, //babor - Port
            Starboard, //estribor - starboard
            Bow, //proa - bow
            Stern //popa - stern
        }

        public enum PlankingDamage
        {
            NoDamage,
            LightDamage,
            SeriousDamage,
            HeavilyDamaged,
            Destroyed
        }

        // Type of sail. This determines wind effects.
        public enum SailType
        {
            Staysail, //Stays,Foques - Staysail
            SquareSail, //Velas cuadradas - Squaresail
            Gaff //Cangrejas, Latinas - Gaff
        }

        public enum SailDamage
        {
            None,
            Light,
            Moderate,
            Heavy
        }

        public enum SailHeraldry
        {
            None,
            Nation,
            Faction,
            Fleet
        }

        public enum AmmunitionType
        {
            Round = 0, //Redonda, rasa
            Shrapnel = 1, //Metralla (Canister or Grape Shot is also used)
            Chain = 2,   //Cadena, Palanqueta o enramada
            DoubleRound = 3 //Bala doble
        }

        //GAME DATA
        public enum Job
        {
            Farmer = 0,
            Worker = 1,
            Artist = 2,
            Merchant = 3,
            Functionary = 4,
            Soldier = 5,
            TavernKeeper = 6,
            Governor = 7,
            Harbourmaster = 8, //Jefe del Puerto
            Captain = 9,
            Admiral = 10,
            Sailor = 11,
            Gunner = 12,
            Helmsman = 13, //Timonel
            Mayor = 14, //Alcalde,
            BusinessOwner = 15,
            Unemployed = 16,
            Bosun = 17,
            Carpenter = 18,
            GunSmith = 19,
            Surgeon = 20,
            ChiefGunner = 21,
            ShipSteward = 22,
            ShipSecondOfficer = 23,
            Officer = 24
        }

        public enum FleetStateTypes
        {
            ProvisioningInHarbour = 0,
            Patrolling = 1,
            GoingHomeport = 2,
            Chasing = 3,
            RunningAway = 4,
            Traveling = 5
        }

        public enum ShipCrewType
        {
            Warship = 0,
            Corsair = 1,
            Pirate = 2,
            Trader = 3,
            Others = 4
        }

        public enum PersonAptitude
        {
            Military = 0,
            Corsair = 1,
            Pirate = 2,
            Trader = 3,
            Worker = 4,
            Craftsman = 5,
            Scholar = 6,
            Others = 7
        }

        public enum Goods
        {
            Food,
            Utilities, //As tools, or anything used in daylife
            Art,
            Materials
        }

        public enum QualityEnum
        {
            Low,
            Medium,
            High
        }

        public enum NationAttitudeBaseEnum
        {
            Defensive = 0,
            Neutral = 1,
            Aggresive = 2
        }

        public enum NationMilitaryPowerEnum
        {
            None = 1,
            Insignificant = 2,
            Insufficient = 3,
            Little = 4,
            Average = 5,
            Sufficient = 6,
            Great = 7,
            Impressive = 8,
            Overwhelming = 9,
            Dreadful = 10
        }

        public enum NationTerritoryEnum
        {
            None = 1,
            Insignificant = 2,
            Sparse = 3,
            Little = 4,
            Average = 5,
            Quite = 6,
            Big = 7,
            VeryBig = 8,
            Enormous = 9,
            Huge = 10
        }

        public enum NationWealthEnum
        {
            None = 1,
            Insignificant = 2,
            Poor = 3,
            Limited = 4,
            Average = 5,
            Sufficient = 6,
            Great = 7,
            High = 8,
            Rich = 9,
            Huge = 10
        }

        public enum RarityEnum
        {
            VeryRare = 0, //4% 
            Rare = 1, //7%
            Sparse = 2, //11%
            Uncommon = 3, // 18%
            Common = 4, //26%
            VeryCommon = 5 //34%
        }

        public enum Frequency
        {
            None,
            Hourly,
            Daily,
            Monthly,
            Yearly,
            Delta
        }

        public enum ShipType
        {
            Brig = 0,
            TraderBrig = 1,
            Snow = 2,
            Cutter = 3,
            Frigate = 4,
            HeavyFrigate = 5,
            Galleon = 6,
            HeavyGalleon = 7,
            Corvette = 8,
            FourthRate = 9,
            ThirdRate = 10,
            SecondRate = 11,
            FirstRate = 12,
            Indiaman = 13,
            Fluyt = 14,
            Schooner = 15,
            Ketch = 16,
            Caravel = 17
        }

        public enum ShipSize
        {
            VerySmall = 0, //Cutter, Schooner..
            Small = 1, //Brig
            Medium = 2, //frigate
            Big = 3, //Galleon, Indiaman, 3st and 4st classes
            Enormous = 4 // 1st and 2st classes,Heavy Galleon, Wasa, Couronne...
        }

        public enum ShipRolTag
        {
            Trader = 0, //very big cargo space, few cannons
            ArmedTrader = 1, //big cargo space and some cannons
            Escort = 2, //armed ships, between fast and armed
            Piratery = 3, //fast armed ships, with some cargo space
            Warship = 4, //very armed ships
            Guardcoast = 5, //fast and armed ships
            Smuggler = 6, //fast and cargo ships, few cannons
            Corsair = 7, //fast armed ships, with some cargo space
            TroopsTransport = 8, //big cargo space and some cannons
            FishingBoat = 9, //no or few cannons, big cargo space, slow
            Frigate = 10, // Its a rol by its own, to Multirole ships
            Messenger = 11 //Small and fast ships to carry messages and passengers, can be schooner, brigs, corvettes and small frigates
        }

        public enum FleetType
        {
            MerchantFleet = 0,
            WarFleet = 1,
            GuardCoastFleet = 2,
            CorsairsFleet = 3,
            FishingFleet = 4,
            TreasureFleet = 5,
            ArmyConvoyFleet = 6,
            PirateFleet = 7,
            SmugglerFleet = 8,
            MessengerFleet = 9,
            MultiroleFleet = 10 //Frigate fleet
        }

        public enum ShipRolInFleet
        {
            Flagship = 0,
            Escort = 1,
            Escorted = 2,
            Others = 3
        }

        public enum NewsType
        {
            NavalSightings, //Avistamientos
            General,
            Assignment, //Encargo
            Opportunities,
            Threats,
            MarketPrices //Precios en cada ciudad
        }

        public enum CrewNumbers
        {
            Depleted = 0,
            Down = 1,
            Medium = 2,
            High = 3,
            Full = 4
        }

        public enum GovermentTypesEnum
        {
            Monarchy = 0,
            PirateRepublic = 1,
            Empire = 2,
            Republic = 3,
            HighCouncil = 4
        }

        public enum TownLevel
        {
            Farm = 0, //Granja
            Hamlet = 1, //Aldea
            Village = 2, //Pueblo
            Villa = 3, //Villa
            SmallTown = 4, //Ciudad pequeña
            Town = 5, //Ciudad
            City = 6, //Gran ciudad
            Capital = 7, //Capital de nación
            Random = 8
        }

        public enum TownPurpose
        {
            //Must be visible in map
            NaturalResources = 0,
            MiningSettlement = 1,
            MilitaryOrStrategicValue = 2,
            CrossRoads = 3,
            RiverCross = 4,
            Farming = 5,
            Farmlands = 6,
            Nothing = 7
        }

        public enum TownFeaturesEnum
        {
            DeepHarbour = 0,
            Cathedral = 1,
            University = 2,
            ShallowHarbour = 3,
            Rivercross = 4,
            Defendible = 5,
            MerchantCity = 6,
            TavernsCity = 7,
            MiningCity = 8,
            GarrisonCity = 9,
            ShipyardsCity = 10,
            WarFleetBaseCity = 11,
            FortifiedCity = 12,
            HeavilyFortifiedCity = 13
        }

        public enum IndustriesEnum
        {
            WeaponsFactory = 0,
            Brewing = 1,
            WineCellar = 2,
            Vineyards = 3,
            CerealFields = 4,
            Bakery = 5,
            GoldMine = 6,
            SilverMine = 7,
            IronMine = 8,
            Farm = 9,
            Hopfields = 10,
            Sawmill = 11,
            FishingLure = 12,
            Quarry = 13,
            OilPlantation = 14,
            HuntingPost = 15,
            Carpentry = 16,
            Foundry = 17,
            VegetableFields = 18
        }

        public enum ProductsEnum
        {
            Wine = 0,
            Beer = 1,
            Gold = 2,
            Silver = 3,
            RoundCannonball = 4,
            Vegetables = 5,
            WoodPlank = 6,
            Weapons = 7,
            Steel = 8,
            Bread = 9,
            Grapes = 10,
            Hop = 11,
            GoldOre = 12,
            SilverOre = 13,
            Grain = 14,
            Wood = 15,
            Fish = 16,
            Stone = 17,
            Meat = 18,
            IronOre = 19,
            Bushmeat = 20, //Carne de caza
            Oil = 21,
            Candles = 22,
            c4pdCannon = 23,
            c6pdCannon = 24,
            c8pdCannon = 25,
            c12pdCannon = 26,
            c18pdCannon = 27,
            c24pdCannon = 28,
            c36pdCannon = 29,
        }

        public enum CannonCaliber
        {
            c4pdCannon = 0,
            c6pdCannon = 1,
            c8pdCannon = 2,
            c12pdCannon = 3,
            c18pdCannon = 4,
            c24pdCannon = 5,
            c36pdCannon = 6
        }

        public enum CannonBaseCaliberPenetration
        {
            c4pd = 90,
            c6pd = 94,    //-5  Decrease by each 25 meters
            c8pd = 96,    //-5
            c12pd = 100,  //-4
            c18pd = 106,  //-4
            c24pd = 112,  //-3
            c36pd = 124   //-3
        }

        public enum DefensiveStructuresEnum
        {
            Palisade = 0,
            CoastalBattery = 1
        }

        public enum MethodToPerformByDelegate
        {
            PassTime = 1,
            FleetArriveAtNode = 2,
            FleetAwaitToDate = 3
        }

        //DeltaTime is in DAYS. Its the days that pass in one turn in Simulator. The minimum time for any action (repair, sailing to another node, etc) 
        public static uint DeltaTime { get { return 3; } }

        public static int InitFleetsAbundance { get { return 15; } } //% over 100 to get a fleet

        public static uint ScheduleMaxMonths { get { return 24; } }
        public static uint MonthsInYear { get { return 8; } }
        public static uint DaysInMonth { get { return 30; } }
        public static uint HoursInDay { get { return 24; } }
        public static uint NewsExpirationTimeInMonths { get { return 6; } }

        private static DataLoader dataLoaderInstance;
        public static DataLoader DataLoaderInstance
        {
            get
            {
                if (dataLoaderInstance != null)
                {
                    return dataLoaderInstance;
                }
                dataLoaderInstance = new DataLoader();
                return dataLoaderInstance;
            }
        }

        private static WorldEngineLogsLoader worldEngineLogsLoaderInstance;
        public static WorldEngineLogsLoader WorldEngineLogsLoaderInstance
        {
            get
            {
                if (worldEngineLogsLoaderInstance != null)
                {
                    return worldEngineLogsLoaderInstance;
                }
                worldEngineLogsLoaderInstance = new WorldEngineLogsLoader();
                return worldEngineLogsLoaderInstance;
            }
        }

        private static LanguageResourcesLoader languageResourcesLoaderInstance;
        public static LanguageResourcesLoader LanguageResourcesLoaderInstance
        {
            get
            {
                if (languageResourcesLoaderInstance != null)
                {
                    return languageResourcesLoaderInstance;
                }
                languageResourcesLoaderInstance = new LanguageResourcesLoader();
                return languageResourcesLoaderInstance;
            }
        }

        //Is not dynamic yes, but add a language support is hard, so, normally will implie a game version release, no?
        public enum LanguagesEnum
        {
            English = 1,
            Spanish = 2
        }

        public static int IdLanguageSelected {get;set;}

        public static string TranslateKey(string key)
        {
            return LanguageManager.GetKeyValue(key, (SRM.LanguagesEnum)IdLanguageSelected);
        }

        public static string TranslateText(string text)
        {
            return LanguageManager.TranslateText(text, (SRM.LanguagesEnum)IdLanguageSelected);
        }

        public static Simulator SimulatorInstance { get; set; }
        public static SaveGameManager SaveGameManagerInstance { get; set; }
        public static SimProceduralGenerator SimProceduralGeneratorInstance { get; set; }
        public static Printer Logger { get; set; }

        private static NameGeneratorHelper nameGeneratorHelperInstance;
        public static NameGeneratorHelper NameGeneratorHelperInstance
        {
            get
            {
                if (nameGeneratorHelperInstance == null)
                {
                    nameGeneratorHelperInstance = new NameGeneratorHelper();
                }
                return nameGeneratorHelperInstance;
            }
        }

        //Load from Data types
        public static List<DataShipType> DataShipTypes { get; set; }
        public static List<DataFaction> DataFactions { get; set; }
        public static List<DataNation> DataNations { get; set; }
        public static List<DataTile> DataTiles { get; set; }
        public static List<DataCulture> DataCultures { get; set; }
        public static List<DataNaturalResource> DataNaturalResources { get; set; }
        public static List<DataTownFeature> DataTownFeatures { get; set; }
        public static List<DataDefensiveStructure> DataDefensiveStructures { get; set; }
        
        //Load from here, this can be migrated to Data classes in future if necessary
        private static List<SimIndustryType> industriesTypes;
        public static List<SimIndustryType> IndustriesTypes
        {
            get
            {
                if (industriesTypes == null)
                {
                    industriesTypes = LoadIndustriesTypes(industriesTypes);                    
                }
                return industriesTypes;
            }
        }        
              
        private static List<SimProduct> products;
        public static List<SimProduct> Products
        {
            get
            {
                if (products == null)
                {
                    products = LoadProducts(products);
                }
                return products;
            }
        }

        private static List<SimCannon> cannonTypes;
        public static List<SimCannon> CannonTypes
        {
            get
            {
                if (cannonTypes == null)
                {
                    cannonTypes = LoadCannonTypes(cannonTypes);
                }
                return cannonTypes;
            }
        }
        
        //Load Methods
        private static List<SimIndustryType> LoadIndustriesTypes(List<SimIndustryType> industries)
        {
            industries = new List<SimIndustryType>();
            //industries.Add(new SimIndustryType("Books Printing", "Books", "Paper", Frequency.Monthly));
            industries.Add(new SimIndustryType("%Weapons_Factory", new List<SRM.ProductsEnum>() { SRM.ProductsEnum.Weapons },null, null,
                new List<SRM.ProductsEnum>() { SRM.ProductsEnum.Steel, SRM.ProductsEnum.WoodPlank }, Frequency.Monthly,IndustriesEnum.WeaponsFactory,30,SRM.Job.Worker)); 
            industries.Add(new SimIndustryType("%Brewing",new List<SRM.ProductsEnum>() { SRM.ProductsEnum.Beer } ,
                 new List<SRM.ProductsEnum>() { SRM.ProductsEnum.Hop }, null,null, Frequency.Monthly, IndustriesEnum.Brewing, 30, SRM.Job.Worker)); //Hop-> Lúpulo
            industries.Add(new SimIndustryType("%Wine_cellar", new List<SRM.ProductsEnum>() { SRM.ProductsEnum.Wine } ,
                new List<SRM.ProductsEnum>() { SRM.ProductsEnum.Grapes }, null, null, Frequency.Monthly, IndustriesEnum.WineCellar, 30, SRM.Job.Worker)); //Wine cellars -> Bodegas de vino   
            industries.Add(new SimIndustryType("%Vineyards", null,null,SRM.ProductsEnum.Grapes,null, Frequency.Monthly, IndustriesEnum.Vineyards, 30, SRM.Job.Farmer));
            industries.Add(new SimIndustryType("%Cereal_fields", null, null, SRM.ProductsEnum.Grain, null, Frequency.Monthly, IndustriesEnum.CerealFields, 30, SRM.Job.Farmer));
            industries.Add(new SimIndustryType("%Bakery", new List<SRM.ProductsEnum>() { SRM.ProductsEnum.Bread },
                new List<SRM.ProductsEnum>() { SRM.ProductsEnum.Grain }, null, null, Frequency.Monthly, IndustriesEnum.Bakery, 8, SRM.Job.Worker));
            industries.Add(new SimIndustryType("%Gold_mine", null,null,SRM.ProductsEnum.GoldOre, null, Frequency.Monthly, IndustriesEnum.GoldMine, 50, SRM.Job.Worker));
            industries.Add(new SimIndustryType("%Silver_mine", null, null, SRM.ProductsEnum.SilverOre, null, Frequency.Monthly, IndustriesEnum.SilverMine, 50, SRM.Job.Worker));
            industries.Add(new SimIndustryType("%Iron_mine", null, null, SRM.ProductsEnum.IronOre, null, Frequency.Monthly, IndustriesEnum.IronMine, 50, SRM.Job.Worker));
            industries.Add(new SimIndustryType("%Farm", null, null, SRM.ProductsEnum.Meat, null, Frequency.Monthly, IndustriesEnum.Farm, 30, SRM.Job.Farmer));
            industries.Add(new SimIndustryType("%Hop_fields", null, null, SRM.ProductsEnum.Hop, null, Frequency.Monthly, IndustriesEnum.Hopfields, 30, SRM.Job.Farmer));
            industries.Add(new SimIndustryType("%Sawmill", null, null, SRM.ProductsEnum.Wood, null, Frequency.Monthly, IndustriesEnum.Sawmill, 50, SRM.Job.Worker));
            industries.Add(new SimIndustryType("%Fishing_lure", null, null, SRM.ProductsEnum.Fish, null, Frequency.Monthly, IndustriesEnum.FishingLure, 30, SRM.Job.Worker));//lonja de pescado
            industries.Add(new SimIndustryType("%Quarry", null, null, SRM.ProductsEnum.Stone, null, Frequency.Monthly, IndustriesEnum.Quarry, 50, SRM.Job.Worker));//cantera
            industries.Add(new SimIndustryType("%Oil_plantation", null, null, SRM.ProductsEnum.Oil, null, Frequency.Monthly, IndustriesEnum.OilPlantation, 30, SRM.Job.Farmer));// Plantación de aceite: olivares + Almazara, donde se produce aceite
            industries.Add(new SimIndustryType("%Hunting_post", null, null, SRM.ProductsEnum.Bushmeat, null, Frequency.Monthly, IndustriesEnum.HuntingPost, 10, SRM.Job.Farmer));//Coto de caza
            industries.Add(new SimIndustryType("%Carpentry", new List<SRM.ProductsEnum>() { SRM.ProductsEnum.WoodPlank },
                new List<SRM.ProductsEnum>() { SRM.ProductsEnum.Wood }, null, null, Frequency.Monthly, IndustriesEnum.Carpentry, 15, SRM.Job.Worker));
            industries.Add(new SimIndustryType("%Foundry", new List<SRM.ProductsEnum>() { SRM.ProductsEnum.Steel,
                SRM.ProductsEnum.Gold, SRM.ProductsEnum.Silver },
                new List<SRM.ProductsEnum>() { SRM.ProductsEnum.IronOre,
                SRM.ProductsEnum.GoldOre, SRM.ProductsEnum.SilverOre }, null, null, Frequency.Monthly, IndustriesEnum.Foundry, 20, SRM.Job.Worker));
            industries.Add(new SimIndustryType("%Vegetables_fields", null, null, SRM.ProductsEnum.Vegetables, null, Frequency.Monthly, IndustriesEnum.VegetableFields, 40, SRM.Job.Farmer));
            return industries;
        }
                        
        private static List<SimProduct> LoadProducts(List<SimProduct> products)
        {
            //The cost of the most basic and cheaper product must be the base. No decimals allowed for money.             
            //1 unit of food is the food for a adult in a whole month

            products = new List<SimProduct>();
            products.Add(new SimProduct("Round shot",SRM.ProductsEnum.RoundCannonball, SRM.QualityEnum.Low,Goods.Materials, 10,117,50));
            products.Add(new SimProduct("Wood Planks", SRM.ProductsEnum.WoodPlank, SRM.QualityEnum.Medium, Goods.Materials, 1, 1, 10));
            products.Add(new SimProduct("Wine", SRM.ProductsEnum.Wine, SRM.QualityEnum.High, Goods.Food, 1, 0.75f, 5));
            products.Add(new SimProduct("Beer", SRM.ProductsEnum.Beer, SRM.QualityEnum.Medium, Goods.Food, 1, 0.5f, 3));
            products.Add(new SimProduct("Steel", SRM.ProductsEnum.Steel, SRM.QualityEnum.High, Goods.Materials, 1, 2, 20));
            products.Add(new SimProduct("Weapons", SRM.ProductsEnum.Weapons, SRM.QualityEnum.High, Goods.Materials, 1, 1.5, 500));
            products.Add(new SimProduct("Gold", SRM.ProductsEnum.Gold, SRM.QualityEnum.High, Goods.Materials, 1, 5, 36925));
            products.Add(new SimProduct("Silver", SRM.ProductsEnum.Silver, SRM.QualityEnum.High, Goods.Materials, 1, 1.5f, 570));
            products.Add(new SimProduct("Bread", SRM.ProductsEnum.Bread, SRM.QualityEnum.Medium, Goods.Food, 1, 0.25f, 5));
            products.Add(new SimProduct("Candles", SRM.ProductsEnum.Candles, SRM.QualityEnum.Medium, Goods.Utilities, 1, 0.05f, 10));
            products.Add(new SimProduct("4pd Cannon", SRM.ProductsEnum.c4pdCannon, SRM.QualityEnum.High, Goods.Materials, 1, 1800, 1950));
            products.Add(new SimProduct("6pd Cannon", SRM.ProductsEnum.c6pdCannon, SRM.QualityEnum.High, Goods.Materials, 1, 2500, 2600)); 
            products.Add(new SimProduct("8pd Cannon", SRM.ProductsEnum.c8pdCannon, SRM.QualityEnum.High, Goods.Materials, 1, 3000, 3750)); 
            products.Add(new SimProduct("12pd Cannon", SRM.ProductsEnum.c12pdCannon, SRM.QualityEnum.High, Goods.Materials, 1, 3400, 4250)); 
            products.Add(new SimProduct("18pd Cannon", SRM.ProductsEnum.c18pdCannon, SRM.QualityEnum.High, Goods.Materials, 1, 4000, 4800)); 
            products.Add(new SimProduct("24pd Cannon", SRM.ProductsEnum.c24pdCannon, SRM.QualityEnum.High, Goods.Materials, 1, 4760, 5199)); 
            products.Add(new SimProduct("36pd Cannon", SRM.ProductsEnum.c36pdCannon, SRM.QualityEnum.High, Goods.Materials, 1, 6000, 6950)); 
            return products;
        }

        private static List<SimCannon> LoadCannonTypes(List<SimCannon> cannons)
        {
            //Alcances (% sobre el máximo)
            //Palanquetas y metralla max 400m (40%)
            //Bala rasa max 1000m (en realidad 3000m, pero efectivo, 1000m) (100%)
            //Doble bala max 200m (20%)

            /*
            Cannons historical data(from wikipedia: https://es.wikipedia.org/wiki/Ca%C3%B1%C3%B3n_de_a_36_libras)
            Type        Effective Range(meters)        Max range(meters)      Weight(with carriage)       Crew(1 jefe artillero + X artilleros + 1 chico de la pólvora)
            6 pounds
            8 pounds    1000m                           3000m                   975kg                       5 - 7
            12 pounds   1000m                           3000m                   1470kg                      8 - 12
            18 pounds   1000m                           3100m                   2060kg                      9 - 12
            24 pounds   1000m                           3300m                   2500kg                      12
            36 pounds   1000m                           3700m                   3200kg                      14
            */
                        
            //600m in game = 1200m in real live. Considering only effective range, not maximum range

            //http://www.navalactionwiki.com/index.php?title=Weapons
            cannons = new List<SimCannon>();
            cannons.Add(new SimCannon("4pd Cannon", SRM.CannonCaliber.c4pdCannon, 1, 1800,   1950,  425, 4,  30, 3,  35)); 
            cannons.Add(new SimCannon("6pd Cannon", SRM.CannonCaliber.c6pdCannon, 1, 2500,   2600,  450, 6,  32, 4,  40)); 
            cannons.Add(new SimCannon("8pd Cannon", SRM.CannonCaliber.c8pdCannon, 1, 3000,   3750,  475, 8,  34, 5,  45));
            //https://es.wikipedia.org/wiki/Ca%C3%B1%C3%B3n_de_a_8_libras
            cannons.Add(new SimCannon("12pd Cannon", SRM.CannonCaliber.c12pdCannon, 1, 3400, 4250,  500, 12, 36, 8,  51));
            //https://es.wikipedia.org/wiki/Ca%C3%B1%C3%B3n_de_a_12_libras
            cannons.Add(new SimCannon("18pd Cannon", SRM.CannonCaliber.c18pdCannon, 1, 4000, 4800,  525, 18, 38, 9,  55));
            //https://es.wikipedia.org/wiki/Ca%C3%B1%C3%B3n_de_a_18_libras
            cannons.Add(new SimCannon("24pd Cannon", SRM.CannonCaliber.c24pdCannon, 1, 4760, 5199,  550, 24, 40, 12, 58));
            //https://es.wikipedia.org/wiki/Ca%C3%B1%C3%B3n_de_a_24_libras
            cannons.Add(new SimCannon("36pd Cannon", SRM.CannonCaliber.c36pdCannon, 1, 6000, 6950,  600, 36, 42, 14, 61));
            //https://es.wikipedia.org/wiki/Ca%C3%B1%C3%B3n_de_a_36_libras
            return cannons;
        }
    }
}
