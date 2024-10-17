using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortQuest : Quest
{
    private int requiredAmount;
    private bool caravanArrived;

    public int RequiredAmount
    {
        get { return requiredAmount; }
        set { requiredAmount = value; }
    }

    public EscortQuest(int _requiredAmount, int _companionReward = 0, int _goldReward = 0,
                     int _healthReward = 0, int _staminaReward = 0)
    {
        QuestName = "Escort Quest";
        GoldReward = _goldReward;
        HealthPotionReward = _healthReward;
        StaminaPotionReward = _staminaReward;
        IsQuestCompleted = false;
        requiredAmount = _requiredAmount;
        QuestDescription = "Escort the caravan and defend it." + '\n'
                            + "Rewards: " + GoldReward.ToString() + " coins\n" + HealthPotionReward.ToString()
                            + " health potions\n" + StaminaPotionReward.ToString() + " stamina potions\n"
                            + CompanionReward.ToString() + " companions";
        QuestGoal = new EscortGoal(false, "Escort caravan.", false, 0, RequiredAmount);
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
