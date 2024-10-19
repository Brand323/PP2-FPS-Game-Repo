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
        CombatManager.instance.exitToMap();

        //if(gameManager.instance.playerScript.playerIsDead)
        //{
        //    gameManager.instance.playerScript.currentHealth = gameManager.instance.playerScript.MaxHealthPoints;
        //}
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

    #endregion

    public void startGame()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSound(AudioManager.instance.menuButtonSound, AudioManager.instance.sfxVolume);
        }
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.UnpauseGame();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

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

    #endregion


    public void exitToMain()
    {
        editInput(UIManager.instance.mainWindow);
    }

    public void exitToCity()
    {
        editInput(UIManager.instance.cityMapWindow);
    }

    public void fight()
    {
        //if (gameManager.instance == null)
        //{
        //    Debug.LogError("gameManager.instance is null in buttonFunctions.");
        //    return;
        //}


        //Vector3 playerPosition = gameManager.instance.mapPlayer.transform.position;
        //Debug.Log("Player position: " + playerPosition);

        //Transform nearestCity = MapKingdomManager.Instance.GetNearestCity(playerPosition);

        //if (nearestCity != null)
        //{
        //    Debug.Log("Nearest city found: " + nearestCity.name);

        //    CombatManager.instance.InitiateCityCombat(nearestCity);
        //    CombatManager.instance.enemyArmySize = Random.Range(3, 5);
        //    SceneManager.LoadScene("CombatSceneArctic");
        //    StartCoroutine(DelayedCheckToSpawn());
        //}
        //else
        //{
        //    Debug.LogError("No nearby city found for combat.");
        //}
        CombatManager.instance.enemyArmySize = Random.Range(3, 5);
        SceneManager.LoadScene("CombatSceneArctic");
        CombatManager.instance.CheckToSpawn();
    }
    private IEnumerator DelayedCheckToSpawn()
    {
        yield return new WaitForSeconds(3);
        CombatManager.instance.CheckToSpawn();
    }

    public void defend()
    {
        CombatManager.instance.enemyArmySize = Random.Range(3, 5);
        SceneManager.LoadScene("CombatSceneArctic");
        CombatManager.instance.CheckToSpawn();
        gameManager.instance.isDefendingCaravan = true;
    }

    public void buyHealthPotion()
    {
        if(gameManager.instance.PlayerMoneyValue >= 3)
        {
            gameManager.instance.AddHealthPotions(1);
            gameManager.instance.PlayerMoneyValue -= 3;
        }
        else
        {
            UIManager.instance.notEnoughMoneyWindow.SetActive(true);
        }
        StartCoroutine(UIManager.instance.activatePotionsInstructions());
    }

    public void buyStaminaPotion()
    {
        if (gameManager.instance.PlayerMoneyValue >= 2)
        {
            gameManager.instance.AddStaminaPotions(1);
            gameManager.instance.PlayerMoneyValue -= 2;
        }
        else
        {
            UIManager.instance.notEnoughMoneyWindow.SetActive(true);
        }
        StartCoroutine(UIManager.instance.activatePotionsInstructions());
    }

    public void buyCompanion()
    {
        if (gameManager.instance.PlayerMoneyValue >= 5)
        {
            gameManager.instance.AddCompanion();
            gameManager.instance.PlayerMoneyValue -= 5;
        }
        else
        {
            UIManager.instance.notEnoughMoneyWindow.SetActive(true);
        }
    }

    public void exitNEM()
    {
        UIManager.instance.notEnoughMoneyWindow.SetActive(false);
    }

    public void goBackToMap()
    {
        CombatManager.instance.exitToMap();
        gameManager.instance.isQuestInProgress = false;
    }

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
