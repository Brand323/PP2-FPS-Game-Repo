using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, I_Pickup, I_Interactable
{
    [Header("----- Item Name -----")]
    [SerializeField] public string itemName;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Interact()
    {
        Debug.Log("Interacted with the object.");
    }

    public string GetInteractableName()
    {
        return itemName;
    }

    public void Pickup(Transform itemPickup)
    {
        // Move the item under the item container within the player gameobj
        transform.SetParent(itemPickup);

        // Adjust the position and rotation of the object relative to the Item Pickup transform
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
            rb.isKinematic = true;

        Collider col = GetComponent<Collider>();

        if (col != null)
            col.enabled = false;
    }

    public float GetDistanceFromPlayer(Transform playerTransform)
    {
       
       if (playerTransform != null)
       {
            //Returns distance between player and item
            return Vector3.Distance(playerTransform.position, transform.position);
       }

       //returns Large distance Value if not found
       return Mathf.Infinity;
    }

}
