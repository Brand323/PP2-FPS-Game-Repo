using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapEnemyAi : MonoBehaviour
{

    [Header("Roaming Settings")]
    [SerializeField] float roamRadius = .4f;
    [SerializeField] float roamTime = .4f;
    [SerializeField] float returnSpeed = 20f;
    [SerializeField] float roamSpeed = 20f;

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
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
            Vector3 finalPosition = hit.position;

            agent.SetDestination(finalPosition);

        }
    }

    void ReturnHome()
    {
        agent.speed = returnSpeed;
        agent.SetDestination(homeBase.position);

        if(Vector3.Distance(transform.position, homeBase.position) < 1f)
        {
            roamTimer = roamTime;
            agent.speed = 1.5f;
        }
    }


    public void SetHomeBase(Transform home)
    {
        homeBase = home;
    }





}
