using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : BasicEnemyAI
{
    [SerializeField] GameObject bullet;
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
        if (!isShooting)
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        isShooting = true;
        // Creates the bullet and launches it forward
        Instantiate(bullet, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
