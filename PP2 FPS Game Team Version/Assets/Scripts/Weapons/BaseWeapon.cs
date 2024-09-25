using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, I_Interactable
{

    // References
    protected Rigidbody weaponRigidBody;
    protected Collider weaponCollider;
    protected Transform playerPosition, weaponContainer, shieldContainer, mainCam;

    // Pickup and Drop Settings
    public float dropForwardForce = 2f, dropUpwardForce = 1f;
    public bool isEquipped = false;

    //Purchasing Settings
    [SerializeField] public bool isPurchased = false;
    [SerializeField] public int weaponPrice = 1;
    [SerializeField] protected Material defaultMaterial;
    [SerializeField] protected Material grayedOutMaterial;
    protected money playerMoney;
    protected Renderer weaponRenderer;

    // UI
    protected gameManager gameManagerInstance;
    protected string weaponName;

    //test
    //Animation
    protected Animator weaponAnimator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Initialize common references

        playerPosition = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerPosition == null)
        {
            Debug.LogError("Player object with 'Player' tag not found.");
            return;  
        }

        // Get the money component from the player
        playerMoney = playerPosition.GetComponent<money>();
        if (playerMoney == null)
        {
            Debug.LogError("money component is missing from the player object.");
            return; 
        }

        // Initialize the game manager instance
        gameManagerInstance = gameManager.instance;
        if (gameManagerInstance == null)
        {
            Debug.LogError("gameManager instance is null.");
            return; 
        }

        // Get the weapon renderer
        weaponRenderer = GetComponent<Renderer>();
        if (weaponRenderer == null)
        {
            Debug.LogError("Renderer component is missing from the weapon object.");
            return; 
        }
        weaponAnimator = GetComponent<Animator>();
        if (weaponAnimator == null)
        {
            Debug.LogError("Animator component is missing from the weapon object.");
            return;
        }
        shieldContainer = GameObject.FindGameObjectWithTag("ShieldContainer")?.transform;
        if (shieldContainer == null)
        {
            Debug.LogError("Shield container not found. Please ensure the ShieldContainer tag is set correctly.");
        }
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        weaponContainer = GameObject.FindGameObjectWithTag("WeaponContainer").transform;
        weaponCollider = GetComponent<Collider>();
        weaponRigidBody = GetComponent<Rigidbody>();

        if (isEquipped)
        {
            weaponAnimator.enabled = true;
        }
        else
        {
            weaponAnimator.enabled = false;
        }

        if (gameManager.instance == null)
        {
            Debug.LogError("GameManager is not assigned.");
        }

        ActivateWeapon();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleDrop();
    }

    protected void ActivateWeapon()
    {
        if (!isPurchased)
        {
            weaponRenderer.material = grayedOutMaterial;
            weaponRigidBody.isKinematic = false;
            weaponCollider.isTrigger = false;
        }
        else
        {
            weaponRenderer.material = defaultMaterial;
            weaponRigidBody.isKinematic = true;
            weaponCollider.isTrigger = false;
        }
    }
    public virtual void PickupWeapon()
    {
        isEquipped = true;

        //Sets weapon's parent to weapon container
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        weaponRigidBody.isKinematic = true;
        weaponCollider.isTrigger = true;

        //Set current weapon reference
        gameManager.instance.playerScript.currentWeapon = gameObject;


        weaponAnimator.enabled = true;

    }

    protected virtual void DropWeapon()
    {
        isEquipped = false;

        //Detach from player
        transform.SetParent(null);

        weaponRigidBody.isKinematic = false;
        weaponCollider.isTrigger = false;

        //Clear Weapon Reference
        gameManager.instance.playerScript.currentWeapon = null;

        // Apply drop force
        weaponRigidBody.velocity = playerPosition.GetComponent<Rigidbody>().velocity;
        weaponRigidBody.AddForce(mainCam.forward * dropForwardForce, ForceMode.Impulse);
        weaponRigidBody.AddForce(mainCam.up * dropUpwardForce, ForceMode.Impulse);

        gameManager.instance.playerScript.currentWeapon = null;

        weaponAnimator.enabled = false;
    }

    protected void HandleDrop()
    {
      GameObject currentWeapon = gameManager.instance.playerScript.currentWeapon;

        // Drop Weapon if Equipped
        if (isEquipped && Input.GetKeyDown(KeyCode.Q) && currentWeapon != null)
        {
            DropWeapon();
        }
    }
    

    public void TryPurchaseWeapon()
    {
        Debug.Log($"Attempting to purchase: {GetInteractableName()}");
        Debug.Log($"Player has {playerMoney.GetCoinCount()} coins.");
        if (playerMoney.GetCoinCount() >= weaponPrice)
        {

            playerMoney.SetCoinCount(playerMoney.GetCoinCount()-weaponPrice);
            isPurchased = true;
        
                weaponRenderer.material = defaultMaterial;
        
            gameManager.instance.deactivateItemUI();
        }
        else
        {
            gameManager.instance.BlinkRed();
        }
    }

    //GETTERS

    public string GetInteractableName()
    {
        return GetWeaponName();
    }

    public void Interact()
    {
    }

    protected abstract string GetWeaponName();

}
