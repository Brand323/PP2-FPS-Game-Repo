using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponContainer;
    public Transform shieldContainer;

    public Weapon equippedWeapon;
    public Weapon equippedShield;


    void Update()
    {
        HandleWeaponInput();
    }

    public void HandleWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && equippedWeapon != null)
            DropWeapon();

        if (Input.GetKeyDown(KeyCode.Alpha1) && equippedShield != null)
            DropShield();
    }

    public void PickupWeapon(Weapon weapon)
    {
        if (equippedWeapon == null)
        {
            equippedWeapon = weapon;
            weapon.Pickup(weaponContainer);

            Debug.Log("Picked up weapon: " + weapon.weaponName);
        }
    }

    public void PickupShield(Weapon shield)
    {
        if (equippedShield == null)
        {
            equippedShield = shield;
            shield.Pickup(shieldContainer);

            Debug.Log("Picked up shield: " +  shield.weaponName);
        }
    }

    public void DropWeapon()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.Drop();
            equippedWeapon = null;
        }
    }

    public void DropShield()
    {
        if (equippedShield != null)
        {
            equippedShield.Drop();
            equippedShield = null;
        }
    }
}
