using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseWeapon
{
    private Animator shieldAnimator;
    
    protected override void Start()
    {
        base.Start();
        weaponName = "Shield";
        shieldAnimator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        HandleShieldBlock();
    }

    void HandleShieldBlock()
    {
        if (shieldAnimator != null)
        {
            if (Input.GetMouseButton(1))
                shieldAnimator.SetBool("IsBlocking", true);
            else
                shieldAnimator.SetBool("IsBlocking", false);
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
