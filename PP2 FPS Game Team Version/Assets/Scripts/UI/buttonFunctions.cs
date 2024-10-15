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
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
    }

    public void restart()
    {
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void respawn()
    {
        gameManager.instance.playerScript.spawnPlayer();
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
    }

    #endregion


    //public void activateWave()
    //{
    //    WaveManager.instance.triggerGate.StartWave();
    //}

    public void startGame()
    {
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.UnpauseGame();
    }

    public void acceptQuest()
    {
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.UnpauseGame();
        UIManager.instance.activateQuestWindow(UIManager.instance.questDescriptionWindow);
    }

    public void rejectQuest()
    {
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
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        gameManager.instance.PauseGame(window);
    }

    float checkInput(float variable, TMP_InputField input)
    {
        if (input.text != "")
        {
            variable = float.Parse(input.text);
        }
        return variable;
    }

    int checkInputInt(int variable, TMP_InputField input)
    {
        if (input.text != "")
        {
            variable = int.Parse(input.text);
        }
        return variable;
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
