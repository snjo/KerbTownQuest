using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KerbTownQuest.Util;

namespace KerbTownQuest.Inventory
{
    public class Backpack // a list of backpack items
    {
        public List<BackPackItem> items = new List<BackPackItem>();
        public float weightCapacity = 100f;
        public float weightUsed;
        public int itemCapacity = 10;
        public int itemSlotsUsed;        

        public bool AddItem(BackPackItem item)
        {
            if (itemSlotsUsed + item.itemSlots < itemCapacity && weightUsed + item.totalWeight < weightCapacity)
            {
                items.Add(item);
                itemSlotsUsed += item.itemSlots;
                weightUsed += item.totalWeight;
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
                return true;
            }
            else
                return false;
        }
    }
}
