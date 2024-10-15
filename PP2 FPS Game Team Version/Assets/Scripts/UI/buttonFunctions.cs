using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    private string input;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
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

    public void optionsMenu()
    {
        editInput(UIManager.instance.optionsWindow);
    }

    public void creditsMenu()
    {
        editInput(UIManager.instance.creditsWindow);
    }

    public void exitToMain()
    {
        editInput(UIManager.instance.mainWindow);
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
