using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    float speedX, speedY;   // Movement speed on X and Y axes
    Rigidbody2D rb;         // Reference to Rigidbody2D component
    Animator anim;          // Reference to Animator component
    AudioManager audioManager; // Reference to the AudioManager

    public Vector2Int FacingDirection { get; private set; } = Vector2Int.right;

    // Directions for movement (right, up, left, down)
    private Vector2[] directions = new Vector2[]
    { Vector2.right, Vector2.up, Vector2.left, Vector2.down };

    // Key bindings for movement (arrow keys and WASD)
    private KeyCode[] keys = new KeyCode[]
    {
        KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow,
        KeyCode.D,          KeyCode.W,       KeyCode.A,         KeyCode.S
    };

    private int lastDirHeld = 0; // Stores the last direction the player was moving in
    private bool isMoving = false; // Track whether the player is moving

    [Header("---------- Sprint Setting ----------")]
    public float sprintSpeed = 10f;           // Speed while sprinting
    public float walkSpeed = 5f;              // Speed while walking
    public float maxStamina = 1f;             // Maximum stamina value
    public float staminaDecreaseRate = 0.5f;  // Rate at which stamina decreases when sprinting
    public float staminaRegenRate = 0.2f;     // Rate at which stamina regenerates when not sprinting
    [SerializeField] private float footstepInterval = 0.4f; // Adjust based on speed
    private float footstepTimer;

    [Header("---------- UI Component----------")]
    public Image staminaImage;              // Reference to the stamina UI slider
    public float currentStamina;             // Current stamina value
    private bool isSprinting = false;         // Whether the player is currently sprinting
    private bool canSprint = true;            // Whether the player is allowed to sprint
    private float activeSpeed;                // Variable to track current speed

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // Get the Rigidbody2D component
        anim = GetComponent<Animator>();    // Get the Animator component
        audioManager = FindObjectOfType<AudioManager>(); // Find the AudioManager in the scene

        activeSpeed = walkSpeed;     // Set dash to player's normal movement speed initially.
        currentStamina = maxStamina;
        if (staminaImage != null)
        {
            staminaImage.fillAmount = currentStamina;
        }
        if (audioManager == null)
        {
            Debug.Log("AudioManager not found on the scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        HandleSprintInput();          // Check for sprint input
        UpdateStamina();              // Update stamina based on sprinting state
        UpdateUI();                   // Update the UI to reflect current stamina
        
    }

    void Movement()
    {
        // Standard movement
        speedX = Input.GetAxisRaw("Horizontal") * activeSpeed;
        speedY = Input.GetAxisRaw("Vertical") * activeSpeed;
        Vector2 movement = new Vector2(speedX, speedY);

        int dirHeld = -1; // Tracks which direction key is held

        // Check for key press and determine direction
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKey(keys[i]))
            {
                dirHeld = i % 4;  // Get corresponding direction from array
                FacingDirection = new Vector2Int(Mathf.RoundToInt(directions[dirHeld].x), Mathf.RoundToInt(directions[dirHeld].y));
                break; // Exit loop once a key is found
            }
        }

        Vector2 vel = Vector2.zero;

        // Apply velocity based on direction held
        if (dirHeld > -1)
        {
            vel = directions[dirHeld];
            lastDirHeld = dirHeld; // Update last direction when a key is pressed
            if (!isMoving)
            {
                isMoving = true; // Set moving state to true
            }
        }
        else
        {
            isMoving = false; // Set moving state to false
        }
        rb.velocity = vel * activeSpeed;    // Set velocity (using activeMoveSpeed to account for dashing)

        // Animation logic
        if (dirHeld == -1) // No key pressed, play idle animation based on last direction
        {
            anim.Play("AdamIdle_" + lastDirHeld);
        }
        else // A key is pressed, run the corresponding run animation
        {
            anim.Play("AdamRun_" + dirHeld);
        }

        anim.speed = 1; // Ensure animation speed is set to 1

        HandleFootsteps(movement); // Play footstep audio when movement starts

    }

    private void HandleSprintInput()
    {
        // Prevent sprinting if stamina is not full or sprint key is not pressed
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && canSprint)
        {
            isSprinting = true;
            activeSpeed = sprintSpeed;
        }
        else
        {
            isSprinting = false;
            activeSpeed = walkSpeed;
        }
    }

    private void UpdateStamina()
    {
        if (isSprinting)
        {
            // Reduce stamina while sprinting
            currentStamina -= staminaDecreaseRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            // If stamina reaches 0, stop sprinting and set canSprint to false
            if (currentStamina <= 0)
            {
                isSprinting = false;
                canSprint = false;
                activeSpeed = walkSpeed;
            }
        }
        else
        {
            // Regenerate stamina when not sprinting
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            // Allow sprinting again only when stamina is full
            if (currentStamina >= maxStamina)
            {
                canSprint = true;
            }
        }
    }

    private void UpdateUI()
    {
        // Update the stamina slider value to reflect current stamina
        if (staminaImage != null)
        {
            staminaImage.fillAmount = currentStamina / maxStamina;
        }
    }

    private void HandleFootsteps(Vector2 movement)
    {
        // Only play footsteps when the player is moving
        if (movement.magnitude > 0)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                if (isSprinting)
                {
                    audioManager.PlaySFX("Sprint"); // Play sprint footstep sound
                    footstepTimer = footstepInterval/2; // Reset the timer

                }
                else
                {
                    audioManager.PlaySFX("Footstep"); // Play normal footstep sound
                    footstepTimer = footstepInterval; // Reset the timer

                }
            }
        }
        else
        {
            footstepTimer = 0f; // Reset the timer if the player is not moving
        }
    }
}