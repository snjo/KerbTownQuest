using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KerbTownQuest.Inventory;
using KerbTownQuest.Quest;

namespace KerbTownQuest
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class KerbTownQuestLogic : MonoBehaviour
    {
        public static string folderName = "FShousingProgram/";
        public static string modulePath = "GameData/" + folderName;
        public static string configPath = modulePath + "Configs/";
        private KTKerbalRoster _kerbalRoster = new KTKerbalRoster();
        private ItemLibrary _itemLibrary = new ItemLibrary();
        private QuestLog _questLog = new QuestLog();
        private KTKerbal _activeKerbal;
        public HotBar hotBar = new HotBar();
        public InventoryGUI inventoryGUI = new InventoryGUI();
        private Vessel activeVessel;
        private bool activeVesselIsKerbal;
        private string kerbalName;

        public static KerbTownQuestLogic Instance = null;

        

        public static KTKerbalRoster kerbalRoster
        {
            get { return Instance._kerbalRoster; }
        }
        public static ItemLibrary itemLibrary
        {
            get { return Instance._itemLibrary; }
        }
        public static QuestLog questLog
        {
            get { return Instance._questLog; }
        }
        public static KTKerbal activeKerbal
        {
            get { return Instance._activeKerbal; }
            set { Instance._activeKerbal = value; }
        }

        public void OnSave(Game data)
        {
            Debug.Log("KTQuestLogic: OnSave");
            //itemLibrary.OnSave(); // only save for testing purposes. The library should be a fixed file
            kerbalRoster.Save();
        }

        public void OnLoad(Game data)
        {
            Debug.Log("KTQuestLogic: OnLoad");
            itemLibrary.Load();
            kerbalRoster.Load();
        }

        public void OnDestroy()
        {
            //Debug.Log("KTQL: OnDestroy");
            GameEvents.onGameStateSaved.Remove(OnSave);
            GameEvents.onGameStateCreated.Remove(OnLoad);
        }

        public string savePath
        {
            get
            {
                return string.Format(KSPUtil.ApplicationRootPath + "saves/" + HighLogic.SaveFolder + "/");
            }
        }

        private bool isActiveVesselKerbal()
        {
            Part firstPart = activeVessel.parts[0];
            if (firstPart != null)
            {
                if (firstPart.GetComponent<KerbalEVA>() != null)
                {
                    kerbalName = activeVessel.vesselName;
                    return true;
                }
            }
            return false;
        }        

        private void updateActiveVessel()
        {
            if (activeVessel != FlightGlobals.ActiveVessel)
            {
                activeVessel = FlightGlobals.ActiveVessel;
                activeVesselIsKerbal = isActiveVesselKerbal();
                if (activeVesselIsKerbal)
                {
                    activeKerbal = kerbalRoster.findKerbalByName(kerbalName);
                    if (activeKerbal == null)
                    {
                        activeKerbal = kerbalRoster.AddbyName(kerbalName);

                        // test:
                        activeKerbal.backpack.AddItem(itemLibrary.items["Money"]);
                        //for (int i = 0; i < itemLibrary.items.Count; i++)
                        //{
                        //    activeKerbal.backpack.AddItem(itemLibrary.items.ElementAt(i).Value);
                        //}


                        activeKerbal.transform = activeVessel.transform;
                    }
                    inventoryGUI.createInventoryGrid(activeKerbal.backpack);
                }                
                //Debug.Log("KTQlogic: Vessel is Kerbal: " + activeVesselIsKerbal);
            }
        }

        public void Awake()
        {
            //Debug.Log("KTQL: Awake");
            Instance = this;
            GameEvents.onGameStateSaved.Add(OnSave);
            GameEvents.onGameStateCreated.Add(OnLoad);
            //OnLoad();
        }

        public void Start()
        {
            //Debug.Log("KerbTownQuestLogic: Start");
            inventoryGUI.OnStart();            
            hotBar.OnStart();
            hotBar.toggleInventory = inventoryGUI.toggleInventory;
        }

        public void FixedUpdate()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                updateActiveVessel(); // updates the reference in activeVessel, and checks whether it's a kerbal or a craft    
                if (activeVesselIsKerbal)
                {
                    inventoryGUI.updateGrid();
                }
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                //Debug.Log("Dropping bag");
                activeKerbal.backpack.items[0].findModel();
                activeKerbal.backpack.items[0].Spawn(activeVessel.transform);
            }
        }

        public void OnGUI()
        {
            if (activeVesselIsKerbal)
            {
                inventoryGUI.OnGUI();
                hotBar.OnGUI();
                //Debug.Log("drawing inv and hotbar");
            }
        }
    }
}
