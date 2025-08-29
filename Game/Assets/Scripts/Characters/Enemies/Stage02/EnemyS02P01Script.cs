using UnityEngine;
using System.Collections;

/// <summary>
/// The stage 1 phase 1 enemy logic.
/// </summary>
public class EnemyS02P01Script : BaseEnemyScript
{
    public Vector3 initialPosition;

    public int rotationDirection;

    // Phase variables to manage phases
    private bool activateNextPhase;
    private int currentPhase;
    private float phaseTime;

    /// <summary>
    /// Is called once before the first execution of Update
    /// after the MonoBehaviour is created.
    /// </summary>
    void Start()
    {
        EnemyStart();

        activateNextPhase = true;
        currentPhase = -1;
    }

    /// <summary>
    /// Is called once per frame.
    /// </summary>
    void Update()
    {
        // Only activates another phase if it can
        if (activateNextPhase)
        {
            activateNextPhase = false;

            // Depending on the phase of the enemy, does different things
            switch (currentPhase)
            {
                case -1:
                    StartCoroutine(StartPhase());
                    break;
                case 0:
                    StartCoroutine(Phase00());
                    break;
                case 1:
                    StartCoroutine(Phase01());
                    break;
                case 2:
                    StartCoroutine(Phase02());
                    break;
                case 3:
                    StartCoroutine(Phase03());
                    break;
            }
        }

        // Rotates its sprites
        sprites[0].transform.Rotate(0, 0, rotationDirection * Time.deltaTime * 120);
        sprites[1].transform.Rotate(0, 0, rotationDirection * Time.deltaTime * -80);
    }

    /// <summary>
    /// The first phase, used to get to its starting
    /// position.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartPhase()
    {
        lastMovement = -1;

        // Moves the enemy towards its starting position
        StartCoroutine(MoveToFrom(transform.position, initialPosition, 1.5f));

        // Waits untis the coroutine ends
        while (lastMovement == -1)
        {
            yield return null;
        }

        // Enables the next phase
        currentPhase = 0;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number zero.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase00()
    {
        phaseTime = 3;

        bulletTimer = 0.07f;
        bulletCounter = bulletTimer;

        // Waits for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                // Resets the counter
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(2, transform.position, 2, rotationDirection * phaseTime / 3 * 720, 0, "0");
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 1;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number one.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase01()
    {
        phaseTime = 3;

        bulletTimer = 0.07f;
        bulletCounter = bulletTimer;

        // Waits for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                // Resets the counter
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(2, transform.position, 2, -rotationDirection * phaseTime / 3 * 720, 0, "0");
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 2;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number two.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase02()
    {
        lastMovement = -1;

        // Moves the enemy to its first checkpoint
        StartCoroutine(MoveToFrom(transform.position, checkpoints[0], 4));

        bulletTimer = 0.25f;
        bulletCounter = bulletTimer;

        // Repeats until the movement ends
        while (lastMovement == -1)
        {
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                // Resets the counter
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(2, transform.position, 1, 0, 0, "60");
                BulletManager.PlaceRound(2, transform.position, 1, -5, 0, "0");
                BulletManager.PlaceRound(2, transform.position, 1, 5, 0, "0");
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 3;
        activateNextPhase = true;
    }
    
    /// <summary>
    /// The phase number three
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase03()
    {
        lastMovement = -1;

        // Moves the enemy to its starting position
        StartCoroutine(MoveToFrom(transform.position, initialPosition, 4));

        bulletTimer = 0.25f;
        bulletCounter = bulletTimer;

        // Repeats until the movement ends
        while (lastMovement == -1)
        {
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                // Resets the counter
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(2, transform.position, 1, 0, 0, "60");
                BulletManager.PlaceRound(2, transform.position, 1, -5, 0, "0");
                BulletManager.PlaceRound(2, transform.position, 1, 5, 0, "0");
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 0;
        activateNextPhase = true;
    }
}