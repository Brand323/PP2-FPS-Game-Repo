using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Dagger : MonoBehaviour
{
    // dagger script reference when implemented
    public Rigidbody daggerRB;
    public BoxCollider daggerCol;

    public Transform playerPosition, weaponContainer, mainCam;

    public float pickupRange = 5f;

    public float dropForwardForce = 2f, dropUpwardForce = 1f;

    public bool DaggerEquipped;

    public static bool slotFull; // tracks if weapon slot is occupied

    private void Start()
    {
        if (!DaggerEquipped)
        {
            // disable dagger script if not equipped.
            daggerRB.isKinematic = false;
            daggerCol.isTrigger = false;
        }
        else
        {
            // enable dagger script
            daggerRB.isKinematic = true;
            daggerCol.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = playerPosition.position - transform.position;

        // Pickup the dagger if within range and slot is not full
        if (!DaggerEquipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
            PickupDagger();

        // Drop the dagger if equipped
        if (DaggerEquipped && Input.GetKeyDown(KeyCode.Q))
            DropDagger();
    }

    private void PickupDagger()
    {
        DaggerEquipped = true;
        slotFull = true;

        // Set dagger's parent to the weapon container within the player's arms 
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // Make the dagger kinematic (briefly disables physics)
        daggerRB.isKinematic = true;
        daggerCol.isTrigger = true;

        // enable the dagger script

        // Update UI to display interaction text

    }

    private void DropDagger()
    {
        DaggerEquipped = false;
        slotFull = false;

        // Detatch from the player and weaponContainer
        transform.SetParent(null);

        // enable dagger physics
        daggerRB.isKinematic = false;
        daggerCol.isTrigger = false;

        // Apply forces for a realistic looking drop on the item
        daggerRB.velocity = playerPosition.GetComponent<Rigidbody>().velocity;

        daggerRB.AddForce(mainCam.forward * dropForwardForce, ForceMode.Impulse);
        daggerRB.AddForce(mainCam.up * dropUpwardForce, ForceMode.Impulse);

        // Add random rotation
        float random = Random.Range(-1f, 1f);
        daggerRB.AddTorque(new Vector3(random, random, random) * 10);

        // Disable dagger functionality by disabling dagger script

        // Clear the reference for the UI
    }



}
