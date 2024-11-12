using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public float enemySpeed = 3f;
    public float rotationSpeed = 0.1f;
    public float randomizedSpeed = 1f;

    public float speedIncreaseFactor = 0.5f;
    public float rotationSpeedIncreaseFactor = 0.1f;
    public float randomizedSpeedIncreaseFactor = 0.2f;

    public void IncreaseDifficulty()
    {
        // Debug logging to track each increment
        Debug.Log($"Before Increase - EnemySpeed: {enemySpeed}, RotationSpeed: {rotationSpeed}, RandomizedSpeed: {randomizedSpeed}");

        // Increase difficulty variables
        enemySpeed += speedIncreaseFactor;
        rotationSpeed += rotationSpeedIncreaseFactor;
        randomizedSpeed += randomizedSpeedIncreaseFactor;

        // Debug logging after setting new difficulty
        Debug.Log($"After Increase - EnemySpeed: {enemySpeed}, RotationSpeed: {rotationSpeed}, RandomizedSpeed: {randomizedSpeed}");
    }

    public void DifficultyReset()
    {
        enemySpeed = 3f;
        rotationSpeed = 0.1f;
        Debug.Log("DifficultyReset");
    }
}