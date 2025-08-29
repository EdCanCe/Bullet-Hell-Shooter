using UnityEngine;

/// <summary>
/// The stage 1 phase 1 enemy logic.
/// </summary>
public class EnemyS01P01Script : BaseEnemyScript
{
    /// <summary>
    /// Is called once before the first execution of Update
    /// after the MonoBehaviour is created.
    /// </summary>
    void Start()
    {
        EnemyStart();
        bulletTimer = 0.15f;
    }

    /// <summary>
    /// Is called once per frame.
    /// </summary>
    void Update()
    {
        bulletCounter -= Time.deltaTime;

        // When the counter reaches 0, it's time to fire another round
        if (bulletCounter < 0)
        {
            // Resets the counter
            bulletCounter = bulletTimer;

            // Places a round of bullets
            BulletManager.PlaceRound(1, transform.position, 6, 0, 0, "");
        }
    }
}


/*

        // Continues the movement
        currentCheckpoint += 1;
        if (currentCheckpoint == checkpoints.Count) {
            currentCheckpoint = 0;
        }
        StartCoroutine(MoveToFrom(endingPoint, checkpoints[currentCheckpoint], travelTime, currentCheckpoint));

*/