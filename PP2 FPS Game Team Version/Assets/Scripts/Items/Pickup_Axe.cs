using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Axe : MonoBehaviour
{
    // axe script reference when implemented
    public Rigidbody axeRB;
    public BoxCollider axeCol;

    public Transform playerPosition, weaponContainer, mainCam;

    public float pickupRange = 5f;

    public float dropForwardForce = 2f, dropUpwardForce = 1f;

    public bool AxeEquipped;

    public static bool slotFull; // tracks if weapon slot is occupied

    private void Start()
    {
        if (!AxeEquipped)
        {
            // disable axe script if not equipped.
            axeRB.isKinematic = false;
            axeCol.isTrigger = false;
        }
        else
        {
            // enable axe script
            axeRB.isKinematic = true;
            axeCol.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = playerPosition.position - transform.position;

        // Pickup the axe if within range and slot is not full
        if (!AxeEquipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
            PickupAxe();

        // Drop the axe if equipped
        if (AxeEquipped && Input.GetKeyDown(KeyCode.Q))
            DropAxe();
    }

    private void PickupAxe()
    {
        AxeEquipped = true;
        slotFull = true;

        // Set axe's parent to the weapon container within the player's arms 
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // Make the axe kinematic (briefly disables physics)
        axeRB.isKinematic = true;
        axeCol.isTrigger = true;

        // enable the axe script

        // Update UI to display interaction text

    }

    private void DropAxe()
    {
        AxeEquipped = false;
        slotFull = false;

        // Detatch from the player and weaponContainer
        transform.SetParent(null);

        // enable axe physics
        axeRB.isKinematic = false;
        axeCol.isTrigger = false;

        // Apply forces for a realistic looking drop on the item
        axeRB.velocity = playerPosition.GetComponent<Rigidbody>().velocity;

        axeRB.AddForce(mainCam.forward * dropForwardForce, ForceMode.Impulse);
        axeRB.AddForce(mainCam.up * dropUpwardForce, ForceMode.Impulse);

        // Add random rotation
        float random = Random.Range(-1f, 1f);
        axeRB.AddTorque(new Vector3(random, random, random) * 10);

        // Disable axe functionality by disabling axe script

        // Clear the reference for the UI
    }



}
