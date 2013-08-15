using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerbTownQuest.Inventory
{
    public class ItemLibrary // defines items used in quests or just lying around, should be loaded from an xml/cfg file
    {
        public static Dictionary<string, BackPackItem> items; //dictionary or list?

        public ItemLibrary()
        {
            items = new Dictionary<string, BackPackItem>();
            // Fill from file
            items.Add("fakeBeard", new BackPackItem("fakeBeard", "fakeBeard"));
            items.Add("Wrench", new BackPackItem("Wrench"));
            items.Add("Jetpack", new BackPackItem("Jetpack"));
        }
    }
}
