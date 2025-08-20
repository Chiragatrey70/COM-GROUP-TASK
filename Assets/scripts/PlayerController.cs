using UnityEngine;

// This attribute ensures that a CharacterController component is attached to the same GameObject.
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // --- Player Movement Variables ---
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpHeight = 2f;
    public float crouchSpeed = 2.5f;
    public float crouchHeight = 0.9f; // The height of the controller when crouching
    private float standingHeight; // The original height of the controller

    // --- Camera Control Variables ---
    [Header("Camera Settings")]
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f; // Limit for looking up and down

    // --- Physics Variables ---
    [Header("Physics")]
    public float gravity = -19.62f; // A more realistic gravity value (2 * 9.81)

    // --- Private Variables ---
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool isCrouching = false;

    // --- Input Variables ---
    private bool canMove = true; // Can be used to disable player movement during cutscenes, etc.

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();

        // Store the original height of the Character Controller
        standingHeight = characterController.height;

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Movement Logic ---
        HandleMovement();

        // --- Camera Logic ---
        HandleCameraLook();
    }

    private void HandleMovement()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Check for running input (Left Shift)
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Determine current speed based on whether the player is running, crouching, or walking
        float curSpeedX = canMove ? (isRunning && !isCrouching ? runSpeed : (isCrouching ? crouchSpeed : walkSpeed)) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning && !isCrouching ? runSpeed : (isCrouching ? crouchSpeed : walkSpeed)) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // --- Jumping Logic ---
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !isCrouching)
        {
            moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below)
        // This is because gravity is an acceleration (m/s^2)
        if (!characterController.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        // --- Crouching Logic ---
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                // Stand up
                characterController.height = standingHeight;
                isCrouching = false;
            }
            else
            {
                // Crouch down
                characterController.height = crouchHeight;
                isCrouching = true;
            }
        }


        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleCameraLook()
    {
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}