using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public GameObject waveMenu; // Reference to the wave menu UI panel
    public gameManager gameManager; // Reference to the gameManager

    private bool waveStarted = false; // Prevents re-triggering the wave menu once the wave starts

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !waveStarted)
        {
            // Show the wave menu when the player enters the trigger
            waveMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !waveStarted)
        {
            // Hide the wave menu when the player leaves the trigger
            waveMenu.SetActive(false); 
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void StartWave()
    {
        Debug.Log("StartWave method called");

        waveStarted = true;
        waveMenu.SetActive(false); // Hide the wave menu once the wave starts

        // Call the gameManager to start the next wave
        gameManager.StartNextWave();
    }

    public void ResetTrigger()
    {
        waveStarted = false; // Reset the wave trigger for the next wave
    }

}
