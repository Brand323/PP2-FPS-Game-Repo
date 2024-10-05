using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class QuestGoal 
{
    private string description;
    private bool isReached;
    private int currentAmount;
    private int requiredAmount;

#region Getters and Setters

    public string GoalDescription
    {
        get { return description; }
        set { description = value; }
    }

    public bool IsGoalReached
    {
        get { return isReached; }
        set { isReached = value; }
    }

    public int CurrentAmount
    {
        get { return currentAmount; }
        set { currentAmount = value; }
    }

    public int RequiredAmount
    {
        get { return requiredAmount; }
        set { requiredAmount = value; }
    }

    #endregion

    public void evaluateGoalState()
    {
        if(currentAmount >= requiredAmount)
        {
            isReached = true;
        }
    }
}