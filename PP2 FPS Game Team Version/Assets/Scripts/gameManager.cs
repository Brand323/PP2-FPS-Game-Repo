using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static gameManager instance;

    //Serialize fields
    [SerializeField] GameObject activeWindow;
    [SerializeField] GameObject pauseWindow;

    int enemyCount;
    float originalTimeScale;

    public bool isPaused;

    void Awake()
    {
        instance = this;
        originalTimeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                PauseGame();
            }    
            else
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame()
    {
        activeWindow = pauseWindow;
        Time.timeScale = 0; //pause game
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        activeWindow.SetActive(isPaused);
        activeWindow = null;
    }

    public void UnpauseGame()
    {
        activeWindow = pauseWindow;
        Time.timeScale = originalTimeScale;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        activeWindow.SetActive(isPaused);
        activeWindow = null;
    }
}
