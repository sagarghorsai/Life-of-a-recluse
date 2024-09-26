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
    [SerializeField]
    private float _rotationSpeed;

    WaypointMovement _waypointMovement;

    //public ScriptReference movementScript;
    private Rigidbody2D _rigidbody;
    private Vector2 CurrentDestination;
    private Vector2 PreviousDestination;
    private Vector2 _targetDirection;
    public Animator anim;
    public GameObject interactorZone;

    public bool movingRight;
    public bool movingLeft;
    public bool movingUp;
    public bool movingDown;
    bool directionChange;
    // Start is called before the first frame update
    private void Awake()
    {
        anim.GetComponent<Animator>();
        _waypointMovement = GetComponent<WaypointMovement>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //_targetDirection = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        PreviousDestination = _waypointMovement.GetPreviousDestination();
        CurrentDestination = _waypointMovement.GetCurrentDestination();
        UpdateAnimation();

        if (PreviousDestination.x < CurrentDestination.x)
        {
            movingRight = true;
            movingLeft = false;
            movingUp = false;
            movingDown = false;
            // _rigidbody.transform.up = CurrentDestination;
            _rigidbody.MoveRotation(ConvertToDegrees(Mathf.Atan2(CurrentDestination.y, CurrentDestination.x)));

            interactorZone.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (PreviousDestination.x > CurrentDestination.x)
        {
            interactorZone.transform.rotation = Quaternion.Euler(0, 0, 180);

            movingLeft = true;
            movingRight = false;
            movingUp = false;
            movingDown = false;
            // _rigidbody.transform.up = CurrentDestination;
            _rigidbody.MoveRotation(ConvertToDegrees(Mathf.Atan2(CurrentDestination.y, CurrentDestination.x)));
        }
        if (PreviousDestination.y < CurrentDestination.y)
        {
            interactorZone.transform.rotation = Quaternion.Euler(0, 0, 90);

            movingUp = true;
            movingRight = false;
            movingLeft = false;
            movingDown = false;
            //_rigidbody.transform.up = CurrentDestination;
            _rigidbody.MoveRotation(ConvertToDegrees(Mathf.Atan2(CurrentDestination.y, CurrentDestination.x)));
        }
        if (PreviousDestination.y > CurrentDestination.y)
        {
            interactorZone.transform.rotation = Quaternion.Euler(0, 0, 270);

            movingDown = true;
            movingRight = false;
            movingLeft = false;
            movingUp = false;
            //_rigidbody.transform.up = CurrentDestination;
            _rigidbody.MoveRotation(ConvertToDegrees(Mathf.Atan2(CurrentDestination.y, CurrentDestination.x)));
        }
    }

    void UpdateAnimation()
    {
        anim.SetBool("isMovingUp", movingUp);
        anim.SetBool("isMovingDown", movingDown);
        anim.SetBool("isMovingLeft", movingLeft);
        anim.SetBool("isMovingRight", movingRight);


    }

    public static float ConvertToDegrees(float radians)
    {
        float angle = radians * 180f / Mathf.PI;
        return angle;
    }
}