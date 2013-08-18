using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KerbTownQuest.Util;
using UnityEngine;

namespace KerbTownQuest.Inventory
{
    public class Backpack : MonoBehaviour // a list of backpack items
    {
        public string displayName;
        public List<BackPackItem> items = new List<BackPackItem>(); // should be private
        public float weightCapacity = 100f;
        public float weightUsed = 0f;
        public int itemCapacity = 10;
        public int itemSlotsUsed = 0;
        public bool contentsHaveChanged = false;

        public bool AddItem(BackPackItem item)
        {
            if (itemSlotsUsed + item.itemSlots <= itemCapacity && weightUsed + item.totalWeight <= weightCapacity)
            {
                items.Add(item);
                itemSlotsUsed += item.itemSlots;
                weightUsed += item.totalWeight;
                contentsHaveChanged = true;
                return true;
            }
            else
                return false;
        }

        public bool RemoveItemAt(int ID)
        {
            if (items.Count > ID)
            {
                weightUsed -= items[ID].totalWeight;
                itemSlotsUsed -= items[ID].itemSlots;
                items.RemoveAt(ID);
                contentsHaveChanged = true;
                return true;
            }
            else
                return false;
        }

        public bool RemoveItem(BackPackItem item)
        {
            if (items.Contains(item))
            {
                weightUsed -= item.totalWeight;
                itemSlotsUsed -= item.itemSlots;
                items.Remove(item);
                contentsHaveChanged = true;
                return true;
            }
            else
                return false;
        }

        public bool DropItem(BackPackItem item, Transform transform)
        {
            if (item.removable)
            {
                item.findModel();
                item.Spawn(transform);
                RemoveItem(item);
            }
            return false;
        }

        public ConfigNode getNode()
        {
            ConfigNode node = new ConfigNode("backpack");

            node.AddValue("displayName", displayName);
            node.AddValue("weightCapacity", weightCapacity);
            node.AddValue("weightUsed", weightUsed);
            node.AddValue("itemCapacity", itemCapacity);
            node.AddValue("itemSlotsUsed", itemSlotsUsed);
        
            foreach (BackPackItem item in items)
            {
                node.AddNode(item.getNode());
            }
            return node;
        }
    }
}
