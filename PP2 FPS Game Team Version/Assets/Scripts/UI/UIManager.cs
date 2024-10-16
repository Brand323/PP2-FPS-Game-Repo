using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    //Serialize fields
    [SerializeField] public GameObject activeWindow;
    [SerializeField] GameObject pauseWindow;
    [SerializeField] public GameObject winWindow;
    [SerializeField] GameObject loseWindow;

    //Main window variables
    [SerializeField] public GameObject mainWindow;
    [SerializeField] public GameObject difficultyWindow;
    [SerializeField] public GameObject optionsWindow;
    [SerializeField] public GameObject creditsWindow;

    //HUD variables
    [SerializeField] public TMP_Text moneyText;
    public TMP_Text healthPotionText;
    public TMP_Text staminaPotionText;
    public Image playerHPBar;
    public Image playerStaminaBar;

    //Quest variables
    [SerializeField] public GameObject questWindow;
    [SerializeField] public GameObject questDescriptionWindow;
    [SerializeField] public GameObject HealthRewardImage;
    [SerializeField] public GameObject GoldRewardImage;
    [SerializeField] public GameObject StaminaRewardImage;
    [SerializeField] public TMP_Text questCompletionText;
    public TMP_Text questName;
    public TMP_Text questDescription;
    public TMP_Text healthRewardText;
    public TMP_Text goldRewardText;
    public TMP_Text staminaRewardText;

    //map variables
    [SerializeField] public GameObject cityMapWindow;
    [SerializeField] public GameObject enemyCityMapWindow;

    //Options menu variables
    [SerializeField] public Slider sfxVolume;
    [SerializeField] public Slider musicVolume;

    //Win condition
    public TMP_Text victoryPopUp;

    float originalTimeScale;

    public bool isPaused;

    private string mapSceneName = "Map Scene";

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
        originalTimeScale = Time.timeScale;
        if (SceneManager.GetActiveScene().name != "CombatSceneArctic")
        {
            StartCoroutine(startGame());
        }
        //if (SceneManager.GetActiveScene().name != mapSceneName)
        //{
        //    // Shows everytime I swap to first person scene
        //    // StartCoroutine(startGame());
        //}
        //else
        //{
        //    // Do not lock cursor on map scene
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //}
        sfxVolume.value = 0.4f;
        sfxVolume.maxValue = 1;
        musicVolume.value = 0.35f;
        musicVolume.maxValue = 1;
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
        if (AudioManager.instance != null && !AudioManager.instance.backgroundMusicIsPlaying)
        {
            AudioManager.instance.backgroundMusicIsPlaying = true;
            AudioManager.instance.backgtoundAudioSource.UnPause();
        }
        Time.timeScale = originalTimeScale;
        if (SceneManager.GetActiveScene().name == "CombatSceneArctic")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
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

    public void activateQuestWindow(GameObject window)
    {
        isPaused = !isPaused;
        PauseGame(window);
    }

    public void activateRewardUI()
    {
        if (gameManager.instance.currentQuest != null)
        {
            if (gameManager.instance.currentQuest.GoldReward > 0)
            {
                StartCoroutine(rewardFeedBack(GoldRewardImage));
                moneyText.text = gameManager.instance.GetPlayerMoney().ToString();
            }
            if (gameManager.instance.currentQuest.HealthPotionReward > 0)
            {
                StartCoroutine(rewardFeedBack(HealthRewardImage));
                healthPotionText.text = gameManager.instance.GetHealthPotions().ToString();
            }
            if (gameManager.instance.currentQuest.StaminaPotionReward > 0)
            {
                StartCoroutine(rewardFeedBack(StaminaRewardImage));
                staminaPotionText.text = gameManager.instance.GetStaminaPotions().ToString();
            }
        }
    }

    public IEnumerator rewardFeedBack(GameObject window)
    {
        window.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        window.SetActive(false);
    }
}
