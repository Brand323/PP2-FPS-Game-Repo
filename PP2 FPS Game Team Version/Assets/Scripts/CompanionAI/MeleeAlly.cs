using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeAlly : AllyBase
{
    [SerializeField] enum Race { Normal, Ogre, Dwarf }
    [SerializeField] Race race;
    [SerializeField] Collider meleeCol;

    bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        if (race == Race.Normal)
        { HP *= 1; Speed *= 1; Dmg *= 1; AttackRate *= 1; }
        if (race == Race.Ogre)
        { HP *= 1.5f; Speed *= 1; Dmg *= 1.5f; AttackRate *= 0.7f; gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.3f, gameObject.transform.localScale.y * 1.3f, gameObject.transform.localScale.z * 1.3f); }
        if (race == Race.Dwarf)
        { HP *= 0.7f; Speed *= 1; Dmg *= 1.5f; AttackRate *= 0.7f; gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 0.5f, gameObject.transform.localScale.y * 0.5f, gameObject.transform.localScale.z * 0.5f); }

        currentTarget = AllyCombatManager.instance.TargetEnemy();
        weaponAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null || !currentTarget.activeInHierarchy)
        {
            currentTarget = AllyCombatManager.instance.TargetEnemy();
        }

        CombatMovement();

        if (enemyInRange)
        {
            if (currentTarget != null && currentTarget.activeInHierarchy)
            {
                float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (distance <= AttackRange && !isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                currentTarget = AllyCombatManager.instance.TargetEnemy();
            }
        }
    }

    IEnumerator Attack()
    {
        // Turns on the weapon collider
        ColliderOn();

        isAttacking = true;

        // Trigger the attack animation
        weaponAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(AttackRate);

        // Turns off the weapon collider
        ColliderOff();
        isAttacking = false;
    }

    public void ColliderOn()
    {
        meleeCol.enabled = true;
    }
    public void ColliderOff()
    {
        meleeCol.enabled = false;
    }
}