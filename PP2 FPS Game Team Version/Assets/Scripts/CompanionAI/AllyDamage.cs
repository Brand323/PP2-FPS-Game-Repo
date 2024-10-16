using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDamage : MonoBehaviour
{
    // Type of damage
    [SerializeField] enum damageType { Magic, Melee }
    [SerializeField] damageType type;

    // Parent script to access all the stats
    MeleeAlly script;

    // Amount of damage to apply to the enemy.
    float damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the damage to the parent damage stat
        script = GetComponentInParent<MeleeAlly>();
        damageAmount = script.Damage;
    }

    // Detect enemy collision.
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.CompareTag("Enemy"))
        {
            // Do the damage.
            I_Damage dmg = other.GetComponent<I_Damage>();
            if (dmg != null)
            {
                dmg.TakeDamage(damageAmount);
            }
        }
    }
}
