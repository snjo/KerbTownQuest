using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShousingProgram
{
    public class KTKerbal //: Kerbal //extends the stock game's Kerbal
    {
        public string name; // use crewMemberName
        public Kerbal kerbal;
        public float health;
        public int XP;
        public int level;
        public int money;
        public Backpack backpack = new Backpack();
        //public QuestLog questLog = new QuestLog();
    }
}
