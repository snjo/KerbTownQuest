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
        public static KTKerbalRoster kerbalRoster = new KTKerbalRoster();
        public static ItemLibrary itemLibrary = new ItemLibrary();
        public static QuestLog questLog = new QuestLog();
        public static KTKerbal activeKerbal;
        public HotBar hotBar = new HotBar();
        public InventoryGUI inventoryGUI = new InventoryGUI();
        private Vessel activeVessel;
        private bool activeVesselIsKerbal;
        private string kerbalName;

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
                        activeKerbal.backpack.AddItem(ItemLibrary.items["Wrench"]);                        
                    }
                    inventoryGUI.createInventoryGrid(activeKerbal.backpack);
                }                
                Debug.Log("KTQlogic: Vessel is Kerbal: " + activeVesselIsKerbal);
            }
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
                Debug.Log("Dropping bag");
                activeKerbal.backpack.items[0].findModel();
                activeKerbal.backpack.items[0].Spawn(activeVessel.transform.position, activeVessel.transform.rotation);
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
