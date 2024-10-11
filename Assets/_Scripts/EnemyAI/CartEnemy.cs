using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartEnemy : EnemyController
{
    // Update is called once per frame

    private void Awake()
    {
        base.Awake();
    }
    protected virtual void Update()
    {
        UpdateAnimation();
        PreviousDestination = _waypointMovement.GetPreviousDestination();
        CurrentDestination = _waypointMovement.GetCurrentDestination();

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




    protected virtual void UpdateAnimation()
    {
        anim.SetBool("isMovingUp", movingUp);
        anim.SetBool("isMovingDown", movingDown);
        anim.SetBool("isMovingLeft", movingLeft);
        anim.SetBool("isMovingRight", movingRight);
    }
}