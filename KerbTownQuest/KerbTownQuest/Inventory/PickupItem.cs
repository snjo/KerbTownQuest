using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KerbTownQuest.Inventory
{
    class PickupItem : MonoBehaviour
    {
        public string displayName = "Bag of Junk";
        public List<BackPackItem> items = new List<BackPackItem>();
        public bool autoPickup = false;
        public float pickupRange = 3f;

        public void tryPickup()
        {
            Debug.Log("Picking up item");
            List<BackPackItem> removalList = new List<BackPackItem>();
            foreach (BackPackItem item in items)
            {
                if (KerbTownQuestLogic.activeKerbal.backpack.AddItem(item))
                {
                    Debug.Log("pickup succesful: " + item.displayName);
                    removalList.Add(item);
                }
                else
                {
                    Debug.Log("pickup failed: " + item.displayName);
                }
            }
            foreach (BackPackItem removalItem in removalList)
            {
                items.Remove(removalItem);
            }
            if (items.Count <= 0)
            {
                destroyPickup();
            }
        }

        public void destroyPickup()
        {
            Debug.Log("Destroying pickup");
            Destroy(this.gameObject, 0.5f);
        }

        public void OnTriggerEnter(Collider other)
        {
            //Debug.Log("on trigger enter, other: " + other.gameObject);
            if (autoPickup)
            {
                tryPickup();
            }
            // destroy item and add it to the right kerbal backpack
        }

        public void OnCollisionEnter(Collision collision)
        {
            //Debug.Log("on collision enter");
            // destroy item and add it to the right kerbal backpack
        }

        public void OnMouseDown()
        {
            //Debug.Log("mouseDown");
            if (Vector3.Distance(FlightGlobals.ActiveVessel.transform.position, transform.position) < pickupRange)
            {                
                tryPickup();
            }
        }
    }
}
