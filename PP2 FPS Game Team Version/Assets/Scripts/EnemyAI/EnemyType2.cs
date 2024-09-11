using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : BasicEnemyAI
{
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform shootPos;
    [SerializeField] float shootRate;

    bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        
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

    IEnumerator shoot()
    {
        isShooting = true;
        // Creates the fireBall and launches it forward
        Instantiate(fireBall, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
