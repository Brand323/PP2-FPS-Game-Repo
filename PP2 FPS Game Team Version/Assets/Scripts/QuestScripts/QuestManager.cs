using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Quest
{
    private QuestGoal goal = new QuestGoal();
    private string questName;
    private string questDescription;
    private int goldReward;
    private int companionReward;
    private int healthPotionReward;
    private int staminaPotionReward;
    private bool isCompleted;

#region Getters and Setters

    public QuestGoal QuestGoal
    {
        get { return goal; }
        set { goal = value; }
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

    public int HealthPotionReward
    {
        get { return healthPotionReward; }
        set { healthPotionReward = value; }
    }

    public int StaminaPotionReward
    {
        get { return staminaPotionReward; }
        set { staminaPotionReward = value; }
    }

    public bool IsQuestCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }
    }

    #endregion

    public virtual void CheckGoalState()
    {
        isCompleted = goal.IsGoalReached;
        if(isCompleted)
        {
            GiveRewards();
        }
    }

    void GiveRewards()
    {
        gameManager.instance.AddMoneyToPlayer(goldReward);
        gameManager.instance.AddHealthPotions(healthPotionReward);
        gameManager.instance.AddStaminaPotions(staminaPotionReward);
        //Add companion rewards
        UIManager.instance.staminaPotionText.text = staminaPotionReward.ToString();
        UIManager.instance.moneyText.text = goldReward.ToString();
    }
}
