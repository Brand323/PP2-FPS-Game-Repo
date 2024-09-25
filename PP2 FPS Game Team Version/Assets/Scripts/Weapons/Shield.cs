using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseWeapon
{
    
    protected override void Start()
    {
        base.Start();
        weaponName = "Shield";
        weaponAnimator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        HandleShieldBlock();
    }
    public override void PickupWeapon()
    {
        base.PickupWeapon();      
        transform.SetParent(shieldContainer);
    }
    void HandleShieldBlock()
    {
        if (weaponAnimator != null)
        {
            if (Input.GetMouseButton(1))
                weaponAnimator.SetBool("IsBlocking", true);
            else
                weaponAnimator.SetBool("IsBlocking", false);
        }
        else
        {
            Debug.Log("Shield Animator is null");
        }
    }

    protected override string GetWeaponName()
    {
        return "Shield";
    }
}
