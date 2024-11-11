using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*PhantomRealm Studio - Life of a Recluse
 * Austin Horn
 * CSCI 448, Davenport University
 * Instructor: David Kroggman
 * 
 * Script: EnemyController
 * Summary: A script for universal items for each enemy.
 *          Including: 
 *                 + Change Animations based on direction the enemy is moving
 *                 + flip directions when necessary
 *                 + rotate towards next designation
 *                 
 */
public class EnemyController : MonoBehaviour
{

    [Header("---------- Refrences ----------")]
    public WaypointMovement _waypointMovement;
    //public ScriptReference movementScript;
    private Rigidbody2D _rigidbody;
    [HideInInspector]
    public Vector2 CurrentDestination;
    [HideInInspector]
    public Vector2 PreviousDestination;
    [HideInInspector]
    public Vector2 _targetDirection;
    public Animator anim;
    public GameObject interactorZone;
    public float movementSpeed;


    [Header("---------- Direction ----------")]

    public bool movingRight;
    public bool movingLeft;
    public bool movingUp;
    public bool movingDown;
    bool directionChange;
    // Start is called before the first frame update


    public virtual void Awake()
    {
        _waypointMovement = GetComponent<WaypointMovement>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void UpdateAnimation()
    {
        if (anim != null) // Check if the animator is available
        {
            anim.SetBool("isMovingUp", movingUp);
            anim.SetBool("isMovingDown", movingDown);
            anim.SetBool("isMovingLeft", movingLeft);
            anim.SetBool("isMovingRight", movingRight);
        }
    }


}
