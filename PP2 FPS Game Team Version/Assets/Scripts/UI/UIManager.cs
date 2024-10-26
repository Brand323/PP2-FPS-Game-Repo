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
    [SerializeField] GameObject pauseMapWindow;
    [SerializeField] public GameObject winWindow;
    [SerializeField] GameObject loseWindow;
    [SerializeField] GameObject loseBattleWindow;

    //Main window variables
    [SerializeField] public GameObject mainWindow;
    [SerializeField] public GameObject difficultyWindow;
    [SerializeField] public GameObject optionsWindow;
    [SerializeField] public GameObject creditsWindow;
    [SerializeField] public GameObject tutorialWindow;

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
    [SerializeField] public GameObject notEnoughMoneyWindow;
    [SerializeField] public GameObject limitWindow;
    public TMP_Text limitText;
    [SerializeField] public GameObject enemyCityMapWindow;
    [SerializeField] public GameObject caravanAttackWindow;
    [SerializeField] public GameObject escortFailWindow;

    //Options menu variables
    [SerializeField] public Slider sfxVolume;
    [SerializeField] public Slider musicVolume;

    //Win condition
    [SerializeField] public GameObject victoryPopUp;

    //Instructions
    [SerializeField] public GameObject potionInstructions;

    //Notification 
    [SerializeField] public GameObject notificationWindow;
    public TMP_Text notificationText;
    [SerializeField] public GameObject battleNotificationWindow;
    public TMP_Text battleNotificationText;
    [SerializeField] public GameObject loseProgressWindow;

    float originalTimeScale;

    public bool isPaused;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
        originalTimeScale = Time.timeScale;

        if (gameManager.instance != null)
        {
            if (gameManager.instance.soundEffectVolume != 0)
            {
                sfxVolume.value = gameManager.instance.soundEffectVolume;
                musicVolume.value = gameManager.instance.backgroundVolume;
            }
            else
            {
                sfxVolume.value = 0.3f;
                musicVolume.value = 0.4f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                if(SceneManager.GetActiveScene().name == "Map Scene")
                {
                    PauseGame(pauseMapWindow);
                }
                else
                {
                    PauseGame(pauseWindow);
                }
            }
            else
            {
                UnpauseGame();
            }
        }
        moneyText.text = gameManager.instance.PlayerMoneyValue.ToString();
        healthPotionText.text = gameManager.instance.PlayerHealthPotions.ToString();
        staminaPotionText.text = gameManager.instance.PlayerStaminaPotions.ToString();
    }

    public IEnumerator startGame()
    {
        yield return new WaitForSeconds(0f);
        isPaused = true;
        PauseGame(mainWindow);
        sfxVolume.value = 0.3f;
        musicVolume.value = 0.4f;
    }

    public void PauseGame(GameObject window)
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuPopSound, AudioManager.instance.sfxVolume);
            if(AudioManager.instance.backgroundMusicIsPlaying || AudioManager.instance.mapBackgroundMusicIsPlaying)
            {
                AudioManager.instance.backgroundAudioSource.Pause();
            }
        }
        Time.timeScale = 0; //pause game
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        activateWindow(window);
    }

    public void UnpauseGame()
    {
        if(AudioManager.instance.backgroundMusicIsPlaying || AudioManager.instance.mapBackgroundMusicIsPlaying)
        {
            AudioManager.instance.backgroundAudioSource.UnPause();
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

    public void LoseUpdate()
    {
        if(gameManager.instance.isDefendingCaravan)
        {
            gameManager.instance.isDefendingCaravan = false;
            gameManager.instance.caravanArrived = false;
            gameManager.instance.isQuestInProgress = false;
            if (!isPaused)
            {
                isPaused = true;
                PauseGame(escortFailWindow);
            }
        }
        else if (!isPaused)
        {
            isPaused = true;
            PauseGame(loseWindow);
        }
    }

    public void LoseBattleUpdate()
    {
        if (gameManager.instance.isDefendingCaravan)
        {
            gameManager.instance.isDefendingCaravan = false;
            gameManager.instance.caravanArrived = false;
            gameManager.instance.isQuestInProgress = false;
            if (!isPaused)
            {
                isPaused = true;
                PauseGame(escortFailWindow);
            }
        }
        else if (!isPaused)
        {
            isPaused = true;
            PauseGame(loseBattleWindow);
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
            }
            if (gameManager.instance.currentQuest.HealthPotionReward > 0)
            {
                StartCoroutine(rewardFeedBack(HealthRewardImage));
            }
            if (gameManager.instance.currentQuest.StaminaPotionReward > 0)
            {
                StartCoroutine(rewardFeedBack(StaminaRewardImage));
            }
        }
    }

    public IEnumerator rewardFeedBack(GameObject window)
    {
        window.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        window.SetActive(false);
    }

    public IEnumerator caravanAttackFeedBack()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        if (gameManager.instance.isQuestInProgress)
        {
            isPaused = !isPaused;
            PauseGame(caravanAttackWindow);
        }
    }

    public IEnumerator activatePotionsInstructions()
    {
        potionInstructions.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        potionInstructions.SetActive(false);
    }
}
