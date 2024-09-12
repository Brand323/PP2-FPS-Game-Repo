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

    public void saveMP()
    {
        gameManager.instance.playerScript.WalkSpeed = checkInput(gameManager.instance.playerScript.WalkSpeed, gameManager.instance.walkSpeed);
        gameManager.instance.playerScript.SprintSpeed = checkInput(gameManager.instance.playerScript.SprintSpeed, gameManager.instance.sprintSpeed);
        gameManager.instance.playerScript.CrouchSpeed = checkInput(gameManager.instance.playerScript.CrouchSpeed, gameManager.instance.crouchSpeed);
        gameManager.instance.playerScript.SlopeFallSpeed = checkInput(gameManager.instance.playerScript.SlopeFallSpeed, gameManager.instance.slideSpeed);
        save();
    }

    public void saveJP()
    {
        gameManager.instance.playerScript.Gravity = checkInput(gameManager.instance.playerScript.Gravity, gameManager.instance.gravity);
        gameManager.instance.playerScript.JumpForce = checkInput(gameManager.instance.playerScript.Gravity, gameManager.instance.jumpForce);
        save();
    }

    public void saveCP()
    {
        gameManager.instance.playerScript.CrouchHeight = checkInput(gameManager.instance.playerScript.CrouchHeight, gameManager.instance.crouchHeight);
        gameManager.instance.playerScript.StandingHeight = checkInput(gameManager.instance.playerScript.StandingHeight, gameManager.instance.standingHeight);
        save();
    }

    public void saveHBP()
    {
        gameManager.instance.playerScript.WalkBobSpeed = checkInput(gameManager.instance.playerScript.WalkBobSpeed, gameManager.instance.walkBobSpeed);
        gameManager.instance.playerScript.SprintBobSpeed = checkInput(gameManager.instance.playerScript.SprintBobSpeed, gameManager.instance.sprintBobSpeed);
        gameManager.instance.playerScript.CrouchBobSpeed = checkInput(gameManager.instance.playerScript.CrouchBobSpeed, gameManager.instance.crouchBobSpeed);
        save();
    }

    public void saveAP()
    {
        gameManager.instance.playerScript.MaxHealthPoints = checkInput(gameManager.instance.playerScript.MaxHealthPoints, gameManager.instance.maxHealth);
        gameManager.instance.playerScript.MaxStaminaPoints = checkInput(gameManager.instance.playerScript.MaxStaminaPoints, gameManager.instance.maxStamina);
        gameManager.instance.playerScript.currentHealth = gameManager.instance.playerScript.MaxHealthPoints;
        gameManager.instance.playerScript.currentStamina = gameManager.instance.playerScript.MaxStaminaPoints;
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

    public void attributeParameters()
    {
        editInput(gameManager.instance.attributeParametersWindow);
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

    float checkInput(float variable, TMP_InputField input)
    {
        if (input.text != "")
        {
            variable = float.Parse(input.text);
        }
        return variable;
    }
}
