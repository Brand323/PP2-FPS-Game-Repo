using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : BasicEnemyAI
{
    [SerializeField] float attackRate;
    bool isAttacking;
    [SerializeField] float meleeRange;

    // Start is called before the first frame update
    void Start()
    {
        adjustForDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
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
    }
    IEnumerator attack()
    {
        isAttacking = true;
        gameManager.instance.playerScript.currentHealth -= (CombatManager.instance.GetDifficulty() + 1);
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;

    }
    public void Strengthen()
    {

    }

    public void Weaken()
    {

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
                agent.speed = GetSpeedOrig() * 1.5f;
                agent.angularSpeed = GetAngularSpeedOrig() * 1.5f;
            }
        }
    }
}
