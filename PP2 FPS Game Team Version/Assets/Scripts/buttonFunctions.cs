using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    private string input;
    public void resume()
    {
        gameManager.instance.isPaused = !gameManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.isPaused = !gameManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
    }

    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void saveFO()
    {
        if (gameManager.instance.canSprint.value == 0)
        {
            gameManager.instance.playerScript.CanSprint = true;
        }
        else
        {
            gameManager.instance.playerScript.CanSprint = false;
        }
        if (gameManager.instance.canJump.value == 0)
        {
            gameManager.instance.playerScript.CanJump = true;
        }
        else
        {
            gameManager.instance.playerScript.CanJump = false;
        }
        if (gameManager.instance.canCrouch.value == 0)
        {
            gameManager.instance.playerScript.CanCrouch = true;
        }
        else
        {
            gameManager.instance.playerScript.CanCrouch = false;
        }
        if (gameManager.instance.useHeadbob.value == 0)
        {
            gameManager.instance.playerScript.CanUseHeadBob = true;
        }
        else
        {
            gameManager.instance.playerScript.CanUseHeadBob = false;
        }
        if (gameManager.instance.canSlide.value == 0)
        {
            gameManager.instance.playerScript.WillSlideOnSlopes = true;
        }
        else
        {
            gameManager.instance.playerScript.WillSlideOnSlopes = false;
        }
        save();
    }


    void save()
    {
        gameManager.instance.isPaused = !gameManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
    }

    public void functionalOptions()
    {
        editInput(gameManager.instance.functionalOptionsWindow);
    }

    public void movementParameters()
    {
        editInput(gameManager.instance.movementParametersWindow);
    }

    public void lookParameters()
    {
        editInput(gameManager.instance.lookParametersWindow);
    }

    public void jumpParameters()
    {
        editInput(gameManager.instance.jumpParametersWindow);
    }

    public void crouchParameters()
    {
        editInput(gameManager.instance.crouchParametersWindow);
    }

    public void headBobParameters()
    {
        editInput(gameManager.instance.headBobParametersWindow);
    }

    void editInput(GameObject window)
    {
        gameManager.instance.isPaused = !gameManager.instance.isPaused;
        gameManager.instance.UnpauseGame();
        gameManager.instance.isPaused = !gameManager.instance.isPaused;
        gameManager.instance.PauseGame(window);
    }
}
