using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShousingProgram
{
    /// <summary>
    /// Holds all possible quests. The player will only see the Quests with the correct Quest.QuestStatus.
    /// </summary>
    public class QuestLog
    {
        public List<Quest> quests = new List<Quest>();
        public int currentQuest;
        public int maxActiveQuests = 20;

        public bool ActivateQuest(Quest quest)
        {
            if (activeQuestCount < maxActiveQuests)
            {
                quest.status = Quest.QuestStatus.active;
                return true;
            }
            else
                return false;
        }

        public int activeQuestCount
        {
            get
            {
                return quests.FindAll(isActiveQuest).Count;
            }
        }

        private bool isActiveQuest(Quest quest)
        {
            if (quest.status == Quest.QuestStatus.active)
                return true;
            else
                return false;            
        }

        public List<Quest> getQuestsByStatus(Quest.QuestStatus status)
        {
            List<Quest> questList = new List<Quest>();
            foreach (Quest quest in quests)
            {
                if (quest.status == status)
                    questList.Add(quest);
            }
            return questList;
        }
    }
}
