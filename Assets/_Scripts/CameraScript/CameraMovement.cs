using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;   // Reference to the player's transform
    public Vector3 offset;     // Offset between the camera and the player

    public float smoothSpeed = 0.125f;

    void Update()
    {
        if (player != null)
        {
            Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

}
