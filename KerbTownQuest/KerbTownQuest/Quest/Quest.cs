using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShousingProgram
{
    /// <summary>
    /// A single Quest for the QuestLog. Has a list of QuestGoals to complete to finish the mission.
    /// </summary>
    public class Quest
    {
        public string name; // simple internal name
        private string _displayName; // optional fancy display name, changeable    
        public QuestLog questLog;
        public enum QuestStatus
        {
            undefined,
            active,
            inactive,
            completed,
            failed,
            repeatable,
            hidden,
            completedhidden,
        }
        public QuestStatus status = QuestStatus.undefined;
        public List<QuestGoal> questGoals;
        public string prerequisiteQuest;
        public string prerequisiteItem;
        public string rewardItem;
        public float rewardAmount;
        public string followUpQuest; // In a quest chain, progress to this one upon completion
        public bool tracked; // track in a sidebar

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

        public Quest(string questName, QuestLog parent)
        {
            name = questName;
            questLog = parent;
        }
    }
}
