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
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void PauseGame(GameObject window)
    {
        UIManager.instance.PauseGame(window);
    }

    public void UnpauseGame()
    {
        UIManager.instance.UnpauseGame();
    }

    public void StartNextWave()
    {
        WaveManager.instance.StartNextWave();
    }

    public void EnemyDefeated()
    {
        WaveManager.instance.EnemyDefeated();
    }


    public void UpdateGameGoal(int enemy)
    {
        UIManager.instance.UpdateGameGoal(enemy);
    }

    public void LoseUpdate()
    {
        UIManager.instance.LoseUpdate();
    }


    public void activateItemUI(string message, GameObject window = null)
    {
        UIManager.instance.activateItemUI(message, window);
    }

    public void deactivateItemUI()
    {
        UIManager.instance.deactivateItemUI();
    }

    //Makes the text blink red
    public void BlinkRed()
    {
        StartCoroutine(UIManager.instance.BlinkRed());
    }

    public void deactivateWaveEndMenu()
    {
        UIManager.instance.deactivateWaveEndMenu();
    }
}
