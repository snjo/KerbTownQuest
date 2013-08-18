using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KerbTownQuest.Inventory;
using UnityEngine;

namespace KerbTownQuest
{
    public class KTKerbal //: Kerbal //extends the stock game's Kerbal
    {
        public string name; // use crewMemberName
        //public Kerbal kerbal;
        public Transform transform;
        public float health;
        public int XP;
        public int level;
        public int money;
        public Backpack backpack = new Backpack();
        //public QuestLog questLog = new QuestLog();

        public ConfigNode getNode()
        {
            ConfigNode node = new ConfigNode("kerbal");

            node.AddValue("name", name);
            node.AddValue("health", health);
            node.AddValue("XP", XP);
            node.AddValue("level", level);
            node.AddValue("money", money);

            node.AddNode(backpack.getNode());
            return node;
        }
    }
}
