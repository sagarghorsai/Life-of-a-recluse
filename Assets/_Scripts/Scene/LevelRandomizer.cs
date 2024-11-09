using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRandomizer : MonoBehaviour
{
    public int randomGen = 0;
    System.Random rnd = new System.Random();

    private void Start()
    {
        randomGen = rnd.Next(1, 3);
    }

    public void RandomizedLevel()
    {
        //Needs to change to randomize
        
        UnityEngine.SceneManagement.SceneManager.LoadScene($"L1");

    }




}
