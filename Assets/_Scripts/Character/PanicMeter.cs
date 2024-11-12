using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*PhantomRealm Studio - Life of a Recluse
 * Austin Horn
 * CSCI 448, Davenport University
 * Instructor: David Kroggman
 * 
 * Title: PanicMeter
 * Summary: When the player enters the interaction area of an enemy (most likely designated by a hitbox) a "panic" value should increase. 
 *      Panic value should be tied to a UI element to provide a visual representation for the player
 *      Panic value should slowly decrease overtime, to a limit of 0, as long as it is not in the enemy interaction zones
 *      If panic reaches a max value it should initiate the "Lose" process
 */

public class PanicMeter : MonoBehaviour
{
    private AudioManager audioManager;
    private EXPManager expManager;
    private PlayerStats playerStats;
    private DifficultyManager difficultyManager;

    [Header("---------- Panic Values ----------")]
    public float panicValue;
    public float panicMax = 10; //Maximum number the panic value can go up to
    public float panicScale = 1; //How quickly panic increases
    public float calmDownScale = 1; //How quickly the player calms down
    public float calmDownPause = 10; // Pause before the calming down process starts


    float calmDownPauseTimer; //The timer to count down the pause

    bool inInteractionZone;
    bool freakedOut; //Whether or not the panic max has been reached and player has "freaked out" (resulting in game loss or life loss)
    private void Start()
    {
        expManager = FindAnyObjectByType<EXPManager>();
        playerStats = FindAnyObjectByType<PlayerStats>();
        difficultyManager = FindAnyObjectByType<DifficultyManager>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {


        if (panicValue >= panicMax)
        {
            freakedOut = true;
        }

        if (freakedOut)
        {
            //Start Lose Process
            FreakedOut();
        }

        if (!freakedOut)
        {

            if (inInteractionZone) // Increase panic when in an interaction zone
            {
                panicValue = panicValue + (panicScale * Time.deltaTime);
            }


            if (!inInteractionZone) //Decreases panic when outside an interaction zone
            {
                calmDownPauseTimer = calmDownPauseTimer - Time.deltaTime;

                if (calmDownPauseTimer <= 0) //If pause is over
                {

                    if (panicValue > 0) // Panic value cannot go below 0
                    {
                        panicValue = panicValue - (calmDownScale * Time.deltaTime);
                    }
                    if (panicValue < 0)
                    {
                        panicValue = 0;
                    }

                    calmDownPauseTimer = 0;
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InteractionZone")
        {
            inInteractionZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InteractionZone")
        {
            inInteractionZone = false;
            calmDownPauseTimer = calmDownPause;
        }
    }

    private void FreakedOut()
    {
        expManager.ResetProgress();
        playerStats.ResetStats();
        difficultyManager.DifficultyReset();
        AudioManager.Instance.PlaySFX("Scream");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
        Debug.Log("You freakedOut");
    }

}
