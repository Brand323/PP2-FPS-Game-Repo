using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Reflection.Emit;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonFunctions : MonoBehaviour
{
    private string input;
    public Transform cityTransform;

    #region Basic Functions

    public void resume()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
    }

    public void restart()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        foreach(Transform city in MapKingdomManager.instance.citiesInHumanKingdom)
        {
            GameObject.Destroy(city.gameObject);
        }
        MapKingdomManager.instance.citiesInHumanKingdom.Clear();
        gameManager.instance.PlayerMoneyValue = 0;
        gameManager.instance.PlayerHealthPotions = 0;
        gameManager.instance.PlayerStaminaPotions = 0;
        AllyCombatManager.instance.AllyArmySize = 0;
        CombatManager.instance.exitToMap();
        gameManager.instance.gameStarted = false;
    }

    public void quit()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void respawn()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        gameManager.instance.playerScript.spawnPlayer();
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
    }

    public void startGame()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        gameManager.instance.PlayerMoneyValue = 0;
        gameManager.instance.PlayerHealthPotions = 0;
        gameManager.instance.PlayerStaminaPotions = 0;
        if (MapKingdomManager.instance.citiesInHumanKingdom.Count > 0)
        {
            foreach (Transform city in MapKingdomManager.instance.citiesInHumanKingdom)
            {
                GameObject.Destroy(city.gameObject);
            }
            MapKingdomManager.instance.citiesInHumanKingdom.Clear();
            AllyCombatManager.instance.AllyArmySize = 0;
        }
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.UnpauseGame();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    #endregion

    #region Quest Functions

    public void giveQuest()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.UnpauseGame();
        if (gameManager.instance.questGiver != null)
        {
            gameManager.instance.questGiver.GiveQuest();
        }
    }

    public void acceptQuest()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.UnpauseGame();
        UIManager.instance.activateQuestWindow(UIManager.instance.questDescriptionWindow);
        UIManager.instance.notificationText.text = "Current Quest: " + gameManager.instance.currentQuest.QuestGoal.GoalDescription;
        UIManager.instance.notificationWindow.SetActive(true);
    }

    public void rejectQuest()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
        gameManager.instance.isQuestInProgress = false;
    }

    public void loseEscortQuest()
    {
        gameManager.instance.caravanArrived = false;
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
        gameManager.instance.isQuestInProgress = false;
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.PauseGame(UIManager.instance.escortFailWindow);
    }

    public void defend()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        CombatManager.instance.enemyArmySize = Random.Range(3, 5);
        SceneManager.LoadScene("CombatSceneArctic");
        CombatManager.instance.CheckToSpawn();
        gameManager.instance.isDefendingCaravan = true;
    }

    #endregion

    #region Difficulty Functions

    public void difficultyMenu()
    {
        editInput(UIManager.instance.difficultyWindow);
    }

    public void easy()
    {
        difficultyInput(1);
    }

    public void medium()
    {
        difficultyInput(2);
    }

    public void hard()
    {
        difficultyInput(3);
    }

    public void veryHard()
    {
        difficultyInput(4);
    }

    #endregion

    #region Options Functions

    GameObject previousWindow;

    public void optionsMenu()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        previousWindow = UIManager.instance.activeWindow;
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.UnpauseGame();
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.PauseGame(UIManager.instance.optionsWindow);
    }

    public void exitOptions()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.UnpauseGame();
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.PauseGame(previousWindow);
    }

    public void creditsMenu()
    {
        editInput(UIManager.instance.creditsWindow);
    }

    public void tutorialMenu()
    {
        editInput(UIManager.instance.tutorialWindow);
    }

    public void exitToMain()
    {
        editInput(UIManager.instance.mainWindow);
    }

    public void exitToCity()
    {
        editInput(UIManager.instance.cityMapWindow);
    }

    public void goBackToMap()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        CombatManager.instance.exitToMap();
        gameManager.instance.isQuestInProgress = false;
    }

    public void progressLoss()
    {
        editInput(UIManager.instance.loseProgressWindow);
    }

    #endregion

    public void fight()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        CombatManager.instance.enemyArmySize = Random.Range(3, 5);
        SceneManager.LoadScene("CombatSceneArctic");
        CombatManager.instance.CheckToSpawn();
    }

    #region StoreFunctions

    public void buyHealthPotion()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.buySound, AudioManager.instance.sfxVolume);
        }
        if(gameManager.instance.PlayerHealthPotions >= 10)
        {
            UIManager.instance.limitText.text = "You reached the limit of health potions.";
            UIManager.instance.limitWindow.SetActive(true);
        }
        else
        {
            StartCoroutine(UIManager.instance.activatePotionsInstructions());
            if (gameManager.instance.PlayerMoneyValue >= 3)
            {
                gameManager.instance.AddHealthPotions(1);
                gameManager.instance.PlayerMoneyValue -= 3;
            }
            else
            {
                UIManager.instance.notEnoughMoneyWindow.SetActive(true);
            }
        }
    }

    public void buyStaminaPotion()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.buySound, AudioManager.instance.sfxVolume);
        }
        if (gameManager.instance.PlayerStaminaPotions >= 10)
        {
            UIManager.instance.limitText.text = "You have reached the limit of stamina potions.";
            UIManager.instance.limitWindow.SetActive(true);
        }
        else
        {
            StartCoroutine(UIManager.instance.activatePotionsInstructions());
            if (gameManager.instance.PlayerMoneyValue >= 2)
            {
                gameManager.instance.AddStaminaPotions(1);
                gameManager.instance.PlayerMoneyValue -= 2;
            }
            else
            {
                UIManager.instance.notEnoughMoneyWindow.SetActive(true);
            }
        }
    }

    public void companionStore()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.companionStoreWindow.SetActive(true);
    }

    public void buyMeleeCompanion()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.buySound, AudioManager.instance.sfxVolume);
        }
        if (AllyCombatManager.instance.AllyArmySize >= 10)
        {
            UIManager.instance.limitText.text = "You have reached the limit of companions.";
            UIManager.instance.limitWindow.SetActive(true);
        }
        else
        {
            UIManager.instance.companionStoreWindow.SetActive(false);
            if (gameManager.instance.PlayerMoneyValue >= 7)
            {
                AllyCombatManager.instance.RecruitMeleeCompanion();
                gameManager.instance.PlayerMoneyValue -= 7;
            }
            else
            {
                UIManager.instance.notEnoughMoneyWindow.SetActive(true);
            }
        }
    }

    public void buyRangedCompanion()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.buySound, AudioManager.instance.sfxVolume);
        }
        if (AllyCombatManager.instance.AllyArmySize >= 10)
        {
            UIManager.instance.limitText.text = "You have reached the limit of companions.";
            UIManager.instance.limitWindow.SetActive(true);
        }
        else
        {
            UIManager.instance.companionStoreWindow.SetActive(false);
            if (gameManager.instance.PlayerMoneyValue >= 10)
            {
                AllyCombatManager.instance.RecruitRangedCompanion();
                gameManager.instance.PlayerMoneyValue -= 10;
            }
            else
            {
                UIManager.instance.notEnoughMoneyWindow.SetActive(true);
            }
        }
    }

    public void exitNEM()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.notEnoughMoneyWindow.SetActive(false);
    }

    public void exitLimit()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.limitWindow.SetActive(false);
    }

    #endregion

    #region Private Functions

    void editInput(GameObject window)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.PauseGame(window);
    }

    void difficultyInput(int difficulty)
    {
        CombatManager.instance.SetDifficulty(difficulty);
        editInput(UIManager.instance.mainWindow);
    }

    void save()
    {
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
    }

    #endregion
}
