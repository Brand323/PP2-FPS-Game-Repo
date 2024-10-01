using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private List<QuestGoal> goals = new List<QuestGoal>();
    private string questName;
    private string questDescription;
    private int goldReward;
    private int companionReward;
    //item reward (weapons and potions) needs to be added
    private bool isCompleted;

#region Getters and Setters

    public List<QuestGoal> QuestGoals
    {
        get { return goals; }
        set { goals = value; }
    }

    public string QuestName
    {
        get { return questName; }
        set { questName = value; }
    }

    public string QuestDescription
    {
        get { return questDescription; }
        set { questDescription = value; }
    }

    public int GoldReward
    {
        get { return goldReward; }
        set { goldReward = value; }
    }

    public int CompanionReward
    {
        get { return companionReward; }
        set { companionReward = value; }
    }

    public bool IsQuestCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }
    }

    #endregion

    public void CheckGoalsState()
    {
        isCompleted = goals.All(g => g.IsGoalReached);
        if(isCompleted)
        {
            GiveRewards();
        }
    }

    void GiveRewards()
    {
        //Add reward amounts to player inventory
    }
}
