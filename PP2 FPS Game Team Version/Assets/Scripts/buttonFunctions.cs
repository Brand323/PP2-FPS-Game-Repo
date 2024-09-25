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

    public void saveFO()
    {
        if (UIManager.instance.canSprint.value == 0)
        {
            gameManager.instance.playerScript.CanSprint = true;
        }
        else
        {
            gameManager.instance.playerScript.CanSprint = false;
        }
        if (UIManager.instance.canJump.value == 0)
        {
            gameManager.instance.playerScript.CanJump = true;
        }
        else
        {
            gameManager.instance.playerScript.CanJump = false;
        }
        if (UIManager.instance.canCrouch.value == 0)
        {
            gameManager.instance.playerScript.CanCrouch = true;
        }
        else
        {
            gameManager.instance.playerScript.CanCrouch = false;
        }
        if (UIManager.instance.useHeadbob.value == 0)
        {
            gameManager.instance.playerScript.CanUseHeadBob = true;
        }
        else
        {
            gameManager.instance.playerScript.CanUseHeadBob = false;
        }
        if (UIManager.instance.canSlide.value == 0)
        {
            gameManager.instance.playerScript.WillSlideOnSlopes = true;
        }
        else
        {
            gameManager.instance.playerScript.WillSlideOnSlopes = false;
        }
        save();
    }

    public void saveMP()
    {
        gameManager.instance.playerScript.WalkSpeed = checkInput(gameManager.instance.playerScript.WalkSpeed, UIManager.instance.walkSpeed);
        gameManager.instance.playerScript.SprintSpeed = checkInput(gameManager.instance.playerScript.SprintSpeed, UIManager.instance.sprintSpeed);
        gameManager.instance.playerScript.CrouchSpeed = checkInput(gameManager.instance.playerScript.CrouchSpeed, UIManager.instance.crouchSpeed);
        gameManager.instance.playerScript.SlopeFallSpeed = checkInput(gameManager.instance.playerScript.SlopeFallSpeed, UIManager.instance.slideSpeed);
        save();
    }

    public void saveJP()
    {
        gameManager.instance.playerScript.Gravity = checkInput(gameManager.instance.playerScript.Gravity, UIManager.instance.gravity);
        gameManager.instance.playerScript.JumpForce = checkInput(gameManager.instance.playerScript.Gravity, UIManager.instance.jumpForce);
        save();
    }

    public void saveCP()
    {
        gameManager.instance.playerScript.CrouchHeight = checkInput(gameManager.instance.playerScript.CrouchHeight, UIManager.instance.crouchHeight);
        gameManager.instance.playerScript.StandingHeight = checkInput(gameManager.instance.playerScript.StandingHeight, UIManager.instance.standingHeight);
        save();
    }

    public void saveHBP()
    {
        gameManager.instance.playerScript.WalkBobSpeed = checkInput(gameManager.instance.playerScript.WalkBobSpeed, UIManager.instance.walkBobSpeed);
        gameManager.instance.playerScript.SprintBobSpeed = checkInput(gameManager.instance.playerScript.SprintBobSpeed, UIManager.instance.sprintBobSpeed);
        gameManager.instance.playerScript.CrouchBobSpeed = checkInput(gameManager.instance.playerScript.CrouchBobSpeed, UIManager.instance.crouchBobSpeed);
        save();
    }

    public void saveAP()
    {
        gameManager.instance.playerScript.MaxHealthPoints = checkInput(gameManager.instance.playerScript.MaxHealthPoints, UIManager.instance.maxHealth);
        gameManager.instance.playerScript.MaxStaminaPoints = checkInput(gameManager.instance.playerScript.MaxStaminaPoints, UIManager.instance.maxStamina);
        gameManager.instance.playerScript.currentHealth = gameManager.instance.playerScript.MaxHealthPoints;
        gameManager.instance.playerScript.currentStamina = gameManager.instance.playerScript.MaxStaminaPoints;
        save();
    }

    public void saveEPW()
    {
        WaveManager.instance.enemiesPerWave = checkInputInt(WaveManager.instance.enemiesPerWave, UIManager.instance.enemiesPerWaveText);
        save();
    }

    public void functionalOptions()
    {
        editInput(UIManager.instance.functionalOptionsWindow);
    }

    public void movementParameters()
    {
        editInput(UIManager.instance.movementParametersWindow);
    }

    public void attributeParameters()
    {
        editInput(UIManager.instance.attributeParametersWindow);
    }

    public void jumpParameters()
    {
        editInput(UIManager.instance.jumpParametersWindow);
    }

    public void crouchParameters()
    {
        editInput(UIManager.instance.crouchParametersWindow);
    }

    public void headBobParameters()
    {
        editInput(UIManager.instance.headBobParametersWindow);
    }

    public void enemiesPerWaveParameter()
    {
        editInput(UIManager.instance.enemiesPerWaveWindow);
    }

    public void activateWave()
    {
        WaveManager.instance.triggerGate.StartWave();
    }

    public void turnEndWaveMenuOff()
    {
        gameManager.instance.deactivateWaveEndMenu();
    }

    public void startGame()
    {
        UIManager.instance.isPaused = !UIManager.instance.isPaused;
        UIManager.instance.UnpauseGame();
    }

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
}
