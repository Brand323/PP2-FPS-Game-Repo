using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour, I_Damage
{
    [SerializeField] protected Renderer model;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Transform headPos;
    [SerializeField] protected int faceTargetSpeed;
    [SerializeField] protected enum Type
    {
        Normal,
        Dwarf,
        Elf,
        Ogre,
        Jelly
    }
    [SerializeField] protected Type type;
    [Range(0, 50)][SerializeField] float HP;
    

    public GameObject coinPrefab;
    public GameObject healthPotion;
    public GameObject staminaPotion;
    GameObject nextPotion = null;

    bool playerInRange;
    protected Color colorOrig;
    protected Vector3 playerDir;
    public float stopDistOrig;
    [Range(0, 12)] public float speedOrig;
    public float angularSpeedOrig;

    public bool isEngaged;

    // Start is called before the first frame update
    void Start()
    {
        colorOrig = model.material.color;
        CombatManager.instance.enemiesExisting++;
    }
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
            this.Death();
        }
        agent.stoppingDistance = stopDistOrig;
    }

    public virtual void Death()
    {
        CombatManager.instance.enemiesExisting--;
        Destroy(gameObject);
    }
    public virtual void DropLoot()
    {

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
