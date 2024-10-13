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

    public GatherQuest(int _companionReward, string _collectionItemType, int _requiredAmount)
    {
        QuestName = "Collection Quest";
        QuestDescription = "Collect the indicated number of items to get rewards.";
        GoldReward = 3;
        CompanionReward = _companionReward;
        HealthPotionReward = 0;
        StaminaPotionReward = 3;
        IsQuestCompleted = false;
        collectionItemType = _collectionItemType;
        requiredAmount = _requiredAmount;
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
