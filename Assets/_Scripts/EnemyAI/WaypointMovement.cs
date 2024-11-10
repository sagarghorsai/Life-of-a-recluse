using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/*PhantomRealm Studio - Life of a Recluse
 * Austin Horn
 * CSCI 448, Davenport University
 * Instructor: David Kroggman
 * 
 * Title: WaypointMovement
 * Summary: A basic point to point movement script for basic Enemy AI in "Life of a Recluse". 
 *          Concept is to use a list of waypoints to take the enemy on a designated path and could potentially be reused for different enemies.
 */

public class WaypointMovement : MonoBehaviour
{

    public List<GameObject> enemyWaypoints; //Waypoint list to designate where the enemy should move towards
    private GameObject currentDestination; //The waypoint the enemy is set to move to
    private GameObject previousDestination; //The waypoint the enemy just passed through
    private int waypointIndex; //Number to help interact with list

    public float speed = 3.0f;


    private void FixedUpdate()
    {

        if (enemyWaypoints == null) { Debug.Log("No waypoint(s) listed"); } //if there are no waypoints in the list, return console error


        if (currentDestination == null)
        { //if no previous waypoint, go to first waypoint in list
            currentDestination = enemyWaypoints[1]; // set to 1 as 0 should be same as starting point, not first destination
            previousDestination = enemyWaypoints[0];
            waypointIndex = 1;
        }

        // Debug.Log("Move called");

        //Go to current Destination
        if (currentDestination != null)
        {
            transform.position = Vector3.MoveTowards(
              transform.position,
              currentDestination.transform.position,
              speed * Time.fixedDeltaTime);

        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemyWaypoint")
        {
            //Debug.Log("Collision called");

            //After reaching destination, put next waypoint as current destination
            /********* change destination on collision??? Enemy hits waypoint hitbox to confirm location has been reached **************/
            if (currentDestination != previousDestination)
            {
                if (waypointIndex < (enemyWaypoints.Count - 1))
                {
                    previousDestination = currentDestination;
                    currentDestination = enemyWaypoints[waypointIndex + 1];
                    waypointIndex++;
                }
                else //if no next waypoint, return to first waypoint in list
                {
                    previousDestination = currentDestination;
                    currentDestination = enemyWaypoints[0];
                    waypointIndex = 0;
                }


                //Debug.Log("current:" + currentDestination + "/n previous:" + previousDestination + "/n waypointIndex:" + waypointIndex);
            }


        }
    }

    public Vector2 GetCurrentDestination()
    {
        return currentDestination.transform.position;
    }

    public Vector2 GetPreviousDestination()
    {
        return previousDestination.transform.position;
    }
}