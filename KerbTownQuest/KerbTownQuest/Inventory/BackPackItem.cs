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
        public bool belongsTo; //is this stolen goods, or does it belongs to this kerbal?
        public string questTag; //used to separate this item from similar items for quests
        public string iconName; // a square image for the inventory grid
        public string meshName = string.Empty;
        public GameObject mesh;
        public bool removable = true;
        public string defaultModelName = "bagOfJunk";
        public string modelRootURL = "FShousingProgram/models/";

        public enum ItemType
        {
            undefined,
            consumable,
            partResource, //fuel
            part,
            vessel,
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
                Debug.Log("loading default model " + modelRootURL + defaultModelName);
                if (GameDatabase.Instance.ExistsModel(modelRootURL + defaultModelName))
                {
                    Debug.Log("found default model");
                    mesh = GameDatabase.Instance.GetModel(modelRootURL + defaultModelName);
                }
                else
                {
                    Debug.Log("missing default model");
                }
            }
            else
            {
                Debug.Log("loading model " + meshName);
                if (GameDatabase.Instance.ExistsModel(meshName))
                {                    
                    mesh = GameDatabase.Instance.GetModel(meshName);
                }
            }
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            if (mesh != null)
            {                
                GameObject newObject = (GameObject)GameObject.Instantiate(mesh, position, rotation);
                newObject.SetActive(true);
                Rigidbody rigidbody = newObject.AddComponent<Rigidbody>();
                rigidbody.mass = 0.1f;
                rigidbody.useGravity = true;
                PickupItem newPickup = newObject.AddComponent<PickupItem>();
                Debug.Log("Instantiated model, pos: " + newObject.transform.position);

                newPickup.items.Add(ItemLibrary.items["fakeBeard"]);
                newPickup.items.Add(ItemLibrary.items["Jetpack"]);
            }
            else
            {
                Debug.Log("Spawn item: mesh is null");
            }
        }
    }
}
