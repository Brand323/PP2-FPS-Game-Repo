using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour, I_Damage
{


    [SerializeField] Renderer model;
    [SerializeField] protected UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] Transform headPos;
    Color colorOrig;


    [SerializeField] int faceTargetSpeed;
    Vector3 playerDir;
    Vector3 startingPos;


    [Range(1, 50)][SerializeField] float HP;

    bool playerInRange;
    bool isEngaged;


    [SerializeField] int viewAngle;
    [SerializeField] int roamDist;
    [SerializeField] int roamTimer;
    float stopDistOrig;
    float angleToPlayer;
    bool isRoaming;
    private Coroutine coroutine;


    [SerializeField] int animSpeedTrans;

 
    public GameObject coinPrefab;


    // Start is called before the first frame update
    void Start()
    {
        colorOrig = model.material.color;
        gameManager.instance.UpdateGameGoal(1);
        stopDistOrig = agent.stoppingDistance;
        startingPos = transform.position;

    }
    //do speed orig in the enemytype
    // Update is called once per frame
    void Update()
    {
    } 
    public void Movement()
    {
        if (playerInRange && CanSeePlayer())
        {
            playerDir = gameManager.instance.player.transform.position - headPos.position;
            agent.SetDestination(gameManager.instance.player.transform.position);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
            //if there is attack slot and they are not attacking, attack
            stopDistOrig=agent.stoppingDistance;
            if (CombatManager.instance.attackingPlayerCurr < CombatManager.instance.GetAttackingPlayerMax()&&!isEngaged)
            {
                if (this is not EnemyType2)
                {
                    agent.stoppingDistance = stopDistOrig;
                    isEngaged = true;
                    CombatManager.instance.attackingPlayerCurr++;
                }
            }
        }
        // Starts the roaming coroutine
        else if (playerInRange && !CanSeePlayer())
        {
            if (!isRoaming && agent.remainingDistance < 0.05f && coroutine == null)
                coroutine = StartCoroutine(Roam());
        }
        else if (!playerInRange)
        {
            if (!isRoaming && agent.remainingDistance < 0.05f && coroutine == null)
                coroutine = StartCoroutine(Roam());
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.isTrigger)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            //engage enemy
            if (CombatManager.instance.attackingPlayerCurr < CombatManager.instance.GetAttackingPlayerMax()&&!isEngaged)
            {
                if (this is not EnemyType2)
                {
                    isEngaged = true;
                    CombatManager.instance.attackingPlayerCurr++;
                }


            }
            //disengage enemy
            else if (CombatManager.instance.attackingPlayerCurr >= CombatManager.instance.GetAttackingPlayerMax()&&!isEngaged)
            {
                isEngaged = false;
                agent.stoppingDistance = 10;
                if (this is not EnemyType2)
                {
                    isEngaged = false;
                    agent.stoppingDistance = 7;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Collider sphereCollider = other;
        if (other.CompareTag("Player") && other != null && other.name == "Sphere Collider")
        {
            playerInRange = false;
            if (isEngaged)
            {
                CombatManager.instance.attackingPlayerCurr--;
                isEngaged=false;
            }
        }
    }
    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }
    public void TakeDamage(float amount)
    {
        HP -= amount;
        playerInRange = true;
        Movement();
        isEngaged = true;
        agent.stoppingDistance = stopDistOrig;
    }

    IEnumerator Roam()
    {
        isRoaming = true;
        yield return new WaitForSeconds(roamTimer);

        agent.stoppingDistance = 0;
        Vector3 randomPos = Random.insideUnitSphere * roamDist;
        randomPos += startingPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, roamDist, 1);
        agent.SetDestination(hit.position);

        isRoaming = false;
        coroutine = null;
    }

    bool CanSeePlayer()
    {
        playerDir = gameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);

        Debug.DrawRay(headPos.position, playerDir);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewAngle)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }

                agent.stoppingDistance = stopDistOrig;
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }

    public virtual void Death()
    {
        if (isEngaged)
        {
            CombatManager.instance.attackingPlayerCurr--;
        }
        //Sets coin spawn amount on enemy death
        int coinCount = Random.Range(1, 4);

        for (int i = 0; i < coinCount; i++)
        {
        //Changes coins position
        Vector3 coinSpawnPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y + 1f, transform.position.z + Random.Range(-2f, 2f));
        Quaternion coinRotation = Quaternion.Euler(0, 0, 0);
        Instantiate(coinPrefab, coinSpawnPosition, coinRotation);
        }

        gameManager.instance.EnemyDefeated();
        Destroy(gameObject);
    }

    // Getters
    public bool GetPlayerInRange(){ return playerInRange; }

    public float GetHP(){ return HP; }

    public int GetAnimationSpeed() { return animSpeedTrans; }

    public NavMeshAgent GetAgent(){ return agent; }

    public int GetFaceTargetSpeed(){ return faceTargetSpeed; }

    // Setters
    public void SetFaceTargetSpeed(int amount){ faceTargetSpeed = amount; }

    public void SetPlayerInRange(bool value){ playerInRange = value; }

    public void SetHP(float value) { HP = value; }

    public void SetAnimationSpeed(int value) { animSpeedTrans = value; }

}
