using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyType3 : BasicEnemyAI
{
    [Range(1.5f, 5)] [SerializeField] float attackRate;
    [SerializeField] float meleeRange;
    [SerializeField] GameObject Golem;
    [SerializeField] int animSpeedTrans;
    [SerializeField] bool canSplit = true;

    Animator anim;
    bool isAttacking;
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
        //player position needs to be updated to the AI
        if (GetPlayerInRange())
        {
            float distance = Vector3.Distance(transform.position, gameManager.instance.player.transform.position);
            if (distance <= meleeRange)
            {
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
    public void Split()
    {
     
    }
    public override void Death() {

        if(canSplit == true )
        {
            //Seperates golem
            Vector3 spawnOffset = new Vector3(1f, 0f, 0f);

            GameObject miniGolem1 = Instantiate(Golem, transform.position + spawnOffset, transform.rotation);
            GameObject miniGolem2 = Instantiate(Golem, transform.position - spawnOffset, transform.rotation);

            // Scale down golam size
            miniGolem1.transform.localScale *= 0.5f;
            miniGolem2.transform.localScale *= 0.5f;

            miniGolem1.GetComponent<EnemyType3>().canSplit = false;
            miniGolem2.GetComponent<EnemyType3>().canSplit = false;

        }

        Destroy(gameObject);
        
    }
    public void adjustForDifficulty()
    {
        if (CombatManager.instance != null)
        {
            if (CombatManager.instance.GetDifficulty() == 2)
            {
                SetHP(GetHP() + 3);

            }
            else if (CombatManager.instance.GetDifficulty() == 3)
            {
                SetHP(GetHP() + 6);
                agent.speed *= 1.3f;
                agent.angularSpeed *= 1.3f;
            }
            else if (CombatManager.instance.GetDifficulty() >= 4)
            {
                SetHP(GetHP() + 9);
                agent.speed *= 1.5f;
                agent.angularSpeed *= 1.5f;
            }
        }
    }

    void PunchDamage()
    {
        gameManager.instance.playerScript.currentHealth -= (CombatManager.instance.GetDifficulty() + 1);
    }
}
