using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public Transform PlayerTransform;

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
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                PlayerTransform = playerObject.transform;
            }
            return;
        }
        if (gameManagerInstance == null)
        {
            Debug.LogError("gameManagerInstance is null!");
            return;
        }
        //HandleRayCast();
    }

    
    //void HandleRayCast()
    //{
    //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    //    RaycastHit hit;

    //    Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green);

    //    //Checks is ray hit 
    //    if (Physics.Raycast(ray, out hit, interactDistance) ) 
    //    {

    //    I_Interactable interactable = hit.collider.GetComponent<I_Interactable>();
    //    BaseWeapon weapon = interactable as BaseWeapon;

    //        //Checks if object is interactable
    //        if (interactable != null)
    //        {
    //            //Makes sure its not the same object
    //            if (interactable != lastInteractedObject)
    //            {
    //                //Debug.Log($"Raycast hit: {hit.collider.name}");

    //                //Updates Last Object hit
    //                lastInteractedObject = interactable;

    //                if (weapon != null)
    //                {

    //                    Debug.Log($"Interacting with weapon: {weapon.GetInteractableName()}");
    //                    if (!weapon.isPurchased)
    //                    {
    //                        gameManagerInstance.activateItemUI($"Buy {weapon.GetInteractableName()}: {weapon.weaponPrice.ToString()} Coins", UIManager.instance.itemBuyWindow);

    //                    }
    //                    else if(!weapon.isEquipped)
    //                    {
    //                        gameManagerInstance.activateItemUI("Pick UP ", UIManager.instance.itemPickUpWindow);

    //                    }
    //                }
    //            }

    //            if (Input.GetKeyDown(KeyCode.E) && weapon != null)
    //            {
    //                if (!weapon.isPurchased)
    //                {
    //                    weapon.TryPurchaseWeapon();
    //                }
    //                else
    //                {
    //                    weapon.PickupWeapon();
    //                }

    //                gameManagerInstance.deactivateItemUI();
    //            }
               
                
    //        }
    //    }
    //    else  
    //    {
    //        //Deactivates Ui if nothing hit
    //        if ((lastInteractedObject != null))
    //        {
    //            lastInteractedObject = null;
    //            gameManagerInstance.deactivateItemUI();
    //        }
    //    }
    //}
}
