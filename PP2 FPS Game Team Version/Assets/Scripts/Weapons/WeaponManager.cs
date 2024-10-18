using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponContainer;
    public Transform shieldContainer;
    public Transform bowContainer;

    public Sword equippedSword;
    public Shield equippedShield;
    public Bow equippedBow;

    public Animator swordAnimator, shieldAnimator, bowAnimator;



    private bool Melee = false;
    private bool Range = false;

    private bool isWeaponBusy = false; // Track if a weapon is used

    private void Awake()
    {
        equippedSword = weaponContainer.GetComponentInChildren<Sword>();
        equippedBow = bowContainer.GetComponentInChildren<Bow>();
        equippedShield = shieldContainer.GetComponentInChildren<Shield>();
        swordAnimator = weaponContainer.GetComponentInChildren<Animator>();
        bowAnimator = bowContainer.GetComponentInChildren<Animator>();
        shieldAnimator = shieldContainer.GetComponentInChildren<Animator>();

        EquipSwordAndShield();

        if (equippedSword == null && equippedShield == null && equippedBow == null)
            return; // No weapons working

    }

    private void Update()
    {
        HandleCombatInput();
        HandleWeaponSwitching();
        
    }

    private void HandleCombatInput()
    {
        // Prevent additional input events if weapon is active
        if (!isWeaponBusy)
        {
            if (Melee && swordAnimator.enabled && shieldAnimator.enabled)
            {
                if (equippedSword.isEquipped && Input.GetMouseButtonDown(0))
                {

                    equippedSword.TriggerSwordAttack();
                    isWeaponBusy = true;
                }

               if (equippedShield.isEquipped)
                {
                    if (Input.GetMouseButton(1))
                        equippedShield.TriggerBlock(true);

                    if (Input.GetMouseButtonUp(1))
                    {
                        equippedShield.TriggerBlock(false);
                        isWeaponBusy = false;
                    }
                }



            }

            if (Range && bowAnimator.enabled)
            {
                if (equippedBow.isEquipped)
                    equippedBow.HandleBowInput();

            }




        }
    }

    private void HandleWeaponSwitching()
    {
        if (!isWeaponBusy)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                bowAnimator.enabled = false;
                EquipSwordAndShield();
            }
            //else if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    //swordAnimator.enabled = false;
            //    //shieldAnimator.enabled = false;
            //    //EquipBow();
            //}
        }
    }

    private void EquipSwordAndShield()
    {
        bowContainer.gameObject.SetActive(false); // Disable the bow container

        weaponContainer.gameObject.SetActive(true); // Enable the sword container
        shieldContainer.gameObject.SetActive(true); // Enable the shield container
        swordAnimator.enabled = true;
        shieldAnimator.enabled = true;

        equippedSword.Equip();
        equippedShield.Equip();

        Melee = true;
        Range = false;

    }

    private void EquipBow()
    {

        weaponContainer.gameObject.SetActive(false); // Disable the sword container
        shieldContainer.gameObject.SetActive(false); // Disable the shield container

        bowContainer.gameObject.SetActive(true); // Enable the bow container
        bowAnimator.enabled = true;

        equippedShield.Unequip();
        equippedSword.Unequip();

        Range = true;
        Melee = false;

    }

    public void ResetWeaponState()
    {
        isWeaponBusy = false;
    }
}
