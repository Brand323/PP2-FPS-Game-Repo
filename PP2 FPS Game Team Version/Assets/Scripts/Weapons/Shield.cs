using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapon
{
    private Animator shieldAnimator;
    
    void Start()
    {
        weaponName = "Shield";
        shieldAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleShieldBlock();
    }

    void HandleShieldBlock()
    {
        if (Input.GetMouseButton(1))
            shieldAnimator.SetBool("IsBlocking", true);
        else
            shieldAnimator.SetBool("IsBlocking", false);
    }
}
