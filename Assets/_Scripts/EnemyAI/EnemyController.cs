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

    //public ScriptReference movementScript;
    private Rigidbody2D _rigidbody;
    private Vector2 _targetDirection;

    // Start is called before the first frame update
    private void Awake()
    {
        _targetDirection = transform.up;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotateInDirectionOfMovement();
    }

    private void rotateInDirectionOfMovement()
    {

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward , _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }
}
