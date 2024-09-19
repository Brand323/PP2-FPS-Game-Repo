using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public Transform PlayerTransform;

    private I_Interactable closestInteractable;
    private I_Interactable previousInteractable;

    [SerializeField] float interactDistance = 1f;

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform == null)
        {
            Debug.LogError("PlayerTransform is not assigned in InteractionManager!");
            return;
        }

        FindClosestInteractable();
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
        if(nearestInteractable != closestInteractable)
        {
            if(closestInteractable != null)
            {
                DeactivateInteractableUI();
            }
        }

        //Sets closest Interactable
        closestInteractable = nearestInteractable;

        //ActivateUI for new Item

        if (closestInteractable != null && closestDistance <= interactDistance) 
        {
            ActivateInteractableUI(closestInteractable);
        }

        //if (closestInteractable != null)
        //{
        //    Debug.Log($"Nearest interactable: {closestInteractable.GetInteractableName()}, Distance: {closestDistance}");
        //}

        //Check if object found
        if (closestInteractable != null && closestDistance <= interactDistance) 
        {
           // Debug.Log($"Closest inter: {closestInteractable.GetInteractableName()}, Distance: {closestDistance}");

            if(Input.GetKeyDown(KeyCode.E))
            {
                closestInteractable.Interact();
            }
        }
    }

    void DeactivateInteractableUI()
    {
        gameManager.instance.deactivateItemUI();
    }

    void ActivateInteractableUI(I_Interactable interactable)
    {
        gameManager.instance.activateItemUI($"Interact with {interactable.GetInteractableName()}");
    }

}
