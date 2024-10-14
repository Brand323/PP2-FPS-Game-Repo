using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyMovement : MonoBehaviour
{
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform headPos;
    [SerializeField] float moveSpeed;
    [SerializeField] int faceTargetSpeed;

    Vector3 enemyPos;

    GameObject currentTarget;

    bool enemyInRange;
    bool isEngaged;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Movement()
    {
        // Move towards enemy army general position
        if (enemyInRange)
        {
            // Ally engages enemy if in range
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.CompareTag("Enemy") && !enemyInRange)
        {
            enemyInRange = true;
            currentTarget = other.gameObject;

            if (!isEngaged)
            {
                // Update Combat Manager
            }
        }
    }
}
