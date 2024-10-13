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

    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Collection Quest";
        QuestDescription = "Collect the indicated number of items to get rewards.";
        GoldReward = 0;
        CompanionReward = 2;
        HealthPotionReward = 0;
        StaminaPotionReward = 0;
        IsQuestCompleted = false;
        QuestGoals.Add(new GatherGoal(collectionItemType, gameManager.instance.GetPlayerMoney(), gameManager.instance.GetHealthPotions(), gameManager.instance.GetStaminaPotions(), "Collect " + requiredAmount.ToString() + collectionItemType, false, 0, requiredAmount));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
