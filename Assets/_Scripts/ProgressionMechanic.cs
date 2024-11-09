using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionMechanic : MonoBehaviour
{
    [Header("---------- Enemies ----------")]
    public List<EnemyController> activeEnemies;
    public List<RotateOnlyMovement> rotatingEnemies;
    public List<RandomizedMovement> randomMovementEnemies;


    [Header("---------- Refrences ----------")]
    private TaskList taskList;
    private CountdownClock countdownClock;


    [Header("---------- Changable Variables ----------")]
    public float enemySpeed = 5f;
    public float countdownTime = 150f;
    public int taskAmount = 5;
    public float rotationSpeed = 0.1f;
    public float randomizedSpeed = 1f;

    private void Awake()
    {
        activeEnemies = new List<EnemyController>(FindObjectsOfType<EnemyController>());
        rotatingEnemies = new List<RotateOnlyMovement>(FindObjectsOfType<RotateOnlyMovement>());
        randomMovementEnemies = new List<RandomizedMovement>(FindObjectsOfType<RandomizedMovement>());

        taskList = GetComponent<TaskList>();
        countdownClock =FindAnyObjectByType<CountdownClock>();

        SetDifficulty();
        Debug.Log("Set Difficulty");
    }

  

    public void SetDifficulty()
    {
        CountDownClockDifficulty();
        TaskListDifficulty();
        EnemyDifficulty();
    }



    void CountDownClockDifficulty()
    {
        countdownClock.startingTime = countdownTime;
    }

    void TaskListDifficulty()
    {
        taskList.numberOfTasks = taskAmount;
    }

    void EnemyDifficulty()
    {
        foreach (EnemyController enemy in activeEnemies)
        {
            if (enemy._waypointMovement != null)
            {
                enemy._waypointMovement.speed = enemySpeed; 
            }
        }

        foreach (RotateOnlyMovement enemy in rotatingEnemies)
        {
            enemy.RotationSpeed = rotationSpeed;
        }

        foreach (RandomizedMovement enemy in randomMovementEnemies)
        {
            enemy.movementSpeed = randomizedSpeed;

        }



    }


}
