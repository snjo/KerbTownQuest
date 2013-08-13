using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FShousingProgram
{
    /// <summary>
    /// A sub goal of a Quest. A quest must have at least one to be completeable through normal means
    /// </summary>
    public class QuestGoal
    {
        public string name; // simple internal name
        public Quest quest; // points back to the parent
        public string neededItem;
        public enum GoalType
        {
            undefined,
            item,
            poistion,
            orbit,
            landing,
            destruction,
            creation,
        }
        public GoalType goalType = GoalType.undefined;
        public enum Status
        {
            undefined,
            completed,
            failed,
            queued,
            hidden,
        }
        public float positionRadius; // how close to get to the position if that's the type.
        public Vector3 position;
        public bool staysCompleted = true; // must this remain true for the quest to be completed, or do you only need to fulfill it once. (Stay in this position till the end, keep this number of items, etc.)
        public float progress; // for collection quests for instance, or for going a certain distance...
        public float target; // the target amount

        public QuestGoal(string goalName, Quest parent)
        {
            name = goalName;
            quest = parent;
        }
    }
}
