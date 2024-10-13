using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestGiver : MonoBehaviour 
{
    private Quest quest;
    private int randomizer;
    private int requiredAmount;
    private bool questInProgress;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "QuestGiver")
        {
            //UIManager.instance.questWindow.SetActive(true);
            randomizer = 1;//Random.Range(1, 3);
            switch (randomizer)
            {
                case 1:
                    quest = new GatherQuest(Random.Range(1, 3), "Health Potions", 3);
                    break;
            }
        }
    }

    private void Update()
    {
        if(quest != null)
        {
            quest.CheckGoalState();
        }
    }
}
