using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    // Sword Paramaters
    public bool canAttack = true;
    public bool swordIsAttacking = false;
    public float attackCooldown = 1.0f;
    [SerializeField] private float damageAmount = 1f;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        weaponName = "Sword";

        // Check if the sword is equipped at the start
        if (transform.parent != null)
        {
            isEquipped = true;
            canAttack = true;
        }

        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }

        if (weaponAnimator != null)
            weaponAnimator.enabled = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other is CapsuleCollider)
            {
                I_Damage enemyDamage = other.GetComponent<I_Damage>();
                if (enemyDamage != null && weaponCollider.enabled)
                {
                    enemyDamage.TakeDamage(damageAmount);
                    Debug.Log($"{GetWeaponName()} did {damageAmount} damage to {other.name}");
                }
            }
        }
    }
    public void TriggerSwordAttack()
    {
        if (weaponAnimator != null && canAttack)
        {
            // Trigger the sword swing animation
            weaponAnimator.SetTrigger("SwordAttack");
            swordIsAttacking = true;
            StartCoroutine(AttackCooldown());
        }
        else
        {
            Debug.Log("No animator component found or cannot attack...");
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        while (weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack"))
        {
            yield return null; // wait for swing anim to complete
        }

        swordIsAttacking = false;
        canAttack = true;
        Debug.Log("Attack finished.");
        FindObjectOfType<WeaponManager>().ResetWeaponState();
    }

    protected override string GetWeaponName()
    {
        return "Sword";
    }

    public bool SwordIsAttacking()
    {
        return swordIsAttacking;
    }

    public override void Equip()
    {
        isEquipped = true;
        
    }

    public override void Unequip()
    {
        isEquipped = false;
    }
}
