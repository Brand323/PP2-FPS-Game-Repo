using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon
{
    // Sword Paramaters
    private bool canAttack = true;
    private bool isAttacking = false;
    public float attackCooldown = 1.0f;
    [SerializeField] private float damageAmount = 1f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (isEquipped && canAttack && Input.GetMouseButtonDown(0))
        {
            TriggerSwordAttack();
        }
    }

    public override void PickupWeapon()
    {
        base.PickupWeapon();
        isEquipped = true;
        canAttack = true;
        weaponCollider.isTrigger = true;
        Debug.Log("Sword is equipped.");
    }

    protected override void DropWeapon()
    {
        if (!isAttacking)
        {
            base.DropWeapon();
            isEquipped = false;
            Debug.Log("Sword dropped.");
        }
        else
        {
            Debug.Log("Cannot drop the sword while attacking!");
        }

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
            isAttacking = true;
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

        isAttacking = false;
        canAttack = true;
        Debug.Log("Attack finished.");
    }

    protected override string GetWeaponName()
    {
        return "Sword";
    }
}
