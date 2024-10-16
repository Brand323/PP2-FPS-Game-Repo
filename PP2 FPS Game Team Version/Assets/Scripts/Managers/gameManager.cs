using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static gameManager instance;

    public GameObject player;
    public FirstPersonController playerScript;

    public GameObject playerSpawnPosition;

    private money playerMoney;
    private PotionManager playerPotions;
    public List<ResourceData> playerInventory;

    public Quest currentQuest;
    public bool isQuestInProgress;

    public MapKingdomManager kingdomManager;

    #region Inventory Fields

    public bool buyItem;

    #endregion

    void Awake()
    {
        //Code for Ensuring Singleton Setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<FirstPersonController>();
            player.AddComponent<money>();
            playerMoney = player.GetComponent<money>();
            playerPotions = player.GetComponent<PotionManager>();
            playerInventory = new List<ResourceData>();
        }

        playerSpawnPosition = GameObject.FindWithTag("Player Spawn Position");
        kingdomManager = FindObjectOfType<MapKingdomManager>();
            
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    public void AddMoneyToPlayer(int amount)
    {
        if (playerMoney != null)
        {
            playerMoney.SetCoinCount(amount);
        }
    }

    public void AddHealthPotions(int amount)
    {
        playerPotions.AddHealthPotions(amount);
    }

    public void AddStaminaPotions(int amount)
    {
        playerPotions.AddStaminaPotions(amount);
    }

    public int GetPlayerMoney()
    {
        if (playerMoney != null)
        {
            return playerMoney.GetCoinCount();
        }
        else
        {
            return 0;
        }
    }

    public int GetHealthPotions()
    {
        return playerPotions.HealthPotions;
    }

    public int GetStaminaPotions()
    {
        return playerPotions.StaminaPotions;
    }

    public void SetCurrentQuest(Quest quest)
    {
        currentQuest = quest;
    }

    public void PauseGame(GameObject window)
    {
        UIManager.instance.PauseGame(window);
    }

    public void UnpauseGame()
    {
        UIManager.instance.UnpauseGame();
    }

    public void AddItemToInventory(ResourceData item)
    {
        playerInventory.Add(item);
    }

    public void LoseUpdate()
    {
        UIManager.instance.LoseUpdate();
    }
}
