using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{

    // References
    protected Rigidbody weaponRigidBody;
    protected BoxCollider weaponCollider;
    protected Transform playerPosition, weaponContainer, mainCam;

    // Pickup and Drop settings
    public float pickupRange = 5f;
    public float dropForwardForce = 2f, dropUpwardForce = 1f;
    public bool isEquipped = false;

    // UI
    protected gameManager gameManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize common references
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        weaponContainer = GameObject.FindGameObjectWithTag("RightItemContainer").transform;
        weaponRigidBody = GetComponent<Rigidbody>();
        weaponCollider = GetComponent<BoxCollider>();

        gameManagerInstance = gameManager.instance;

        ActivateWeapon();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void ActivateWeapon()
    {
        if (!isEquipped)
        {
            weaponRigidBody.isKinematic = false;
            weaponCollider.isTrigger = false;
        }
        else
        {
            weaponRigidBody.isKinematic = true;
            weaponCollider.isTrigger = true;
        }
    }



}
