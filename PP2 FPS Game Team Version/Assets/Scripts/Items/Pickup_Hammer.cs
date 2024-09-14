using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Hammer : MonoBehaviour
{
    // hammer script reference when implemented
    public Rigidbody hammerRB;
    public BoxCollider hammerCol;

    [SerializeField] Transform playerPosition, weaponContainer, mainCam;

    public float pickupRange = 5f;

    public float dropForwardForce = 2f, dropUpwardForce = 1f;

    public bool HammerEquipped;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        weaponContainer = GameObject.FindGameObjectWithTag("RightItemContainer").transform;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;

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
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = playerPosition.position - transform.position;
        GameObject currentWeapon = gameManager.instance.playerScript.currentWeapon;

        // Pickup the hammer if within range and slot is not full
        //if (!HammerEquipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && currentWeapon == null)
        //    PickupHammer();
        if (distanceToPlayer.magnitude > pickupRange)
        {
            gameManager.instance.deactivateItemUI();
        }
        if (distanceToPlayer.magnitude <= pickupRange && !HammerEquipped && currentWeapon == null)
        {
            gameManager.instance.activateItemUI("Pick Up Sword (Press E)");
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupHammer();
                gameManager.instance.deactivateItemUI();
            }
        }

        // Drop the hammer if equipped
        if (HammerEquipped && Input.GetKeyDown(KeyCode.Q) && currentWeapon != null)
            DropHammer();
    }

    private void PickupHammer()
    {
        HammerEquipped = true;

        // Set hammer's parent to the weapon container within the player's arms 
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // Make the hammer kinematic (briefly disables physics)
        hammerRB.isKinematic = true;
        hammerCol.isTrigger = true;

        // Set the current weapon reference to this weapon
        gameManager.instance.playerScript.currentWeapon = gameObject;

        // enable the hammer script

        // Update UI to display interaction text

    }

    private void DropHammer()
    {
        HammerEquipped = false;

        // Detatch from the player and weaponContainer
        transform.SetParent(null);

        // enable hammer physics
        hammerRB.isKinematic = false;
        hammerCol.isTrigger = false;

        // Set the current weapon reference to null 
        gameManager.instance.playerScript.currentWeapon = null;

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
