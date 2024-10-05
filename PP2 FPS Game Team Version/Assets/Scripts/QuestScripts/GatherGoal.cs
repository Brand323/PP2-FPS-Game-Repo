using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherGoal : QuestGoal
{
    string itemType;

    public GatherGoal(string _itemType, string _description, bool _isReached, int _currentAmount, int _requiredAmount)
    {
        itemType = _itemType;
        GoalDescription = _description;
        IsGoalReached = _isReached;
        CurrentAmount = _currentAmount;
        RequiredAmount = _requiredAmount;
    }

    public void gatherItemCheck()
    {
        //if itemType = item gathered type
        //CurrentAmount++;
        evaluateGoalState();
    }
}
