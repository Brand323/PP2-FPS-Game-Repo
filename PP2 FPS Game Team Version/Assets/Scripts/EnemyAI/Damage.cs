using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Type of damage.
    [SerializeField] enum damageType { Magic, Ranged, Melee, Enviromental }
    [SerializeField] damageType type;
    [SerializeField] Rigidbody rigidBody;

    // Amount of damage to apply to the player.
    [SerializeField] float damageAmount = 1f;

    // In case of ranged object
    [SerializeField] float speed;
    [SerializeField] float destroyTime;

    // Getters
    public float DestroySpeed { get { return destroyTime; } }
    public float Speed { get { return speed; } }
    public Rigidbody RigidBody { get { return rigidBody; } }

    // Start is called before the first frame update
    void Start()
    {
        if (type == damageType.Ranged)
        {
            rigidBody.velocity = transform.forward * speed;
            Destroy(gameObject, destroyTime);
        }
    }

    // Detect player collision.
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.CompareTag("Player")||other.CompareTag("Companion"))
        {
            // Do the damage.
            I_Damage dmg = other.GetComponent<I_Damage>();
            if (dmg != null)
            {
                dmg.TakeDamage(damageAmount);
            }
            //once damage is done, disable collider until next animation.
            GetComponent<BoxCollider>().enabled = false;
            
        }
        if (type == damageType.Ranged)
        {
            Destroy(gameObject);
        }
    }

}
