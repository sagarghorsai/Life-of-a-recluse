using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*PhantomRealm Studio - Life of a Recluse
 * Austin Horn
 * CSCI 448, Davenport University
 * Instructor: David Kroggman
 * 
 * Title: CountdownClock
 * Summary: A simplified timer to countdown to the player. This is intended to be utilized with enemy spawning scripts such as for the ManagerEnemy
 * 
 *  Made off the template provided by Jumbo Jump; https://www.youtube.com/watch?v=WxRsNge6Zuk&t=9s&ab_channel=JumboJump
 */
public class CountdownClock : MonoBehaviour
{
    private float currentTime;
    public float startingTime = 10f;
    public TextMeshProUGUI countdownText;
    public GameObject[] toSpawn;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= (1 * Time.deltaTime);
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            Spawn(toSpawn[0]);
        }
    }

    void Spawn (GameObject spawnThis)
    {
        spawnThis.SetActive(true);
    }
}

