using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherGoal : QuestGoal
{
    string itemType;
    int startingMoney;
    int startingHealthPotions;
    int startingStaminaPotions;

    public GatherGoal(string _itemType, int _startingMoney, int _startingHealthPotions, int _startingStaminaPotions, string _description, bool _isReached, int _currentAmount, int _requiredAmount)
    {
        itemType = _itemType;
        startingMoney = _startingMoney;
        startingHealthPotions = _startingHealthPotions;
        startingStaminaPotions = _startingStaminaPotions;
        GoalDescription = _description;
        IsGoalReached = _isReached;
        CurrentAmount = _currentAmount;
        RequiredAmount = _requiredAmount;
    }

    public void gatherItemCheck()
    {
        if(itemType == "Health Potions")
        {
            if(gameManager.instance.GetHealthPotions() > startingHealthPotions)
            {
                CurrentAmount = gameManager.instance.GetHealthPotions() - startingHealthPotions;
            }
        }
        else if (itemType == "Stamina Potions")
        {
            if (gameManager.instance.GetStaminaPotions() > startingStaminaPotions)
            {
                CurrentAmount = gameManager.instance.GetStaminaPotions() - startingStaminaPotions;
            }
        }
    }

    public override void evaluateGoalState()
    {
        gatherItemCheck();
        base.evaluateGoalState();
    }
}
