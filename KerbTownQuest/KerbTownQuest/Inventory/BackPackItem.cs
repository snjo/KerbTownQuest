using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerbTownQuest.Inventory
{
    public class BackPackItem
    {
        public string name; // simple internal name
        private string _displayName; // optional, the fancy display name of the item
        public int itemSlots = 1; // how many backpack slots it takes up
        public float baseWeight; // the dry weight of the item        
        public float units; // for the amount of fuel in a jerry can, the intactness of a pitcher of grog, or the health of a goldfish
        public float maxUnits;
        public float weightPerUnit;
        public bool belongsTo; //is this stolen goods, or does it belongs to this kerbal?
        public string questTag; //used to separate this item from similar items for quests
        public string iconName; // a square image for the inventory grid
        public string meshName;
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
                if (_displayName != string.Empty)
                    return _displayName;
                else
                    return name;
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
    }
}
