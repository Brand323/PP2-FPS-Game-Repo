using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerWeapon : BaseWeapon
{
    [SerializeField] private float damageAmount = 2f;
    [SerializeField] private Collider daggerCollider;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        daggerCollider = GetComponent<Collider>();
        daggerCollider.enabled = true;
    }

    protected override string GetWeaponName()
    {
        return "Dagger";
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
