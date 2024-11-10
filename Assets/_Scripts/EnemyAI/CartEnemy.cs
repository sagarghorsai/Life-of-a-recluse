using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartEnemy : EnemyController
{
    // Update is called once per frame

    public override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
        _waypointMovement.speed = _waypointMovement.speed * movementSpeed; // Attempt to make speed adhere to changes to EnemyController speed, however this has failed before. [AKA if speed bug, look here]
    }
    void Update()
    {
        PreviousDestination = _waypointMovement.GetPreviousDestination();
        CurrentDestination = _waypointMovement.GetCurrentDestination();

        float changeInY = transform.position.y - CurrentDestination.y;
        float changeInX = transform.position.x - CurrentDestination.x;

        // Check Y-axis movement first
        if (Mathf.Abs(changeInY) >= Mathf.Abs(changeInX))
        {
            if (PreviousDestination.y < CurrentDestination.y)
            {
                SetMovementDirection(90, true, false, false, false);
            }
            else if (PreviousDestination.y > CurrentDestination.y)
            {
                SetMovementDirection(270, false, false, false, true);
            }
        }
        else // Otherwise, check X-axis movement
        {
            if (PreviousDestination.x < CurrentDestination.x)
            {
                SetMovementDirection(0, false, true, false, false);
            }
            else if (PreviousDestination.x > CurrentDestination.x)
            {
                SetMovementDirection(180, false, false, true, false);
            }
        }
    }

    // SetMovementDirection to immediately call UpdateAnimation after setting directions
    private void SetMovementDirection(float rotationAngle, bool up, bool right, bool left, bool down)
    {
        // Update animation based on the flags set
        UpdateAnimation();
        // Rotate interactorZone to face the appropriate direction
        interactorZone.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

        // Set movement direction flags and trigger animation
        movingUp = up;
        movingRight = right;
        movingLeft = left;
        movingDown = down;

       
    }
}