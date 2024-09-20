using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public Transform PlayerTransform;

    private I_Interactable closestInteractable;
    private I_Interactable previousInteractable;
    private I_Interactable lastInteractedObject = null;

    [SerializeField] float interactDistance = 3f;

    protected gameManager gameManagerInstance;

    private void Start()
    {
        gameManagerInstance = gameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform == null)
        {
            Debug.LogError("PlayerTransform is not assigned in InteractionManager!");
            return;
        }
        if (gameManagerInstance == null)
        {
            Debug.LogError("gameManagerInstance is null!");
            return;
        }
        HandleRayCast();
    }

    void FindClosestInteractable()
    {
        I_Interactable[] allInteractables = FindObjectsOfType<MonoBehaviour>().OfType<I_Interactable>().ToArray();

        float closestDistance = Mathf.Infinity;
        I_Interactable nearestInteractable = null;

        //Interate over objects to find closest
        for (int i = 0; i < allInteractables.Length; i++)
        {
            float distance = allInteractables[i].GetDistanceFromPlayer(PlayerTransform);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestInteractable = allInteractables[i];
            }
        }

        //Deavtivate UI for Last Item
        if (nearestInteractable != closestInteractable)
        {
            if (closestInteractable != null)
            {
                //Debug.Log("Deactivate is being called");
                gameManagerInstance.deactivateItemUI();
            }
        }

        //Sets closest Interactable
        closestInteractable = nearestInteractable;


        //ActivateUI for new Item

        if (closestInteractable != null && closestDistance <= interactDistance)
        {

            if (closestInteractable != null)
            {
                Debug.Log($"Closest interactable type: {closestInteractable.GetType().Name}");
            }
            BaseWeapon weapon = closestInteractable as BaseWeapon;

            Debug.Log(weapon.GetInteractableName());

            if (weapon != null)
            {
                Debug.Log("weapon is found");


                if (!weapon.isPurchased)
                {
                    Debug.Log("if not purchased is being called");
                    gameManagerInstance.activateItemUI($"Buy {weapon.GetInteractableName()}: {weapon.weaponPrice.ToString()} Coins", gameManager.instance.itemBuyWindow);
                }
                else if (weapon.isPurchased)
                {
                    gameManagerInstance.activateItemUI("Pick UP ", gameManager.instance.itemPickUpWindow);
                }
                else
                {
                    gameManagerInstance.deactivateItemUI();
                }

                //ActivateInteractableUI(closestInteractable);
            }
        }

        //if (closestInteractable != null)
        //{
        //    Debug.Log($"Nearest interactable: {closestInteractable.GetInteractableName()}, Distance: {closestDistance}");
        //}

        //Check if object found
        if (closestInteractable != null && closestDistance <= interactDistance)
        {
            //Debug.Log($"Closest inter: {closestInteractable.GetInteractableName()}, Distance: {closestDistance}");

            if (Input.GetKeyDown(KeyCode.E))
            {
                closestInteractable.Interact();
            }
        }
    }
    
    void HandleRayCast()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green);

        //Checks is ray hit 
        if (Physics.Raycast(ray, out hit, interactDistance) ) 
        {

        I_Interactable interactable = hit.collider.GetComponent<I_Interactable>();
        BaseWeapon weapon = interactable as BaseWeapon;

            //Checks if object is interactable
            if (interactable != null)
            {
                //Makes sure its not the same object
                if (interactable != lastInteractedObject)
                {
                    //Debug.Log($"Raycast hit: {hit.collider.name}");

                    //Updates Last Object hit
                    lastInteractedObject = interactable;

                    if (weapon != null)
                    {

                        Debug.Log($"Interacting with weapon: {weapon.GetInteractableName()}");
                        if (!weapon.isPurchased)
                        {
                            gameManagerInstance.activateItemUI($"Buy {weapon.GetInteractableName()}: {weapon.weaponPrice.ToString()} Coins", gameManager.instance.itemBuyWindow);

                        }
                        else
                        {
                            gameManagerInstance.activateItemUI("Pick UP ", gameManager.instance.itemPickUpWindow);

                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.E) && weapon != null)
                {
                    if (!weapon.isPurchased)
                    {
                        weapon.TryPurchaseWeapon();
                    }
                    else
                    {
                        weapon.PickupWeapon();
                    }

                    gameManagerInstance.deactivateItemUI();
                }
               
                
            }
        }
        else  
        {
            //Deactivates Ui if nothing hit
            if ((lastInteractedObject != null))
            {
                lastInteractedObject = null;
                gameManagerInstance.deactivateItemUI();
            }
        }
    }
}
