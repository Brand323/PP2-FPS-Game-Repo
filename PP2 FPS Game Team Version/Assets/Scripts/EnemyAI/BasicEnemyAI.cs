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

    protected bool targetInRange;
    protected Color colorOrig;
    protected Vector3 targetDir;
    public float stopDistOrig;
    [Range(0, 12)] public float speedOrig;
    public float angularSpeedOrig;
    public bool isAttacking;
    public float attackRate;
   
    public bool isEngaged;
    protected GameObject target;

    public Collider detectionCol;
    // Start is called before the first frame update
    void Start()
    {
        colorOrig = model.material.color;
        CombatManager.instance.enemiesExisting++;
    }
    void Update()
    {
    } 
    //moves to target
    public void Movement()
    {
        if (targetInRange)
        {
            if (target != null) {
                targetDir = target.transform.position - headPos.position;
                agent.SetDestination(target.transform.position);
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }
        }
    }
    //sees what the target is
    protected void OnTriggerEnter(Collider other)
    {

        if (other.isTrigger)
        {
            return;
        }
        if (other.CompareTag("Player") || other.CompareTag("Companion"))
        {
            targetInRange = true;
            target = other.gameObject;
        }
        else
        {
            Debug.Log("Not a target: " + other.name);
        }
    }
    //does not work properly for now.
    private void OnTriggerExit(Collider other)
    {
        
        if (other == detectionCol)
        {
            if ((other.CompareTag("Player") || other.CompareTag("Companion")) && other is SphereCollider)
            {
                targetInRange = false;
                if (isEngaged)
                {
                    CombatManager.instance.attackingPlayerCurr--;
                    isEngaged = false;
                }
            }
        }
    }
    //faces target if they are too close to move
    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }
    //takes damage
    public void TakeDamage(float amount)
    {
        HP -= amount;
        targetInRange = true;
        Movement();
        if (HP <= 0)
        {
            this.Death();
        }
        agent.stoppingDistance = stopDistOrig;
    }
    //dies
    public virtual void Death()
    {
        CombatManager.instance.enemiesExisting--;
        Destroy(gameObject);
    }
    //drop loot, coins and potions
    public virtual void DropLoot()
    {

    }
    void WalkBob()
    {

    }
    // Getters
    public bool GetPlayerInRange(){ return targetInRange; }

    public float GetHP(){ return HP; }

    public NavMeshAgent GetAgent(){ return agent; }

    public int GetFaceTargetSpeed(){ return faceTargetSpeed; }

    public float GetSpeedOrig() { return speedOrig; }

    public float GetAngularSpeedOrig() { return angularSpeedOrig; }
    // Setters
    public void SetFaceTargetSpeed(int amount){ faceTargetSpeed = amount; }

    public void SetPlayerInRange(bool value){ targetInRange = value; }

    public void SetHP(float value) { HP = value; }

}
