using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyType1 : BasicEnemyAI
{

    [SerializeField] float attackRate;
    [SerializeField] float meleeRange;
    [SerializeField] int animSpeedTrans;
    bool isAttacking;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        adjustForDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetAgent();
        float agentSpeed = agent.velocity.normalized.magnitude;
        float animSpeed = anim.GetFloat("Speed");
        anim.SetFloat("Speed", Mathf.Lerp(animSpeed, agentSpeed, Time.deltaTime * animSpeedTrans));
        Movement();

        // Checks is player is in attack range
        if (GetPlayerInRange())
        {
            // Calculates the distance between the enemy and the player
            float distance = Vector3.Distance(transform.position,gameManager.instance.player.transform.position);

            if (distance <= meleeRange) 
            {
                // In attack range
                if (!isAttacking) 
                {
                    StartCoroutine(attack());
                }

            }
        }

        if (GetHP() <= 0)
        {
            agent.velocity = Vector3.zero;
            SetPlayerInRange(false);
            SetFaceTargetSpeed(0);
            anim.SetTrigger("Death");
        }
    }
    IEnumerator attack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
    }
    //difficulty under 3 does not change damage done to player for this enemy. 
    //difficulty 4 and up doubles damage for this enemy.
    public void DoDamage()
    {
        if (CombatManager.instance.GetDifficulty()>=4) {
            gameManager.instance.playerScript.currentHealth-=2;

        }
        else
        {
            gameManager.instance.playerScript.currentHealth--;
        }
    }
    //increase health
    public void adjustForDifficulty()
    {
        if (CombatManager.instance != null)
        {
            if (CombatManager.instance.GetDifficulty()==2)
            {
                SetHP(GetHP()+1);
            } 
            else if (CombatManager.instance.GetDifficulty()<2){
                SetHP(GetHP() + 2);
                agent.speed = GetSpeedOrig()* 1.2f;
                agent.angularSpeed = GetAngularSpeedOrig() * 1.2f;
            }
        }
    }
}
