using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Pickup_Sword : MonoBehaviour
{
    // sword script reference when implemented
    public Rigidbody swordRB;
    public BoxCollider swordCol;

    [SerializeField] Transform playerPosition, weaponContainer, mainCam;

    public float pickupRange = 5f;

    public float dropForwardForce = 2f, dropUpwardForce = 1f;

    public bool SwordEquipped = true;

    [Header("Purchase Settings")]
    [SerializeField] bool isPurchased = false;
    [SerializeField] int swordPrice = 1;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material grayedOutMaterial;

    private money playerMoney;
   
    private Renderer swordRenderer;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        weaponContainer = GameObject.FindGameObjectWithTag("RightItemContainer").transform;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        playerMoney = playerPosition.GetComponent<money>();
        swordRenderer = GetComponent<Renderer>();

        if (!isPurchased)
        {
            swordRenderer.material = grayedOutMaterial;
            // disable sword if not purchased.
            swordRB.isKinematic = false;
            swordCol.isTrigger = false;
        }
        else
        {
            swordRenderer.material = defaultMaterial;
            // enable sword
            swordRB.isKinematic= true;
            swordCol.isTrigger = true;
        }
    }
    private void testingE()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = playerPosition.position - transform.position;
        GameObject currentWeapon = gameManager.instance.playerScript.currentWeapon;

        if (distanceToPlayer.magnitude <= pickupRange && !SwordEquipped && currentWeapon == null)
        {
            if (!isPurchased)
            {
                gameManager.instance.activateItemUI("Buy Sword: " + swordPrice + " Coins");

                if (Input.GetKeyDown(KeyCode.E) && playerMoney.GetCoinCount() >= swordPrice)
                {
                    PurchaseSword();
                }
            }

            else
            {
                gameManager.instance.activateItemUI("Pick Up Sword");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickupSword();
                    gameManager.instance.deactivateItemUI();
                }
            }
        }
        else if (distanceToPlayer.magnitude > pickupRange)
        {
            gameManager.instance.deactivateItemUI();
        }

        // Drop the sword if equipped
        if (SwordEquipped && Input.GetKeyDown(KeyCode.Q) && currentWeapon != null)
        {
            DropSword();
        }
    }

    private void PurchaseSword()
    {
        int newCoinCount = playerMoney.GetCoinCount() - swordPrice;
        playerMoney.SetCoinCount(newCoinCount);

        isPurchased = true;

        swordRenderer.material = defaultMaterial;

        swordRB.isKinematic = true;
        swordCol.isTrigger = true;

        gameManager.instance.deactivateItemUI();

        Debug.Log("sword Purchased");
    }
    private void PickupSword()
    {
        SwordEquipped = true;

        // Set sword's parent to the weapon container within the player's arms 
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // Make the sword kinematic (briefly disables physics)
        swordRB.isKinematic = true;
        swordCol.isTrigger = true;

        // Set the current weapon reference to this weapon
        gameManager.instance.playerScript.currentWeapon = gameObject;

        // enable the sword script
        swordCol.enabled = false;

        // Update UI to display interaction text

    }

    private void DropSword()
    {
        SwordEquipped = false;

        // Detatch from the player and weaponContainer
        transform.SetParent(null);

        // enable sword physics
        swordRB.isKinematic = false;
        swordCol.isTrigger = false;

        //re-enable collider when dropped
        swordCol.enabled = true;

        // Set the current weapon reference to null 
        gameManager.instance.playerScript.currentWeapon = null;

        // Apply forces for a realistic looking drop on the item
        swordRB.velocity = playerPosition.GetComponent<Rigidbody>().velocity;

        swordRB.AddForce(mainCam.forward * dropForwardForce, ForceMode.Impulse);
        swordRB.AddForce(mainCam.up * dropUpwardForce, ForceMode.Impulse);

        // Add random rotation
        float random = Random.Range(-1f, 1f);
        swordRB.AddTorque(new Vector3(random, random, random) * 10);

        // Disable sword functionality by disabling sword script
        swordCol.enabled = true;

        // Clear the reference for the UI
    }



}
