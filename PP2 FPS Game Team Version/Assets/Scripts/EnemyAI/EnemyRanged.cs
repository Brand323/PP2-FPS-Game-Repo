using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : BasicEnemyAI
{
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform shootPos;
    [Range(1, 10)][SerializeField] float shootRate;
    [SerializeField] int animSpeedTrans;
    [SerializeField] LayerMask ignoreMask;
    Animator anim;
    bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        CombatManager.instance.enemiesExisting++;
        colorOrig = model.material.color;
        stopDistOrig = agent.stoppingDistance;
        angularSpeedOrig = agent.angularSpeed;
        speedOrig = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null || (target != null && !target.activeSelf))
        {
            if (target != null && !target.activeSelf)
            {
                target = null;
            }
            if (detectionCoroutine == null)
            {
                detectTarget();
            }
            targetInRange = false;
        }
        Movement();
        if (targetInRange)
        {
            if (detectionCoroutine != null)
            {
                StopCoroutine(detectionCoroutine);
                detectionCoroutine = null;
                isExpanding = false;
            }
            if (target != null)
            {
                if (!isShooting)
                {
                    StartCoroutine(shoot());
                }
            }
        }
    }
    IEnumerator shoot()
    {
        isShooting = true;
        //anim.SetTrigger("Attack");
        CreateFireBall();
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
    void CreateFireBall()
    {
        GameObject fireBallInstance = Instantiate(fireBall, shootPos.position, transform.rotation);
        Rigidbody rb = fireBallInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = targetDir*fireBallInstance.GetComponent<Damage>().Speed;
        }


        //fireBall.GetComponent<Rigidbody>().velocity = (target.transform.position - transform.position).normalized * fireBall.GetComponent<Damage>().Speed;
        //RaycastHit hit;
/*        if (Physics.Raycast(shootPos.position, targetDir, out hit,500))
        {

        }*/


    }
}
