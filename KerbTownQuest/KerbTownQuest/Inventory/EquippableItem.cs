using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerbTownQuest.Inventory
{
    public class EquippableItem
    {
        public string name;
        public string slot;

        public ConfigNode  getNode()
        {
            ConfigNode node = new ConfigNode("equippableItem");
            node.AddValue("name", name);
            node.AddValue("slot", slot);
            return node;
        }

        public void setValues(ConfigNode node)
        {
            name = node.GetValue("name");
            slot = node.GetValue("slot");
        }
    }
}
