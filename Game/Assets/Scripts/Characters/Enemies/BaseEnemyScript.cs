using UnityEngine;
using System.Collections;

/// <summary>
/// The base methods and attributes every enemy has.
/// </summary>
public class BaseEnemyScript : MonoBehaviour
{
    // The time between each of the rounds the enemy has
    public float bulletTimer;
    protected float bulletCounter;

    // The game area that the enemy is held in
    public GameObject gameArea;

    // The type of bullet the enemy has
    public int bulletType;

    // The path the enemy follows
    public Vector3 startingPosition;
    public Vector3 endingPosition;
    public float travelTime;

    // The health points the enemy has
    public int healthPoints;

    /// <summary>
    /// Every enemy needs to do this just after spawning.
    /// </summary>
    protected void EnemyStart()
    {
        bulletCounter = 0;
        StartCoroutine(MoveToFrom(startingPosition, endingPosition, travelTime));
    }

    /// <summary>
    /// A corroutine used to move the enemy.
    /// </summary>
    /// <param name="startingPoint">The place the enemy will start.</param>
    /// <param name="endingPoint">The place the enemy will end.</param>
    /// <param name="travelTime">The amount of time the travel will take.</param>
    /// <returns></returns>
    protected IEnumerator MoveToFrom(Vector3 startingPoint, Vector3 endingPoint, float travelTime)
    {
        transform.position = startingPoint;

        Vector3 currentPoint = startingPoint;

        float timeElapsed = 0;

        while (timeElapsed < travelTime)
        {
            transform.position = Vector3.Lerp(currentPoint, endingPoint, timeElapsed / travelTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        // Goes back to its starting point
        StartCoroutine(MoveToFrom(endingPoint, startingPoint, travelTime));
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
        if (collision.gameObject.tag != gameObject.tag)
        {
            healthPoints -= 1;

            // If has no health points, the enemy dies
            if (healthPoints <= 0)
            {
                SoundManager.KillSFX();
                Destroy(gameObject);
            }
            else
            {
                SoundManager.HitSFX();
            }

            // Disables the bullet
            collision.gameObject.SetActive(false);
        }
    }
}
