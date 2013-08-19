using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KerbTownQuest.Inventory
{
    public class ItemLibrary // defines items used in quests or just lying around, should be loaded from an xml/cfg file
    {
        public Dictionary<string, BackPackItem> items; //dictionary or list?
        public string nodeName = "ItemLibrary";

        public ItemLibrary()
        {
            items = new Dictionary<string, BackPackItem>();
            // Fill from file
            /*items.Add("fakeBeard", new BackPackItem("fakeBeard", "fakeBeard"));
            items.Add("Wrench", new BackPackItem("Wrench"));
            items.Add("Jetpack", new BackPackItem("Jetpack"));

            BackPackItem bomb = new BackPackItem("bomb");
            bomb.meshName = BackPackItem.modelRootURL + "bomb";
            items.Add("bomb", bomb);

            BackPackItem money = new BackPackItem("Money", "Money");
            money.meshName = BackPackItem.modelRootURL + "Money";
            money.units = 100;
            items.Add("Money", money);*/
        }        

        public void Save()
        {
            //Debug.Log("KTQ: itemLibrary OnSave");
            ConfigNode libraryNode = new ConfigNode(nodeName);
            foreach (KeyValuePair<string, BackPackItem> entry in items)
            {
                //ConfigNode itemNode = libraryNode.AddNode("item");
                //itemNode.AddValue("first", entry.Value.displayName);
                libraryNode.AddNode(entry.Value.getNode());
            }

            libraryNode.Save(KerbTownQuestLogic.configPath + nodeName + ".cfg", "KerbTown Inventory Item Library. Will only be loaded, not written to by the game.");            
        }

        public void Load()
        {
            string configPath = KerbTownQuestLogic.configPath + nodeName + ".cfg";
            Debug.Log("KTQ: itemLibrary OnLoad: " + configPath);
            items = new Dictionary<string, BackPackItem>();
            ConfigNode libraryNode = ConfigNode.Load(configPath);
            if (libraryNode != null)
            {
                ConfigNode[] itemNodes = libraryNode.GetNodes("item");
                foreach (ConfigNode itemNode in itemNodes)
                {
                    BackPackItem newItem = new BackPackItem();
                    newItem.setValues(itemNode);
                    items.Add(newItem.name, newItem);
                }
                Debug.Log("Filled itemLibrary with " + items.Count + " items");
            }
            else
            {
                Debug.Log("KTQ: libraryNode is null, have you put the mode in the wrong folder? Tried loading: " + configPath);
            }
        }
    }
}
