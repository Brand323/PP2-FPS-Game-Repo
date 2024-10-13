using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherQuest : Quest
{
    private string collectionItemType;
    private int requiredAmount;

    public string CollectionItemType
    {
        get { return collectionItemType; }
        set { collectionItemType = value; }
    }

    public int RequiredAmount
    {
        get { return requiredAmount; }
        set { requiredAmount = value; }
    }

    public GatherQuest(int _collectionItemType, int _requiredAmount, int _companionReward = 0,
                      int _goldReward = 0, int _healthReward = 0, int _staminaReward = 0)
    {
        QuestName = "Collection Quest";
        GoldReward = _goldReward;
        CompanionReward = _companionReward;
        HealthPotionReward = _healthReward;
        StaminaPotionReward = _staminaReward;
        IsQuestCompleted = false;
        if (_collectionItemType <= 5)
        {
            collectionItemType = "Health Potions";
        }
        else
        {
            collectionItemType = "Stamina Potions";
        }
        requiredAmount = _requiredAmount;
        QuestDescription = "Collect " + requiredAmount.ToString() + " " + collectionItemType + '\n'
                            + "Rewards: " + GoldReward.ToString() + " coins\n" + HealthPotionReward.ToString()
                            + " health potions\n" + StaminaPotionReward.ToString() + " stamina potions";
        QuestGoal = new GatherGoal(collectionItemType, gameManager.instance.GetPlayerMoney(), gameManager.instance.GetHealthPotions(), gameManager.instance.GetStaminaPotions(), "Collect " + requiredAmount.ToString() + collectionItemType, false, 0, requiredAmount);
    }

    public override void CheckGoalState()
    {
        if(QuestGoal != null)
        {
            QuestGoal.evaluateGoalState();
        }
        base.CheckGoalState();
    }
}
