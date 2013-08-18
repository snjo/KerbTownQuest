using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KerbTownQuest.Inventory
{
    public class BackPackItem
    {
        public string name; // simple internal name
        private string _displayName = ""; // optional, the fancy display name of the item
        public int itemSlots = 1; // how many backpack slots it takes up
        public float baseWeight; // the dry weight of the item        
        public float units; // for the amount of fuel in a jerry can, the intactness of a pitcher of grog, or the health of a goldfish
        public float maxUnits;
        public float weightPerUnit;
        public bool stackable;
        public EquippableItem equippableItem;
        public string belongsTo; //is this stolen goods, or does it belongs to this kerbal?
        public string questTag; //used to separate this item from similar items for quests
        public string iconName; // a square image for the inventory grid
        public string meshName = string.Empty;
        public GameObject mesh;
        public bool removable = true;
        public string defaultModelName = "bagOfJunk";
        public static string modelRootURL = KerbTownQuestLogic.folderName +  "models/";
        public static string iconRootURL = KerbTownQuestLogic.folderName + "icons/";

        public enum ItemType
        {
            undefined,
            consumable,
            partResource, //fuel
            part,
            vessel,
            equippable,
            usable,
            error,
        }

        public ItemType itemType = ItemType.undefined;

        public string displayName
        {
            get
            {
                if (_displayName != "")
                {
                    return _displayName;
                }
                else
                {
                    return name;
                }
            }
            set
            {
                _displayName = value;
            }
        }

        public float totalWeight
        {
            get
            {
                return baseWeight + (units * weightPerUnit);
            }
        }

        public BackPackItem()
        {
        }

        public BackPackItem(string uniqueName)
        {
            name = uniqueName;
        }

        public BackPackItem(string uniqueName, string icon)
        {
            name = uniqueName;
            iconName = icon;
        }

        public void findModel()
        {
            if (meshName == string.Empty)
            {
                //Debug.Log("loading default model " + modelRootURL + defaultModelName);
                if (GameDatabase.Instance.ExistsModel(modelRootURL + defaultModelName))
                {
                    //Debug.Log("found default model");
                    mesh = GameDatabase.Instance.GetModel(modelRootURL + defaultModelName);
                }
                else
                {
                    Debug.Log("missing default model");
                }
            }
            else
            {
                //Debug.Log("loading model " + meshName);
                if (GameDatabase.Instance.ExistsModel(meshName))
                {
                    mesh = GameDatabase.Instance.GetModel(meshName);
                }
                else
                {
                    Debug.Log("Can't find model " + meshName);
                }
            }
        }

        public void Spawn(Transform transform)
        {
            if (mesh != null)
            {
                GameObject newObject = (GameObject)GameObject.Instantiate(mesh, transform.position, transform.rotation);
                newObject.SetActive(true);
                Rigidbody rigidbody = newObject.AddComponent<Rigidbody>();
                rigidbody.mass = 0.1f;
                rigidbody.useGravity = true;
                Container newPickup = newObject.AddComponent<Container>();                
                newPickup.items.Add(this);
                newPickup.destroyWhenEmpty = true;

                //Debug.Log("Instantiated model, pos: " + newObject.transform.position);
                //newPickup.items.Add(ItemLibrary.items["fakeBeard"]);
                //newPickup.items.Add(ItemLibrary.items["Jetpack"]);
            }
            else
            {
                Debug.Log("Spawn item: mesh is null");
            }
        }

        public ConfigNode getNode()
        {
            ConfigNode node = new ConfigNode("item");
            node.AddValue("name", name);
            node.AddValue("displayName", displayName);
            node.AddValue("itemSlots", itemSlots);
            node.AddValue("baseWeight", baseWeight);
            node.AddValue("units", units);
            node.AddValue("maxUnits", maxUnits);
            node.AddValue("weightPerUnit", weightPerUnit);
            node.AddValue("stackable", stackable);
            node.AddValue("belongsTo", belongsTo);
            node.AddValue("questTag", questTag);
            node.AddValue("iconName", iconName);
            node.AddValue("meshName", meshName);
            node.AddValue("removable", removable);
            node.AddValue("itemType", itemType);

            if (equippableItem != null)
            {
                node.AddNode(equippableItem.getNode());
            }
           
            return node;
        }

        public void setValues(ConfigNode node)
        {        
            name = node.GetValue("name");
            displayName = node.GetValue("displayName");
            int.TryParse(node.GetValue("itemSlots"), out itemSlots);
            float.TryParse(node.GetValue("baseWeight"), out baseWeight);
            float.TryParse(node.GetValue("units"), out units);
            float.TryParse(node.GetValue("maxUnits"), out maxUnits);
            float.TryParse(node.GetValue("weightPerUnit"), out weightPerUnit);
            
            stackable = valueToBool(node.GetValue("stackable"));

            belongsTo = node.GetValue("belongsTo");
            questTag = node.GetValue("questTag");
            iconName = node.GetValue("iconName");
            meshName = node.GetValue("meshName");
            removable = valueToBool(node.GetValue("removable"));

            itemType = stringToItemType(node.GetValue("itemType"));
            // tryparse doesn't exist in KSP! whyyy?
            //if (!Enum.TryParse(node.GetValue("itemType"), out itemType))
            //{
            //    itemType = ItemType.undefined;
            //}            

            ConfigNode equippableNode = node.GetNode("equippableItem");
            if (equippableNode != null)
            {
                equippableItem = new EquippableItem();
                equippableItem.name = equippableNode.GetValue("name");
                equippableItem.slot = equippableNode.GetValue("slot");
            }
        }

        private bool valueToBool(string inValue)
        {
            bool result = false;
            try
            {
                result = Boolean.Parse(inValue);
            }
            catch
            {
                Debug.Log("can't parse " + inValue);
            }
            return result;
        }

        private ItemType stringToItemType(string inValue)
        {
            if (inValue == ItemType.consumable.ToString()) return ItemType.consumable;
            if (inValue == ItemType.equippable.ToString()) return ItemType.equippable;
            if (inValue == ItemType.part.ToString()) return ItemType.part;
            if (inValue == ItemType.partResource.ToString()) return ItemType.partResource;
            if (inValue == ItemType.usable.ToString()) return ItemType.usable;
            if (inValue == ItemType.vessel.ToString()) return ItemType.vessel;
            if (inValue == ItemType.undefined.ToString()) return ItemType.undefined;

            Debug.Log("KTQ backpackitem: unhandled enum value");
            return ItemType.error;
        }
    }
}
