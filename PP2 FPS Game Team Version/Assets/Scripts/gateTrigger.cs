using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public GameObject waveMenu; // Reference to the wave menu UI panel
    public GateController gateController; //Reference to the GateController

    private bool waveStarted = false; // Prevents re-triggering the wave menu once the wave starts
    private float gateCloseDelay = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !waveStarted)
        {
            // Show the wave menu when the player enters the trigger
            //waveMenu.SetActive(true);
            gameManager.instance.waveWindow.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !waveStarted)
        {
            // Hide the wave menu (if not hidden already) when the player leaves the trigger
            //waveMenu.SetActive(false);
            gameManager.instance.waveWindow.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void StartWave()
    {
        Debug.Log("StartWave method called");

        // Hide the wave menu and cursor once the wave starts
        waveStarted = true;
        //waveMenu.SetActive(false); 
        gameManager.instance.waveWindow.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //close the gate behind player(If not already closed)
        gateController.Raisegate();

        StartCoroutine(CloseGateAfterDelay());

        // Call the gameManager to start the next wave
        gameManager.instance.StartNextWave();
    }

    private IEnumerator CloseGateAfterDelay()
    {
        // Close the gate after the delay
        yield return new WaitForSeconds(gateCloseDelay);
       
        gateController.LowerGate();
    }

    public void ResetTrigger()
    {
        waveStarted = false; // Reset the wave trigger for the next wave
    }
    public void EndWave()
    {
        //open gate when enemies are died
        gateController.Raisegate();
        ResetTrigger();

    }

}
