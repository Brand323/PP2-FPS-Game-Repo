using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest
{
    private int requiredAmount;

    public int RequiredAmount
    {
        get { return requiredAmount; }
        set { requiredAmount = value; }
    }

    public KillQuest(int _requiredAmount, int _companionReward = 0, int _goldReward = 0, 
                     int _healthReward = 0, int _staminaReward = 0)
    {
        QuestName = "Kill Quest";
        GoldReward = _goldReward;
        HealthPotionReward = _healthReward;
        StaminaPotionReward = _staminaReward;
        IsQuestCompleted = false;
        requiredAmount = _requiredAmount;
        QuestDescription = "Kill " + requiredAmount.ToString() + " bandits in a single battle" + '\n'
                            + "Rewards: " + GoldReward.ToString() + " coins\n" + HealthPotionReward.ToString()
                            + " health potions\n" + StaminaPotionReward.ToString() + " stamina potions\n";
        QuestGoal = new KillGoal(CombatManager.instance.enemiesExisting, "Kill " + requiredAmount.ToString() + " bandits.", false, 0, requiredAmount);
    }

    public override void CheckGoalState()
    {
        if (QuestGoal != null)
        {
            QuestGoal.evaluateGoalState();
        }
        base.CheckGoalState();
    }
}
