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
 *          Used the following tutorial as a reference: https://www.youtube.com/watch?v=WK0fBiytW_8&ab_channel=KetraGames
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
    private float directionChangeCooldown;

    // NavMesh
    NavMeshAgent agent;

    //Collision Avoidance variables
    [SerializeField]
    private float obstacleCheckCircleRadius;

    [SerializeField]
    private float obstacleCheckDistance;

    [SerializeField]
    private LayerMask obstacleLayerMask;


    private RaycastHit2D[] obstacleCollisions;
    private Vector2 _obstacleAvoidanceTargetDirection;
    private float _obstacleAvoidanceCooldown;

    private void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //obstacleCollisions = new RaycastHit2D[10];

        //if(movementSpeed == null || movementSpeed == 0)
        //{
        //    movementSpeed = 1;
        //}
    }

     void FixedUpdate()
    {
        HandlePlayerTargeting();
        SetAgentPosition();
        //HandleObstacles();
        UpdateDirection();
        //MoveTowardsTargetDestination();
        UpdateAnimation();
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(targetDirection.x, targetDirection.y, transform.position.z));
    }

    private void UpdateDirection() 
    {
        //directionChangeCooldown -= Time.deltaTime;

        //if (directionChangeCooldown <= 0)
        //{

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
        //    }
        //directionChangeCooldown = Random.Range(1f, 5f);
    }

}

    private void HandlePlayerTargeting()
    {
        targetDirection = player.transform.position;
    }

    //private void MoveTowardsTargetDestination()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, targetDirection, movementSpeed * Time.deltaTime);
    //}

    //private void HandleObstacles()
    //{
    //    var contactFilter = new ContactFilter2D();
    //    contactFilter.SetLayerMask(obstacleLayerMask);

    //    int numberOfCollisions = Physics2D.CircleCast(transform.position, obstacleCheckCircleRadius, transform.up, contactFilter, obstacleCollisions, obstacleCheckDistance);

    //    for (int index = 0; index < numberOfCollisions; index++)
    //    {
    //        var obstacleCollision = obstacleCollisions[index];

    //        if (obstacleCollision.collider.gameObject == gameObject)
    //        {
    //            continue;
    //        }

    //        if (_obstacleAvoidanceCooldown <= 0)
    //        {
    //            _obstacleAvoidanceTargetDirection = obstacleCollision.normal;
    //            _obstacleAvoidanceCooldown = 0.5f;
    //        }

    //        var targetRotation = Quaternion.LookRotation(transform.forward, _obstacleAvoidanceTargetDirection);
    //        var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    //        _targetDirection = rotation * Vector2.up;
    //        thisRigidbody.velocity = transform.up * movementSpeed;
    //        break;
    //    }
    //}


    protected virtual void UpdateAnimation()
    {
        anim.SetBool("isMovingUp", movingUp);
        anim.SetBool("isMovingDown", movingDown);
        anim.SetBool("isMovingLeft", movingLeft);
        anim.SetBool("isMovingRight", movingRight);
    }

    //private void OnCollisionStay(Collision collision)
    //{

    //    if (collision.gameObject.tag == "Walls")
    //    {
            
    //        if(movingUp && movingRight)
    //        {
    //            targetDirection.x = targetDirection.x + 10;
    //            targetDirection.y = targetDirection.y + 10;
    //            transform.position = Vector3.MoveTowards(transform.position, targetDirection, movementSpeed * Time.deltaTime);
                
    //        }
    //        if (movingUp && movingLeft)
    //        {
    //            targetDirection.x = targetDirection.x - 10;
    //            targetDirection.y = targetDirection.y + 10;
    //            transform.position = Vector3.MoveTowards(transform.position, targetDirection, movementSpeed * Time.deltaTime);
    //        }
    //        if (movingDown && movingRight)
    //        {
    //            targetDirection.x = targetDirection.x + 10;
    //            targetDirection.y = targetDirection.y - 10;
    //            transform.position = Vector3.MoveTowards(transform.position, targetDirection, movementSpeed * Time.deltaTime);
    //        }
    //        if (movingDown && movingLeft)
    //        {
    //            targetDirection.x = targetDirection.x - 10;
    //            targetDirection.y = targetDirection.y - 10;
    //            transform.position = Vector3.MoveTowards(transform.position, targetDirection, movementSpeed * Time.deltaTime);
    //        }
    //        if(movingUp || movingDown)
    //        {
    //            targetDirection.x += 10;
    //            targetDirection.y = transform.position.y;
    //            transform.position = Vector3.MoveTowards(transform.position, targetDirection, movementSpeed * Time.deltaTime);
    //        }
            
    //        if (movingLeft || movingRight)
    //        {
    //            targetDirection.y += 10;
    //            targetDirection.x = transform.position.x;
    //            transform.position = Vector3.MoveTowards(transform.position, targetDirection, movementSpeed * Time.deltaTime);
    //        }
          

    //        UpdateAnimation();
    //    }
    //}
}
