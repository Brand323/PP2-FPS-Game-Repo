using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : QuestGoal
{
    int startingBanditCount;

    public KillGoal(int _startingBandidCount, string _description, bool _isReached, int _currentAmount, int _requiredAmount)
    {
        startingBanditCount = _startingBandidCount; 
        GoalDescription = _description;
        IsGoalReached = _isReached;
        CurrentAmount = _currentAmount;
        RequiredAmount = _requiredAmount;
    }

    public void EnemyDiedCheck()
    {
        
        //if(CombatManager.instance.enemiesExisting < startingBanditCount)
        //{
        //    CurrentAmount = startingBanditCount - CombatManager.instance.enemiesExisting;
        //}
        if(CombatManager.instance.enemiesExisting > 0 && startingBanditCount == 0)
        {
            startingBanditCount = CombatManager.instance.enemiesExisting;
        }
        if (CombatManager.instance.enemiesExisting < startingBanditCount)
        {
            CurrentAmount = startingBanditCount - CombatManager.instance.enemiesExisting;
        }
    }

    public override void evaluateGoalState()
    {
        EnemyDiedCheck();
        base.evaluateGoalState();
    }
}
