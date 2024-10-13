using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestGiver : MonoBehaviour 
{
    private Quest quest;
    private int randomizer;
    private int requiredAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "QuestGiver")
        {
            //UIManager.instance.questWindow.SetActive(true);
            //randomizer = 1;//Random.Range(1, 3);
            //switch (randomizer)
            //{
            //    case 1:
            //        quest = new GatherQuest(Random.Range(1, 3), "Health Potions", 3);
            //        break;
            //}
            if (!gameManager.instance.isQuestInProgress)
            {
                UIManager.instance.activateQuestWindow(UIManager.instance.questWindow);
                quest = new GatherQuest(Random.Range(1, 10), Random.Range(3, 5), Random.Range(1, 3), Random.Range(3, 5));
                gameManager.instance.SetCurrentQuest(quest);
                gameManager.instance.isQuestInProgress = true;
                UIManager.instance.questName.text = quest.QuestName;
                UIManager.instance.questDescription.text = quest.QuestDescription;
            }
            else
            {
                UIManager.instance.activateQuestWindow(UIManager.instance.questDescriptionWindow);
            }
        }
    }

    private void Update()
    {
        if(!gameManager.instance.isQuestInProgress)
        {
            quest = null;
        }
        if(quest != null)
        {
            quest.CheckGoalState();
        }
    }
}
