using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Axe : MonoBehaviour
{
    // axe script reference when implemented
    public Rigidbody axeRB;
    public BoxCollider axeCol;

    [SerializeField] Transform playerPosition, weaponContainer, mainCam;

    public float pickupRange = 5f;

    public float dropForwardForce = 2f, dropUpwardForce = 1f;

    public bool AxeEquipped;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        weaponContainer = GameObject.FindGameObjectWithTag("RightItemContainer").transform;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;

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
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = playerPosition.position - transform.position;
        GameObject currentWeapon = gameManager.instance.playerScript.currentWeapon;

        // Pickup the axe if within range and slot is not full
        //if (!AxeEquipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && currentWeapon == null)
        //    PickupAxe();
        if (distanceToPlayer.magnitude > pickupRange)
        {
            gameManager.instance.deactivateItemUI();
        }
        if (distanceToPlayer.magnitude <= pickupRange && !AxeEquipped && currentWeapon == null)
        {
            gameManager.instance.activateItemUI();
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupAxe();
                gameManager.instance.deactivateItemUI();
            }
        }

        // Drop the axe if equipped
        if (AxeEquipped && Input.GetKeyDown(KeyCode.Q) && currentWeapon != null)
            DropAxe();
    }

    private void PickupAxe()
    {
        AxeEquipped = true;

        // Set axe's parent to the weapon container within the player's arms 
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // Make the axe kinematic (briefly disables physics)
        axeRB.isKinematic = true;
        axeCol.isTrigger = true;

        // Set the current weapon reference to this weapon
        gameManager.instance.playerScript.currentWeapon = gameObject;

        // enable the axe script

        // Update UI to display interaction text

    }

    private void DropAxe()
    {
        AxeEquipped = false;

        // Detatch from the player and weaponContainer
        transform.SetParent(null);

        // enable axe physics
        axeRB.isKinematic = false;
        axeCol.isTrigger = false;

        // Set the current weapon reference to null 
        gameManager.instance.playerScript.currentWeapon = null;

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
