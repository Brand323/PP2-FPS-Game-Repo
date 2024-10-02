using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Weapon Parameters
    public string weaponName;
    protected Collider weaponCollider;
    protected Animator weaponAnimator;
    public bool isEquipped;
    protected bool isCurrentlyUsing;

    
    protected virtual void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        weaponAnimator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {

    }

    protected abstract string GetWeaponName();

    public abstract void Equip();
    public abstract void Unequip();

}
