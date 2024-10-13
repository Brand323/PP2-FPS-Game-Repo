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
        }

        playerSpawnPosition = GameObject.FindWithTag("Player Spawn Position");
        player.AddComponent<money>();
        playerMoney = player.GetComponent<money>();
        playerPotions = player.GetComponent<PotionManager>();
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

    public void PauseGame(GameObject window)
    {
        UIManager.instance.PauseGame(window);
    }

    public void UnpauseGame()
    {
        UIManager.instance.UnpauseGame();
    }

    //public void StartNextWave()
    //{
    //    WaveManager.instance.StartNextWave();
    //}

    //public void EnemyDefeated()
    //{
    //    WaveManager.instance.EnemyDefeated();
    //}


    //public void UpdateGameGoal(int enemy)
    //{
    //    UIManager.instance.UpdateGameGoal(enemy);
    //}

    public void LoseUpdate()
    {
        UIManager.instance.LoseUpdate();
    }
}
