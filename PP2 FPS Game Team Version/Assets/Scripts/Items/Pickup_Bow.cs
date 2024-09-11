using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Bow : MonoBehaviour
{
    // bow script reference when implemented
    public Rigidbody bowRB;
    public BoxCollider bowCol;

    public Transform playerPosition, weaponContainer, mainCam;

    public float pickupRange = 5f;

    public float dropForwardForce = 2f, dropUpwardForce = 1f;

    public bool BowEquipped;

    public static bool slotFull; // tracks if weapon slot is occupied

    private void Start()
    {
        if (!BowEquipped)
        {
            // disable bow script if not equipped.
            bowRB.isKinematic = false;
            bowCol.isTrigger = false;
        }
        else
        {
            // enable bow script
            bowRB.isKinematic = true;
            bowCol.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = playerPosition.position - transform.position;

        // Pickup the bow if within range and slot is not full
        if (!BowEquipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
            PickupBow();

        // Drop the bow if equipped
        if (BowEquipped && Input.GetKeyDown(KeyCode.Q))
            DropBow();
    }

    private void PickupBow()
    {
        BowEquipped = true;
        slotFull = true;

        // Set bow's parent to the weapon container within the player's arms 
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // Make the bow kinematic (briefly disables physics)
        bowRB.isKinematic = true;
        bowCol.isTrigger = true;

        // enable the bow script

        // Update UI to display interaction text

    }

    private void DropBow()
    {
        BowEquipped = false;
        slotFull = false;

        // Detatch from the player and weaponContainer
        transform.SetParent(null);

        // enable bow physics
        bowRB.isKinematic = false;
        bowCol.isTrigger = false;

        // Apply forces for a realistic looking drop on the item
        bowRB.velocity = playerPosition.GetComponent<Rigidbody>().velocity;

        bowRB.AddForce(mainCam.forward * dropForwardForce, ForceMode.Impulse);
        bowRB.AddForce(mainCam.up * dropUpwardForce, ForceMode.Impulse);

        // Add random rotation
        float random = Random.Range(-1f, 1f);
        bowRB.AddTorque(new Vector3(random, random, random) * 10);

        // Disable bow functionality by disabling bow script

        // Clear the reference for the UI
    }



}
