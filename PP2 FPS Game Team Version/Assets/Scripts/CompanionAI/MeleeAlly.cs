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
        // Adds companion to the Combat Manager's list
        AllyCombatManager.instance.companionList.Add(gameObject);
        AllyCombatManager.instance.allyArmySize += 1;

        currentTarget = AllyCombatManager.instance.TargetEnemy();
        weaponAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            currentTarget = AllyCombatManager.instance.TargetEnemy();
        }

        CombatMovement();

        if (enemyInRange)
        {
            if (currentTarget != null)
            {
                float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (distance <= AttackRange)
                {
                    if (!isAttacking)
                    {
                        StartCoroutine(Attack());
                    }
                }
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