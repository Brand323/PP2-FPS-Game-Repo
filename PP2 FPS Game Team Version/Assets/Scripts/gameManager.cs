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
    [SerializeField] GameObject waveEndWindow;
    [SerializeField] public GameObject waveWindow;

    // Need to implement a Player gameobject for the BasicEnemyAI for movement

    [SerializeField] public GameObject functionalOptionsWindow;
    [SerializeField] public GameObject movementParametersWindow;
    [SerializeField] public GameObject attributeParametersWindow;
    [SerializeField] public GameObject jumpParametersWindow;
    [SerializeField] public GameObject crouchParametersWindow;
    [SerializeField] public GameObject headBobParametersWindow;

    //Item menus
    [SerializeField] GameObject activeItemWindow;
    [SerializeField] public GameObject itemPickUpWindow;
    [SerializeField] public GameObject itemBuyWindow;

    public GameObject saveButton;

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

    //Wave Variables
    public GameObject enemyType1Prefab;
    public GameObject enemyType2Prefab;
    public GameObject enemyType3Prefab;
    //public Transform[] spawnPoints;
    public List<Transform> spawnPoints;
    public Transform spawnPosition;
    public int currentWave = 0;
    public int enemiesPerWave = 5;
    private int remainingEnemies;
    private bool waveInProgress = false;
    public GameObject Lever;
    public int totalWaves = 3;

    //UI Elements for wave System
    public TMP_Text waveTextHUD;
    public TMP_Text waveTextNumber;
    public GameObject lever;
    public GateTrigger triggerGate;


    //Item UI Variables
    public TextMeshProUGUI itemUIText;

    public GameObject player;
    public FirstPersonController playerScript;

    int enemyCount;
    float originalTimeScale;

    public bool isPaused;
    public bool itemUIisDisplayed;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
        originalTimeScale = Time.timeScale;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<FirstPersonController>();
        lever = GameObject.FindWithTag("Lever");
        triggerGate = lever.GetComponent<GateTrigger>();
        spawnEnemiesRandomizer();
        //Intialize First Wave
        UpdateWaveUI();

    }

    // Update is called once per frame
    void Update()
    {
        enemyCountText.text = remainingEnemies.ToString("F0");
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
        if(remainingEnemies <= 0 && waveInProgress)
        {
            EndWave();
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

    public void StartNextWave()
    {
        spawnEnemiesRandomizer();
        currentWave++;

        if (currentWave == totalWaves)
        {
            //sets it to one for final boss
            remainingEnemies = 1;
        }
        else
        {
            // Increase enemy count each wave
            remainingEnemies = enemiesPerWave * currentWave;
        }
        waveInProgress = true;

        StartCoroutine(SpawnWaveEnemies());

        UpdateWaveUI();
    }

    IEnumerator SpawnWaveEnemies()
    {

        if (currentWave == totalWaves)
        {
            SpawnFinalEnemy();
        }
        else
        {
            for (int i = 0; i < remainingEnemies; i++)
            {
                SpawnEnemy();
            }
            // Delay between spawns
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy()
    {
        GameObject nextEnemy;
        float randomValue = Random.value;

        if(randomValue < 0.7f)
        {
            nextEnemy = enemyType1Prefab;
        }
        else
        {
            nextEnemy = enemyType2Prefab;
        }

        int spawnIndex = Random.Range(0, spawnPoints.Count);
        Instantiate(nextEnemy, spawnPoints[spawnIndex].position, UnityEngine.Quaternion.identity);
    }
    void SpawnFinalEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        Instantiate(enemyType3Prefab, spawnPoints[spawnIndex].position, UnityEngine.Quaternion.identity);
    }

    void EndWave()
    {
        waveInProgress = false;

        triggerGate.EndWave();

        //Checks for Win
        if(currentWave >= totalWaves)
        {
            isPaused = !isPaused;
            PauseGame(winWindow);
        }
        activateWaveEndMenu();
    }

    void UpdateWaveUI()
    {
        waveTextHUD.text = currentWave.ToString("F0");
        waveTextNumber.text = (currentWave + 1).ToString("F0");
    }
    public void EnemyDefeated()
    {
        remainingEnemies--;

        if(remainingEnemies <= 0)
        {
            EndWave();
        }
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
        itemUIisDisplayed = false;
    }

    //makes the text blink red
    public IEnumerator BlinkRed()
    {
        Color originalColor = itemUIText.color;
        Color blinkColor = Color.red;
        float blinkDuration = 0.5f;  // Time in seconds for each blink

         itemUIText.color = blinkColor;
         yield return new WaitForSeconds(blinkDuration);

        itemUIText.color = originalColor;
    }

    void spawnEnemiesRandomizer()
    {
        if (spawnPoints == null)
        {
            spawnPoints = new List<Transform>();
        }
        spawnPoints.Clear();
        UnityEngine.Vector3 coordenates;
        for(int index = 0; index < 6; ++index)
        {
            coordenates = new UnityEngine.Vector3(Random.Range(-100f, -70f), 1, Random.Range(-25f,15f));
            spawnPosition.SetPositionAndRotation(coordenates, UnityEngine.Quaternion.identity);
            spawnPoints.Add(spawnPosition);
        }
    }

    void activateWaveEndMenu()
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
