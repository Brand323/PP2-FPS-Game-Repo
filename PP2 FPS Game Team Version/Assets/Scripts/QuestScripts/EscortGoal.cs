using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortGoal : QuestGoal
{
    Vector3 initialPosition;
    Vector3 finalPosition;

    public EscortGoal(Vector3 _initialPosition, Vector3 _finalPosition, string _description, bool _isReached, int _currentAmount, int _requiredAmount)
    {
        initialPosition = _initialPosition;
        finalPosition = _finalPosition;
        GoalDescription = _description;
        IsGoalReached = _isReached;
        CurrentAmount = _currentAmount;
        RequiredAmount = _requiredAmount;
    }

    public void EscortDoneCheck()
    {
        //Check if the object escorted is in finalPosition and not dead
    }

    public override void evaluateGoalState()
    {
        EscortDoneCheck();
        base.evaluateGoalState();
    }
}
