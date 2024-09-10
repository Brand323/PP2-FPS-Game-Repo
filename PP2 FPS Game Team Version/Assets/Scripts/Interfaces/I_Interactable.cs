using UnityEngine;

// This interface is used to distribute the Item's name and interaction status information
// To the UI, etc.
public interface I_Interactable
{

    // Function to interact with the object
    public void Interact();

    // Function to get the interactable's name to the UI
    public string GetInteractableName();
}
