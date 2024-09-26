using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour, I_Damage
{
    [SerializeField] Renderer model;
    [SerializeField] protected UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] Transform headPos;
    [SerializeField] int faceTargetSpeed;
    public GameObject coinPrefab;
    public GameObject healthPotion;
    public GameObject staminaPotion;
    GameObject nextPotion = null;

    [Range(0, 50)][SerializeField] float HP;
    bool playerInRange;
    Color colorOrig;
    Vector3 playerDir;
    [Range(1, 20)] float stopDistOrig;
    [Range(0, 12)] float speedOrig;
    float angularSpeedOrig;

    bool isEngaged;

    // Start is called before the first frame update
    void Start()
    {
        colorOrig = model.material.color;
        gameManager.instance.UpdateGameGoal(1);

    }
    //do speed orig in the enemytype
    // Update is called once per frame
    void Update()
    {
    } 
    public void Movement()
    {
        if (playerInRange)
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
        if (HP <= 0)
        {

            // Destroys the enemy if it reaches 0 HP and updates the winning condition
        }
        isEngaged = true;
        agent.stoppingDistance = stopDistOrig;
    }

    public virtual void Death()
    {
        if (isEngaged)
        {
            CombatManager.instance.attackingPlayerCurr--;
        }
        //Sets coin spawn amount on enemy death
        int coinCount = Random.Range(1, 4);
        float randomValue = Random.value;
        for (int i = 0; i < coinCount; i++)
        {
            if (randomValue < 0.4f)
            {
                //spawns in potion
                if (randomValue < 0.5f)
                {
                    nextPotion = healthPotion;
                }
                else
                {
                    nextPotion = staminaPotion;
                }

                Vector3 potionSpawnPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y + 1f, transform.position.z + Random.Range(-2f, 2f));
                Instantiate(nextPotion, potionSpawnPosition, Quaternion.identity);
            }
            else
            {
                 //spawns in coins
                Vector3 coinSpawnPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y + 1f, transform.position.z + Random.Range(-2f, 2f));
                Quaternion coinRotation = Quaternion.Euler(0, 0, 0);
                Instantiate(coinPrefab, coinSpawnPosition, coinRotation);
                
            }
        }

        gameManager.instance.EnemyDefeated();
        Destroy(gameObject);
    }

    // Getters
    public bool GetPlayerInRange(){ return playerInRange; }

    public float GetHP(){ return HP; }

    public NavMeshAgent GetAgent(){ return agent; }

    public int GetFaceTargetSpeed(){ return faceTargetSpeed; }

    public float GetSpeedOrig() { return speedOrig; }

    public float GetAngularSpeedOrig() { return angularSpeedOrig; }
    // Setters
    public void SetFaceTargetSpeed(int amount){ faceTargetSpeed = amount; }

    public void SetPlayerInRange(bool value){ playerInRange = value; }

    public void SetHP(float value) { HP = value; }

}
