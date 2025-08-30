using UnityEngine;

/// <summary>
/// The stage 1 phase 2 enemy logic.
/// </summary>
public class EnemyS01P02Script : BaseEnemyScript
{
    public Vector3 initialPosition;

    /// <summary>
    /// Is called once before the first execution of Update
    /// after the MonoBehaviour is created.
    /// </summary>
    void Start()
    {
        EnemyStart();
        bulletTimer = 0.5f;
        lastMovement = -1;

        // Moves from outside the game area to inside of it
        StartCoroutine(MoveToFrom(transform.position, initialPosition, 3));
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
            BulletManager.PlaceRound(1, transform.position, 1, -10, 0, "0");
            BulletManager.PlaceRound(1, transform.position, 1, 10, 0, "0");
        }

        // Waits for the flag to move again
        if (activateNextMovement)
        {
            if (lastMovement == checkpoints.Count)
            {
                lastMovement = 0;
            }
            StartCoroutine(MoveToFrom(transform.position, checkpoints[lastMovement], 1));
        }
    }
}