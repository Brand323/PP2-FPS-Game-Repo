using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectileAlly : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotSpeed;
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - rb.position;
        Vector3 rot = Vector3.Cross(transform.forward, dir);
        rb.angularVelocity = rot * rotSpeed;
        rb.velocity = transform.forward * speed;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Destroy the fireball on contact with the enemy
            Destroy(gameObject);

            I_Damage damageable = other.GetComponent<I_Damage>();
            if (damageable != null)
            {
                damageable.TakeDamage(3);
            }
        }
    }

    // Setter

    public void SetTarget(Transform newTarget) { target = newTarget; }
}
