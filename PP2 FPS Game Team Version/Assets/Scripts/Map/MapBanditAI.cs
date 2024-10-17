using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MapBanditAI : MonoBehaviour
{
    public float roamRadius = 50f;
    public int minArmySize = 1;
    public int maxArmySize = 10;
    public TextMeshProUGUI banditArmySizeText;

    private NavMeshAgent agent;
    private int banditArmySize;
    public BanditSpawner spawner;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Roam();
        //Repeats roam evey 10 seconds
        InvokeRepeating("Roam", 5f, 10f);

        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            GameObject mapCameraObject = GameObject.Find("Map Camera");

            if (mapCameraObject != null)
            {
                Camera mapCamera = mapCameraObject.GetComponent<Camera>();
                if (mapCamera != null)
                {
                    canvas.worldCamera = mapCamera;
                    //    Debug.Log("Assigned Map Camera to the World Space Canvas");
                }
                else
                {
                    Debug.LogError("Map Camera component not found on 'Map Camera' GameObject!");
                }
            }
            else
            {
                Debug.LogError("Map Camera not found in the scene!");
            }
        }
        else
        {
            Debug.LogError("Canvas not found in the enemy prefab!");
        }

        banditArmySizeText = GetComponentInChildren<TextMeshProUGUI>();

        UpdateArmySizeUI();
    }

    public void SetArmySize(int size)
    {
        banditArmySize = size;
        // Debug.Log("Army Size set to: " + armySize);
        UpdateArmySizeUI();
    }

    void UpdateArmySizeUI()
    {
        if (banditArmySizeText != null)
        {
            banditArmySizeText.text = banditArmySize.ToString();
            // Debug.Log("Army Size Text Updated: " + armySizeText.text);
        }
        else
        {
            // Debug.Log("armySizeText is null");
        }
    }

    void Roam()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);

        agent.SetDestination(hit.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MapPlayer"))
        {
            CombatManager.instance.enemyArmySize = banditArmySize;
            SceneManager.LoadScene("CombatSceneArctic");
            CombatManager.instance.CheckToSpawn();
        }
    }
    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.RemoveBandit();
        }
    }
}
