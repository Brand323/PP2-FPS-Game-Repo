using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FirstPersonController : MonoBehaviour, I_Damage
{
    #region Lambda Expression Explanation

    /*
        The '=>' symbol is a lambda expression syntax, or a expression-bodied member syntax.
            It allows us to write shorter methods more concisely.
        
        For example, instead of writing:
            
                private bool IsSprinting
                {
                    get
                    {
                        return CanSprint && Input.GetKey(sprintKey) && characterController.isGrounded;
                    }
                }

        Instead we can use the lambda expression syntax to write the same logic in one line, like below.
        It basically means "returns the value of the expression on the right side", to my understanding.

    */
    #endregion

    // Determines if the user can move the player character.
    // Default value is set to true, meaning movement is allowed upon launch of game.
    // 'private set' ensures that only THIS class can change the value.
    public bool CanMove { get; private set; } = true;

    // Checks if the player is sprinting.
    // Sprinting is allowed if:
    //      1.  CanSprint is true (player is allowed to sprint).
    //      2.  The sprint key is being HELD down (ex. Left Shift key).
    //      3.  The character is grounded (not in the air), using the character controller component.
    private bool IsSprinting => CanSprint && Input.GetKey(sprintKey) && characterController.isGrounded;

    // Checks if the player should jump.
    // Jumping is allowed when:
    //      1.  The jump key is pressed (ex. Space).
    //      2.  The character is grounded (not in the air), using the character controller component.
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;

    // Checks if the player should crouch.
    // Crouching is allowed when:
    //      1.  The crouch key is pressed (ex. Left Ctrl).
    //      2.  The character is grounded (not in the air), using the character controller component.
    //      3.  No crouch animation is currently in progress.
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && characterController.isGrounded && !duringCrouchAnimation;

    #region Serialized Variables
    // ----- Functional Options -----
    // These options control whether certain player actions are enabled or disabled.
    [Header("----- Functional Options -----")]
    [SerializeField] private bool CanSprint = true;
    [SerializeField] private bool CanJump = true;
    [SerializeField] private bool CanCrouch = true;
    [SerializeField] private bool CanUseHeadBob = true;
    [SerializeField] private bool WillSlideOnSlopes = true;

    // ----- Controls -----
    // These variables store the key bindings for the player controls.
    [Header("----- Controls -----")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    // ----- Attribute Parameters -----
    // These variables control the character's stamina and health.
    [Header("----- Attribute Parameters -----")]
    [SerializeField, Range(0f, 100f)] private float maxHealthPoints;
    [SerializeField, Range(0f, 100f)] private float maxStaminaPoints;
    [SerializeField] private float timeBeforeStaminaRegenStarts = 3f;
    [SerializeField] private float staminaPointsValueIncrement = 1f;
    [SerializeField] private float staminaTimeIncrement = 0.1f;
    private float currentHealth;
    private float currentStamina;


    // ----- Movement Parameters -----
    // These variables control player movement speeds in different states.
    [Header("----- Movement Parameters -----")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float slopeFallSpeed = 8.0f;

    // ----- Look Parameters -----
    // These parameters control how the camera responds to mouse input for looking around.
    [Header("----- Look Parameters -----")]
    [SerializeField, Range(1f, 10f)] private float lookSpeedX = 2.0f; // Mouse sense for horizontal.
    [SerializeField, Range(1f, 10f)] private float lookSpeedY = 2.0f; // Mouse sense for vertical.
    [SerializeField, Range(1f, 180f)] private float upperLookLimit = 80f; // Max upwards camera rotation (clamped).
    [SerializeField, Range(1f, 180f)] private float lowerLookLimit = 80f; // Max downwards camera rotation (clamped).

    // ----- Jumping Parameters -----
    // These parameters control the player's jumping physics and gravity effects.
    [Header("----- Jumping Parameters -----")]
    [SerializeField] private float gravity = 30f;
    [SerializeField] private float jumpForce = 8f; // Force applied to the player when jumping.

    // ----- Crouching Parameters -----
    // These parameters control the players height and center when crouching.
    [Header("----- Crouching Parameters -----")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0); // Center point of the character controller while crouching.
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0); // Center point of the character controller when standing.
    private bool isCrouching;       // Tracks whether the player is currently crouched.
    private bool duringCrouchAnimation; // Tracks whether the crouch animation is still playing.

    // ----- HeadBob Parameters -----
    // These parameters control the head bob effect, which gives a slightly more realistic feel.
    [Header("----- HeadBob Parameters -----")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.11f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.02f;
    private float defaultCamYPosition = 0f; // Default position of the camera (used for headbobbing effect).
    private float headbobTimer;             // Timer used to calculate headbob movement.
    #endregion

    // ----- Slope Sliding ( Player falls down slopes ) -----
    // Detects if the player is sliding down a slope based on the angle of the ground below them.
    private Vector3 hitPointNormal; // Stores the normal vector of the surface the player is on.
    private bool IsSliding
    {
        get
        {
            // Check if the player is grounded and if the slope they're standing on is steep enough to slide.
            if (characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
            {
                hitPointNormal = slopeHit.normal; // Get the normal of the slope surface.
                // Check if the angle of the slope is greater than the slope limit set in the character controller.
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    // Cached components for performance
    private Camera playerCamera;                        // Reference to the camera.
    private CharacterController characterController;    // Reference to the character controller.

    // Movement and Input Tracking
    private Vector3 moveDirection; // Stores the players movement direction.
    private Vector2 currentInput; // Stores the players input.

    // Player rotation
    private float playerRotationX = 0f; // Tracks the players vertical camera rotation.

    #region Unity Methods

    void Awake()
    {
        // Cache the camera and character controller components.
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultCamYPosition = playerCamera.transform.localPosition.y; // Stores the default Y position of the camera for head bobbing.

        // Lock the mouse cursor to the center of the screen and make it invisible.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Cache the player's health and stamina
        currentHealth = maxHealthPoints;
        currentStamina = maxStaminaPoints;

    }

    void Update()
    {
        if (gameManager.instance.isPaused || currentHealth == 0.0f)
            CanMove = false;
        else
        CanMove = true;

        if (CanMove) // Check if the player can currently move.
        {
            HandleMovementInput();  // Process movement input (WASDSPACE).
            HandleMouseLook();      // Handle looking around with the mouse.

            if (CanJump)
            {
                HandleJump();       // Process jump input if jumping is enabled.
            }

            if (CanCrouch)
            {
                HandleCrouch();       // Process crouch input if crouching is enabled.
            }

            if (CanUseHeadBob)
            {
                HandleHeadBob();       // Process jump input if jumping is enabled.
            }






            ApplyFinalMovements(); // Apply the final calculated movement to the player. Must stay at the end.
        }

        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }
    #endregion


    #region Private Methods

    // Handle player movement input.
    // It calculates the player's input based on whether they're crouching, sprinting, or walking,
    //    and then applies the corresponding speed to both vertical and horizontal movement directions.
    private void HandleMovementInput()
    {
        // Determine current movement speed based on player state (crouching, sprinting, or walking).
        currentInput = new Vector2(
            (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Vertical"),
            (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Horizontal")
            );


        // Store the current vertical movement (Y-Axis) so it isn't overriden.
        float moveDirectionY = moveDirection.y;

        // Calculate the new movement direction based on the player's input for forward/backward (Z) and left/right (X).
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) +
                        (transform.TransformDirection(Vector3.right) * currentInput.y);

        // Reapply the vertical movement (Y-Axis) after recalculating the other directions.
        moveDirection.y = moveDirectionY;
    }


    // Handles the camera look direction based on mouse input.
    // The camera rotates vertically, while the player character rotates horizontally.
    private void HandleMouseLook()
    {
        // Adjust vertical camera rotation based on mouse Y-axis input.
        playerRotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;

        // Clamp the vertical rotation to prevent the camera from rotating too far up/down.
        playerRotationX = Mathf.Clamp(playerRotationX, -upperLookLimit, lowerLookLimit);

        // Apply the vertical rotation to the camera's local rotation (only affects the player's view not their movement).
        playerCamera.transform.localRotation = Quaternion.Euler(playerRotationX, 0, 0);

        // Rotate the player character horizontally based on mouse X-axis input.
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }
    

    // Applies the final movement to the character based on gravity and slope sliding.
    // It also handles character controller movement per frame.
    private void ApplyFinalMovements()
    {
        // If the player is not grounded, apply gravity to simulate falling.
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        // If sliding is enabled, and the player is on a slope, apply sliding forces based on the slopes angle.
        if (WillSlideOnSlopes && IsSliding)
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeFallSpeed;

        // Move the player based on the calculated movement direction.
        characterController.Move(moveDirection * Time.deltaTime);
    }


    // Handles the players jumping logic.
    // It checks if the player should jump and applies upward force to simulate a jump.
    private void HandleJump()
    {
        // If the player is allowd to jump, apply upward force to simulate a jump.
        if (ShouldJump)
            moveDirection.y = jumpForce;
    }


    // Handles the player's crouching logic.
    // It checks if the player should crouch and starts the crouch animation if necessary.
    private void HandleCrouch()
    {
        // If the player should crouch, start the coroutine that handles crouching and standing.
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());
    }


    
    // Coroutine that smoothly transitions the player's height and center between crouching and standing.
    // It ensures the transitions look smoother rather than instantaneous.
    private IEnumerator CrouchStand()
    {
        // If the player is crouching and there's an obstacle above them, don't stand up.
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;

        duringCrouchAnimation = true;   // Indicate that crouching is in progress

        // Store the starting time and the current time && the target heights/centers for the transitions.
        float timeElasped = 0f;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        // Smoothly transition the players height and center using Lerp (Linear interpolation).
        while (timeElasped < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElasped / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElasped / timeToCrouch);

            timeElasped += Time.deltaTime;

            yield return null;
        }

        // Once the transition is complete, update the player's height and center.
        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching; // Toggle the current crouching state.
        duringCrouchAnimation = false;  // Indicate that the crouch is finished.

    }



    // Handles the head bobbing effect based on the player's movement.
    // The camera moves up and down slightly to simulate natural head movement while walking or running.
    private void HandleHeadBob()
    {
        // Dont apply head bob if the player is not grounded.
        if (!characterController.isGrounded)
            return;

        // If the player is moving either forward/backward or left/right, apply the head bob effect.
        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            // Increment the head bob timer based on the players movement state.
            headbobTimer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);

            // Apply the head bob effect to the camera's local Y position using a sine wave for smooth oscillation.
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultCamYPosition + Mathf.Sin(headbobTimer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z
                );
        }




    }

    // Handles the player taking damage and updating the player's health respectively.
    // Uses the I_Damage interface.
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            KillPlayer();


    }

    // Handles the player's death.
    // Sets the player's health to 0 and brings up the loss menu.
    private void KillPlayer()
    {
        currentHealth = 0;

        gameManager.instance.LoseUpdate();
    }


    #endregion
}
