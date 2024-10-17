using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static gameManager instance;

    public bool gameStarted;

    public GameObject player;
    public FirstPersonController playerScript;

    public GameObject playerSpawnPosition;

    private money playerMoney;
    private PotionManager playerPotions;

    public QuestGiver questGiver;
    public Quest currentQuest;
    public bool isQuestInProgress;

    public MapKingdomManager kingdomManager;

    void Awake()
    {
        //Code for Ensuring Singleton Setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        }

        playerSpawnPosition = GameObject.FindWithTag("Player Spawn Position");
        kingdomManager = FindObjectOfType<MapKingdomManager>();
        questGiver = FindObjectOfType<QuestGiver>();
            
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerScript = player.GetComponent<FirstPersonController>();
                player.AddComponent<money>();
                playerMoney = player.GetComponent<money>();
                playerPotions = player.GetComponent<PotionManager>();
            }
            playerSpawnPosition = GameObject.FindWithTag("Player Spawn Position");
            kingdomManager = FindObjectOfType<MapKingdomManager>();
            questGiver = FindObjectOfType<QuestGiver>();
            questGiver.SetQuest = currentQuest;
        }
        if (SceneManager.GetActiveScene().name != "CombatSceneArctic" && !gameStarted)
        {
            if (UIManager.instance != null)
            {
                StartCoroutine(UIManager.instance.startGame());
            }
            gameStarted = true;
        }
    }

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

    public void LoseUpdate()
    {
        UIManager.instance.LoseUpdate();
    }
}
