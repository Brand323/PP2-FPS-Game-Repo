using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    //Wave Variables
    public GameObject enemyType1Prefab;
    public GameObject enemyType2Prefab;
    public GameObject enemyType3Prefab;
    UnityEngine.Vector3 coordenates;
    public int currentWave = 0;
    public int enemiesPerWave;
    public int remainingEnemies;
    private bool waveInProgress = false;
    //public GameObject Lever;
    public int totalWaves = 3;

    //Gate variables
    public GameObject lever;
    public GateTrigger triggerGate;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        lever = GameObject.FindWithTag("Lever");
        if(lever != null)
        {
            triggerGate = lever.GetComponent<GateTrigger>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingEnemies <= 0 && waveInProgress)
        {
            EndWave();
        }
    }

    public void StartNextWave()
    {
        currentWave++;
        enemiesPerWave = enemiesPerWave * currentWave;
        if (currentWave == totalWaves)
        {
            //sets it to one for final boss
            enemiesPerWave = 1;
        }
        waveInProgress = true;
        StartCoroutine(spawnEnemies());
        UIManager.instance.UpdateWaveUI();
    }

    IEnumerator spawnEnemies()
    {
        for (int i = 1; i <= enemiesPerWave; i++)
        {
            spawnCheck();

            //Time between spawns
            yield return new WaitForSeconds(1f);
        }
    }

    void spawnCheck()
    {
        if (currentWave == totalWaves)
        {
            SpawnFinalEnemy();
        }
        else
        {
            SpawnEnemy();
        }
        remainingEnemies++;
    }

    void SpawnEnemy()
    {
        GameObject nextEnemy;
        float randomValue = Random.value;

        if (randomValue < 0.6f)
        {
            nextEnemy = enemyType1Prefab;
        }
        else
        {
            nextEnemy = enemyType2Prefab;
        }

        coordenates = new UnityEngine.Vector3(Random.Range(-100f, -70f), 1, Random.Range(-25f, 15f));
        Instantiate(nextEnemy, coordenates, UnityEngine.Quaternion.identity);
    }

    void SpawnFinalEnemy()
    {
        coordenates = new UnityEngine.Vector3(Random.Range(-100f, -70f), 1, Random.Range(-25f, 15f));
        Instantiate(enemyType3Prefab, coordenates, UnityEngine.Quaternion.identity);
    }

    void EndWave()
    {
        waveInProgress = false;

        triggerGate.EndWave();

        //Checks for Win
        if (currentWave >= totalWaves)
        {
            UIManager.instance.isPaused = !UIManager.instance.isPaused;
            UIManager.instance.PauseGame(UIManager.instance.winWindow);
        }
        UIManager.instance.activateWaveEndMenu();
    }


    public void EnemyDefeated()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            EndWave();
        }
    }
}
