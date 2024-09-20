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

    // Start is called before the first frame update
    void Start()
    {
        colorOrig = model.material.color;
        gameManager.instance.UpdateGameGoal(1);
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        SphereCollider sphereCollider = other as SphereCollider;

        if (other.CompareTag("Player")&& sphereCollider != null)
        {
            playerInRange = false;
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

        if (HP <= 0)
        {
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

    // Setters
    public int SetFaceTargetSpeed(int amount){ return faceTargetSpeed = amount; }

    public bool SetPlayerInRange(bool value){ return playerInRange = value; }
}
