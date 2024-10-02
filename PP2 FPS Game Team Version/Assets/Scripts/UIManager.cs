using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    //Serialize fields
    [SerializeField] public GameObject activeWindow;
    [SerializeField] GameObject pauseWindow;
    [SerializeField] public GameObject winWindow;
    [SerializeField] GameObject loseWindow;
    [SerializeField] GameObject mainEditWindow;

    [SerializeField] public GameObject functionalOptionsWindow;
    [SerializeField] public GameObject movementParametersWindow;
    [SerializeField] public GameObject attributeParametersWindow;
    [SerializeField] public GameObject jumpParametersWindow;
    [SerializeField] public GameObject crouchParametersWindow;
    [SerializeField] public GameObject headBobParametersWindow;

    //Main window variables
    [SerializeField] public GameObject mainWindow;
    [SerializeField] public GameObject difficultyWindow;

    #region EditVariables
    //Input variables functional options 
    public TMP_Dropdown canSprint;
    public TMP_Dropdown canJump;
    public TMP_Dropdown canCrouch;
    public TMP_Dropdown useHeadbob;
    public TMP_Dropdown canSlide;

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

    //Input variables headBob parameters
    public TMP_InputField walkBobSpeed;
    public TMP_InputField sprintBobSpeed;
    public TMP_InputField crouchBobSpeed;

    //Input variables attribute parameters
    public TMP_InputField maxHealth;
    public TMP_InputField maxStamina;
    #endregion

    //HUD variables
    [SerializeField] public TMP_Text moneyText;
    public TMP_Text healthPotionText;
    public TMP_Text staminaPotionText;
    public Image playerHPBar;
    public Image playerStaminaBar;

    float originalTimeScale;

    public bool isPaused;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this; 

        Time.timeScale = 1f;
        originalTimeScale = Time.timeScale;

        StartCoroutine(startGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame(pauseWindow);
            }
            else
            {
                UnpauseGame();
            }
        }

        //Edit game Window
        if (Input.GetButtonDown("Edit"))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame(mainEditWindow);
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(0f);
        isPaused = true;
        PauseGame(mainWindow);
    }

    public void PauseGame(GameObject window)
    {
        if(AudioManager.instance != null && AudioManager.instance.backgroundMusicIsPlaying)
        {
            AudioManager.instance.backgroundMusicIsPlaying = false;
            AudioManager.instance.backgtoundAudioSource.Pause();
        }
        Time.timeScale = 0; //pause game
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        activateWindow(window);
    }

    public void UnpauseGame()
    {
        if (AudioManager.instance != null && !AudioManager.instance.backgroundMusicIsPlaying && WaveManager.instance.currentWave > 0)
        {
            AudioManager.instance.backgroundMusicIsPlaying = true;
            AudioManager.instance.backgtoundAudioSource.UnPause();
        }
        Time.timeScale = originalTimeScale;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        activateWindow(activeWindow);
        activeWindow = null;
    }


    public void UpdateGameGoal(int enemy)
    {
        //Apply new game goal functionality
    }

    public void LoseUpdate()
    {
        if (!isPaused)
        {
            isPaused = true;
            PauseGame(loseWindow);
        }
    }

    public void activateWindow(GameObject window)
    {
        activeWindow = window;
        activeWindow.SetActive(isPaused);
    }

}
