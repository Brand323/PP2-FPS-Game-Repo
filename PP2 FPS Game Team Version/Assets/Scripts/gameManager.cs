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
    [SerializeField] GameObject winWindow;

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
                PauseGame(pauseWindow);
            }    
            else
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame(GameObject window)
    {
        Time.timeScale = 0; //pause game
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        activateWindow(window);
    }

    public void UnpauseGame()
    {
        Time.timeScale = originalTimeScale;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        activateWindow(pauseWindow);
    }

    public void UpdateGameGoal(int enemy)
    {
        enemyCount += enemy;
        if(enemyCount <= 0)
        {
            isPaused = !isPaused;
            PauseGame(winWindow);
        }
    }

    void activateWindow(GameObject window)
    {
        activeWindow = window;
        activeWindow.SetActive(isPaused);
        activeWindow = null;
    }
}
