using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    [SerializeField] Renderer model;
    [SerializeField] UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;

    [SerializeField] int HP;

    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;

    Color colorOrig;

    bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        colorOrig = model.material.color;
        gameManager.instance.UpdateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        // Follows the player
        agent.SetDestination(gameManager.instance.player.transform.position);

        // Shoots at all times
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

    public void takeDamage(int amount)
    {
        HP -= amount;

        if (HP <= 0)
        {
            // Destroys the enemy if it reaches 0 HP and updates the winning condition
            Destroy(gameObject);
            gameManager.instance.UpdateGameGoal(-1);
        }

        StartCoroutine(flashColor());
    }

    IEnumerator flashColor()
    {
        // Turns the enemy model red for a milisecond
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }
}
