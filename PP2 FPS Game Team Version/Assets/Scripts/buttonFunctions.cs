using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
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

    public void save()
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
