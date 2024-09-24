using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponContainer;
    public Transform shieldContainer;
    public Transform bowContainer;

    public Weapon equippedWeapon;
    public Weapon equippedShield;
    public Weapon equippedBow;

    private bool isSwordAndShieldEquipped = false;
    private bool isBowEquipped = false;

    private void Awake()
    {
        EquipSwordAndShield(); // Start with sword and shield by default
    }
    private void Update()
    {
        HandleWeaponSwitching();
    }

    public void HandleWeaponSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isSwordAndShieldEquipped)
            EquipSwordAndShield();

        if (Input.GetKeyDown(KeyCode.Alpha2) && !isBowEquipped)
            EquipBow();
    }

    public void EquipSwordAndShield()
    {
        if (isBowEquipped)
        {
            bowContainer.gameObject.SetActive(false);
            isBowEquipped = false;
        }

        weaponContainer.gameObject.SetActive(true); // enable sword
        shieldContainer.gameObject.SetActive(true); // enable shield
        isSwordAndShieldEquipped = true;

        Debug.Log("Sword and Shield equipped!");
    }

    public void EquipBow()
    {
        if (isSwordAndShieldEquipped)
        {
            weaponContainer.gameObject.SetActive(false); // disable sword
            shieldContainer.gameObject.SetActive(false); //disable shield
            isSwordAndShieldEquipped = false;
        }

        bowContainer.gameObject.SetActive(true); // enable bow
        isBowEquipped = true;

        Debug.Log("Bow is equipped!");
    }
}
