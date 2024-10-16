using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDamage : MonoBehaviour
{
    // Type of damage
    [SerializeField] enum damageType { Magic, Melee }
    [SerializeField] damageType type;

    // Parent scripts to access all the stats
    MeleeAlly meleeScript;
    RangedAlly rangedScript;

    // Amount of damage to apply to the enemy.
    float damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the damage to the parent damage stat

        if (type == damageType.Melee)
        {
            meleeScript = GetComponentInParent<MeleeAlly>();
            damageAmount = meleeScript.Damage;
        }
        else if (type == damageType.Magic)
        {
            //rangedScript = GetComponentInParent<RangedAlly>();
            //damageAmount = rangedScript.Damage;

            damageAmount = 3;
        }
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
