using UnityEngine;
using System.Collections;

/// <summary>
/// The stage 1 phase 1 enemy logic.
/// </summary>
public class EnemyS03P01Script : BaseEnemyScript
{
    public Vector3 initialPosition;

    public float rotationSpeed;

    // The stages these phases are linked to
    public int activatePhase2;
    public int activatePhase3;
    public int lifeToPhase2;
    public int lifeToPhase3;

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
                case 4:
                    StartCoroutine(Phase04());
                    break;
                case 5:
                    StartCoroutine(Phase05());
                    break;
                case 6:
                    StartCoroutine(Phase06());
                    break;
                case 7:
                    StartCoroutine(Phase07());
                    break;
                case 8:
                    StartCoroutine(Phase08());
                    break;
                case 9:
                    StartCoroutine(Phase09());
                    break;
                case 10:
                    StartCoroutine(Phase10());
                    break;
                case 11:
                    StartCoroutine(Phase11());
                    break;
                case 12:
                    StartCoroutine(Phase12());
                    break;
                case 13:
                    StartCoroutine(Phase13());
                    break;
                case 14:
                    StartCoroutine(Phase14());
                    break;
                case 15:
                    StartCoroutine(Phase15());
                    break;
                case 16:
                    StartCoroutine(Phase16());
                    break;
                case 17:
                    StartCoroutine(Phase17());
                    break;
                case 18:
                    StartCoroutine(Phase18());
                    break;
                case 19:
                    StartCoroutine(Phase19());
                    break;
                case 20:
                    StartCoroutine(Phase20());
                    break;
                case 21:
                    StartCoroutine(Phase21());
                    break;
                case 22:
                    StartCoroutine(Phase22());
                    break;
                case 23:
                    StartCoroutine(Phase23());
                    break;
                case 24:
                    StartCoroutine(Phase24());
                    break;
                case 25:
                    StartCoroutine(Phase25());
                    break;
                case 26:
                    StartCoroutine(Phase26());
                    break;
                case 27:
                    StartCoroutine(Phase27());
                    break;
            }
        }

        // Rotates its sprites
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].transform.Rotate(0, 0, rotationSpeed * (1 + i / sprites.Length) * Time.deltaTime * (i % 2 == 0 ? -1 : 1));
        }
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

        // Waits until the coroutine ends
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
        phaseTime = 3;

        // Moves the enemy to its first checkpoint
        StartCoroutine(MoveToFrom(transform.position, checkpoints[0], phaseTime));

        bulletTimer = 0.3f;
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
                BulletManager.PlaceRound(3, transform.position, 50, 0, 0, "Abs((2 / Pi) * Asin(Sin(5 * x * (Pi / 360)))) * 100");
            }

            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase2 && !enoughDamage)
            {
                currentPhase = activatePhase2;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
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
        phaseTime = 2;

        // Moves the enemy to its checkpoint no. 1
        StartCoroutine(MoveToFrom(transform.position, checkpoints[1], phaseTime));

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
                BulletManager.PlaceRound(3, transform.position, 10, 0, 0, "0");
            }

            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase2 && !enoughDamage)
            {
                currentPhase = activatePhase2;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
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
        phaseTime = 2;

        // Moves the enemy to its checkpoint no. 2
        StartCoroutine(MoveToFrom(transform.position, checkpoints[2], phaseTime));

        bulletTimer = 0.1f;
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
                BulletManager.PlaceRound(3, transform.position, 1, 0, 0, "80");
            }

            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase2 && !enoughDamage)
            {
                currentPhase = activatePhase2;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 3;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number three.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase03()
    {
        lastMovement = -1;
        phaseTime = 2;

        // Moves the enemy to its first checkpoint
        StartCoroutine(MoveToFrom(transform.position, checkpoints[0], phaseTime));

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
                BulletManager.PlaceRound(3, transform.position, 10, 0, 0, "0");
            }

            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase2 && !enoughDamage)
            {
                currentPhase = activatePhase2;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 0;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number four
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase04()
    {
        lastMovement = -1;

        phaseTime = 3f;

        // Needs an extra frame to stop other movements
        yield return null;

        enoughDamage = false;

        // Moves the enemy to its checkpoint no. 3
        StartCoroutine(MoveToFrom(transform.position, checkpoints[3], phaseTime));

        // Makes a hit animation
        int currentTicks = tickAmount;
        tickAmount = (int)phaseTime * 8;

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            yield return null;
            rotationSpeed += Time.deltaTime * 30f;
        }

        tickAmount = currentTicks;

        // Enables the next phase
        currentPhase = 5;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number five.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase05()
    {
        lastMovement = -1;
        phaseTime = 2;

        // Moves the enemy to its checkpoint no. 4
        StartCoroutine(MoveAround(checkpoints[4], 2, phaseTime));

        bulletTimer = 0.25f;
        bulletCounter = bulletTimer;

        // Used to shoot in the pattern: right, down, left, up
        int currentShot = 3;

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
                BulletManager.PlaceRound(3, transform.position, 1, currentShot * 90, 0, "90");
                BulletManager.PlaceRound(3, transform.position, 1, currentShot * 90 + 15, 0, "90");
                BulletManager.PlaceRound(3, transform.position, 1, currentShot * 90 - 15, 0, "90");

                currentShot = (currentShot + 1) % 4;
            }

            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase3)
            {
                currentPhase = activatePhase3;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 6;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number six.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase06()
    {
        lastMovement = -1;
        phaseTime = 3;

        // Moves the enemy to its checkpoint no. 4
        StartCoroutine(MoveToFrom(transform.position, checkpoints[4], phaseTime));

        bulletTimer = 0.08f;
        bulletCounter = bulletTimer;

        // Used to manage the rotation given to the bullets
        float phaseCounter = 0;

        // Repeats until the movement ends
        while (lastMovement == -1)
        {
            bulletCounter -= Time.deltaTime;
            phaseCounter += Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                // Resets the counter
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(3, transform.position, 5, phaseCounter / phaseTime * 150, 0, "30");
                BulletManager.PlaceRound(3, transform.position, 5, -phaseCounter / phaseTime * 150, 0, "30");
            }

            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase3)
            {
                currentPhase = activatePhase3;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 7;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number seven.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase07()
    {
        lastMovement = -1;

        // Moves the enemy to its checkpoint no. 5
        StartCoroutine(MoveToFrom(transform.position, checkpoints[5], 0.5f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase3)
            {
                currentPhase = activatePhase3;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 8;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number eight.
    /// </summary>
    /// <returns></returns>    
    private IEnumerator Phase08()
    {
        phaseTime = 2;

        bulletTimer = 0.2f;

        // Repeats for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(3, transform.position, 50, 0, 0, "Abs((2 / Pi) * Asin(Sin(3 * x * (Pi / 360)))) * 220");
            }

            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase3)
            {
                currentPhase = activatePhase3;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 9;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number nine.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase09()
    {
        lastMovement = -1;

        // Moves the enemy to its checkpoint no. 1
        StartCoroutine(MoveToFrom(transform.position, checkpoints[6], 0.5f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase3)
            {
                currentPhase = activatePhase3;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 10;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number ten.
    /// </summary>
    /// <returns></returns>    
    private IEnumerator Phase10()
    {
        phaseTime = 3;

        bulletTimer = 0.2f;

        // Repeats for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(3, transform.position, 70, 0, 0, "Abs((2 / Pi) * Asin(Sin(5 * x * (Pi / 360)))) * 220");
            }

            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase3)
            {
                currentPhase = activatePhase3;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 11;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number eleven.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase11()
    {
        lastMovement = -1;

        // Moves the enemy to its checkpoint no. 7
        StartCoroutine(MoveToFrom(transform.position, checkpoints[7], 0.5f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase3)
            {
                currentPhase = activatePhase3;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 12;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number twelve.
    /// </summary>
    /// <returns></returns>    
    private IEnumerator Phase12()
    {
        phaseTime = 2;

        bulletTimer = 0.2f;

        // Repeats for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(3, transform.position, 50, 0, 0, "Abs((2 / Pi) * Asin(Sin(3 * x * (Pi / 360)))) * 220");
            }

            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase3)
            {
                currentPhase = activatePhase3;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 13;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number thirteen.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase13()
    {
        lastMovement = -1;

        // Moves the enemy to its checkpoint no. 3
        StartCoroutine(MoveToFrom(transform.position, checkpoints[3], 0.5f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            // If it has enough damage to start the next phase, stops the phase
            if (healthPoints <= lifeToPhase3)
            {
                currentPhase = activatePhase3;
                activateNextPhase = true;
                enoughDamage = true;
                yield break;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 5;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number fourteen
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase14()
    {
        lastMovement = -1;

        phaseTime = 4;

        // Needs an extra frame to stop other movements
        yield return null;

        enoughDamage = false;

        // Moves the enemy to its checkpoint no. 8
        StartCoroutine(MoveToFrom(transform.position, checkpoints[8], phaseTime));

        // Makes a hit animation
        int currentTicks = tickAmount;
        tickAmount = (int)phaseTime * 8;

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            yield return null;
            rotationSpeed += Time.deltaTime * 50f;
        }

        tickAmount = currentTicks;

        // Enables the next phase
        currentPhase = 15;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number fifteen.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase15()
    {
        lastMovement = -1;

        // Places a round of bullets
        BulletManager.PlaceRound(3, transform.position, 80, 0, 0, "Pow(Cos(3 * x * (Pi/180)), 2) * 120");

        // Moves the enemy to its checkpoint no. 9
        StartCoroutine(MoveToFrom(transform.position, checkpoints[9], 0.2f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            yield return null;
        }

        // Enables the next phase
        currentPhase = 16;
        activateNextPhase = true;
    }

    private IEnumerator Phase16()
    {
        phaseTime = 2.1f;

        bulletTimer = 0.35f;

        bool roundType = false;

        // Repeats for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(3, transform.position, 40, (roundType ? 45 : 0), 15, "60");
                roundType = !roundType;
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 17;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number seventeen.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase17()
    {
        lastMovement = -1;

        // Moves the enemy to its checkpoint no. 10
        StartCoroutine(MoveToFrom(transform.position, checkpoints[10], 0.2f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            yield return null;
        }

        // Places a round of bullets
        BulletManager.PlaceRound(3, transform.position, 80, 0, 0, "Pow(Cos(3 * x * (Pi/180)), 2) * 120");

        // Enables the next phase
        currentPhase = 18;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number eighteen.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase18()
    {
        lastMovement = -1;

        // Moves the enemy to its checkpoint no. 11
        StartCoroutine(MoveToFrom(transform.position, checkpoints[11], 0.2f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            yield return null;
        }

        // Enables the next phase
        currentPhase = 19;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number nineteen.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase19()
    {
        lastMovement = -1;
        phaseTime = 3;

        // Moves the enemy to its checkpoint no. 9
        StartCoroutine(MoveAround(checkpoints[9], 8, phaseTime));

        bulletTimer = 0.15f;
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
                BulletManager.PlaceRound(3, transform.position, 3, 0, 0, "80");
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 20;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number twenty.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase20()
    {
        phaseTime = 1;

        bulletTimer = 0.05f;

        // Repeats for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(3, transform.position, 4, 0, 40, "300");
                BulletManager.PlaceRound(3, transform.position, 4, 0, -40, "300");
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 21;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number twenty one.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase21()
    {
        lastMovement = -1;

        // Moves the enemy to its checkpoint no. 8
        StartCoroutine(MoveToFrom(transform.position, checkpoints[8], 0.2f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            yield return null;
        }

        // Enables the next phase
        currentPhase = 22;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number twenty two.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase22()
    {
        phaseTime = 2;

        bulletTimer = 0.07f;

        // Repeats for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(3, transform.position, 4, phaseTime / 3 * 120, 0, "100");
                BulletManager.PlaceRound(3, transform.position, 4, -phaseTime / 3 * 120, 0, "100");
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 23;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number twenty three.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase23()
    {
        lastMovement = -1;

        // Moves the enemy to its checkpoint no. 10
        StartCoroutine(MoveToFrom(transform.position, checkpoints[10], 0.3f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            yield return null;
        }

        // Enables the next phase
        currentPhase = 24;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number twenty four.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase24()
    {
        phaseTime = 2;

        bulletTimer = 0.07f;

        // Repeats for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(3, transform.position, 4, phaseTime / 3 * 120, 0, "100");
                BulletManager.PlaceRound(3, transform.position, 4, -phaseTime / 3 * 120, 0, "100");
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 25;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number twenty five.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase25()
    {
        lastMovement = -1;

        // Moves the enemy to its checkpoint no. 9
        StartCoroutine(MoveToFrom(transform.position, checkpoints[9], 0.3f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            yield return null;
        }

        // Enables the next phase
        currentPhase = 26;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number twenty six.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Phase26()
    {
        phaseTime = 4;

        bulletTimer = 0.6f;

        // Repeats for a given amount of time
        while (phaseTime > 0)
        {
            phaseTime -= Time.deltaTime;
            bulletCounter -= Time.deltaTime;

            // When the counter reaches 0, it's time to fire another round
            if (bulletCounter < 0)
            {
                bulletCounter = bulletTimer;

                // Places a round of bullets
                BulletManager.PlaceRound(3, transform.position, 70, phaseTime / 5 * 90, 0, "Abs(Cos(5 * x * (Pi/180))) * 120");
            }

            yield return null;
        }

        // Enables the next phase
        currentPhase = 27;
        activateNextPhase = true;
    }

    /// <summary>
    /// The phase number twenty seven.
    /// </summary>
    /// <returns></returns>    
    private IEnumerator Phase27()
    {
        lastMovement = -1;

        // Moves the enemy to its checkpoint no. 8
        StartCoroutine(MoveToFrom(transform.position, checkpoints[8], 1f));

        // Waits until the coroutine ends
        while (lastMovement == -1)
        {
            yield return null;
        }

        // Enables the next phase
        currentPhase = 15;
        activateNextPhase = true;
    }

}