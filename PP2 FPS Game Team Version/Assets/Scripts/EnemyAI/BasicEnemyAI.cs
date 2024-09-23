using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour, I_Damage
{
    [SerializeField] Renderer model;
    [SerializeField] protected UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] Transform headPos;
    [SerializeField] int faceTargetSpeed;
    public GameObject coinPrefab;

    [SerializeField] float HP;
    bool playerInRange;
    Color colorOrig;
    Vector3 playerDir;
    float stopDistOrig;
    float speedOrig;
    float angularSpeedOrig;

    bool isEngaged;

    // Start is called before the first frame update
    void Start()
    {
        colorOrig = model.material.color;
        gameManager.instance.UpdateGameGoal(1);
        stopDistOrig = agent.stoppingDistance;
        speedOrig = agent.speed;
        angularSpeedOrig = agent.angularSpeed;
    }

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
            if (CombatManager.instance.attackingPlayerCurr < CombatManager.instance.GetAttackingPlayerMax()&&!isEngaged)
            {
                agent.stoppingDistance=stopDistOrig;
                isEngaged=true;
                CombatManager.instance.attackingPlayerCurr++;
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
            if (CombatManager.instance.attackingPlayerCurr > CombatManager.instance.GetAttackingPlayerMax())
            {
                isEngaged = true;
                CombatManager.instance.attackingPlayerCurr++;
            }
            //disengage enemy
            if (CombatManager.instance.attackingPlayerCurr == CombatManager.instance.GetAttackingPlayerMax())
            {
                isEngaged = false;
                agent.stoppingDistance = 10;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
    //    SphereCollider sphereCollider = other as SphereCollider;
        Collider sphereCollider = other;
        if (other.CompareTag("Player")&& sphereCollider != null)
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
            if (isEngaged)
            {
                CombatManager.instance.attackingPlayerCurr--;
            }
            Vector3 coinSpawnPosition = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
            Quaternion coinRotation = Quaternion.Euler(0, 0, 0);
            Instantiate(coinPrefab, coinSpawnPosition, coinRotation);
            

            gameManager.instance.EnemyDefeated();
            // Destroys the enemy if it reaches 0 HP and updates the winning condition
        }
    }

    public void Death()
    {
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
