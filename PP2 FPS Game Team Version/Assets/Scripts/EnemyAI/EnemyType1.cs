using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyType1 : BasicEnemyAI
{
    [SerializeField] float attackRate;
    bool isAttacking;
    [SerializeField] float meleeRange;

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
            float distance = Vector3.Distance(transform.position,gameManager.instance.player.transform.position);
            if (distance <= meleeRange) {
                if (!isAttacking) {
                    StartCoroutine(attack());
                }
                
            }

        }
    }
    IEnumerator attack()
    {
        isAttacking = true;
        gameManager.instance.playerScript.currentHealth--;
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;

    }




}
