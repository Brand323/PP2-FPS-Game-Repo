using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyBase : MonoBehaviour, I_Damage
{
    [Header("Stats")]
    [Range(0,20)][SerializeField] protected float HP = 10;
    [Range(0, 20)][SerializeField] protected float Speed = 4;
    [Range(0,5)][SerializeField] protected float Dmg = 1;
    [SerializeField] protected float AttackRate = 1;
    [SerializeField] protected float AttackRange = 2;

    [Header("Model")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform headPos;

    [Header("Movement")]
    [SerializeField] int faceTargetSpeed;

    protected Vector3 rotDir;

    protected GameObject currentTarget;
    protected Animator weaponAnimator;

    protected bool enemyInRange;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void CombatMovement()
    {
        if ((enemyInRange && currentTarget != null) || (!enemyInRange && currentTarget != null))
        {
            // Moves the companion towards the enemy position
            rotDir = currentTarget.transform.position - headPos.position;
            agent.SetDestination(currentTarget.transform.position);

            // If target is too close to move, then rotate to face the target
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
        else
        {
            // If no enemy in range and no current target, then move foward
            agent.SetDestination(transform.position + transform.forward * agent.speed * Time.deltaTime);
        }
    }

    void FaceTarget()
    {
        // Rotates towards target
        Quaternion rot = Quaternion.LookRotation(rotDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.CompareTag("Enemy") && !enemyInRange)
        {
            enemyInRange = true;
            currentTarget = other.gameObject;
        }
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;

        int randomOption = Random.Range(1, 4);

        switch (randomOption)
        {
            case 1:
                AudioManager.instance.playSound(AudioManager.instance.CompanionHit1, AudioManager.instance.sfxVolume);
                break;
            case 2:
                AudioManager.instance.playSound(AudioManager.instance.CompanionHit2, AudioManager.instance.sfxVolume);
                break;
            case 3:
                AudioManager.instance.playSound(AudioManager.instance.CompanionHit3, AudioManager.instance.sfxVolume);
                break;
        }

        if (HP <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject); 
    }

    // Getters

    public float Damage { get { return Dmg; } }
    public GameObject Target { get { return currentTarget; } }
}
