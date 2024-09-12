using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movSpeed;  // Assigning the variable name
    float speedX, speedY;   // Assigning variable sub-names (Line above is name of line while these 2 names are variables for the x axis and y axis and how fast one moves in each direction.)
    Rigidbody2D rb;         // This is to have it assigned to assets with a 'Rigidbody2D' game component/component.


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // This line is to call out or look for game objects with the Rigidbody2D component.

    }

    // Update is called once per frame
    void Update()
    {

        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;         // Dictates how fast one moves in the X axis
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;           // Dictates how fast one moves in the Y axis
        rb.velocity = new Vector2(speedX, speedY);                  // Determines the velocity based on inputs multiplied by movSpeed variable

    }
}
