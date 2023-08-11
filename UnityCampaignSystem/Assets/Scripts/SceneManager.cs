#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Analytics;

namespace LonelyCastle.A2
{    
    public class SceneManager : MonoBehaviour
    {
        public LogicLoop Loop { get; set; }
        public MainFactory Factory { get; set; }
        public GameBaseConfig CurrentGameBaseConfig {get;set;}
        public Map Map { get; set; }
        public Terrain UnityTerrain { get; set; }
        public Player CurrentPlayer { get; set; }
        public BuildingManager BuildingManagerInstance { get; set; }
        public NameGeneratorHelper NameGeneratorHelperInstance { get; set; }
        public ForestManager ForestManagerInstance { get; set; }
        public TerrainTextureManager TerrainTextureManagerInstance { get; set; }

        public LanguageManager LanguageManagerInstance { get; set; }
        public LonelySkyManager Sky { get; set; }
        public SaveGameManager SaveGameManagerInstance { get; set; }

        public Camera CameraInstance
        {
            get
            {
                return GameObject.Find("CamaraPrefab").GetComponent<Camera>();
            }
        }
        public UIButtonFunctionManager UIButtonFunctionManagerInstance
        {
            get
            {
                return GameObject.Find("BaseCanvasPrefab").GetComponent<UIButtonFunctionManager>();
            }
        }
        public GameObject BuildingMenu
        {
            get
            {
                return GameObject.Find("BuildingMenu"); 
            }
        }

        public bool wasPaused;
        public bool generateTerrain;

        void Awake()
        {
            // NOTE: NEVER INSTANCIATE A MONOBEHAVIOR WITH NEW()
            // CAUSES RANDOM ERRORS   
            Loop = new LogicLoop();
            Loop.Initialize();
            Factory = new MainFactory();
            CurrentPlayer = new Player();
            BuildingManagerInstance = new BuildingManager();
            ForestManagerInstance = new ForestManager(Loop.state.Random);
            TerrainTextureManagerInstance = new TerrainTextureManager(Loop.state.Random);
            SaveGameManagerInstance = new SaveGameManager();
            LanguageManagerInstance = new LanguageManager();
            NameGeneratorHelperInstance = new NameGeneratorHelper();
            var consoleResource = Resources.Load("Console");
            if (consoleResource != null)
            {
                Instantiate(consoleResource);
                new ConsoleInterface().Initialize();
            }
            LoadWorld();
            
            //Unity Analytics trigger
            //Analytics.CustomEvent("SceneManager awaking", new Dictionary<string, object>
            //  {
            //    { "factions", Loop.state.Factions }
            //  });
        }

        void Start()
        {
#if UNITY_EDITOR
            EditorApplication.playmodeStateChanged = OnPlayModeChanged;
#endif
            LoadConfiguration();
            wasPaused = false;
            UIButtonFunctionManagerInstance.CreateUIButtons();
        }

        void Update()
        {
            Loop.Update();
        }

        public void LoadConfiguration()
        {
            //Here load config parameters
        }

        public void LoadWorld()
        {
            //This is only for test. Uncomment for test. In standard gameplay, TerrainMap must be saved and here, we will load a New or Saved Game
            if (generateTerrain)
            {
                // This Map variable is bad, it shadows the Map class name
                // and it should be tied anyway to the state
                TerrainGenerator.CreateWorld();                
            }            
            else
            {
                SaveGameManagerInstance.LoadGame("prueba18");
            }
            Sky = new LonelySkyManager();
            Sky.Setup(CameraInstance);
        }

        public void OnPlayModeChanged()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPaused && ! wasPaused)
            {
                OnPause();
                wasPaused = true;
            }
            else if (EditorApplication.isPlaying && wasPaused)
            {
                OnUnpause();
                wasPaused = false;
            }
            else if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
            {
                OnStop();
            }
#endif
        }

        public void OnPause()
        {
            ThreadManager.PauseAllThreads();
            wasPaused = true;
        }

        public void OnUnpause()
        {
            ThreadManager.UnpauseAllThreads();
            wasPaused = false;
        }

        public void OnStop()
        {
            ThreadManager.StopAllThreads();
        }
    }
}
