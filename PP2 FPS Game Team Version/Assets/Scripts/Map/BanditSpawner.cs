using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BanditSpawner : MonoBehaviour
{
    public GameObject banditPrefab;
    public float spawnInterval = 1f;
    public int maxBandits = 3;

    private int currentBandits = 0;

    void Start()
    {
        for (int i = 0; i < maxBandits; i++)
        {
            SpawnBandit();
        }

        //InvokeRepeating("SpawnBandit", spawnInterval, spawnInterval);
        //StartCoroutine(SpawnBanditRepeatedly());


    }

    void SpawnBandit()
    {
        if (currentBandits >= maxBandits) return;

        float randomX = Random.Range(-300f, 300f);
        float randomZ = Random.Range(-150f, 150f);

        Vector3 spawnPosition = new Vector3(randomX, 1, randomZ);

        GameObject bandit = Instantiate(banditPrefab, spawnPosition, Quaternion.identity);
        currentBandits++;

        MapBanditAI banditScript = bandit.GetComponent<MapBanditAI>();

        if (banditScript != null)
        {
            int randomArmySize = Random.Range(1, 11);
            banditScript.SetArmySize(randomArmySize);

            banditScript.spawner = this;
        }
    }

    public void RemoveBandit()
    {
        currentBandits--;
    }
    IEnumerator SpawnBanditRepeatedly()
    {
        while (true)
        {

            if (currentBandits < maxBandits)
            {
                SpawnBandit();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
