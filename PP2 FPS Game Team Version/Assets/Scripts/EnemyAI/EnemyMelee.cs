using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : BasicEnemyAI
{

    protected Animator weaponAnimator;
    [SerializeField] private float dmgAmount;
    [SerializeField] Collider meleeCol;
    [SerializeField] float meleeRange;
    bool hasDamaged=false;
    // Start is called before the first frame update
    void Start()
    {
        colorOrig = model.material.color;
        stopDistOrig = agent.stoppingDistance;
        angularSpeedOrig = agent.angularSpeed;
        speedOrig = agent.speed;
        detectionCol = GetComponentInChildren<SphereCollider>();
        // CombatManager.instance.enemiesExisting++;
        weaponAnimator = GetComponentInChildren<Animator>();
        if (meleeCol != null) { 
            meleeCol.enabled = false;
        }
        weaponAnimator.Play("TestSwordIdle");
    }
    //do speed orig in the enemytype
    // Update is called once per frame
    void Update()
    {
        Movement();
        if (targetInRange)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance <= meleeRange)
            {
                if (!isAttacking)
                {
                    StartCoroutine(attack());
                }
            }
            else
            {
                
            }
        }
    }
   private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (meleeCol.enabled && !hasDamaged)
        {

            if (other.CompareTag("Player") || other.CompareTag("Companion"))
            {
                I_Damage dmg = GetComponent<I_Damage>();
                if (dmg != null)
                {
                    dmg.TakeDamage(dmgAmount);
                    hasDamaged = true;
                    meleeColOff();
                }
            }
        }
    }
    IEnumerator attack()
    {
        isAttacking = true;
        //weaponAnimator.SetTrigger("TestSwordAnim");
        TriggerSwordAttack();
        yield return new WaitForSeconds(attackRate);
        hasDamaged = false;
        meleeColOn();
        hasDamaged = false;
       // yield return new WaitForSeconds(attackRate);
        isAttacking = false;
        weaponAnimator.Play("TestSwordIdle");
    }
    public void meleeColOn()
    {
        meleeCol.enabled = true;
    }
    public void meleeColOff()
    {
        meleeCol.enabled = false;
    }
    public void TriggerSwordAttack()
    {
        if (weaponAnimator != null)
        {
            // Trigger the sword swing animation
            weaponAnimator.Play("TestSwordAnim");
            //weaponAnimator.SetTrigger("TestSwordAnim");
        }
        else
        {
            Debug.Log("No animator component found or cannot attack...");
        }
    }
    public override void Death()
    {
        Destroy(gameObject);
    }


}
