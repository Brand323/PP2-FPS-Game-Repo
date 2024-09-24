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
    [SerializeField] GameObject waveEndWindow;
    [SerializeField] public GameObject waveWindow;

    [SerializeField] public GameObject functionalOptionsWindow;
    [SerializeField] public GameObject movementParametersWindow;
    [SerializeField] public GameObject attributeParametersWindow;
    [SerializeField] public GameObject jumpParametersWindow;
    [SerializeField] public GameObject crouchParametersWindow;
    [SerializeField] public GameObject headBobParametersWindow;
    [SerializeField] public GameObject enemiesPerWaveWindow;

    //Item menus
    [SerializeField] GameObject activeItemWindow;
    [SerializeField] public GameObject itemPickUpWindow;
    [SerializeField] public GameObject itemBuyWindow;

    //Input variable enemiesPerWave
    public TMP_InputField enemiesPerWaveText;

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

    //HUD variables
    [SerializeField] public TMP_Text moneyText;
    [SerializeField] TMP_Text enemyCountText;
    public Image playerHPBar;
    public Image playerStaminaBar;

    //UI Elements for wave System
    public TMP_Text waveTextHUD;
    public TMP_Text waveTextNumber;

    //Item UI Variables
    public TextMeshProUGUI itemUIText;

    int enemyCount;
    float originalTimeScale;

    public bool isPaused;
    public bool itemUIisDisplayed;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this; 

        Time.timeScale = 1f;
        originalTimeScale = Time.timeScale;

        UpdateWaveUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(WaveManager.instance != null)
        {
            enemyCountText.text = WaveManager.instance.remainingEnemies.ToString("F0");
        }
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

    public void UpdateWaveUI()
    {
        if(WaveManager.instance != null)
        {
            waveTextHUD.text = WaveManager.instance.currentWave.ToString("F0");
            waveTextNumber.text = (WaveManager.instance.currentWave + 1).ToString("F0");
        }
    }

    public void UpdateGameGoal(int enemy)
    {
        enemyCount += enemy;
        if (enemyCount <= 0)
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

    public void activateWindow(GameObject window)
    {
        activeWindow = window;
        activeWindow.SetActive(isPaused);
    }

    public void activateItemUI(string message, GameObject window = null)
    {

        if (!itemUIisDisplayed && itemUIText != null)
        {
            itemUIText.text = message;
            if (window != null)
            {
                activeItemWindow = window;
                activeItemWindow.SetActive(true);
            }
            itemUIisDisplayed = true;
        }
    }

    public void deactivateItemUI()
    {
        itemUIText.text = "";

        if (activeItemWindow != null)
        {
            activeItemWindow.SetActive(false);
        }

        if (itemBuyWindow != null)
        {
            itemBuyWindow.SetActive(false);
        }
        itemUIisDisplayed = false;
    }

    public IEnumerator BlinkRed()
    {
        Color originalColor = itemUIText.color;
        Color blinkColor = Color.red;
        float blinkDuration = 0.1f;  // Time in seconds for each blink

        itemUIText.color = blinkColor;
        yield return new WaitForSeconds(blinkDuration);

        itemUIText.color = originalColor;
    }

    public void activateStartWaveMenu()
    {
        waveWindow.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void deactivateStartWaveMenu()
    {
        waveWindow.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void activateWaveEndMenu()
    {
        waveEndWindow.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void deactivateWaveEndMenu()
    {
        waveEndWindow.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
