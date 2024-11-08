using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/*PhantomRealm Studio - Life of a Recluse
 * Austin Horn
 * CSCI 448, Davenport University
 * Instructor: David Kroggman
 * 
 * Title: HuntPlayerMovement
 * Summary: A movement script intended for the Manager Enemy in "Life of a Recluse"
 *          Script should identify current location of player, move towards player and try to avoid enviromental objects (shelves, displays, etc.)
 *          
 *          Utilizes NavMeshPlus to avoid obstacles
 *          
 */



public class HuntPlayerMovement : EnemyController
{
    //Player targeting variables
    public GameObject player;

    //movement variables
    [SerializeField]
    private float rotationSpeed;

    private Rigidbody2D thisRigidbody;
    private Vector3 targetDirection;


    // NavMesh
    NavMeshAgent agent;


    private void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.speed = agent.speed * movementSpeed; // Attempt to make speed adhere to changes to EnemyController speed, however this has failed before. [AKA if speed bug, look here]
    }

     void FixedUpdate()
    {
        HandlePlayerTargeting();
        SetAgentPosition();
        UpdateDirection();
        UpdateAnimation();
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(targetDirection.x, targetDirection.y, transform.position.z));
    }

    private void UpdateDirection() 
    {

            float changeInY = transform.position.y - targetDirection.y;
            float changeInX = transform.position.x - targetDirection.x;

            if (Mathf.Abs(changeInY) >= Mathf.Abs(changeInX)) // if Y is a greater difference or equal to, then move in the y direction
            {
                if (changeInY > 0) //if a positive number then enemy must move down
                {
                    movingDown = true;
                    movingUp = false;
                    movingRight = false;
                    movingLeft = false;
                }
                if (changeInY < 0) //if negative number then enemy must move up
                {
                    movingDown = false;
                    movingUp = true;
                    movingRight = false;
                    movingLeft = false;
                }
            }
        if (Mathf.Abs(changeInY) < Mathf.Abs(changeInX)) // if X is greater difference, then move in the x direction
        {
            if (changeInX > 0) //if a positive number then enemy must move left
            {
                movingDown = false;
                movingUp = false;
                movingRight = false;
                movingLeft = true;
            }
            if (changeInX < 0) //if negative number then enemy must move right
            {
                movingDown = false;
                movingUp = false;
                movingRight = true;
                movingLeft = false;
        }
    }

}

    private void HandlePlayerTargeting()
    {
        targetDirection = player.transform.position;
    }


    protected virtual void UpdateAnimation()
    {
        anim.SetBool("isMovingUp", movingUp);
        anim.SetBool("isMovingDown", movingDown);
        anim.SetBool("isMovingLeft", movingLeft);
        anim.SetBool("isMovingRight", movingRight);
    }
}
