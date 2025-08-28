using UnityEngine;

/// <summary>
/// The stage 1 enemy logic.
/// </summary>
public class Stage01Script : BaseEnemyScript
{
    // The sprite of the enemy
    public GameObject sprite;

    // The speed it's sprite will rotate
    public float rotationSpeed;

    /// <summary>
    /// Is called once before the first execution of Update
    /// after the MonoBehaviour is created.
    /// </summary>
    void Start()
    {
        EnemyStart();
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

        // Rotates the sprite
        sprite.transform.Rotate(bulletType, 0, Time.deltaTime * rotationSpeed);
    }
}
