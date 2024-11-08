using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartEnemy : EnemyController
{
    // Update is called once per frame

    private void Awake()
    {
        base.Awake();

        _waypointMovement.speed = _waypointMovement.speed * movementSpeed; // Attempt to make speed adhere to changes to EnemyController speed, however this has failed before. [AKA if speed bug, look here]
    }
    protected virtual void Update()
    {
        UpdateAnimation();
        PreviousDestination = _waypointMovement.GetPreviousDestination();
        CurrentDestination = _waypointMovement.GetCurrentDestination();

        float changeInY = transform.position.y - CurrentDestination.x;
        float changeInX = transform.position.x - CurrentDestination.x;

        if (Mathf.Abs(changeInY) >= Mathf.Abs(changeInX)) // if Y is a greater difference or equal to, then move in the y direction
        {
            if (PreviousDestination.y < CurrentDestination.y)
            {
                interactorZone.transform.rotation = Quaternion.Euler(0, 0, 90);

                movingUp = true;
                movingRight = false;
                movingLeft = false;
                movingDown = false;

            }
            if (PreviousDestination.y > CurrentDestination.y)
            {
                interactorZone.transform.rotation = Quaternion.Euler(0, 0, 270);

                movingDown = true;
                movingRight = false;
                movingLeft = false;
                movingUp = false;

            }
        }
        if (Mathf.Abs(changeInY) < Mathf.Abs(changeInX)) // if X is greater difference, then move in the x direction
        {
            if (PreviousDestination.x < CurrentDestination.x)
            {
                movingRight = true;
                movingLeft = false;
                movingUp = false;
                movingDown = false;


                interactorZone.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (PreviousDestination.x > CurrentDestination.x)
            {
                interactorZone.transform.rotation = Quaternion.Euler(0, 0, 180);

                movingLeft = true;
                movingRight = false;
                movingUp = false;
                movingDown = false;

            }
        }
    }




}