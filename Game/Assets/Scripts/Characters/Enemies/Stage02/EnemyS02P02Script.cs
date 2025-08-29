using UnityEngine;
using System.Collections;

/// <summary>
/// The stage 1 phase 1 enemy logic.
/// </summary>
public class EnemyS02P02Script : BaseEnemyScript
{
    public Vector3 initialPosition;

    public int rotationDirection;

    // Phase variables to manage phases
    private bool activateNextPhase;
    private int currentPhase;
    private float phaseTime;
    private float currentTime;

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
        lastMovement = -1;
        phaseTime = 4;
        currentTime = 0;

        // Moves the enemy towards its first checkpoint
        StartCoroutine(MoveToFrom(transform.position, checkpoints[0], phaseTime));

        bulletTimer = 0.1f;
        bulletCounter = bulletTimer;

        // Repeats until the movement ends
        while (lastMovement == -1)
        {
            bulletCounter -= Time.deltaTime;
            currentTime += Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                // Resets the counter
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(2, transform.position, 4, rotationDirection * currentTime / 5 * 360 * 2, 0, "0");
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
        lastMovement = -1;
        phaseTime = 4;
        currentTime = 0;

        // Moves the enemy to its starting position
        StartCoroutine(MoveToFrom(transform.position, initialPosition, phaseTime));

        bulletTimer = 0.1f;
        bulletCounter = bulletTimer;

        // Repeats until the movement ends
        while (lastMovement == -1)
        {
            bulletCounter -= Time.deltaTime;
            currentTime += Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                // Resets the counter
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(2, transform.position, 4, -rotationDirection * currentTime / 5 * 360 * 2, 0, "0");
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
        phaseTime = 0.6f;

        bulletTimer = 0.05f;
        bulletCounter = bulletTimer;

        // Repeats for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                // Resets the counter
                bulletCounter = bulletTimer;

                // Spawns a bullet shotgun
                BulletManager.PlaceRound(2, transform.position, 1, 0, 0, "70");
                BulletManager.PlaceRound(2, transform.position, 1, 3, 0, "50");
                BulletManager.PlaceRound(2, transform.position, 1, 6, 0, "10");
                BulletManager.PlaceRound(2, transform.position, 1, 9, 0, "30");
                BulletManager.PlaceRound(2, transform.position, 1, -3, 0, "70");
                BulletManager.PlaceRound(2, transform.position, 1, -6, 0, "50");
                BulletManager.PlaceRound(2, transform.position, 1, -9, 0, "30");
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
        phaseTime = 0.7f;

        // Waits for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;

            yield return null;
        }

        // Enables the next phase
        currentPhase = 0;
        activateNextPhase = true;
    }
}