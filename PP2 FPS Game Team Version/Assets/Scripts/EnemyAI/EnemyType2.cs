using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyType2 : BasicEnemyAI
{
    [Header("Model")]
    [SerializeField] Transform shootPos;
    [Header("Stats")]
    [Range(2, 10)] [SerializeField] float shootRate;
    [Header("Prefab")]
    [SerializeField] GameObject fireBall;



    Animator anim;
    bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        adjustForDifficulty();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetAgent();
        float agentSpeed = agent.velocity.normalized.magnitude;
        float animSpeed = anim.GetFloat("Speed");
        anim.SetFloat("Speed", Mathf.Lerp(animSpeed, agentSpeed, Time.deltaTime * GetAnimationSpeed()));
        Movement();
        if (!isShooting && GetPlayerInRange())
        {
            StartCoroutine(shoot());
        }

        if (GetHP() <= 0)
        {
            agent.velocity = Vector3.zero;
            SetPlayerInRange(false);
            SetFaceTargetSpeed(0);
            anim.SetTrigger("Death");
        }
    }
    public void adjustForDifficulty()
    {
        if (CombatManager.instance != null)
        {
            if (CombatManager.instance.GetDifficulty() == 2)
            {
                SetHP(GetHP() + 1);
                shootRate *= 0.9f;
                
            }
            else if (CombatManager.instance.GetDifficulty() == 3)
            {
                SetHP(GetHP() + 2);
                agent.speed *= 1.2f;
                agent.angularSpeed *= 1.2f;
                shootRate *= 0.7f;
            } else if (CombatManager.instance.GetDifficulty() >= 4)
            {
                SetHP(GetHP() + 2);
                agent.speed *= 1.4f;
                agent.angularSpeed *= 1.4f;
                shootRate *= 0.4f;
            }
        }
    }
    IEnumerator shoot()
    {
        isShooting = true;
        // Creates the fireBall and launches it forward
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    void CreateFireBall()
    {
        Instantiate(fireBall, shootPos.position, transform.rotation);
    }
}
