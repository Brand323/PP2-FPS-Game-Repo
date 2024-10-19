using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestGiver : MonoBehaviour 
{
    private Quest quest;
    private int randomizer;
    private int requiredAmount;

    public Quest SetQuest
    {
        get { return quest; }
        set { quest = value; }
    }

    public void GiveQuest()
    {
        randomizer = Random.Range(1, 20);
        if (!gameManager.instance.isQuestInProgress)
        {
            UIManager.instance.activateQuestWindow(UIManager.instance.questWindow);
            if(randomizer < 11)
            {
                if (gameManager.instance.currentCity != null && gameManager.instance.mapPlayer != null)
                {
                    if (gameManager.instance.currentCity == null)
                    {
                        MapKingdomManager.instance.findNearestCity();
                    }

                    if (gameManager.instance.currentCity != null && gameManager.instance.mapPlayer != null)
                    {
                        MapCity currCity = gameManager.instance.currentCity.GetComponent<MapCity>();

                        //// Check if a caravan has already been spawned
                        //if (!currCity.caravanSpawned)
                        //{
                        //    // Spawn a caravan from the nearest city
                        //    //currCity.SpawnCaravanFromNearestCity(gameManager.instance.mapPlayer.transform);
                        //}
                    }

                    // Create a new Escort Quest
                    quest = new EscortQuest(1, Random.Range(1, 2), Random.Range(5, 15), Random.Range(2, 4), Random.Range(2, 4));
                    StartCoroutine(UIManager.instance.caravanAttackFeedBack());
                }
                quest = new EscortQuest(1, Random.Range(1, 2), Random.Range(5, 15), Random.Range(2, 4), Random.Range(2, 4));
                StartCoroutine(UIManager.instance.caravanAttackFeedBack());
            }
            else if(randomizer > 10 && randomizer < 21)
            {
                quest = new KillQuest(Random.Range(3, 5), Random.Range(1, 2), Random.Range(5, 15), Random.Range(2, 4), Random.Range(2, 4));
            }
            gameManager.instance.SetCurrentQuest(quest);
            gameManager.instance.isQuestInProgress = true;
            UIManager.instance.questName.text = quest.QuestName;
            UIManager.instance.questDescription.text = quest.QuestDescription;
        }
        else
        {
            UIManager.instance.questName.text = quest.QuestName;
            UIManager.instance.questDescription.text = quest.QuestDescription;
            UIManager.instance.activateQuestWindow(UIManager.instance.questDescriptionWindow);
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
