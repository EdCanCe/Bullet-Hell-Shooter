using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The base methods and attributes every enemy has.
/// </summary>
public class BaseEnemyScript : CharacterScript
{
    // The time between each of the rounds the enemy has
    protected float bulletTimer;
    protected float bulletCounter;

    // The path the enemy follows
    public List<Vector3> checkpoints;

    // Used to know when to move the enemy again
    protected bool activateNextMovement;
    protected int lastMovement;

    // The health points the enemy has
    public int healthPoints;

    /// <summary>
    /// Every enemy needs to do this just after spawning.
    /// </summary>
    protected void EnemyStart()
    {
        CharacterStart();

        // Initializes the bullet counter and the movement flags
        bulletCounter = 0;
        lastMovement = -1;
        activateNextMovement = true;

        // Adds an enemy to the UI
        GameManager.ModifyCurrentEnemies(1);
    }

    /// <summary>
    /// A coroutine used to move the enemy.
    /// </summary>
    /// <param name="startingPoint">The place the enemy will start.</param>
    /// <param name="endingPoint">The place the enemy will end.</param>
    /// <param name="travelTime">The amount of time the travel will take.</param>
    /// <returns></returns>
    protected IEnumerator MoveToFrom(Vector3 startingPoint, Vector3 endingPoint, float travelTime)
    {
        // Locks the flag to prevent from moving
        activateNextMovement = false;

        transform.position = startingPoint;

        Vector3 currentPoint = startingPoint;

        float timeElapsed = 0;

        while (timeElapsed < travelTime)
        {
            transform.position = Vector3.Lerp(currentPoint, endingPoint, timeElapsed / travelTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        // Frees the flag to enable movement again
        activateNextMovement = true;
        lastMovement +=1 ;
    }

    /// <summary>
    /// A coroutine used to move the enemy in circles. 
    /// </summary>
    /// <param name="center">The center of the circle.</param>
    /// <param name="amountOfLoops">The amount of loops to do.</param>
    /// <param name="travelTime">The time to do all of the loops.</param>
    /// <returns></returns>
    protected IEnumerator MoveAround(Vector3 center, int amountOfLoops, float travelTime)
    {
        // Locks the flag to prevent from moving
        activateNextMovement = false;

        // Gets the starting distance to the center
        Vector3 startingDistance = transform.position - center;

        // Time variables needed to do the movement in the given time 
        float timeElapsed = 0;
        float timePerLoop = travelTime / amountOfLoops;

        // The current distance the enemy is from the center
        Vector3 currentDistance;

        // Each one of the loops;
        for (int i = 0; i < amountOfLoops; i++)
        {
            while (timeElapsed < travelTime)
            {
                // Updates the distante to the one it needs to be
                currentDistance = VectorManager.Rotate(startingDistance, timeElapsed / timePerLoop * 360f);

                // Adds the distance to the center, to obtain its position
                transform.position = currentDistance + center;

                timeElapsed += Time.deltaTime;

                yield return null;
            }
        }

        // Frees the flag to enable movement again
        activateNextMovement = true;
        lastMovement = -2;
    }

    /// <summary>
    /// A method called when the enemy collides.
    /// ChatGPT was used to debug that I needed to use a 2D trigger
    /// instead of a 3D one. I was the one who tought of all the logic.
    /// </summary>
    /// <param name="collision">The collider that collisioned with the enemy.</param>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collision is with the bullet of a hero
        if (collision.gameObject.tag != gameObject.tag && immortal == false)
        {
            healthPoints -= 1;

            // If has no health points, the enemy dies
            if (healthPoints <= 0)
            {
                SoundManager.KillSFX();
                Destroy(gameObject);

                // Updates the UI by substracting one enemy
                GameManager.ModifyCurrentEnemies(-1);
            }
            else
            {
                // Plays the hit animation and SFX
                SoundManager.HitSFX();
                StartCoroutine(HitAnimation());
            }

            // Disables the bullet and updates de UI
            BulletManager.DisableBullet(collision.gameObject);
        }
    }
}
