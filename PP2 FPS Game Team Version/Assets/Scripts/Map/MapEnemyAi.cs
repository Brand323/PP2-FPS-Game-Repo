using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MapEnemyAi : MonoBehaviour
{

    [Header("Roaming Settings")]
    [SerializeField] float roamRadius = 20f;
    [SerializeField] float roamTime = 10f;
    [SerializeField] float returnSpeed = 10f;
    [SerializeField] float roamSpeed = 5f;

    [Header("Map Boundaries")]
    [SerializeField] Vector2 mapMinBounds;
    [SerializeField] Vector2 mapMaxBounds;

    [Header("Army Settings")]
    public int armySize;
    [SerializeField] TextMeshProUGUI armySizeText;

    private NavMeshAgent agent;
    private Transform homeBase;
    private float roamTimer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        roamTimer = roamTime;

        if (agent != null)
        {
            agent.speed = roamSpeed;
        }


        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            // Find the Map Camera by name or tag
            GameObject mapCameraObject = GameObject.Find("Map Camera");  // Use the name of your Map Camera

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

        armySizeText = GetComponentInChildren<TextMeshProUGUI>();

        //if (armySizeText != null)
        //{
        //    Debug.Log("TextMeshPro component found in the prefab!");
        //}

        //Gets Mins and Max Of Map
        Collider mapCollider = GameObject.FindGameObjectWithTag("Map").GetComponent<Collider>();

        if (mapCollider != null)
        {
            Bounds mapBounds = mapCollider.bounds;

            mapMinBounds = new Vector2(mapBounds.min.x, mapBounds.min.z);
            mapMaxBounds = new Vector2(mapBounds.max.x, mapBounds.max.z);
        }

        UpdateArmySizeUI();

    }

    // Update is called once per frame
    void Update()
    {
        roamTimer -= Time.deltaTime;

        if(roamTimer >0 )
        {
            Roam();
        }
        else
        {
            ReturnHome();
        }
    }

    //void RoamingBrain()
    //{
    //    if()
    //    {
    //        Roam();
    //    }

    //    else
    //    {
    //        //travel to town
    //    }
    //}

    void Roam()
    {
        if (!agent.hasPath)
        {
            float randomX = Random.Range(-roamRadius, roamRadius);
            float randomZ = Random.Range(-roamRadius, roamRadius);
            Vector3 randomDirection = new Vector3(randomX, 0, randomZ);


            Vector3 targetPosition = transform.position + randomDirection;

            targetPosition.x = Mathf.Clamp(targetPosition.x, mapMinBounds.x, mapMaxBounds.x);
            targetPosition.z = Mathf.Clamp(targetPosition.z, mapMinBounds.y, mapMaxBounds.y);
            NavMeshHit hit;

            if (NavMesh.SamplePosition(targetPosition, out hit, roamRadius, NavMesh.AllAreas))
            {
              //  Debug.Log("Valid tearget Found" + hit.position);
                agent.SetDestination(hit.position);
            }
            else
            {
                Debug.Log("No Valid Target found");
            }
        }
    }

    void ReturnHome()
    {
        if (homeBase == null)
        {
            return;
        }

        agent.speed = returnSpeed;

        if (!agent.hasPath)
        {
        agent.SetDestination(homeBase.position);
        }

        if (Vector3.Distance(transform.position, homeBase.position) < 15f)
        {
            agent.ResetPath();

            roamTimer = roamTime;
            agent.speed = roamSpeed;

            Destroy(gameObject);
        }
    }


    public void SetHomeBase(Transform home)
    {
        homeBase = home;
    }

    public void SetArmySize(int size)
    {
        armySize = size;
       // Debug.Log("Army Size set to: " + armySize);
        UpdateArmySizeUI();
    }

    void UpdateArmySizeUI()
    {
        if (armySizeText != null)
        {
            armySizeText.text =  armySize.ToString();
           // Debug.Log("Army Size Text Updated: " + armySizeText.text);
        }
        else
        {
           // Debug.Log("armySizeText is null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MapPlayer"))
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.playSound(AudioManager.instance.mapTriggerSound, AudioManager.instance.sfxVolume);
                AudioManager.instance.fadeEnded = false;
                AudioManager.instance.fadeOut();
            }
            CombatManager.instance.enemyArmySize = armySize;
            SceneManager.LoadScene("CombatSceneArctic");
            CombatManager.instance.CheckToSpawn();
        }
    }
}
