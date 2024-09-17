using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : BaseWeapon
{
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private Collider swordCollider;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        swordCollider = GetComponent<Collider>();
        swordCollider.enabled = true;
    }

    protected override string GetWeaponName()
    {
        return "Sword";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other is CapsuleCollider)
            {
                I_Damage enemyDamage = other.GetComponent<I_Damage>();

                if (enemyDamage != null && swordCollider.enabled)
                {
                    enemyDamage.TakeDamage(damageAmount);
                    Debug.Log($"{GetWeaponName()} did {damageAmount} damage to {other.name}");
                }
            }
        }
    }

    protected override void PickupWeapon()
    {
        base.PickupWeapon();
        swordCollider.enabled = true;
    }

    protected override void DropWeapon()
    {
        base.DropWeapon();
        swordCollider.enabled = false;
    }

}
