using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*PhantomRealm Studio - Life of a Recluse
 * Austin Horn
 * CSCI 448, Davenport University
 * Instructor: David Kroggman
 * 
 * Title: RotateOnlyMovement
 * Summary: A basic rotation script meant to rotate a stationary character from left to right, and vice versa, over time.
 *          Intention is to be used with Taste Stand Employee but follows the logic of security cameras in other games.
 *          
 *          +++++++Consider following player if player enters view, then return to normal pattern when they leave view?
 */
public class RotateOnlyMovement : EnemyController
{
    [SerializeField]
    float CurrentRotation;
    float NewRotation;

    //Designate how far in either direction you would like the enemy to turn to. This is in degrees. (Reccomended 50 and -50 for a 180 coverage on the taste stand employee)
    [SerializeField]
    float RotateLeftToPoint = 50;
    [SerializeField]
    float RotateRightToPoint = -50;

    //Designate How Quickly you would like the enemy to rotate. (Reccomended 0.5 for more realistic outcome)
    [SerializeField]
    float RotationSpeed = 0.5f;
    

    [SerializeField]
    bool TurningLeft;
    [SerializeField]
    bool TurningRight;

    //Direction booleans for Animations
    [SerializeField]
    bool FacingRight;
    [SerializeField]
    bool FacingLeft;
    [SerializeField]
    bool FacingForward;
    


    // Start is called before the first frame update
    void Start()
    {
        CurrentRotation = 0;
        transform.up = transform.forward;
        TurningLeft = true;
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Update Triggered");
        
        //Checks to see if its reached designated points and then changes direction of rotation
        if (CurrentRotation >= RotateLeftToPoint) 
        {
            TurningLeft = false;
            TurningRight = true;
        }
        if (CurrentRotation <= RotateRightToPoint)
        {
            TurningLeft = true;
            TurningRight = false;
        }

        //Rotates the gameobject based off of rotation direction
        if (TurningRight) 
        {
            Debug.Log("Rotation Right triggered");
            NewRotation = CurrentRotation - RotationSpeed;
            
            transform.rotation = Quaternion.Euler(0, 0, NewRotation);
            Debug.Log("Rotation Triggered: " + NewRotation);
            //CurrentRotation = gameObject.transform.rotation.z;
            //Debug.Log("CurrentRotation according to system: " + CurrentRotation);
            CurrentRotation = NewRotation;
        }
        
        if (TurningLeft )
        {
            Debug.Log("Rotation Left triggered");
            NewRotation = CurrentRotation + RotationSpeed;
            
            transform.rotation = Quaternion.Euler(0, 0, NewRotation);
            Debug.Log("Rotation Triggered: " + NewRotation);
            //CurrentRotation = gameObject.transform.rotation.z;
            //Debug.Log("CurrentRotation according to system: " + CurrentRotation);
            CurrentRotation = NewRotation;
        }


    }
}
