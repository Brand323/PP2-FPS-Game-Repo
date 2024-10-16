using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAlly : AllyBase
{
    [SerializeField] enum Race { Elf }
    [SerializeField] Race race;

    [SerializeField] GameObject fireball;
    [SerializeField] Transform shootPos;

    bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        // Adds companion to the Combat Manager's list
        AllyCombatManager.instance.CompanionList.Add(gameObject);
        AllyCombatManager.instance.AllyArmySize += 1;

        currentTarget = AllyCombatManager.instance.TargetEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
            currentTarget = AllyCombatManager.instance.TargetEnemy();

        if (currentTarget == null)
            this.enabled = false;

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
                        StartCoroutine(Shoot());
                    }
                }
            }
        }
    }

    IEnumerator Shoot()
    {
        isAttacking = true;

        CreateFireBall();
        yield return new WaitForSeconds(AttackRate);

        isAttacking = false;
    }
    void CreateFireBall()
    {
        GameObject fireBallInstance = Instantiate(fireball, shootPos.position, transform.rotation);
        FollowProjectileAlly fireballScript = fireBallInstance.GetComponent<FollowProjectileAlly>();
        if (fireballScript != null)
        {
            // Pass the current target's transform to the fireball
            fireballScript.Target = currentTarget.transform;
            fireballScript.Damage = Dmg;
        }
    }
}
