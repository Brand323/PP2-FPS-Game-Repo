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
        if (race == Race.Elf)
        { HP *= 1; Speed *= 1.5f; Dmg *= 1; AttackRate *= 1.2f; }

        currentTarget = AllyCombatManager.instance.TargetEnemy();
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
                    StartCoroutine(Shoot());
                }
            }
            else
            {
                currentTarget = AllyCombatManager.instance.TargetEnemy();
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
