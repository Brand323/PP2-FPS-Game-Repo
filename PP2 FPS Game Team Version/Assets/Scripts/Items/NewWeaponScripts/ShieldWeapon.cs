using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWeapon : BaseWeapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        GameObject[] containers = GameObject.FindGameObjectsWithTag("LeftItemContainer");
        if (containers.Length > 0)
        {
            //Makes sure theres one container minimum
            weaponContainer = containers[0].transform; 
        }
        else
        {
            Debug.LogError("LeftItemContainer not found!");
        }

    }

    protected override string GetWeaponName()
    {
        return "Shield";
    }

}
