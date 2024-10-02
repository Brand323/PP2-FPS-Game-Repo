using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : QuestGoal
{
    public KillGoal(string _description, bool _isReached, int _currentAmount, int _requiredAmount)
    {
        GoalDescription = _description;
        IsGoalReached = _isReached;
        CurrentAmount = _currentAmount;
        RequiredAmount = _requiredAmount;
    }

    public void EnemyDiedCheck()
    {
        //if bandit amount has decreased
        //CurrentAmount++;
        evaluateGoalState();
    }
}
