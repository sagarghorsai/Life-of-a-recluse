using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movSpeed;  // Movement speed
    float speedX, speedY;   // Movement speed on X and Y axes
    Rigidbody2D rb;         // Reference to Rigidbody2D component
    Animator anim;          // Reference to Animator component
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

    private float activeMoveSpeed;
    public float dashSpeed;     // A public variable where you can set how fast the player can dash.

    public float dashLength = .5f, dashCooldown = 1f;   // Set how long the dash lasts and the cooldown before another dash.

    private float dashCounter;            // How long the dash is active.      
    private float dashCoolCounter;        // Cooldown time before dash is available again.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // Get the Rigidbody2D component
        anim = GetComponent<Animator>();    // Get the Animator component

        activeMoveSpeed = movSpeed;     // Set dash to player's normal movement speed initially.
    }

    // Update is called once per frame
    void Update()
    {
        // Standard movement
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;

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
        }

        // Handle dashing
        if (Input.GetKeyDown(KeyCode.Space))    // Dash activation with Space key
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)    // Dash is available
            {
                activeMoveSpeed = dashSpeed;   // Set movement speed to dash speed
                dashCounter = dashLength;      // Set dash duration
            }
        }

        if (dashCounter > 0)        // If dashing is active
        {
            dashCounter -= Time.deltaTime;  // Count down dash time

            if (dashCounter <= 0)       // Dash duration over
            {
                activeMoveSpeed = movSpeed;  // Reset movement speed to normal
                dashCoolCounter = dashCooldown;  // Start cooldown
            }
        }

        if (dashCoolCounter > 0)    // If in cooldown period
        {
            dashCoolCounter -= Time.deltaTime;  // Count down cooldown time
        }

        rb.velocity = vel * activeMoveSpeed;    // Set velocity (using activeMoveSpeed to account for dashing)

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
    }
}
