using System.Collections.Generic;
using UnityEngine;

public class ProgressionMechanic : MonoBehaviour
{
    [Header("---------- Enemies ----------")]
    public List<CartEnemy> activeEnemies;
    public List<RotateOnlyMovement> rotatingEnemies;
    public List<RandomizedMovement> randomMovementEnemies; 
    [Header("---------- References ----------")]
    private TaskList taskList;
    private CountdownClock countdownClock;
    private DifficultyManager difficultyManager;
    private PanicMeter panicMeter;


    private void OnEnable()
    {
        difficultyManager = FindObjectOfType<DifficultyManager>();
        activeEnemies = new List<CartEnemy>(FindObjectsOfType<CartEnemy>());
        rotatingEnemies = new List<RotateOnlyMovement>(FindObjectsOfType<RotateOnlyMovement>());
        randomMovementEnemies = new List<RandomizedMovement>(FindObjectsOfType<RandomizedMovement>());
        panicMeter = FindAnyObjectByType<PanicMeter>();

        taskList = FindAnyObjectByType<TaskList>();
        countdownClock = FindAnyObjectByType<CountdownClock>();

        EnemyDifficulty();
    }

    void EnemyDifficulty()
    {
        foreach (CartEnemy enemy in activeEnemies)
        {
            if (enemy._waypointMovement != null)
            {
                enemy._waypointMovement.speed = difficultyManager.enemySpeed;
                Debug.Log($"{enemy} Speed = {enemy._waypointMovement.speed}");
            }
        }
        foreach (RotateOnlyMovement enemy in rotatingEnemies)
        {
            enemy.RotationSpeed = difficultyManager.rotationSpeed;
            Debug.Log($"{enemy} Speed = {enemy.RotationSpeed}");

        }
        foreach (RandomizedMovement enemy in randomMovementEnemies)
        {
            enemy.movementSpeed = difficultyManager.randomizedSpeed;
            Debug.Log($"{enemy} Speed = {enemy.movementSpeed}");

        }

        //TaskList Difficulty
        taskList.numberOfTasks = difficultyManager.tasknum;
        //Manager countdown Difficulty
        countdownClock.startingTime = difficultyManager.startingTime;


        //PanicMeter Difficulty
        panicMeter.panicScale = difficultyManager.panicScale;
        panicMeter.calmDownScale = difficultyManager.calmDownScale;
        panicMeter.calmDownPause = difficultyManager.calmDownPause;
    }

  

}