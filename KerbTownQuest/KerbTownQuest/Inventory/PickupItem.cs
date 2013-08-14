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

        public void OnColliderEnter()
        {
            Debug.Log("collider enter");
            // destroy item and add it to the right kerbal backpack
        }
    }
}
