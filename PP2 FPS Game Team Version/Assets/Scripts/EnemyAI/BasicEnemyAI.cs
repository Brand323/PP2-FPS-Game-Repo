using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour, I_Damage
{
    [SerializeField] Renderer model;
    [SerializeField] UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] Transform headPos;
    [SerializeField] int faceTargetSpeed;

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
        // Follows the player

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
        if (other.CompareTag("Player"))
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
            gameManager.instance.EnemyDefeated();
            // Destroys the enemy if it reaches 0 HP and updates the winning condition
            Destroy(gameObject);
            gameManager.instance.UpdateGameGoal(-1);
        }

        StartCoroutine(flashColor());
    }

    public IEnumerator flashColor()
    {
        // Turns the enemy model red for a milisecond
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }
    public bool GetPlayerInRange()
    {
        return playerInRange;
    }
    public float GetHP()
    {
        return HP;
    }
    public NavMeshAgent GetAgent()
    {
        return agent;
    }
    public int GetFaceTargetSpeed()
    {
        return faceTargetSpeed;
    }
}
