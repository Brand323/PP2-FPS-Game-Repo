using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damageAmount;
    public float speed = 20f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Arrow hit: {other.gameObject.name}");

        if (other.CompareTag("Enemy"))
        {
            I_Damage enemyDamage = other.GetComponent<I_Damage>();
            if (enemyDamage != null)
            {
                enemyDamage.TakeDamage(damageAmount);
                Debug.Log($"Arrow did {damageAmount} damage to {other.name}");
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Environment"))
        {
            Debug.Log("Arrow hit the environment and stuck.");
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            Destroy(gameObject, 2f);
        }
    }
}
