using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    // Sword Paramaters
    private Animator swordAnimator;
    private bool isEquipped = false;
    private bool canAttack = true;
    private bool isAttacking = false;
    public float attackCooldown = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        weaponName = "Sword";
        swordAnimator = GetComponent<Animator>();

        // Check if the sword is equipped at the start
        if (transform.parent != null)
        {
            isEquipped = true;
            canAttack = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEquipped && canAttack && Input.GetMouseButtonDown(0))
            TriggerSwordAttack();
    }

    public void TriggerSwordAttack()
    {
        if (swordAnimator != null && canAttack)
        {
            // Trigger the sword swing animation
            swordAnimator.SetTrigger("SwordAttack");
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

        while (swordAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack"))
        {
            yield return null; // wait for swing anim to complete
        }

        isAttacking = false;
        canAttack = true;
        Debug.Log("Attack finished.");
    }
}
