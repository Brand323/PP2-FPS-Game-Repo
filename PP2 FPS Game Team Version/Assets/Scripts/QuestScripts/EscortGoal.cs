using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortGoal : QuestGoal
{
    private bool caravanArrived;
    public bool CaravanArrived
    {
        get { return caravanArrived; }
        set { caravanArrived = value; }
    }

    public EscortGoal(bool _caravanArrived, string _description, bool _isReached, int _currentAmount, int _requiredAmount)
    {
        caravanArrived = _caravanArrived;
        GoalDescription = _description;
        IsGoalReached = _isReached;
        CurrentAmount = _currentAmount;
        RequiredAmount = _requiredAmount;
    }

    public void EscortDoneCheck()
    {
        if(gameManager.instance.caravanArrived)
        {
            CurrentAmount = 1;
        }
    }

    public override void evaluateGoalState()
    {
        EscortDoneCheck();
        base.evaluateGoalState();
    }
}
