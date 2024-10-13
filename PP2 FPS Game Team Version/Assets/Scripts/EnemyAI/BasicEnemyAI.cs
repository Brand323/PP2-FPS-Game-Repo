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
    [Range(0, 50)] [SerializeField] float HP;
    [SerializeField] SphereCollider detectionCol;
    //lower is faster
    [Range(.1f, 10f)] [SerializeField] float detectTime;
    [SerializeField] float maxDetectionRange;

    public GameObject coinPrefab;
    public GameObject healthPotion;
    public GameObject staminaPotion;
    GameObject nextPotion = null;

    protected bool targetInRange;
    protected Color colorOrig;
    protected Vector3 targetDir;
    public float stopDistOrig;
    public float speedOrig;
    public float angularSpeedOrig;
    public bool isAttacking;
    public float attackRate;

    protected bool isExpanding = false;
    public bool isEngaged;
    protected GameObject target;


    protected Coroutine detectionCoroutine;
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
            if (detectionCoroutine != null)
            {
                StopCoroutine(detectionCoroutine);
                isExpanding = false;
            }
            detectionCol.radius = 1f;
        }
        else
        {
            Debug.Log("Not a target: " + other.name);
        }
    }
/*    private void OnTriggerExit(Collider other)
    {
        if (target != null)
        {
            if (distanceToTarget() >= detectionCol.radius)
            {
                if ((other.CompareTag("Player") || other.CompareTag("Companion")))
                {
                    targetInRange = false;
                }
                //StartCoroutine(detectTarget());
            }
        }
    }*/


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
    float distanceToTarget()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance;
    }
    protected IEnumerator detectTargetCoroutine()
    {
        Debug.Log("coroutine start***");
        if (isExpanding) {
            yield break;
        }
        isExpanding = true;
        float timeElapsed = 0f;
        while (timeElapsed<=detectTime) {
            detectionCol.radius = Mathf.Lerp(0.1f, maxDetectionRange, timeElapsed/detectTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        detectionCol.radius = maxDetectionRange;
        yield return new WaitForSeconds(.1f);
        isExpanding=false;
        detectionCoroutine = null;
        Debug.Log("coroutine end********");
    }
    protected void detectTarget()
    {
        if (detectionCoroutine != null)
        {
            StopCoroutine(detectionCoroutine);
        }
        if (detectionCoroutine == null)
        {
            detectionCoroutine = StartCoroutine(detectTargetCoroutine());
        }

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
