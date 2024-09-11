using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Hammer : MonoBehaviour
{
    // hammer script reference when implemented
    public Rigidbody hammerRB;
    public BoxCollider hammerCol;

    public Transform playerPosition, weaponContainer, mainCam;

    public float pickupRange = 5f;

    public float dropForwardForce = 2f, dropUpwardForce = 1f;

    public bool HammerEquipped;

    public static bool slotFull; // tracks if weapon slot is occupied

    private void Start()
    {
        if (!HammerEquipped)
        {
            // disable hammer script if not equipped.
            hammerRB.isKinematic = false;
            hammerCol.isTrigger = false;
        }
        else
        {
            // enable hammer script
            hammerRB.isKinematic = true;
            hammerCol.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = playerPosition.position - transform.position;

        // Pickup the hammer if within range and slot is not full
        if (!HammerEquipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
            PickupHammer();

        // Drop the hammer if equipped
        if (HammerEquipped && Input.GetKeyDown(KeyCode.Q))
            DropHammer();
    }

    private void PickupHammer()
    {
        HammerEquipped = true;
        slotFull = true;

        // Set hammer's parent to the weapon container within the player's arms 
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // Make the hammer kinematic (briefly disables physics)
        hammerRB.isKinematic = true;
        hammerCol.isTrigger = true;

        // enable the hammer script

        // Update UI to display interaction text

    }

    private void DropHammer()
    {
        HammerEquipped = false;
        slotFull = false;

        // Detatch from the player and weaponContainer
        transform.SetParent(null);

        // enable hammer physics
        hammerRB.isKinematic = false;
        hammerCol.isTrigger = false;

        // Apply forces for a realistic looking drop on the item
        hammerRB.velocity = playerPosition.GetComponent<Rigidbody>().velocity;

        hammerRB.AddForce(mainCam.forward * dropForwardForce, ForceMode.Impulse);
        hammerRB.AddForce(mainCam.up * dropUpwardForce, ForceMode.Impulse);

        // Add random rotation
        float random = Random.Range(-1f, 1f);
        hammerRB.AddTorque(new Vector3(random, random, random) * 10);

        // Disable hammer functionality by disabling hammer script

        // Clear the reference for the UI
    }



}
