using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapEnemyAi : MonoBehaviour
{

    [Header("Roaming Settings")]
    [SerializeField] float roamRadius = 1f;
    [SerializeField] float roamTime = 1f;
    [SerializeField] float returnSpeed = 1f;

    private NavMeshAgent agent;
    private Transform homeBase;
    private float roamTimer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        roamTimer = roamTime;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += transform.position;



        }
    }

    void ReturnHome()
    {
        
    }








    public void SetHomeBase(Transform home)
    {
        homeBase = home;
    }





}
