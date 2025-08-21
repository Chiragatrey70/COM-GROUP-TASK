using UnityEngine;
using TMPro; // ADDED THIS LINE for Text Mesh Pro

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
    public float crouchHeight = 0.9f;
    private float standingHeight;

    // --- Camera Control Variables ---
    [Header("Camera Settings")]
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    // --- Physics Variables ---
    [Header("Physics")]
    public float gravity = -19.62f;

    // --- Interaction Variables ---
    [Header("Interaction Settings")]
    public float interactionDistance = 3f;
    public TextMeshProUGUI interactionText; // CHANGED THIS LINE for Text Mesh Pro

    // --- Private Variables ---
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool isCrouching = false;
    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        standingHeight = characterController.height;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Make sure the interaction text is hidden at the start
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleCameraLook();
        HandleInteractionCheck();
    }

    // --- NEW PUBLIC METHOD ---
    // Allows other scripts to enable or disable player movement
    public void SetMovement(bool canPlayerMove)
    {
        canMove = canPlayerMove;
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning && !isCrouching ? runSpeed : (isCrouching ? crouchSpeed : walkSpeed)) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning && !isCrouching ? runSpeed : (isCrouching ? crouchSpeed : walkSpeed)) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !isCrouching)
        {
            moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                characterController.height = standingHeight;
                isCrouching = false;
            }
            else
            {
                characterController.height = crouchHeight;
                isCrouching = true;
            }
        }

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

    private void HandleInteractionCheck()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
                if (interactable != null)
                {
                    interactionText.text = "[E] " + interactable.interactionPrompt;
                    interactionText.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactable.Interact();
                    }
                }
            }
            else
            {
                interactionText.gameObject.SetActive(false);
            }
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}