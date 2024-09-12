using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : BasicEnemyAI
{
    [SerializeField] float attackRate;
    bool isAttacking;
    [SerializeField] float meleeRange;
    bool temporaryWeaken=true;
    bool temporaryStrengthen=true;
    // Start is called before the first frame update
    void Start()
    {

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
            if (GetHP() <=5&& temporaryStrengthen) 
            { 
                Strengthen();
                temporaryStrengthen = false;
            } else if (GetHP()<=2&&temporaryWeaken)
            {
                Weaken();
                Weaken();
                Weaken();
                temporaryWeaken = false;
            }

        }
    }
    IEnumerator attack()
    {
        isAttacking = true;
        gameManager.instance.playerScript.currentHealth -= 2;
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;

    }
    public void Strengthen()
    {
        if (GetAgent().speed <= 6)
        {
            GetAgent().speed = GetAgent().speed * 1.2f;
            GetAgent().angularSpeed = GetAgent().angularSpeed * 1.2f;
        }
        if (attackRate > .5)
        {
            attackRate *= 0.8f;
        }
        {

        }
    }

    public void Weaken()
    {
        if (GetAgent().speed >= 1.5)
        {
            GetAgent().speed = GetAgent().speed * 0.8f;
            GetAgent().angularSpeed = GetAgent().angularSpeed * 0.8f;

        }
        if (attackRate < 3)
        {
            attackRate *= 1.2f;
        }

    }

}
