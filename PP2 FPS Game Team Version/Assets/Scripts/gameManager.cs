using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static gameManager instance;

    //Serialize fields
    [SerializeField] GameObject activeWindow;
    [SerializeField] GameObject pauseWindow;
    [SerializeField] GameObject winWindow;
    [SerializeField] GameObject loseWindow;
    [SerializeField] GameObject mainEditWindow;

    // Need to implement a Player gameobject for the BasicEnemyAI for movement

    [SerializeField] public GameObject functionalOptionsWindow;
    [SerializeField] public GameObject movementParametersWindow;
    [SerializeField] public GameObject lookParametersWindow;
    [SerializeField] public GameObject jumpParametersWindow;
    [SerializeField] public GameObject crouchParametersWindow;
    [SerializeField] public GameObject headBobParametersWindow;

    //Input variables movement parameters
    public TMP_InputField walkSpeed;
    public TMP_InputField sprintSpeed;
    public TMP_InputField crouchSpeed;
    public TMP_InputField slideSpeed;

    //Input variables jump parameters
    public TMP_InputField gravity;
    public TMP_InputField jumpForce;

    //Input variables crouch parameters
    public TMP_InputField crouchHeight;
    public TMP_InputField standingHeight;
    public TMP_InputField timeToCrouch;

    //Input variables headBob parameters
    public TMP_InputField walkBobSpeed;
    public TMP_InputField walkBobAmount;
    public TMP_InputField sprintBobSpeed;
    public TMP_InputField sprintBobAmount;
    public TMP_InputField croouchBobSpeed;
    public TMP_InputField crouchBobAmount;

    //HUD variables
    [SerializeField] TMP_Text moneyText;
    public Image playerHPBar;
    public Image playerStaminaBar;

    public GameObject player;
    public FirstPersonController playerScript;

    int enemyCount;
    float originalTimeScale;

    public bool isPaused;

    void Awake()
    {
        //if(instance != null)
            instance = this;
        originalTimeScale = Time.timeScale;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<FirstPersonController>();
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

        //Edit game Window
        if(Input.GetButtonDown("Edit"))
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                PauseGame(mainEditWindow);
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
        activateWindow(activeWindow);
        activeWindow = null;
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

    public void LoseUpdate()
    {
        if (!isPaused)
        {
            isPaused = true;
            PauseGame(loseWindow);
        }
    }

    void activateWindow(GameObject window)
    {
        activeWindow = window;
        activeWindow.SetActive(isPaused);
    }
}