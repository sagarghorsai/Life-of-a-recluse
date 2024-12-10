using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Enemy")]
    public float enemySpeed = 3f;
    public float rotationSpeed = 0.25f;
    public float randomizedSpeed = 1f;

    public float speedIncreaseFactor = 0.5f;
    public float rotationSpeedIncreaseFactor = 0.1f;
    public float randomizedSpeedIncreaseFactor = 0.2f;

    [Header("PanicMeter")]
    public float panicScale = 1;
    public float calmDownScale = 1;
    public float calmDownPause = 1;

    public float panicScaleIncreaseFactor = 0.5f;
    public float calmDownScaleDecreaseeFactor = 0.1f;
    public float calmDownPauseIncreaseeFactor = 0.2f;

    [Header("TaskList")]
    public int tasknum = 1;
    public int taskIncreaseFactor = 1;

    [Header("Manager")]
    public float startingTime = 1;
    public float startingTimeDecreaseFactor = 1;



    public void IncreaseDifficulty()
    {
        // Debug logging to track each increment
        Debug.Log($"Before Increase - EnemySpeed: {enemySpeed}, RotationSpeed: {rotationSpeed}, RandomizedSpeed: {randomizedSpeed}");

        // Increase difficulty variables
        enemySpeed += speedIncreaseFactor;
        rotationSpeed += rotationSpeedIncreaseFactor;
        randomizedSpeed += randomizedSpeedIncreaseFactor;

        panicScale += panicScaleIncreaseFactor;
        calmDownScale -= calmDownScaleDecreaseeFactor;
        calmDownPause += calmDownPauseIncreaseeFactor;

        tasknum += taskIncreaseFactor;
        startingTime -= startingTimeDecreaseFactor;

        if(startingTime <= 0)
        {
            startingTime = 10;
        }
        // Debug logging after setting new difficulty
        Debug.Log($"After Increase - EnemySpeed: {enemySpeed}, RotationSpeed: {rotationSpeed}, RandomizedSpeed: {randomizedSpeed}");
    }

    public void DifficultyReset()
    {
        tasknum = 5;
        startingTime = 300;
        enemySpeed = 3f;
        rotationSpeed = 0.25f;
        Debug.Log("DifficultyReset");
    }
}