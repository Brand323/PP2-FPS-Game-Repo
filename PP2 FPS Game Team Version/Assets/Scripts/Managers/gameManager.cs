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
    private int playerMoneyValue;
    public int PlayerMoneyValue
    {
        get { return playerMoneyValue; }
        set { playerMoneyValue = value; }
    }
    private PotionManager playerPotions;
    private int playerHealthPotions;
    public int PlayerHealthPotions
    {
        get { return playerHealthPotions; }
        set { playerHealthPotions = value; }
    }
    private int playerStaminaPotions;
    public int PlayerStaminaPotions
    {
        get { return playerStaminaPotions; }
        set { playerStaminaPotions = value; }
    }

    public QuestGiver questGiver;
    public Quest currentQuest;
    public bool isQuestInProgress;
    public bool caravanArrived;
    public bool isDefendingCaravan;

    public MapKingdomManager kingdomManager;
    public GameObject currentCity;
    public ClickMove mapPlayer;
    public int currentCityNumber;
    public string currentCityKingdom;
    public bool inBattleForCity;

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
        //kingdomManager = FindObjectOfType<MapKingdomManager>();
        questGiver = FindObjectOfType<QuestGiver>();
        mapPlayer = FindObjectOfType<ClickMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
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
            //kingdomManager = FindObjectOfType<MapKingdomManager>();
            questGiver = FindObjectOfType<QuestGiver>();
            mapPlayer = FindObjectOfType<ClickMove>();
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
        if (CombatManager.instance != null)
        {
            if (CombatManager.instance.wonBattle && SceneManager.GetActiveScene().name == "Map Scene")
            {
                MapKingdomManager.instance.captureCity(currentCityNumber, currentCityKingdom);
                CombatManager.instance.wonBattle = false;
            }
        }
    }

    public void AddMoneyToPlayer(int amount)
    {
        if (playerMoney != null)
        {
            playerMoney.SetCoinCount(amount);
            playerMoneyValue += amount;
        }
    }

    public void AddHealthPotions(int amount)
    {
        playerPotions.AddHealthPotions(amount);
        playerHealthPotions += amount;
    }

    public void AddStaminaPotions(int amount)
    {
        playerPotions.AddStaminaPotions(amount);
        playerStaminaPotions += amount;
    }

    public void AddCompanion()
    {
        int Randomizer = Random.Range(1, 20);
        if (Randomizer > 0 && Randomizer < 14)
        {
            AllyCombatManager.instance.RecruitMeleeCompanion();
        }
        else
        {
            AllyCombatManager.instance.RecruitRangedCompanion();
        }
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
