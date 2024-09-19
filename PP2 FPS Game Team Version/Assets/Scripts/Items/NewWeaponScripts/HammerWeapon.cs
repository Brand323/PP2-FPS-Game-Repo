using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerWeapon : BaseWeapon
{
    [SerializeField] private float damageAmount = 2f;
    [SerializeField] private Collider hammerCollider;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hammerCollider = GetComponent<Collider>();
        hammerCollider.enabled = true;
    }

    protected override string GetWeaponName()
    {
        return "Hammer";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            I_Damage enemyDamage = other.GetComponent<I_Damage>();

            if (enemyDamage != null)
            {
                enemyDamage.TakeDamage(damageAmount);
            }
        }
    }
}
