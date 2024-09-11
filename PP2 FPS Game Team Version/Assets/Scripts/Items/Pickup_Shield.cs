using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Shield : MonoBehaviour
{
    // shield script reference when implemented
    public Rigidbody shieldRB;
    public BoxCollider shieldCol;

    public Transform playerPosition, weaponContainer, mainCam;

    public float pickupRange = 5f;

    public float dropForwardForce = 2f, dropUpwardForce = 1f;

    public bool ShieldEquipped;

    public static bool slotFull; // tracks if weapon slot is occupied

    private void Start()
    {
        if (!ShieldEquipped)
        {
            // disable shield script if not equipped.
            shieldRB.isKinematic = false;
            shieldCol.isTrigger = false;
        }
        else
        {
            // enable shield script
            shieldRB.isKinematic = true;
            shieldCol.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = playerPosition.position - transform.position;

        // Pickup the shield if within range and slot is not full
        if (!ShieldEquipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
            PickupShield();

        // Drop the shield if equipped
        if (ShieldEquipped && Input.GetKeyDown(KeyCode.Q))
            DropShield();
    }

    private void PickupShield()
    {
        ShieldEquipped = true;
        slotFull = true;

        // Set shield's parent to the weapon container within the player's arms 
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // Make the shield kinematic (briefly disables physics)
        shieldRB.isKinematic = true;
        shieldCol.isTrigger = true;

        // enable the shield script

        // Update UI to display interaction text

    }

    private void DropShield()
    {
        ShieldEquipped = false;
        slotFull = false;

        // Detatch from the player and weaponContainer
        transform.SetParent(null);

        // enable shield physics
        shieldRB.isKinematic = false;
        shieldCol.isTrigger = false;

        // Apply forces for a realistic looking drop on the item
        shieldRB.velocity = playerPosition.GetComponent<Rigidbody>().velocity;

        shieldRB.AddForce(mainCam.forward * dropForwardForce, ForceMode.Impulse);
        shieldRB.AddForce(mainCam.up * dropUpwardForce, ForceMode.Impulse);

        // Add random rotation
        float random = Random.Range(-1f, 1f);
        shieldRB.AddTorque(new Vector3(random, random, random) * 10);

        // Disable shield functionality by disabling shield script

        // Clear the reference for the UI
    }



}
