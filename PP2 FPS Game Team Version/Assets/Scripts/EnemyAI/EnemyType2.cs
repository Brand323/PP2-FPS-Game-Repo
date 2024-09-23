using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : BasicEnemyAI
{
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform shootPos;
    [SerializeField] float shootRate;
    float shootRateOrig;

    bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        shootRateOrig = shootRate;
        adjustForDifficulty();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (!isShooting)
        {
            StartCoroutine(shoot());
        }
    }
    public void adjustForDifficulty()
    {
        if (CombatManager.instance != null)
        {
            if (CombatManager.instance.GetDifficulty() == 2)
            {
                SetHP(GetHP() + 1);
                shootRate = shootRateOrig * 0.9f;
                
            }
            else if (CombatManager.instance.GetDifficulty() == 3)
            {
                SetHP(GetHP() + 2);
                agent.speed *= 1.2f;
                agent.angularSpeed *= 1.2f;
                shootRate = shootRateOrig * 0.7f;
            } else if (CombatManager.instance.GetDifficulty() >= 4)
            {
                SetHP(GetHP() + 2);
                agent.speed = GetSpeedOrig()* 1.4f;
                agent.angularSpeed = GetAngularSpeedOrig()* 1.4f;
                shootRate = shootRateOrig * 0.4f;
            }
        }
    }
    IEnumerator shoot()
    {
        isShooting = true;
        // Creates the fireBall and launches it forward
        Instantiate(fireBall, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
