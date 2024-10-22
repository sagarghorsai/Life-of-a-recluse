using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

/*PhantomRealm Studio - Life of a Recluse
 * Austin Horn
 * CSCI 448, Davenport University
 * Instructor: David Kroggman
 * 
 * Title: RandomizedMovement
 * Summary: A script intended for the child customer enemy in "Life of a Recluse"
 *          the script should randomize the direction and distance in which the child enemy moves 
 *              the enemy should either trying to avoid enviromental obstacles (shelves, displays, etc.) or simply stop upon collision with them.
 */              

public class RandomizedMovement : MonoBehaviour
{
    float randomizedDirection;
    float randomizedDistance;
    private Vector3 PreviousDestination;
    private Vector3 CurrentDestination;

    public float movementSpeed = 1f;
    public float directionChangeCooldown;
    public Animator anim;

    [Header("-------Directions------")]
    public bool movingUp;
    public bool movingDown;
    public bool movingLeft;
    public bool movingRight;
    public bool isStationary;

    // Start is called before the first frame update
    void Awake()
    {
        PreviousDestination = transform.position;
        anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Update triggered");
        PreviousDestination = transform.position;
        RandomDirectionChange();
        MoveTowardsCurrentDestination();
        IsStationaryCheck();
        UpdateAnimation();

    }

    private void RandomDirectionChange()
    {
        directionChangeCooldown -= Time.deltaTime;

        if (directionChangeCooldown <= 0)
        {
            randomizedDirection = Random.Range(1, 5);
            Debug.Log("Randomized Direction: " + randomizedDirection);

            randomizedDistance = Random.Range(1, 15);
            Debug.Log("Randomized Distance: " + randomizedDirection);

            switch (randomizedDirection)
            {
                case 1: //Go Up
                    Debug.Log("Switch triggerd 1");
                    movingUp = true;
                    movingRight = false;
                    movingDown = false;
                    movingLeft = false;
                    CurrentDestination = new Vector3(transform.position.x, transform.position.y + randomizedDistance, 0f);
                    break;

                case 2: //Go Right
                    Debug.Log("Switch triggerd 2");
                    movingRight = true;
                    movingUp = false;
                    movingDown = false;
                    movingLeft = false;
                    CurrentDestination = new Vector3(transform.position.x + randomizedDistance, transform.position.y, 0f);
                    break;

                case 3: //Go Down
                    Debug.Log("Switch triggerd 3");
                    movingDown = true;
                    movingRight = false;
                    movingUp = false;
                    movingLeft = false;
                    CurrentDestination = new Vector3(transform.position.x, transform.position.y - randomizedDistance, 0f);
                    break;

                case 4: //Go Left
                    Debug.Log("Switch triggerd 4");
                    movingLeft = true;
                    movingRight = false;
                    movingUp = false;
                    movingDown = false;
                    CurrentDestination = new Vector3(transform.position.x - randomizedDistance, transform.position.y, 0f);
                    break;

                default:
                    Debug.Log("default switch triggered");
                    break;
            }

            directionChangeCooldown = Random.Range(1f, 5f);
        }
    }

    private void MoveTowardsCurrentDestination()
    {
        if (movingUp)
        {
            Debug.Log("move up triggered");
            transform.position = Vector3.MoveTowards(transform.position, CurrentDestination, movementSpeed * Time.deltaTime);
        }
        if (movingDown)
        {
            Debug.Log("move down triggered");
            transform.position = Vector3.MoveTowards(transform.position, CurrentDestination, movementSpeed * Time.deltaTime);
        }
        if (movingLeft)
        {
            Debug.Log("move left triggered");
            transform.position = Vector3.MoveTowards(transform.position, CurrentDestination, movementSpeed * Time.deltaTime);
        }
        if (movingRight)
        {
            Debug.Log("move right triggered");
            transform.position = Vector3.MoveTowards(transform.position, CurrentDestination, movementSpeed * Time.deltaTime);
        }
    }

    protected virtual void UpdateAnimation()
    {
        anim.SetBool("isMovingUp", movingUp);
        anim.SetBool("isMovingDown", movingDown);
        anim.SetBool("isMovingLeft", movingLeft);
        anim.SetBool("isMovingRight", movingRight);
        anim.SetBool("isStationary", isStationary);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Walls") 
        {
            directionChangeCooldown = 0;
            RandomDirectionChange();
            MoveTowardsCurrentDestination();
            UpdateAnimation();
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            directionChangeCooldown = 0;
            RandomDirectionChange();
            MoveTowardsCurrentDestination();
            UpdateAnimation();
        }
    }

    private void IsStationaryCheck()
    {
        if(PreviousDestination == CurrentDestination)
        {
            isStationary = true;
        }
        else
        {
            isStationary = false;
        }
    }
}
